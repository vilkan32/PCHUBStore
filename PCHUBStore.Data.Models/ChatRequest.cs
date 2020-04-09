using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class ChatRequest : BaseModel<int>
    {
        public bool IsAuthenticated { get; set; }

        public string ConnetctionId { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }
    }
}
