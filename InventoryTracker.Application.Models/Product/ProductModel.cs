using InventoryTracker.Application.Models.Base;

namespace InventoryTracker.Application.Models.Product
{
    public record ProductModel(
        Guid Id,
        string Name,
        string Description,
        string Article,
        decimal Price,
        int Quantity,
        string Category,
        Guid SupplierId,
        DateTime? ExpiryDate) : IModel<Guid>, ISupplierModel<Guid>;
}