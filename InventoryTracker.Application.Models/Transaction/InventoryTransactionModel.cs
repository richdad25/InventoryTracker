using InventoryTracker.Application.Models.Base;
using InventoryTracker.Domain.Enums;

namespace InventoryTracker.Application.Models.Transaction
{
    public record InventoryTransactionModel(
        Guid Id,
        Guid ProductId,
        int Quantity,
        TransactionType Type,
        DateTime Date) : IModel<Guid>;
}