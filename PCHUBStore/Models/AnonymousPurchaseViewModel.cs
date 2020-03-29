using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Models
{
    public class AnonymousPurchaseViewModel
    {
        [StringLength(50, MinimumLength = 4)]
        public string Address { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string City { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string[] ProductIds { get; set; }

    }
}
