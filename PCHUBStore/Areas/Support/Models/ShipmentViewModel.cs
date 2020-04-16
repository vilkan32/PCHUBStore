using PCHUBStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class ShipmentViewModel
    {
        public int ShipmentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public ShipmentDetails ShipmentDetails { get; set; }

        public List<ShipmentProductViewModel> ShipmentProducts { get; set; }

        public decimal TotalProductsPrice { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public DateTime? DeliveryConfirmationDate { get; set; }

        public ShipmentImportancy ShipmentImportancy { get; set; }

        public virtual List<ActivityViewModel> Activities { get; set; }

        public decimal ShipmentPrice { get; set; }

        public DateTime? ReceivedOn { get; set; }

        public ShippingCompany ShippingCompany { get; set; }

        public string ShippingCompanyDetails { get; set; }

        public string ShipmentCoveredBy { get; set; }

        public ClientResponse ClientResponse { get; set; }

        public decimal Expenses { get; set; }
   
        public ConfirmationStatus ConfirmationStatus { get; set; }

    }
}
