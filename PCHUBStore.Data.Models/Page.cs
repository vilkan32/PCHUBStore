using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Page : BaseModel<int>
    {
        public Page()
        {
            this.ColorfulBoxes = new List<ColorfulBox>();
            this.Categories = new List<PageCategory>();
        }

        public virtual ICollection<ColorfulBox> ColorfulBoxes { get; set; }


        [Required]
        public string PageName { get; set; }

        public virtual ICollection<PageCategory> Categories { get; set; }

    }
}
