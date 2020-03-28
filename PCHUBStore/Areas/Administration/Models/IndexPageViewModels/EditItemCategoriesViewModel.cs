using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.IndexPageViewModels
{
    public class EditItemCategoriesViewModel
    {

        [Required]
        [Display(Name = "Item Category")]
        public string ItemCategory { get; set; }

        public List<IndexPageItemsViewModel> Items { get; set; }
    }
}
