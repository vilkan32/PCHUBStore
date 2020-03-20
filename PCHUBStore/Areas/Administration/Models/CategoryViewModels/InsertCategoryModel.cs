using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.CategoryViewModels
{
    public class InsertCategoryModel
    {

        public InsertCategoryModel()
        {
            this.ExistingCategories = new List<string>();
        }

        [Required]
        [MaxLength(30), MinLength(3)]
        [RegularExpression(@"[A-Z]{1}[a-z]*", ErrorMessage = "Invalid Category Name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public ICollection<string> ExistingCategories { get; set; }
    }
}
