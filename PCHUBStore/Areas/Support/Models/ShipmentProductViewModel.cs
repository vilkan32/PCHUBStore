using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class ShipmentProductViewModel
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string PictureUrl { get; set; }
        public string ProductUrl { get; set; }
        public decimal Price { get; set; }
        public bool RemoveFromCart { get; set; }
        public int Quantity { get; set; }
    }
}
