// InventoryTracker.Domain/Entities/Supplier.cs
using InventoryTracker.Domain.Entities.Base;

namespace InventoryTracker.Domain.Entities
{
    public class Supplier : Entity<Guid>
    {
        public string Name { get; private set; }
        public string ContactInfo { get; private set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; private set; }

        public Supplier(Guid id, string name, string contactInfo) : base(id)
        {
            Name = name;
            ContactInfo = contactInfo;
        }

        protected Supplier() : base() { }
    }
}