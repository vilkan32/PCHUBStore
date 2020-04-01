using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels
{
    public class PageCategoryViewModel
    {
        public PageCategoryViewModel()
        {
            this.ItemsCategories = new List<CategoryPageItemsCategoryViewModel>();
            this.Pictures = new List<string>();
        }

        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string AllName { get; set; }

        [Required]
        public string AllHref { get; set; }

        public List<CategoryPageItemsCategoryViewModel> ItemsCategories { get; set; }

        public List<string> Pictures { get; set; }
    }
}
