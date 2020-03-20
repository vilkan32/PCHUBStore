using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class AdminCharacteristicsCategory : BaseModel<int>
    {
        [Required]
        public string CategoryName { get; set; }

        [InverseProperty("AdminBasicCharacteristicsCategory")]
        public virtual ICollection<AdminCharacteristic> BasicCharacteristics { get; set; }

        [InverseProperty("AdminFullCharacteristicsCategory")]
        public virtual ICollection<AdminCharacteristic> FullCharacteristics { get; set; }
    }
}
