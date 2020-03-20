using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.CharacteristicsViewModels
{
    public class InsertCharacteristicsViewModel
    {

        public InsertCharacteristicsViewModel()
        {
            this.FullCharacteristics = new List<string>
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                 string.Empty,
            };

            this.BasicCharacteristics = new List<string>
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,

            };
        }
        [Required]
        public string Category { get; set; }

        public List<string> FullCharacteristics { get; set; }

        public List<string> BasicCharacteristics { get; set; }

        public List<string> Categories { get; set; }
    }
}
