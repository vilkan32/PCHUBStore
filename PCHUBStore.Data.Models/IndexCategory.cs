using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class IndexCategory : BaseModel<int>
    {
        public IndexCategory()
        {
            this.Items = new List<IndexCategoryItems>();
        }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string AllName { get; set; }

        [Required]
        public string AllHref { get; set; }

        public virtual ICollection<IndexCategoryItems> Items { get; set; }

        public int IndexPageId { get; set; }
        public virtual IndexPage IndexPage { get; set; }

        public string PictureUrl { get; set; }

    }
}
