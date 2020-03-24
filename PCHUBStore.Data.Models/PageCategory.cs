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
            this.Items = new List<PageCategoryItems>();
        }

        [Required]
        public string CategoryName { get; set; }

        public string CategoryViewName { get; set; }

        [Required]
        public string AllName { get; set; }

        [Required]
        public string AllHref { get; set; }

        public virtual ICollection<PageCategoryItems> Items { get; set; }

        public int IndexPageId { get; set; }
        public virtual Page IndexPage { get; set; }

        public string PictureUrl { get; set; }

    }
}
