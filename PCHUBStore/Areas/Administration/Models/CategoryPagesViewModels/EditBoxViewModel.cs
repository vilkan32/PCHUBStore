using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.CategoryPagesViewModels
{
    public class EditBoxViewModel
    {

        [Required]
        public string Color { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Href { get; set; }

        public bool IsDeleted { get; set; }

    }
}
