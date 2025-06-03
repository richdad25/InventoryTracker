using InventoryTracker.Application.Models.Transaction;
using InventoryTracker.Application.Services.Abstractions;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Repositories.Abstractions;
using AutoMapper;
using MassTransit;

namespace InventoryTracker.Application.Services
{
    public class TransactionApplicationService(
        IInventoryTransactionRepository transactionRepository,
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository,
        IBusControl busControl,
        IMapper mapper) : ITransactionApplicationService
    {
        public async Task<InventoryTransactionModel?> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await transactionRepository.GetByIdAsync(id, cancellationToken);
            return transaction is null ? null : mapper.Map<InventoryTransactionModel>(transaction);
        }

        public async Task<IEnumerable<InventoryTransactionModel>> GetTransactionsAsync(CancellationToken cancellationToken)
            => (await transactionRepository.GetAllAsync(cancellationToken)).Select(mapper.Map<InventoryTransactionModel>);

        public async Task<InventoryTransactionModel?> CreateTransactionAsync(
            CreateInventoryTransactionModel transactionModel,
            CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(transactionModel.ProductId, cancellationToken);
            if (product is null)
                return null;

            var warehouse = await warehouseRepository.GetByIdAsync(transactionModel.WarehouseId, cancellationToken);
            if (warehouse is null)
                return null;

            var transaction = mapper.Map<InventoryTransaction>(transactionModel);
            var createdTransaction = await transactionRepository.AddAsync(transaction, cancellationToken);

            if (createdTransaction is not null)
            {
                return mapper.Map<InventoryTransactionModel>(createdTransaction);
            }

            return null;
        }

        public async Task<bool> DeleteTransactionAsync(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await transactionRepository.GetByIdAsync(id, cancellationToken);
            return transaction is not null && await transactionRepository.DeleteAsync(transaction, cancellationToken);
        }
    }
}