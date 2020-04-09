using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.Hubs;

namespace PCHUBStore.Areas.Support.Controllers
{
    public class ChatController : SupportController
    {
        private readonly IRequestChatServices service;

        public ChatController(IRequestChatServices service)
        {
            this.service = service;
        }

        public async Task<IActionResult> ChatRoom()
        {
            var model = new List<PendingRequestsViewModel>();

            var requests = await this.service.GetAllChatRequestsAsync();

            foreach (var request in requests)
            {
                model.Add(new PendingRequestsViewModel { Subject = request.Subject, Username = request.Email });
            }

            return View(model);
        }

    }
}