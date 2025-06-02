using InventoryTracker.Application.Models.Base;

namespace InventoryTracker.Application.Models.Supplier
{
    public record SupplierModel(
        Guid Id,
        string Name,
        string ContactInfo) : IModel<Guid>;
}