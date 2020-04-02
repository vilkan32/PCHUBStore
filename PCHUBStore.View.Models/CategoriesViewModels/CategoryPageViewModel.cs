using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.CategoriesViewModels
{
    public class CategoryPageViewModel
    {
        public string PageName { get; set; }

        public List<BoxViewModel> Boxes { get; set; }

        public PageCategoryViewModel PageCategory { get; set; }
    }
}
