using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class FilterCategory : BaseModel<int>
    {

        public FilterCategory()
        {
            this.Filters = new List<Filter>();
        }

        public string CategoryName { get; set; }
        public string ViewSubCategoryName { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }

    }
}
