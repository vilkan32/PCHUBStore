using PCHUBStore.Filter.Models;
using PCHUBStore.View.Models.FilterViewModels;
using PCHUBStore.View.Models.Pagination;
using System;
using System.Collections.Generic;

namespace PCHUBStore.View.Models
{
    public class ProductsViewModel
    {

        public ProductsViewModel()
        {
            this.Products = new List<ProductViewModel>();
            this.ProductMakeUrls = new List<string>();
            this.FilterCategory = new List<FilterCategoryViewModel>();
        }

        public Pager Pager { get; set; }
        public string Category { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }

        public ICollection<string> ProductMakeUrls { get; set; }

        public ICollection<FilterCategoryViewModel> FilterCategory { get; set; }
    }
}
