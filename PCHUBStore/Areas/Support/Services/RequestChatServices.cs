using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.HubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Services
{
    public class RequestChatServices : IRequestChatServices
    {
        private readonly PCHUBDbContext context;

        public RequestChatServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        public async Task AddToQueueAsync(ChatRequestModel request)
        {
            if (!await this.ConnectionIdExistsAsync(request.ConnectionId))
            {
                await this.context.ChatRequests.AddAsync(new ChatRequest
                {
                    CreatedOn = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow,
                    ConnetctionId = request.ConnectionId,
                    Email = request.Email,
                    IsAuthenticated = request.IsAuthenticated,
                    Subject = request.Subject,
                });

                await this.context.SaveChangesAsync();
            }
        }

        public async Task<List<ChatRequest>> GetAllChatRequestsAsync()
        {
            return await this.context.ChatRequests.ToListAsync();
        } 

        public async Task EstablishConnectionAsync(string connectionId, string subject, string email, string technicianName)
        {
            await this.context.Chats.AddAsync(new Chat
            {

                ConnectionId = connectionId,
                CreatedOn = DateTime.UtcNow,
                Email = email,
                TechnicianName = technicianName,
                Subject = subject,
                ConnectionTerminated = false,
                ModificationDate = DateTime.UtcNow,

            });

            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ConnectionIdExistsAsync(string connectionId)
        {
            return await this.context.ChatRequests.AnyAsync(x => x.ConnetctionId == connectionId);
        }

        public async Task RemoveChatRequestAsync(string connectionId)
        {
            var requestToRemove = await this.context.ChatRequests.FirstOrDefaultAsync(x => x.ConnetctionId == connectionId && x.IsDeleted == false);
            var chatRequest = this.context.ChatRequests.Remove(requestToRemove);
          
            await this.context.SaveChangesAsync();
        }

        public async Task InsertChatAsync(string technician, string user, string sender, string message)
        {
            var chat = await this.context.Chats.FirstOrDefaultAsync(x => x.TechnicianName == technician && x.ConnectionId == user && x.ConnectionTerminated == false);

            if(chat.Messages == null)
            {
                var arr = new List<JsonChatModel>();
                arr.Add(new JsonChatModel { Sender = sender, Message = message });
                var json = JsonConvert.SerializeObject(arr);

                chat.Messages = json;
            }
            else
            {
                var arr = JsonConvert.DeserializeObject<List<JsonChatModel>>(chat.Messages);

                arr.Add(new JsonChatModel { Sender = sender, Message = message });

                var json = JsonConvert.SerializeObject(arr);

                chat.Messages = json;
            }

            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ConnectionAlreadyEstablishedAsync(string connectionId, string role)
        {
            if(role == "StoreUser")
            {
                return await this.context.Chats.AnyAsync(x => x.ConnectionId == connectionId && x.ConnectionTerminated == false);
            }
            else
            {
                return await this.context.Chats.AnyAsync(x => x.TechnicianName == connectionId && x.ConnectionTerminated == false);
            }
            
        }

        public async Task DisconnectFromChat(string technician, string user)
        {
            var chat = await this.context.Chats.FirstOrDefaultAsync(x => x.TechnicianName == technician && x.ConnectionId == user && x.ConnectionTerminated == false);

            chat.ConnectionTerminated = true;

            await this.context.SaveChangesAsync();
        }

        public async Task<Chat> EstablishedConnectionDetailsAsync(string connectionId, string role)
        {      
            if (role == "StoreUser")
            {
                var chatUser = await this.context.Chats.FirstOrDefaultAsync(x => x.ConnectionId == connectionId && x.ConnectionTerminated == false);
                return chatUser;
            }
            else
            {
                var chatTechnician = await this.context.Chats.FirstOrDefaultAsync(x => x.TechnicianName == connectionId && x.ConnectionTerminated == false);
                return chatTechnician;
            }
        }
    }
}
