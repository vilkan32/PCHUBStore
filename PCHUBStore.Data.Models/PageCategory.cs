using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class PageCategory : BaseModel<int>
    {


        public PageCategory()
        {
            this.ItemsCategories = new List<ItemsCategory>();
            this.Pictures = new List<Picture>();
        }

        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string AllName { get; set; }

        [Required]
        public string AllHref { get; set; }

        public virtual ICollection<ItemsCategory> ItemsCategories { get; set; }

        public int IndexPageId { get; set; }
        public virtual Page IndexPage { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }

    }
}
