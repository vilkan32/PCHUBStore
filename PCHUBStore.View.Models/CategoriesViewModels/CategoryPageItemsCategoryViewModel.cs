using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.CategoriesViewModels
{
    public class CategoryPageItemsCategoryViewModel
    {
        public string Category { get; set; }
        public virtual List<PageCategoryItemsViewModel> Items { get; set; }
    }
}
