using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Products = new List<Product>();
        }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
