using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class PageCategoryItems : BaseModel<int>
    {
        public string Text { get; set; }

        public string Href { get; set; }

        public int CategoryId { get; set; }
        public virtual ItemsCategory Category { get; set; }

    }
}
