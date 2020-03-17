using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class IndexPage : BaseModel<int>
    {
        public IndexPage()
        {
            this.ColorfulBoxes = new List<ColorfulBox>();
            this.Categories = new List<IndexCategory>();
        }

        public virtual ICollection<ColorfulBox> ColorfulBoxes { get; set; }

        public virtual ICollection<IndexCategory> Categories { get; set; }

    }
}
