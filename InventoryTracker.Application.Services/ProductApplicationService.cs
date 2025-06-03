using InventoryTracker.Application.Models.Product;
using InventoryTracker.Application.Services.Abstractions;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Repositories.Abstractions;
using AutoMapper;

namespace InventoryTracker.Application.Services
{
    public class ProductApplicationService(
        IProductRepository productRepository,
        IMapper mapper) : IProductApplicationService
    {
        public async Task<ProductModel?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(id, cancellationToken);
            return product is null ? null : mapper.Map<ProductModel>(product);
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync(CancellationToken cancellationToken)
            => (await productRepository.GetAllAsync(cancellationToken)).Select(mapper.Map<ProductModel>);

        public async Task<ProductModel?> CreateProductAsync(CreateProductModel productModel, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(productModel);
            var createdProduct = await productRepository.AddAsync(product, cancellationToken);
            return createdProduct is null ? null : mapper.Map<ProductModel>(createdProduct);
        }

        public async Task<bool> UpdateProductAsync(ProductModel productModel, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(productModel.Id, cancellationToken);
            if (product is null)
                return false;

            product = mapper.Map<Product>(productModel);
            return await productRepository.UpdateAsync(product, cancellationToken);
        }

        public async Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(id, cancellationToken);
            return product is not null && await productRepository.DeleteAsync(product, cancellationToken);
        }
    }
}