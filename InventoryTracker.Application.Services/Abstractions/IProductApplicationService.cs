using InventoryTracker.Application.Models.Product;
using System.Threading.Tasks;

namespace InventoryTracker.Application.Services.Abstractions
{
    public interface IProductApplicationService
    {
        Task<ProductModel?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ProductModel>> GetProductsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ProductModel>> GetExpiredProductsAsync(DateTime currentDate, CancellationToken cancellationToken);
        Task<ProductModel?> CreateProductAsync(CreateProductModel product, CancellationToken cancellationToken);
        Task<bool> UpdateProductAsync(ProductModel product, CancellationToken cancellationToken);
        Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    }
}