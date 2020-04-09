using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.HubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Services
{
    public interface IRequestChatServices
    {
        Task AddToQueueAsync(ChatRequestModel request);

        Task<bool> ConnectionIdExistsAsync(string connectionId);

        Task RemoveChatRequestAsync(string connectionId);

        Task EstablishConnectionAsync(string connectionId, string subject, string email, string technicianName);

        Task<bool> ConnectionAlreadyEstablishedAsync(string connectionId, string role);

        Task<Chat> EstablishedConnectionDetailsAsync(string connectionId, string role);

        Task InsertChatAsync(string technician, string user, string sender, string message);

        Task DisconnectFromChat(string technician, string user);

        Task<List<ChatRequest>> GetAllChatRequestsAsync();

    }
}
