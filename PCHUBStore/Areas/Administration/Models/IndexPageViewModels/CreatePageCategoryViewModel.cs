using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.IndexPageViewModels
{
    public class CreatePageCategoryViewModel
    {


        [Required]
        [Display(Name= "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "All Name")]
        public string AllName { get; set; }

        [Required]
        [Display(Name = "All Href")]
        public string AllHref { get; set; }

    }
}
