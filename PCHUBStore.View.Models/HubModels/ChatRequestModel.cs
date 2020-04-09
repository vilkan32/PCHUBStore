using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.HubModels
{
    public class ChatRequestModel
    {
        public bool IsAuthenticated { get; set; }

        public string ConnectionId { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }
    }
}
