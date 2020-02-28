using PCHUBStore.Filter.Models;
using PCHUBStore.View.Models.FilterViewModels;
using PCHUBStore.View.Models.Pagination;
using System;
using System.Collections.Generic;

namespace PCHUBStore.View.Models
{
    public class LaptopsViewModel
    {

        public LaptopsViewModel()
        {
            this.Laptops = new List<LaptopViewModel>();
            this.LaptopMakeUrls = new List<string>();
        }

        public Pager Pager { get; set; }

        public ICollection<LaptopViewModel> Laptops { get; set; }

        public List<string> AppliedFilters { get; set; }
        public ICollection<string> LaptopMakeUrls { get; set; }
        public FilterCategoryViewModel FilterCategory { get; set; }
    }
}
