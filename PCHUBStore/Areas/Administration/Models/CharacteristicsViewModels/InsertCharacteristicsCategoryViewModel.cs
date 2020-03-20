using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.CharacteristicsViewModels
{
    public class InsertCharacteristicsCategoryViewModel
    {
        [Required]
        [Display(Name= "Category Name")]
        [RegularExpression(@"[A-Z]{1}[a-z]*", ErrorMessage = "Invalid Category Name")]
        public string CategoryName { get; set; }

        public List<string> Categories { get; set; }
    }
}
