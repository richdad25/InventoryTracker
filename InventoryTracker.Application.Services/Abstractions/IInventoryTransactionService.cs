using InventoryTracker.Application.Models.Transaction;
using InventoryTracker.Domain.Enums;
using System.Threading.Tasks;

namespace InventoryTracker.Application.Services.Abstractions
{
    public interface IInventoryTransactionService
    {
        Task<bool> RegisterIncomingAsync(CreateInventoryTransactionModel transaction, CancellationToken cancellationToken);
        Task<bool> RegisterOutgoingAsync(CreateInventoryTransactionModel transaction, CancellationToken cancellationToken);
        Task<bool> RegisterWriteOffAsync(CreateInventoryTransactionModel transaction, CancellationToken cancellationToken);
    }
}