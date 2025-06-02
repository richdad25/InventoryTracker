namespace InventoryService.Domain.Exceptions
{
    public class WarehouseCapacityExceededException : InventoryException
    {
        public WarehouseCapacityExceededException()
            : base("Warehouse capacity has been exceeded")
        {
        }
    }
}