namespace InventoryTracker.Application.Models.Warehouse
{
    public class CreateWarehouseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}