using InventoryTracker.Domain.Entities;

namespace InventoryTracker.Domain.Repositories.Abstractions
{
    public interface IWarehouseRepository : IRepository<Warehouse, Guid>
    {
        Task<Warehouse?> GetByNameAsync(
            string name,
            CancellationToken cancellationToken);

        Task<IEnumerable<Warehouse>> GetByLocationAsync(
            string location,
            CancellationToken cancellationToken,
            bool asNoTracking = false);
    }
}