using PCHUBStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class ShipmentManagerIndexModel
    {      
        public ShippingCompany ShippingCompany { get; set; }

        public ConfirmationStatus ConfirmationStatus { get; set; }
    }
}
