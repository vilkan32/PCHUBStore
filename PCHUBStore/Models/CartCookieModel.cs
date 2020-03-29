﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Models
{
    public class CartCookieModel
    {
        public CartCookieModel()
        {
            this.ProductIds = new List<string>();
        }
        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ICollection<string> ProductIds { get; set; }

    }
}
