using InventoryTracker.Application.Models.Base;

namespace InventoryTracker.Application.Models.Supplier
{
    public record CreateSupplierModel(
        string Name,
        string ContactInfo) : ICreateModel;
}