using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels
{
    public class EditBoxesViewModel
    {
        public EditBoxesViewModel()
        {
            this.Boxes = new List<EditBoxViewModel>();
        }
        public string PageName { get; set; }

        public List<string> Pages { get; set; }

        public List<EditBoxViewModel> Boxes { get; set; }
    }
}
