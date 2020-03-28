using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.IndexPageViewModels
{
    public class AddBoxViewModel
    {
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
