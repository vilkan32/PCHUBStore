﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PCHUBStore.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Pictures = new List<Picture>();
            this.Shipments = new List<Shipment>();
            this.Activities = new List<Activity>();
        }

        [StringLength(50, MinimumLength = 4)]
        public string Address { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string City { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual Picture ProfilePicture { get; set; }

        public virtual ICollection<Product> LastReviewedProducts { get; set; }

    }
}
