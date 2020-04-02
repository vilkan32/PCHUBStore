using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.View.Models.ShoppingCartViewModels
{
    public class PurchaseProductsAnonymousViewModel
    {
        public string Title { get; set; }

        public string Id { get; set; }
        public string PictureUrl { get; set; }

        public string ProductUrl { get; set; }

        public decimal Price { get; set; }

        public bool RemoveFromCart { get; set; }

        [Range(1, 50)]
        public int Quantity { get; set; }
    }
}
