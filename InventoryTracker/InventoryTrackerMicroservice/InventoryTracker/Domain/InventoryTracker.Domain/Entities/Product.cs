using InventoryTracker.Domain.Entities.Base;
using InventoryTracker.Domain.ValueObjects;

namespace InventoryTracker.Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public ProductName Name { get; private set; }
        public string Description { get; private set; }
        public ArticleNumber Article { get; private set; }
        public Money Price { get; private set; }
        public int Quantity { get; private set; }
        public string Category { get; private set; }
        public Guid SupplierId { get; private set; }
        public DateTime? ExpiryDate { get; private set; }

        // Navigation properties
        public virtual Supplier Supplier { get; private set; }
        public virtual ICollection<InventoryTransaction> Transactions { get; private set; }

        public Product(
            Guid id,
            ProductName name,
            string description,
            ArticleNumber article,
            Money price,
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

        public void UpdatePrice(Money newPrice)
        {
            Price = newPrice;
        }

        protected Product() : base() { }
    }
}