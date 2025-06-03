using System;
using InventoryTracker.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InventoryTracker.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InventoryTracker.Domain.Entities.InventoryTransaction", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<DateTime>("Date")
                    .HasColumnType("timestamp with time zone");

                b.Property<Guid>("ProductId")
                    .HasColumnType("uuid");

                b.Property<int>("Quantity")
                    .HasColumnType("integer");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("character varying(10)");

                b.Property<Guid>("WarehouseId")
                    .HasColumnType("uuid");

                b.HasKey("Id");

                b.HasIndex("ProductId");

                b.HasIndex("WarehouseId");

                b.ToTable("InventoryTransactions");
            });

            modelBuilder.Entity("InventoryTracker.Domain.Entities.Product", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<decimal>("Price")
                    .HasColumnType("numeric(18,2)");

                b.HasKey("Id");

                b.ToTable("Products");
            });

            modelBuilder.Entity("InventoryTracker.Domain.Entities.Supplier", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<string>("ContactInfo")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("character varying(200)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.HasKey("Id");

                b.ToTable("Suppliers");
            });

            modelBuilder.Entity("InventoryTracker.Domain.Entities.Warehouse", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid");

                b.Property<string>("Location")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("character varying(200)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.HasKey("Id");

                b.ToTable("Warehouses");
            });

            modelBuilder.Entity("InventoryTracker.Domain.Entities.InventoryTransaction", b =>
            {
                b.HasOne("InventoryTracker.Domain.Entities.Product", "Product")
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("InventoryTracker.Domain.Entities.Warehouse", "Warehouse")
                    .WithMany()
                    .HasForeignKey("WarehouseId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Product");

                b.Navigation("Warehouse");
            });

        }
    }
}