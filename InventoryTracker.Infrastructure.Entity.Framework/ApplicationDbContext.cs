using InventoryTracker.Domain.Entities;
using InventoryTracker.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Уберите вызов ApplyConfiguration для ProductConfiguration
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());

            // Полная конфигурация Product здесь
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                // Конфигурация Value Objects
                entity.OwnsOne(p => p.Name, name =>
                {
                    name.Property(v => v.Value)
                        .HasColumnName("Name")
                        .HasMaxLength(100);
                });

                entity.OwnsOne(p => p.Article, article =>
                {
                    article.Property(v => v.Value)
                        .HasColumnName("Article")
                        .HasMaxLength(50);
                });

                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(v => v.Value)
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)");
                });

                // Остальные свойства
                entity.Property(p => p.Description).IsRequired();
                entity.Property(p => p.Quantity).IsRequired();
                entity.Property(p => p.Category).IsRequired();
                entity.Property(p => p.ExpiryDate).IsRequired(false);

                // Навигационные свойства
                entity.HasOne(p => p.Supplier)
                    .WithMany(s => s.Products)
                    .HasForeignKey(p => p.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}