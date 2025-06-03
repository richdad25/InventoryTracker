using InventoryTracker.Application.Models.Supplier;
using InventoryTracker.Application.Services.Abstractions;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Repositories.Abstractions;
using AutoMapper;

namespace InventoryTracker.Application.Services
{
    public class SupplierApplicationService(
        ISupplierRepository supplierRepository,
        IMapper mapper) : ISupplierApplicationService
    {
        public async Task<SupplierModel?> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetByIdAsync(id, cancellationToken);
            return supplier is null ? null : mapper.Map<SupplierModel>(supplier);
        }

        public async Task<IEnumerable<SupplierModel>> GetSuppliersAsync(CancellationToken cancellationToken)
            => (await supplierRepository.GetAllAsync(cancellationToken)).Select(mapper.Map<SupplierModel>);

        public async Task<SupplierModel?> CreateSupplierAsync(CreateSupplierModel supplierModel, CancellationToken cancellationToken)
        {
            if (await supplierRepository.GetByIdAsync(supplierModel.Id, cancellationToken) is not null)
                return null;

            var supplier = mapper.Map<Supplier>(supplierModel);
            var createdSupplier = await supplierRepository.AddAsync(supplier, cancellationToken);
            return createdSupplier is null ? null : mapper.Map<SupplierModel>(createdSupplier);
        }

        public async Task<bool> UpdateSupplierAsync(SupplierModel supplierModel, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetByIdAsync(supplierModel.Id, cancellationToken);
            if (supplier is null)
                return false;

            supplier = mapper.Map<Supplier>(supplierModel);
            return await supplierRepository.UpdateAsync(supplier, cancellationToken);
        }

        public async Task<bool> DeleteSupplierAsync(Guid id, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetByIdAsync(id, cancellationToken);
            return supplier is not null && await supplierRepository.DeleteAsync(supplier, cancellationToken);
        }
    }
}