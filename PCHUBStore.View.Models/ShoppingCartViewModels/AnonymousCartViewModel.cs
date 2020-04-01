using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.View.Models.ShoppingCartViewModels
{
    public class AnonymousCartViewModel
    {
        public AnonymousCartViewModel()
        {
            this.Products = new List<PurchaseProductsAnonymousViewModel>();
        }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<PurchaseProductsAnonymousViewModel> Products { get; set; }
    }
}
