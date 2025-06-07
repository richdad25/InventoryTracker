using InventoryTracker.Domain.Entities;
using InventoryTracker.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventoryTracker.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Добавлено для отслеживания SQL-запросов в логах (опционально)
            Console.WriteLine($"DbContext created. Connection string: {Database.GetConnectionString()}");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Важно вызвать базовый метод

            // Применение конфигураций из отдельных классов
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());

            // Конфигурация Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products"); // Явное указание имени таблицы

                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                // Value Objects
                entity.OwnsOne(p => p.Name, name =>
                {
                    name.Property(v => v.Value)
                        .HasColumnName("Name")
                        .HasMaxLength(100)
                        .IsRequired(); // Добавлено IsRequired
                });

                entity.OwnsOne(p => p.Article, article =>
                {
                    article.Property(v => v.Value)
                        .HasColumnName("Article")
                        .HasMaxLength(50)
                        .IsRequired();
                });

                entity.OwnsOne(p => p.Price, price =>
                {
                    price.Property(v => v.Value)
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();
                });

                // Стандартные свойства
                entity.Property(p => p.Description)
                    .HasMaxLength(500); // Добавлена максимальная длина

                entity.Property(p => p.Quantity)
                    .IsRequired()
                    .HasDefaultValue(0); // Значение по умолчанию

                entity.Property(p => p.Category)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(p => p.ExpiryDate)
                    .IsRequired(false);

                // Связи
                entity.HasOne(p => p.Supplier)
                    .WithMany(s => s.Products)
                    .HasForeignKey(p => p.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(); // Добавлено IsRequired если связь обязательная
            });

            // Оптимизация для Npgsql (PostgreSQL)
            modelBuilder.HasPostgresExtension("uuid-ossp"); // Если используете UUID
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Только для разработки - логирование SQL
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
#endif
        }
    }
}