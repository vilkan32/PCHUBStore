using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models
{
    public class LaptopFullCharacteristicsViewModel
    {
        public LaptopFullCharacteristicsViewModel()
        {
            this.AdvancedDetails = new List<LaptopAdvancedDetailsViewModel>();
            this.Pictures = new List<string>();
            this.BasicDetails = new List<string>();
        }

        public string ArticleNumber { get; set; }

        public string Title { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public string Price { get; set; }

        public ICollection<LaptopAdvancedDetailsViewModel> AdvancedDetails { get; set; }

        public ICollection<string> Pictures { get; set; }

        public string MainPicture { get; set; }

        public ICollection<string> BasicDetails { get; set; }
    }
}
