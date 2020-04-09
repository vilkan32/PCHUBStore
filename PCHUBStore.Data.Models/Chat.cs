using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Chat : BaseModel<int>
    {
        public string ConnectionId { get; set; }

        public string Subject { get; set; }

        public string Email { get; set; }

        public string TechnicianName { get; set; }

        public string Messages { get; set; }

        public bool ConnectionTerminated { get; set; }
    }
}
