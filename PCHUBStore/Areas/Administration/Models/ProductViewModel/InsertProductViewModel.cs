using Microsoft.AspNetCore.Http;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.ProductViewModel
{
    public class InsertProductViewModel
    {
        public InsertProductViewModel()
        {
            this.BasicCharacteristics = new List<InserBasicCharacteristicsViewModel>
            {
                new InserBasicCharacteristicsViewModel(),
                 new InserBasicCharacteristicsViewModel(),
                  new InserBasicCharacteristicsViewModel(),
                   new InserBasicCharacteristicsViewModel(),
                    new InserBasicCharacteristicsViewModel(),
                     new InserBasicCharacteristicsViewModel(),
                      new InserBasicCharacteristicsViewModel(),

             
            };

            this.FullCharacteristics = new List<InsertAdvancedCharacteristicsViewModel>
            {
                new InsertAdvancedCharacteristicsViewModel(),
                 new InsertAdvancedCharacteristicsViewModel(),
                  new InsertAdvancedCharacteristicsViewModel(),
                   new InsertAdvancedCharacteristicsViewModel(),
                    new InsertAdvancedCharacteristicsViewModel(),
                     new InsertAdvancedCharacteristicsViewModel(),
                      new InsertAdvancedCharacteristicsViewModel(),
                       new InsertAdvancedCharacteristicsViewModel(),
                        new InsertAdvancedCharacteristicsViewModel(),
                         new InsertAdvancedCharacteristicsViewModel(),
                          new InsertAdvancedCharacteristicsViewModel(),
                           new InsertAdvancedCharacteristicsViewModel(),
                            new InsertAdvancedCharacteristicsViewModel(),
                             new InsertAdvancedCharacteristicsViewModel(),
                              new InsertAdvancedCharacteristicsViewModel(),
                               new InsertAdvancedCharacteristicsViewModel(),
                                new InsertAdvancedCharacteristicsViewModel(),
                                 new InsertAdvancedCharacteristicsViewModel(),
                                  new InsertAdvancedCharacteristicsViewModel(),
                                   new InsertAdvancedCharacteristicsViewModel(),
                                    new InsertAdvancedCharacteristicsViewModel(),


            };
        }

        [Required]
        [Range(20, 20000)]
        public decimal Price { get; set; }


        [Display(Name = "Article Number")]
        [Required]
        public string ArticleNumber { get; set; }

        [Display(Name = "Main Picture")]
        public virtual IFormFile MainPicture { get; set; }

        [Required]
        [MaxLength(50)]
        public string Make { get; set; }
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }
        [Required]
        public int Quantity { get; set; }

        public virtual List<InserBasicCharacteristicsViewModel> BasicCharacteristics { get; set; }

        public virtual List<InsertAdvancedCharacteristicsViewModel> FullCharacteristics { get; set; }


        // thats goona be cool
        [Display(Name = "Html Description")]
        [StringLength(9000)]
        public string HtmlDescription { get; set; }

        public virtual ICollection<IFormFile> Pictures { get; set; }

        public List<string> Categories { get; set; }
    }
}
