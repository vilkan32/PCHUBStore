using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Product : BaseModel<string>
    {
        public Product()
        {   
            this.Id = Guid.NewGuid().ToString();
            this.FullCharacteristics = new List<FullCharacteristic>();
            this.BasicCharacteristics = new List<BasicCharacteristic>();
            this.Pictures = new List<Picture>();
            this.ProductCarts = new List<ProductCart>();
        }


        [Range(1, 20000)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string ArticleNumber { get; set; }

        public int? MainPictureId { get; set; }

        public virtual Picture MainPicture { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Title { get; set; }

        public int Views { get; set; }

        public bool? OnSale { get; set; }

        public bool? OnOffer { get; set; }

        public int Quantity { get; set; }
        [Range(1, 20000)]
        public decimal? PreviousPrice { get; set; }
        [Range(1, 20000)]
        public decimal? CurrentPrice { get; set; }
        public virtual ICollection<BasicCharacteristic> BasicCharacteristics { get; set; }

        public virtual ICollection<FullCharacteristic> FullCharacteristics { get; set; }

        // thats goona be cool
        [StringLength(9000)]
        public string HtmlDescription { get; set; }

        // could be video also
        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<ProductUserReview> ProductUserReviews { get; set; }

        public virtual ICollection<ProductUserFavorite> FavoriteUserProducts { get; set; }
        public virtual List<ShipmentProduct> ShipmentProducts { get; set; }
        public virtual ICollection<ProductCart> ProductCarts { get; set; }
    }
}
