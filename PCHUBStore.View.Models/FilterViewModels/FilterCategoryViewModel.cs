using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.FilterViewModels
{
    public class FilterCategoryViewModel
    {

        public FilterCategoryViewModel()
        {
            this.Filters = new List<FilterViewModel>();
        }
        public ICollection<FilterViewModel> Filters { get; set; }
        public string CategoryName { get; set; }
        public string ViewSubCategoryName { get; set; }
    }
}
