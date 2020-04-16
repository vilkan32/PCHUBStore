using PCHUBStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Activity : BaseModel<string>
    {
        public Activity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public bool ActivityClosed { get; set; }
        public string Description { get; set; }

        public ActivityType ActivityType { get; set; }

        public int? ShipmentId { get; set; }

        public virtual Shipment Shipment { get; set; }

    }
}
