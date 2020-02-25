﻿using System;
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

        public decimal Price { get; set; }
        public string Title { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public ICollection<string> BasicCharacteristics { get; set; }
    }
}
