using Database.SQL.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.SQL
{
    //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
    public class ShopDbContext : DbContext
    {
        //public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ShopEntity> Shops { get; set; }
        public DbSet<HistoryEntity> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer
            modelBuilder.Entity<CustomerEntity>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
                builder.Property(c => c.PhoneNumber).HasMaxLength(20);

                builder.HasMany(c => c.Histories)
                       .WithOne(h => h.Customer)
                       .HasForeignKey(h => h.CustomerId);
            });

            // Shop
            modelBuilder.Entity<ShopEntity>(builder =>
            {
                builder.HasKey(s => s.Id);
                builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
                builder.Property(s => s.Password).IsRequired();
                builder.Property(s => s.Logo);

                builder.HasMany(s => s.Products)
                       .WithOne(p => p.Shop)
                       .HasForeignKey(p => p.ShopId);

                builder.HasMany(s => s.Histories)
                       .WithOne(h => h.Shop)
                       .HasForeignKey(h => h.ShopId);
            });

            // Product
            modelBuilder.Entity<ProductEntity>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
                builder.Property(p => p.Description).HasMaxLength(500);
                builder.Property(p => p.Count).IsRequired();
                builder.Property(p => p.PurchasePrice).IsRequired();
                builder.Property(p => p.Cost).IsRequired();
                builder.Property(p => p.Image);

                builder.HasMany(p => p.Histories)
                       .WithOne(h => h.Product)
                       .HasForeignKey(h => h.ProductId)
                       .OnDelete(DeleteBehavior.Restrict);
            });

            // History
            modelBuilder.Entity<HistoryEntity>(builder =>
            {
                builder.HasKey(h => h.Id);

                builder.HasOne(h => h.Shop)
                       .WithMany(s => s.Histories)
                       .HasForeignKey(h => h.ShopId);

                builder.HasOne(h => h.Customer)
                       .WithMany(c => c.Histories)
                       .HasForeignKey(h => h.CustomerId)
                           .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(h => h.Product)
                       .WithMany(p => p.Histories)
                       .HasForeignKey(h => h.ProductId)
                       .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Shop;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
