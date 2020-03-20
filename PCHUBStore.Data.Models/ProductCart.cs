using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class ProductCart
    {
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string CartId { get; set; }

        public virtual ShoppingCart Cart { get; set; }

        public int Quantity { get; set; }
    }
}
