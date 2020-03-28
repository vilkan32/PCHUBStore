using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.IndexViewModels
{
    public class IndexItemCategoryViewModel
    {
        public IndexItemCategoryViewModel()
        {
            this.Items = new List<IndexCategoryItemViewModel>();
        }
        public string ItemCategory { get; set; }

        public ICollection<IndexCategoryItemViewModel> Items { get; set; }
    }
}
