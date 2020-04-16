using PCHUBStore.Data.Models.Enums;
using PCHUBStore.View.Models.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class ShipmentManagerIndexModel
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime? PurchaseDate { get; set; }
        public ShippingCompany? ShippingCompany { get; set; }

        public ConfirmationStatus? ConfirmationStatus { get; set; }

        public Pager Pager { get; set; }

        public List<AllShipmentsViewModel> AllShipments { get; set; }
    }
}
