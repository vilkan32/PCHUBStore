using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class ItemsCategory : BaseModel<int>
    {

        public ItemsCategory()
        {
            this.Items = new List<PageCategoryItems>();
        }
        public string Category { get; set; }

        public int PageCategoryId { get; set; }
        public virtual PageCategory PageCategory { get; set; }
        public virtual ICollection<PageCategoryItems> Items { get; set; }
    }
}
