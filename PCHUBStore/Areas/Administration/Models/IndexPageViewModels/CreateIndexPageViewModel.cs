using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.IndexPageViewModels
{
    public class CreateIndexPageViewModel
    {
        [Display(Name= "Page Name")]
        [Required]
        public string PageName { get; set; }
    }
}
