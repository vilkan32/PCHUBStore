using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PCHUBStore.View.Models.UserProfileViewModels
{
    public class UserProfileViewModel
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

        public string AccountSettingsActive { get; set; }

        public string ProfilePictureUrl { get; set; }
        public EditDeliveryInformationForm OrderInformation { get; set; }

        public EditAccountSettingsForm AccountSettings { get; set; }

        public ICollection<UserOrdersViewModel> Orders { get; set; }

        public ICollection<ProductHistoryViewModel> History { get; set; }

        public ICollection<ProductFavoriteViewModel> Favorites { get; set; }
    }

}
