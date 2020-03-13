using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.UserProfileViewModels
{
    public class UserOrdersViewModel
    {

        public DateTime OrderDate { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ProductId { get; set; }
    }
}
