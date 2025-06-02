using InventoryTracker.Application.Models.Base;

namespace InventoryTracker.Application.Models.Product
{
    public record CreateProductModel(
        string Name,
        string Description,
        string Article,
        decimal Price,
        int Quantity,
        string Category,
        Guid SupplierId,
        DateTime? ExpiryDate) : ICreateModel, ISupplierModel<Guid>;
}