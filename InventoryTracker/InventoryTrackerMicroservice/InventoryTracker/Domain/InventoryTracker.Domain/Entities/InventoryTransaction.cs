using InventoryTracker.Domain.Entities.Base;

namespace InventoryTracker.Domain.Entities
{
    public enum TransactionType
    {
        Incoming,
        Outgoing,
        Adjustment,
        WriteOff
    }

    public class InventoryTransaction : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime Date { get; private set; }

        // Navigation property
        public virtual Product Product { get; private set; }

        public InventoryTransaction(
            Guid id,
            Guid productId,
            int quantity,
            TransactionType type,
            DateTime date) : base(id)
        {
            ProductId = productId;
            Quantity = quantity;
            Type = type;
            Date = date;
        }

        public void Execute()
        {
            // Business logic for transaction execution
        }

        public void Rollback()
        {
            // Business logic for transaction rollback
        }

        protected InventoryTransaction() : base() { }
    }
}