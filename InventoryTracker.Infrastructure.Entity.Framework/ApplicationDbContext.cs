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
            // Применение конфигураций из отдельных классов
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());

            // Конфигурация Value Objects для Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.OwnsOne(p => p.Name, name =>
                {
                    name.Property(v => v.Value)
                        .HasColumnName("Name")
                        .HasMaxLength(100);

                    // Дополнительные настройки при необходимости
                    name.HasIndex(v => v.Value); // Индекс для имени
                });

                entity.OwnsOne(p => p.Article, article =>
                {
                    article.Property(v => v.Value)
                        .HasColumnName("Article")
                        .HasMaxLength(50);

                    article.HasIndex(v => v.Value).IsUnique(); // Уникальный индекс для артикула
                });

                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(v => v.Value)
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)");
                });
            });

            // Настройка связей
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(t => t.Product)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}