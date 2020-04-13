using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.Data.Models.Enums
{
    public enum ShippingCompany
    {
        Econt,
        Speedy,
        [Display(Name = "BG Post")]
        BGPost
    }
}
