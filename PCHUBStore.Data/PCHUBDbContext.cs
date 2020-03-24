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
                    .UseSqlServer("Server=DESKTOP-SLBT47B\\SQLEXPRESS;Database=PCHUB;Trusted_Connection=True");
            }
        }
        public PCHUBDbContext() { }

        public virtual DbSet<Activity> Activities { get; set; }

        public virtual DbSet<BasicCharacteristic> BasicCharacteristics { get; set; }

        public virtual DbSet<Filter> Filters { get; set; }

        public virtual DbSet<FilterCategory> FilterCategories { get; set; }
        public virtual DbSet<FullCharacteristic> FullCharacteristics { get; set; }

        public virtual DbSet<ColorfulBox> ColorfulBoxes { get; set; }
        public virtual DbSet<ProductUserFavorite> ProductUserFavorites { get; set; }
        public virtual DbSet<PageCategory> PageCategories { get; set; }
        public virtual DbSet<ProductUserReview> ProductUserReviews { get; set; }
        public virtual DbSet<PageCategoryItems> Items { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<ProductCart> ProductCarts { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<AdminCharacteristicsCategory> AdminCharacteristicsCategories { get; set; }
        public virtual DbSet<AdminCharacteristic> AdminCharacteristics { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Picture>()
                .HasOne(a => a.MainPicForProduct)
                .WithOne(b => b.MainPicture)
                .HasForeignKey<Product>(b => b.MainPictureId);


            modelBuilder.Entity<ProductUserReview>()
            .HasKey(bc => new { bc.ProductId, bc.UserId });
            modelBuilder.Entity<ProductUserReview>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.ProductUserReviews)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<ProductUserReview>()
                .HasOne(bc => bc.Product)
                .WithMany(c => c.ProductUserReviews)
                .HasForeignKey(bc => bc.ProductId);

            modelBuilder.Entity<ProductUserFavorite>()
         .HasKey(bc => new { bc.ProductId, bc.UserId });
            modelBuilder.Entity<ProductUserFavorite>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.FavoriteUserProducts)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<ProductUserFavorite>()
                .HasOne(bc => bc.Product)
                .WithMany(c => c.FavoriteUserProducts)
                .HasForeignKey(bc => bc.ProductId);


            modelBuilder.Entity<ProductCart>()
      .HasKey(bc => new { bc.CartId, bc.ProductId });
            modelBuilder.Entity<ProductCart>()
                .HasOne(bc => bc.Cart)
                .WithMany(b => b.ProductCarts)
                .HasForeignKey(bc => bc.CartId);
            modelBuilder.Entity<ProductCart>()
                .HasOne(bc => bc.Product)
                .WithMany(c => c.ProductCarts)
                .HasForeignKey(bc => bc.ProductId);
        }
    }
}
