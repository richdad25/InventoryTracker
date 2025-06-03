using InventoryTracker.Application.Models.Warehouse;
using InventoryTracker.Application.Services.Abstractions;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Repositories.Abstractions;
using AutoMapper;

namespace InventoryTracker.Application.Services
{
    public class WarehouseApplicationService(
        IWarehouseRepository warehouseRepository,
        IMapper mapper) : IWarehouseApplicationService
    {
        public async Task<WarehouseModel?> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await warehouseRepository.GetByIdAsync(id, cancellationToken);
            return warehouse is null ? null : mapper.Map<WarehouseModel>(warehouse);
        }

        public async Task<IEnumerable<WarehouseModel>> GetWarehousesAsync(CancellationToken cancellationToken)
            => (await warehouseRepository.GetAllAsync(cancellationToken)).Select(mapper.Map<WarehouseModel>);

        public async Task<WarehouseModel?> CreateWarehouseAsync(CreateWarehouseModel warehouseModel, CancellationToken cancellationToken)
        {
            var warehouse = mapper.Map<Warehouse>(warehouseModel);
            var createdWarehouse = await warehouseRepository.AddAsync(warehouse, cancellationToken);
            return createdWarehouse is null ? null : mapper.Map<WarehouseModel>(createdWarehouse);
        }

        public async Task<bool> UpdateWarehouseAsync(WarehouseModel warehouseModel, CancellationToken cancellationToken)
        {
            var warehouse = await warehouseRepository.GetByIdAsync(warehouseModel.Id, cancellationToken);
            if (warehouse is null)
                return false;

            warehouse = mapper.Map<Warehouse>(warehouseModel);
            return await warehouseRepository.UpdateAsync(warehouse, cancellationToken);
        }

        public async Task<bool> DeleteWarehouseAsync(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await warehouseRepository.GetByIdAsync(id, cancellationToken);
            return warehouse is not null && await warehouseRepository.DeleteAsync(warehouse, cancellationToken);
        }
    }
}