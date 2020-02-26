using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Picture : BaseModel<int>
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public bool IsVideo { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string MainPicForProductId { get; set; }
        public virtual Product MainPicForProduct { get; set; }

        [InverseProperty("Pictures")]
        public virtual User Uploader { get; set; } 
        public virtual string RedirectTo { get; set; }
    }
}
