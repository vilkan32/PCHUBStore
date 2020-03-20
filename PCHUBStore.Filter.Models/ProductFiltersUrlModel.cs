using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PCHUBStore.Filter.Models
{
    public class ProductFiltersUrlModel
    {

        public ProductFiltersUrlModel()
        {
            this.Page = 1;
        }

        [MinLength(1), MaxLength(20)]
        public string MinPrice { get; set; }


        [MinLength(1), MaxLength(20)]
        public string MaxPrice { get; set; }

        public int Page { get; set; }

        [MinLength(1), MaxLength(20)]
        public string[] Model { get; set; }

        [MinLength(1), MaxLength(20)]
        public string[] Make { get; set; }

        [MinLength(1), MaxLength(20)]
        public string[] VideoCard { get; set; }

        [MinLength(1), MaxLength(20)]
        public string[] Processor { get; set; }

        [MinLength(1), MaxLength(20)]
        public string OrderBy { get; set; }


    }
}
