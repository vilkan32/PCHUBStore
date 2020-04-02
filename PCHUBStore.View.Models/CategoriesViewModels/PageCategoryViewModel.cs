using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.CategoriesViewModels
{
    public class PageCategoryViewModel
    {

        public string CategoryName { get; set; }

        public string AllName { get; set; }

        public string AllHref { get; set; }
        public List<CategoryPageItemsCategoryViewModel> ItemsCategories { get; set; }
        public string Picture { get; set; }
    }
}
