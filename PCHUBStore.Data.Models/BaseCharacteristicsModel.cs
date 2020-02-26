using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public abstract class BaseCharacteristicsModel : BaseModel<int>
    {

        [StringLength(50)]
        [Required]
        public string Key { get; set; }

        [Required]
        [StringLength(50)]
        public string Value { get; set; }


    }
}
