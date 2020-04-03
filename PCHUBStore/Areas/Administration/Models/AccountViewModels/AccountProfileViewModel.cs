using PCHUBStore.View.Models.UserProfileViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Models.AccountViewModels
{
    public class AccountProfileViewModel
    {
  public string Username { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string OrdersActive { get; set; }

        public string ProfileActive { get; set; }

        public string DeliveryInformationActive { get; set; }

        public string ProfilePictureUrl { get; set; }

    }
}
