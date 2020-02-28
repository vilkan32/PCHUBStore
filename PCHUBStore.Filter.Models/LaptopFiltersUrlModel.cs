using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PCHUBStore.Filter.Models
{
    public class LaptopFiltersUrlModel
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Price { get; set; }

        [Required]
        public int Page { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string[] Model { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string[] Make { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string[] Processor { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string[] VideoCard { get; set; }

    }
}
