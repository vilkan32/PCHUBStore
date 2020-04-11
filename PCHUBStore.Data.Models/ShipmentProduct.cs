using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class ShipmentProduct
    {
        public int ShipmentId { get; set; }

        public virtual Shipment Shipment { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
