using PCHUBStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Shipment : BaseModel<int>
    {

        public DateTime? PurchaseDate { get; set; }

        public ShipmentDetails ShipmentDetails { get; set; }

        public virtual List<Product> ShippedProducts { get; set; }

        public decimal TotalProductsPrice { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
        public ShipmentImportancy ShipmentImportancy { get; set; }

        List<Activity> Activities { get; set; }

        public decimal ShipmentPrice { get; set; }

        public DateTime? ShippedOn { get; set; }

        public string DeliveryDetails { get; set; }
        public DateTime? ReceivedOn { get; set; }

        public string ShipmentCoveredBy { get; set; }

        public ClientResponse ClientResponse { get; set; }

        public decimal Expenses { get; set; }

        public ConfirmationStatus ConfirmationStatus { get; set; }
    }
}
