using InventoryTracker.Application.Models.Transaction;
using System.Threading.Tasks;

namespace InventoryTracker.Application.Services.Abstractions
{
    public interface ITransactionApplicationService
    {
        Task<InventoryTransactionModel?> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<InventoryTransactionModel>> GetTransactionsAsync(CancellationToken cancellationToken);
        Task<InventoryTransactionModel?> CreateTransactionAsync(CreateInventoryTransactionModel transaction, CancellationToken cancellationToken);
        Task<bool> DeleteTransactionAsync(Guid id, CancellationToken cancellationToken);
    }
}