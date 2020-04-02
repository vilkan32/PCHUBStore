using Microsoft.AspNetCore.Http;
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
            this.ItemsCategories = new List<CategoryPageItemsCategoryViewModel>
            {
                new CategoryPageItemsCategoryViewModel(),
                new CategoryPageItemsCategoryViewModel(),
                new CategoryPageItemsCategoryViewModel(),
                new CategoryPageItemsCategoryViewModel(),
                new CategoryPageItemsCategoryViewModel(),
            };

            this.Pictures = new List<string>();
        }

        [Required]
        [Display(Name = "Page Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "All Name")]
        public string AllName { get; set; }

        [Required]
        [Display(Name = "All Href")]
        public string AllHref { get; set; }
        public List<CategoryPageItemsCategoryViewModel> ItemsCategories { get; set; }
        public List<string> Pictures { get; set; }
    }
}
