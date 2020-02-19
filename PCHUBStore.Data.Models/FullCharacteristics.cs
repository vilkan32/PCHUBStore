using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class FullCharacteristic : BaseModel<int>
    {

        [StringLength(50)]
        [Required]
        public string Key { get; set; }

        public bool ValueHasArray { get; set; }

        [Required]
        [StringLength(50)]
        public string Value { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
