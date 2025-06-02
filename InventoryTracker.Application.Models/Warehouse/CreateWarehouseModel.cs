using InventoryTracker.Application.Models.Base;

namespace InventoryTracker.Application.Models.Warehouse
{
    public record CreateWarehouseModel(
        string Name,
        string Location,
        double Capacity) : ICreateModel;
}