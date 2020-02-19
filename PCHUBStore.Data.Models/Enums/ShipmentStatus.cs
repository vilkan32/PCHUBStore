using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models.Enums
{
    public enum ShipmentStatus
    {
        Taken,
        AwaitingShippingCompanyResponse,
        Sent,
        Rejected,
        Confirmed,
    }
}
