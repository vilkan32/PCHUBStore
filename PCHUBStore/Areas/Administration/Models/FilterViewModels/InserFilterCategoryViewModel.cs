using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.FilterViewModels
{
    public class InserFilterCategoryViewModel
    {
        public List<string> Categories { get; set; }

        [Required]
        public string Category { get; set; }


        [Required]
        [Display(Name = "Category View Sub Name")]
        public string CategoryViewSubName { get; set; }

    }
}
