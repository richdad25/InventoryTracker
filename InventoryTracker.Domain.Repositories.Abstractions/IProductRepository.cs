using InventoryTracker.Domain.Entities;

namespace InventoryTracker.Domain.Repositories.Abstractions
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(
            string category,
            CancellationToken cancellationToken,
            bool asNoTracking = false);

        Task<IEnumerable<Product>> GetExpiredProductsAsync(
            DateTime currentDate,
            CancellationToken cancellationToken,
            bool asNoTracking = false);

        Task<Product?> GetByArticleNumberAsync(
            string articleNumber,
            CancellationToken cancellationToken);
    }
}