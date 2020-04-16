using PCHUBStore.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Support.Models
{
    public class ActivityViewModel
    {
        public string Id { get; set; }
        public string  OwnerName { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool ActivityClosed { get; set; }
        public string Description { get; set; }
        public ActivityType? ActivityType { get; set; }
    }
}
