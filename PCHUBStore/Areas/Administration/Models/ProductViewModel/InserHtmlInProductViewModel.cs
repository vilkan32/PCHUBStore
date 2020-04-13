using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.ProductViewModel
{
    public class InserHtmlInProductViewModel
    {
        public string ProductId { get; set; }

        public string PictureUrl { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }

        [Display(Name = "Html Content")]
        public string HtmlContent { get; set; }
    }
}
