using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models
{
    public class LaptopViewModel
    {

        public LaptopViewModel()
        {
            this.BasicCharacteristics = new List<string>();
        }

        public string Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string PictureUrl { get; set; }
        public string LaptopAdvancedDetailsUrl { get; set; }
        public ICollection<string> BasicCharacteristics { get; set; }
    }
}
