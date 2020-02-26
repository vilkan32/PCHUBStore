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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer("Server=DESKTOP-SLBT47B\\SQLEXPRESS;Database=PCSmartHUBLatestVersion;Trusted_Connection=True");
            }
        }
        public PCHUBDbContext() { }

        public virtual DbSet<Activity> Activities { get; set; }

        public virtual DbSet<BasicCharacteristic> BasicCharacteristics { get; set; }

        public virtual DbSet<Filter> Filters { get; set; }

        public virtual DbSet<FilterCategory> FilterCategories { get; set; }
        public virtual DbSet<FullCharacteristic> FullCharacteristics { get; set; }

        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Picture>()
                .HasOne(a => a.MainPicForProduct)
                .WithOne(b => b.MainPicture)
                .HasForeignKey<Product>(b => b.MainPictureId);
        }
    }
}
