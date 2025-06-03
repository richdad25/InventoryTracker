using InventoryTracker.Domain.Entities;

namespace InventoryTracker.Domain.Repositories.Abstractions
{
    public interface ISupplierRepository : IRepository<Supplier, Guid>
    {
        Task<Supplier?> GetByNameAsync(
            string name,
            CancellationToken cancellationToken);

        Task<IEnumerable<Supplier>> GetByContactInfoAsync(
            string contactInfo,
            CancellationToken cancellationToken,
            bool asNoTracking = false);
    }
}