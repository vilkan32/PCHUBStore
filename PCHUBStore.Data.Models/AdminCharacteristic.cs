using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class AdminCharacteristic : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }


        public virtual AdminCharacteristicsCategory AdminBasicCharacteristicsCategory { get; set; }

        public virtual AdminCharacteristicsCategory AdminFullCharacteristicsCategory { get; set; }
    }
}
