// InventoryTracker.Domain/Entities/Warehouse.cs
using InventoryTracker.Domain.Entities.Base;

namespace InventoryTracker.Domain.Entities
{
    public class Warehouse : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Location { get; private set; }
        public double Capacity { get; private set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; private set; }

        public Warehouse(Guid id, string name, string location, double capacity) : base(id)
        {
            Name = name;
            Location = location;
            Capacity = capacity;
        }

        public void AddProduct(Product product, int quantity)
        {
            // Business logic for adding product to warehouse
        }

        public void RemoveProduct(Product product, int quantity)
        {
            // Business logic for removing product from warehouse
        }

        protected Warehouse() : base() { }
    }
}