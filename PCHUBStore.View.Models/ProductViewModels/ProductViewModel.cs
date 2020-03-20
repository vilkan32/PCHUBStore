using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models
{
    public class ProductViewModel
    {

        public ProductViewModel()
        {
            this.BasicCharacteristics = new List<string>();
        }

        public string Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string PictureUrl { get; set; }
        public string ProductAdvancedDetailsUrl { get; set; }
        public ICollection<string> BasicCharacteristics { get; set; }
    }
}
