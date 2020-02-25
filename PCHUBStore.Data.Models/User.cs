using System;
using System.Collections.Generic;
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

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual Picture ProfilePicture { get; set; }
    }
}
