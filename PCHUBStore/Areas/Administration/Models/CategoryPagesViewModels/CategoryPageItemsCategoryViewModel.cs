using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels
{
    public class CategoryPageItemsCategoryViewModel
    {
        public CategoryPageItemsCategoryViewModel()
        {
            this.Items = new List<PageCategoryItemsViewModel>();
        }
        public string Category { get; set; }

        public virtual List<PageCategoryItemsViewModel> Items { get; set; }
    }
}
