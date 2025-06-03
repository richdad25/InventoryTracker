//using InventoryTracker.Domain.Entities;
//using InventoryTracker.Domain.ValueObjects;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace InventoryTracker.Infrastructure.EntityFramework.Configurations
//{
//    public class ProductConfiguration : IEntityTypeConfiguration<Product>
//    {
//        public void Configure(EntityTypeBuilder<Product> builder)
//        {
//            builder.HasKey(p => p.Id);

//            // Настройка ProductName
//            builder.Property(p => p.Name)
//                 .HasConversion(
//                     name => name.ToString(),  // Используем ToString()
//                     str => new ProductName(str))
//                 .HasColumnName("Name")
//                 .IsRequired()
//                 .HasMaxLength(100);

//            builder.Property(p => p.Article)
//                .HasConversion(
//                    article => article.ToString(),
//                    str => new ArticleNumber(str))
//                .HasColumnName("Article")
//                .IsRequired()
//                .HasMaxLength(20);

//            // Остальные свойства
//            builder.Property(p => p.Price)
//                .IsRequired()
//                .HasColumnType("decimal(18,2)");

//            builder.HasOne(p => p.Supplier)
//                .WithMany(s => s.Products)
//                .HasForeignKey(p => p.SupplierId)
//                .OnDelete(DeleteBehavior.Restrict);
//        }
//    }
//}