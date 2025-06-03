using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryTracker.Infrastructure.EntityFramework.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(r => r.Content)
                .IsRequired()
                .HasColumnType("jsonb"); // Для PostgreSQL
        }
    }
}