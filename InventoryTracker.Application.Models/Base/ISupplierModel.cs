namespace InventoryTracker.Application.Models.Base
{
    public interface ISupplierModel<out TId> where TId : struct, IEquatable<TId>
    {
        public TId SupplierId { get; }
    }
}
