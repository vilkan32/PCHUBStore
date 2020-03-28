using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.IndexPageViewModels
{
    public class IndexItemsCategoryViewModel
    {
        public IndexItemsCategoryViewModel()
        {
            this.Items = new List<IndexPageItemsViewModel>
            {
                new IndexPageItemsViewModel(),
                new IndexPageItemsViewModel(),
                new IndexPageItemsViewModel(),
                new IndexPageItemsViewModel(),
                new IndexPageItemsViewModel(),
                new IndexPageItemsViewModel(),
            };
        }

        public List<string> ExistingCategories { get; set; }

        [Display(Name = "Page Category")]
        [Required]
        public string PageCategory { get; set; }


        [Display(Name = "Items Category")]
        [Required]
        public string ItemCategory { get; set; }

        public List<IndexPageItemsViewModel> Items { get; set; }
    }
}
