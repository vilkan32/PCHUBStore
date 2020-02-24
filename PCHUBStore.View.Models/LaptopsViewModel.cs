using PCHUBStore.Filter.Models;
using PCHUBStore.View.Models.Pagination;
using System;

namespace PCHUBStore.View.Models
{
    public class LaptopsViewModel
    {
        public Pager Pager { get; set; }

        public LaptopViewModel[] Laptops { get; set; }

        public LaptopFilters Filters { get; set; }
    }
}
