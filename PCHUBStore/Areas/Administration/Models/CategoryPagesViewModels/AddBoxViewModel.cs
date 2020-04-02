using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels
{
    public class AddBoxViewModel
    {
        public string PageName { get; set; }

        public List<string> PageNames { get; set; }

        [Required]
        public string Color { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Href { get; set; }

        [Required]
        [Display(Name = "Delete")]
        public bool IsDeleted { get; set; }
    }
}
