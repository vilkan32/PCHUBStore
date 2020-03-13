using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.View.Models.UserProfileViewModels
{
    public class EditDeliveryInformationForm
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }


    }
}
