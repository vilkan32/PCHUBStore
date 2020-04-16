using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models.Enums
{
    public enum ShipmentStatus
    {
        Taken,
        [Display(Name = "Awaiting Shipping Company Response")]
        AwaitingShippingCompanyResponse,
        Sent,
        Rejected,
        Confirmed,
    }
}
