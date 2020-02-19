using PCHUBStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Ticket : BaseModel<int>
    {
        public Ticket()
        {
            this.Activities = new List<Activity>();
        }
        public DateTime? CloseDate { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public string Description { get; set; }

        public string Resolution { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public virtual ICollection<Activity> Activities { get; set; } 

        public string SerialNumber { get; set; }

        public ConfirmationStatus ConfirmationStatus { get; set; }
    }
}
