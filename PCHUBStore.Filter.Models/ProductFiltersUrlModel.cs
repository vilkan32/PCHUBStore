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


        public string MinPrice { get; set; }

        public string MaxPrice { get; set; }

        public int Page { get; set; }

        public string[] Model { get; set; }

        public string[] Make { get; set; }

        public string[] OS { get; set; }

        public string[] RAM { get; set; }

        public string[] VideoCard { get; set; }

        public string[] Processor { get; set; }

        public string[] Resolution { get; set; }

        public string[] FPS { get; set; }
        public string[] ReactionTime { get; set; }

        public string[] MatrixType { get; set; }
        public string[] Gaming { get; set; }

        public string[] Interface { get; set; }

        public string[] Mechanical { get; set; }

        public string[] Type { get; set; }
        public string[] Connectivity { get; set; }
        public string[] DisplaySize { get; set; }
        public string OrderBy { get; set; }


    }
}
