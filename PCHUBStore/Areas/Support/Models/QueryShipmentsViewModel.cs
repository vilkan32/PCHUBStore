using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class QueryShipmentsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public int ProductsCount { get; set; }

        public decimal ShipmentTotalPrice { get; set; }

        public string ConfirmationStatus { get; set; }

        public string ShippingCompany { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string ShipmentDetails { get; set; }
    }
}
