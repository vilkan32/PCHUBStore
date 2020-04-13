using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.View.Models
{
    public class ProductFullCharacteristicsViewModel
    {
        public ProductFullCharacteristicsViewModel()
        {
            this.AdvancedDetails = new List<ProductAdvancedDetailsViewModel>();
            this.Pictures = new List<string>();
            this.BasicDetails = new List<string>();
            this.SimilarProducts = new List<SimilarProduct>();
        }

        public string Id { get; set; }
        public string ArticleNumber { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public string HtmlDescription { get; set; }

        public string Price { get; set; }

        public ICollection<ProductAdvancedDetailsViewModel> AdvancedDetails { get; set; }

        public ICollection<string> Pictures { get; set; }
        
        public string MainPicture { get; set; }
        public ICollection<SimilarProduct> SimilarProducts { get; set; }
        public ICollection<string> BasicDetails { get; set; }
    }
}
