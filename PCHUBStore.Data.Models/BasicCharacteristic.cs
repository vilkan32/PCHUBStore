using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models
{
    public class BasicCharacteristic : BaseModel<int>
    {
        // description is json because it is light
        [Required]
        public string Description { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
