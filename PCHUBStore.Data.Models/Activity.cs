﻿using PCHUBStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Activity : BaseModel<string>
    {

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public bool ActivityClosed { get; set; }
        public string Description { get; set; }

        public ActivityType ActivityType { get; set; }

        public int? ShipmentId { get; set; }

        public Shipment Shipment { get; set; }

        public int? TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
