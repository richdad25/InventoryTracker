using InventoryTracker.Application.Models.Base;
using InventoryTracker.Domain.Enums;

namespace InventoryTracker.Application.Models.Transaction
{
    public record CreateInventoryTransactionModel(
        Guid ProductId,
        int Quantity,
        TransactionType Type) : ICreateModel;
}