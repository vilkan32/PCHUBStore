using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.ProductViewModel
{
    public class InsertJsonProductViewModel
    {
        [Required]
        public string Category { get; set; }
        [Required]
        [Display(Name = "Basic Characteristics")]
        public string BasicCharacteristics { get; set; }
        [Required]
        [Display(Name = "Full Characteristics")]
        public string FullCharacteristics { get; set; }

        public List<string> Categories { get; set; }
    }
}
