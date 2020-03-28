using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.IndexPageViewModels
{
    public class EditCategoryViewModel
    {
        public string CategoryName { get; set; }

        public string AllName { get; set; }

        public string AllHref { get; set; }
        public List<EditItemCategoriesViewModel> ItemCategories { get; set; }
    }
}
