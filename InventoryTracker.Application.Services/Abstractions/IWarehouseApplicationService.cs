using InventoryTracker.Application.Models.Warehouse;
using System.Threading.Tasks;

namespace InventoryTracker.Application.Services.Abstractions
{
    public interface IWarehouseApplicationService
    {
        Task<WarehouseModel?> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<WarehouseModel>> GetWarehousesAsync(CancellationToken cancellationToken);
        Task<WarehouseModel?> CreateWarehouseAsync(CreateWarehouseModel warehouse, CancellationToken cancellationToken);
        Task<bool> UpdateWarehouseAsync(WarehouseModel warehouse, CancellationToken cancellationToken);
        Task<bool> DeleteWarehouseAsync(Guid id, CancellationToken cancellationToken);
    }
}