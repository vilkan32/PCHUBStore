using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.FilterViewModels
{
    public class InsertBasicFiltersViewModel
    {
        public List<string> Categories { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
