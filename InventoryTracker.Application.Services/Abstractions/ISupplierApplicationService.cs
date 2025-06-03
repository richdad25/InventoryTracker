using InventoryTracker.Application.Models.Supplier;
using System.Threading.Tasks;

namespace InventoryTracker.Application.Services.Abstractions
{
    public interface ISupplierApplicationService
    {
        Task<SupplierModel?> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<SupplierModel>> GetSuppliersAsync(CancellationToken cancellationToken);
        Task<SupplierModel?> CreateSupplierAsync(CreateSupplierModel supplier, CancellationToken cancellationToken);
        Task<bool> UpdateSupplierAsync(SupplierModel supplier, CancellationToken cancellationToken);
        Task<bool> DeleteSupplierAsync(Guid id, CancellationToken cancellationToken);
    }
}