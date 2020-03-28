using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.IndexPageViewModels
{
    public class EditIndexCategoryViewModel
    {
        [Required]
        public string Category { get; set; }
        public List<string> Categories { get; set; }

        public List<EditCategoryViewModel> PageCategory { get; set; }

        public bool DisplayForm { get; set; }
    }
}
