using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class ColorfulBox : BaseModel<int>
    {

        public string Color { get; set; }

        public string Text { get; set; }

        public string Href { get; set; }

        public int IndexPageId { get; set; }
        public virtual Page IndexPage { get; set; }
    }
}
