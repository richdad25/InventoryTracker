using InventoryTracker.Domain.Entities.Base;

namespace InventoryTracker.Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Article { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public string Category { get; private set; }
        public Guid SupplierId { get; private set; }
        public DateTime? ExpiryDate { get; private set; }

        // Navigation properties
        public virtual Supplier Supplier { get; private set; }
        public virtual ICollection<InventoryTransaction> Transactions { get; private set; }

        public Product(
            Guid id,
            string name,
            string description,
            string article,
            decimal price,
            int quantity,
            string category,
            Guid supplierId,
            DateTime? expiryDate) : base(id)
        {
            Name = name;
            Description = description;
            Article = article;
            Price = price;
            Quantity = quantity;
            Category = category;
            SupplierId = supplierId;
            ExpiryDate = expiryDate;
        }

        // Business methods
        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            if (Quantity < amount)
                throw new InvalidOperationException("Insufficient quantity");

            Quantity -= amount;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Price must be positive", nameof(newPrice));

            Price = newPrice;
        }

        protected Product() : base() { }
    }
}