using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels
{
    public class CreateCategoryPageViewModel
    {
        public CreateCategoryPageViewModel()
        {
            this.PageCategory = new PageCategoryViewModel();
        }

        [Display(Name = "Page Name")]
        [Required]
        public string PageName { get; set; }
        public string PreviousPageName { get; set; }
        public List<string> Pages { get; set; }
        [Display(Name = "Is Page Deleted")]
        public bool IsDeleted { get; set; }
        public PageCategoryViewModel PageCategory { get; set; }

    }
}
