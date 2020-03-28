using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.IndexViewModels
{
    public class IndexCategoryViewModel
    {
        public IndexCategoryViewModel()
        {
            this.ItemCategories = new List<IndexItemCategoryViewModel>();
        }

        public string CategoryName { get; set; }


        public string AllName { get; set; }

        public string AllHref { get; set; }

        public ICollection<IndexItemCategoryViewModel> ItemCategories { get; set; }

        public string PictureUrl { get; set; }
    }
}
