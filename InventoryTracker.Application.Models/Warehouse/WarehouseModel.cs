using InventoryTracker.Application.Models.Base;

namespace InventoryTracker.Application.Models.Warehouse
{
    public record WarehouseModel(
        Guid Id,
        string Name,
        string Location,
        double Capacity) : IModel<Guid>;
}