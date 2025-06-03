using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransactionType = InventoryTracker.Domain.Enums.TransactionType;

namespace InventoryTracker.Domain.Repositories.Abstractions
{
    public interface IInventoryTransactionRepository : IRepository<InventoryTransaction, Guid>
    {
        Task<IEnumerable<InventoryTransaction>> GetByProductIdAsync(
            Guid productId,
            CancellationToken cancellationToken,
            bool asNoTracking = false);

        Task<IEnumerable<InventoryTransaction>> GetByTypeAsync(
            TransactionType type,
            CancellationToken cancellationToken,
            bool asNoTracking = false);

        Task<IEnumerable<InventoryTransaction>> GetByDateRangeAsync(
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken,
            bool asNoTracking = false);
    }
}