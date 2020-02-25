using System;
using System.Collections.Generic;

namespace PCHUBStore.Filter.Models
{
    public class LaptopFilters
    {

        public LaptopFilters()
        {
            this.Price = new List<string>();
            this.Model = new List<string>();
            this.LaptopType = new List<string>();
            this.SuitableFor = new List<string>();
            this.Processor = new List<string>();
            this.VideoCard = new List<string>();
        }

        public ICollection<string> Price { get; set; }

        public ICollection<string> Model { get; set; }

        public ICollection<string> LaptopType { get; set; }

        public ICollection<string> SuitableFor { get; set; }

        public ICollection<string> Processor { get; set; }

        public ICollection<string> VideoCard { get; set; }

    }
}
