using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.UserProfileViewModels
{
    public class UserOrdersViewModel
    {
        public int ShipmentId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string PictureUrl { get; set; }

        public string Title { get; set; }

        public int Quantity { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ProductId { get; set; }
    }
}
