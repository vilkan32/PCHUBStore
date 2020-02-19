using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data.Models;

namespace PCHUBStore.Data
{
    public class PCHUBDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public PCHUBDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public PCHUBDbContext() { }

        public virtual DbSet<Activity> Activities { get; set; }

        public virtual DbSet<BasicCharacteristic> BasicCharacteristics { get; set; }

        public virtual DbSet<FullCharacteristic> FullCharacteristics { get; set; }

        public virtual DbSet<Picture> Pictures { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }

    }
}
