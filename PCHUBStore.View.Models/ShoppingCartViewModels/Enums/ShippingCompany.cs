using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.View.Models.ShoppingCartViewModels.Enums
{
    public enum ShippingCompany
    {
        Econt,
        Speedy,
        [Display(Name = "Bulgarian Posts")]
        BGPost
    }
}
