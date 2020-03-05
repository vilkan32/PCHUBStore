using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public abstract class BaseCharacteristicsModel : BaseModel<int>
    {

 
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }


    }
}
