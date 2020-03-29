using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class ShoppingCart : BaseModel<string>
    {
        public ShoppingCart()
        {
            this.ProductCarts = new List<ProductCart>();
            this.Id = Guid.NewGuid().ToString();
        }
        public string UserId { get; set; }
        public virtual User Holder { get; set; }

        public virtual ICollection<ProductCart> ProductCarts {get;set;}

    }
}
