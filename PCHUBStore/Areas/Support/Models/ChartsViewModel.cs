using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class ChartsViewModel
    {
        public List<MostViewedProducts> MostViewedProducts { get; set; }

        public List<MostExpensiveProducts> MostExpensiveProducts { get; set; }
    }
}
