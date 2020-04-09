using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.Services;
using PCHUBStore.View.Models.HubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Hubs
{
    [Authorize]
    public class RequestChatHub : Hub
    {
        private readonly IUserProfileServices userProfileService;
        private readonly IRequestChatServices service;

        public RequestChatHub(IUserProfileServices userProfileService,
            IRequestChatServices service)
        {
            this.userProfileService = userProfileService;
            this.service = service;
        }


        public async Task RequestChat(string subject)
        {
            if (!await this.service.ConnectionIdExistsAsync(this.Context.User.Identity.Name))
            {
                var userInformation = await this.userProfileService.GetUserProfileInformationAsync(this.Context.User.Identity.Name);

                var chatRequest = new ChatRequestModel { IsAuthenticated = true, Email = userInformation.UserName, Subject = subject, ConnectionId = this.Context.User.Identity.Name };

                await this.service.AddToQueueAsync(chatRequest);

                await this.Clients.Group("Technicians").SendAsync("RequestChat", chatRequest);
            }
        }

        public async Task AcceptChat(string connectionId, string subject, string email, string technicianName)
        {
                await this.service.EstablishConnectionAsync(connectionId, subject, email, technicianName);

                var user = await this.userProfileService.GetUserProfileInformationAsync(connectionId);

                await this.Clients.User(user.Id).SendAsync("ChatAccepted", technicianName);         
        }

        public async Task CancelRequest(string connectionId)
        {
            var user = await this.userProfileService.GetUserProfileInformationAsync(connectionId);

            await this.Clients.User(user.Id).SendAsync("RequestRejected");
        }

        public async Task DisconnectFromChat(string technician, string user)
        {
            Console.WriteLine();
            var userInfo = await this.userProfileService.GetUserProfileInformationAsync(user);

            var technicianInfo = await this.userProfileService.GetUserProfileInformationAsync(technician);
            Console.WriteLine();
            await this.service.DisconnectFromChat(technician, user);

            await this.Clients.User(userInfo.Id).SendAsync("Disconnect");

            await this.Clients.User(technicianInfo.Id).SendAsync("Disconnect");
        }

        public async Task RemoveRequest(string connectionId)
        {

            if (await this.service.ConnectionIdExistsAsync(connectionId))
            {
                await this.service.RemoveChatRequestAsync(connectionId);

                await this.Clients.Group("Technicians").SendAsync("RemoveRequest", connectionId);
            }
        }

        public async Task Chat(string username, string message, string technicianName)
        {
            // connection.invoke("Chat", technician, message, user);

            var userInformation = await this.userProfileService.GetUserProfileInformationAsync(username);
            if (this.Context.User.IsInRole("StoreUser"))
            {
                await this.service.InsertChatAsync(username, technicianName, technicianName, message);
            }
            else
            {
                await this.service.InsertChatAsync(technicianName, username, technicianName, message);
            }
            await this.Clients.User(userInformation.Id).SendAsync("Message", message, technicianName);

        }

        public override async Task OnConnectedAsync()
        {

            var userInformation = await this.userProfileService.GetUserProfileInformationAsync(this.Context.User.Identity.Name);

            if (this.Context.User.IsInRole("StoreUser"))
            {
                if (await this.service.ConnectionAlreadyEstablishedAsync(this.Context.User.Identity.Name, "StoreUser"))
                {
                    var chat = await this.service.EstablishedConnectionDetailsAsync(this.Context.User.Identity.Name, "StoreUser");

                    await this.Clients.User(userInformation.Id).SendAsync("RetrieveChatUser", chat.TechnicianName, chat.Messages);
                }
            }

            if (this.Context.User.IsInRole("Admin") || this.Context.User.IsInRole("Support"))
            {
                if (await this.service.ConnectionAlreadyEstablishedAsync(this.Context.User.Identity.Name, "Admin"))
                {
                    var chat = await this.service.EstablishedConnectionDetailsAsync(this.Context.User.Identity.Name, "Admin");

                    await this.Clients.User(userInformation.Id).SendAsync("RetrieveChatSupport", chat.ConnectionId, chat.Messages, chat.Subject);
                }
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "Technicians");
            }

            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {


            if (this.Context.User.IsInRole("StoreUser"))
            {
                var connectionId = this.Context.User.Identity.Name;
                if (await this.service.ConnectionIdExistsAsync(connectionId))
                {
                    await this.service.RemoveChatRequestAsync(connectionId);

                    await this.Clients.Group("Technicians").SendAsync("RemoveRequest", connectionId);
                }
            }

            await base.OnDisconnectedAsync(exception);

        }
    }
}
