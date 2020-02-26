using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class FullCharacteristic : BaseCharacteristicsModel
    {

        public bool ValueHasArray { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
