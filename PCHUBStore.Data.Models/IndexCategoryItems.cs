using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class IndexCategoryItems : BaseModel<int>
    {
        public string Text { get; set; }

        public string Href { get; set; }

        public int CategoryId { get; set; }
        public virtual IndexCategory Category { get; set; }

    }
}
