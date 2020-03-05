using System;
using System.Collections.Generic;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class Filter : BaseModel<int>
    {

        public string Name { get; set; }

        public string Value { get; set; }

        public int FilterCategoryId { get; set; }
        public virtual FilterCategory FilterCategory { get; set; }

    }
}
