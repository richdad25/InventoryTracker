using AutoMapper;
using InventoryTracker.Application.Models;
using InventoryTracker.Application.Models.Product;
using InventoryTracker.Application.Models.Transaction;
using InventoryTracker.Application.Models.Warehouse;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.ValueObjects;
using Otus.QueueDto.Inventory;

namespace InventoryTracker.Application.Services.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            // Product
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src.Article));

            // Warehouse
            CreateMap<Warehouse, WarehouseModel>();

            // Transaction
            CreateMap<InventoryTransaction, InventoryTransactionModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}