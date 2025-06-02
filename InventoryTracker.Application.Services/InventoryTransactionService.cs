using InventoryTracker.Application.Models.Transaction;
using InventoryTracker.Application.Services.Abstractions;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Enums;
using InventoryTracker.Domain.Repositories.Abstractions;
using MassTransit;
using Otus.QueueDto.Inventory;

namespace InventoryTracker.Application.Services
{
    public class InventoryTransactionService(
        IInventoryTransactionRepository transactionRepository,
        IProductRepository productRepository,
        IBus bus) : IInventoryTransactionService
    {
        public async Task<bool> RegisterIncomingAsync(
            CreateInventoryTransactionModel model,
            CancellationToken cancellationToken)
        {
            return await ProcessTransactionAsync(model, TransactionType.Incoming, cancellationToken);
        }

        public async Task<bool> RegisterOutgoingAsync(
            CreateInventoryTransactionModel model,
            CancellationToken cancellationToken)
        {
            return await ProcessTransactionAsync(model, TransactionType.Outgoing, cancellationToken);
        }

        public async Task<bool> RegisterWriteOffAsync(
            CreateInventoryTransactionModel model,
            CancellationToken cancellationToken)
        {
            return await ProcessTransactionAsync(model, TransactionType.WriteOff, cancellationToken);
        }

        public async Task PublishInventoryEventAsync(
            Guid productId,
            int quantityChange)
        {
            await bus.Publish(new InventoryQuantityChangedEvent(
                productId,
                quantityChange,
                DateTime.UtcNow));
        }

        private async Task<bool> ProcessTransactionAsync(
            CreateInventoryTransactionModel model,
            TransactionType type,
            CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(model.ProductId, cancellationToken);
            if (product == null) return false;

            var transaction = new InventoryTransaction(
                Guid.NewGuid(),
                model.ProductId,
                model.Quantity,
                type,
                DateTime.UtcNow);

            // Update quantity
            if (type == TransactionType.Incoming)
                product.IncreaseQuantity(model.Quantity);
            else
                product.DecreaseQuantity(model.Quantity);

            await productRepository.UpdateAsync(product, cancellationToken);
            await PublishInventoryEventAsync(product.Id,
                type == TransactionType.Incoming ? model.Quantity : -model.Quantity);

            return await transactionRepository.AddAsync(transaction, cancellationToken) != null;
        }
    }
}