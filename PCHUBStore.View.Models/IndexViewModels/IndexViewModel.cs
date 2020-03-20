using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models.IndexViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Boxes = new List<BoxViewModel>();
            this.Categories = new List<IndexCategoryViewModel>();
        }

        public virtual ICollection<BoxViewModel> Boxes { get; set; }

        public virtual ICollection<IndexCategoryViewModel> Categories { get; set; }
    }
}
