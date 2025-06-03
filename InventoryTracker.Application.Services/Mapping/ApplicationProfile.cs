using InventoryTracker.Application.Models;
using InventoryTracker.Application.Models.Product;
using InventoryTracker.Application.Models.Supplier;
using InventoryTracker.Application.Models.Transaction;
using InventoryTracker.Application.Models.Warehouse;
using InventoryTracker.Domain.Entities;
using AutoMapper;
using InventoryTracker.Application.Models.Report;

namespace InventoryTracker.Application.Services.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<CreateProductModel, Product>();

            CreateMap<Warehouse, WarehouseModel>();
            CreateMap<CreateWarehouseModel, Warehouse>();

            CreateMap<InventoryTransaction, InventoryTransactionModel>();
            CreateMap<CreateInventoryTransactionModel, InventoryTransaction>();

            CreateMap<Supplier, SupplierModel>();
            CreateMap<CreateSupplierModel, Supplier>();

            CreateMap<InventoryReportModel, InventoryReportModel>();
        }
    }
}