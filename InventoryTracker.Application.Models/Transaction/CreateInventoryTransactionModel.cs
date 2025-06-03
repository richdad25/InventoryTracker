namespace InventoryTracker.Application.Models.Transaction
{
    public class CreateInventoryTransactionModel
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } // "IN" или "OUT"
    }
}