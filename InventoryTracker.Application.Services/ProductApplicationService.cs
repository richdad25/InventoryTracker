using AutoMapper;
using InventoryTracker.Application.Models.Product;
using InventoryTracker.Application.Services.Abstractions;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Repositories.Abstractions;
using MassTransit;
using Otus.QueueDto.Inventory;

namespace InventoryTracker.Application.Services
{
    public class ProductApplicationService(
        IProductRepository productRepository,
        ISupplierRepository supplierRepository,
        IMapper mapper,
        IBus bus) : IProductApplicationService
    {
        public async Task<ProductModel?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(id, cancellationToken);
            return product == null ? null : mapper.Map<ProductModel>(product);
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<IEnumerable<ProductModel>> GetExpiredProductsAsync(
            DateTime currentDate,
            CancellationToken cancellationToken)
        {
            var products = await productRepository.GetExpiredProductsAsync(currentDate, cancellationToken);
            return mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<ProductModel?> CreateProductAsync(
            CreateProductModel model,
            CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetByIdAsync(model.SupplierId, cancellationToken);
            if (supplier == null) return null;

            var product = new Product(
                Guid.NewGuid(),
                model.Name,
                model.Description,
                model.Article,
                model.Price,
                model.Quantity,
                model.Category,
                model.SupplierId,
                model.ExpiryDate);

            var createdProduct = await productRepository.AddAsync(product, cancellationToken);

            await bus.Publish(new InventoryItemCreatedEvent(
                createdProduct.Id,
                createdProduct.Name,
                createdProduct.Quantity),
                cancellationToken);

            return mapper.Map<ProductModel>(createdProduct);
        }

        public async Task<bool> UpdateProductAsync(
            ProductModel model,
            CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(model.Id, cancellationToken);
            if (product == null) return false;

            product.UpdateDetails(
                model.Name,
                model.Description,
                model.Article,
                model.Price,
                model.Category,
                model.ExpiryDate);

            return await productRepository.UpdateAsync(product, cancellationToken);
        }

        public async Task<bool> DeleteProductAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await productRepository.DeleteAsync(id, cancellationToken);
            if (result)
            {
                await bus.Publish(new InventoryItemRemovedEvent(id), cancellationToken);
            }
            return result;
        }
    }
}