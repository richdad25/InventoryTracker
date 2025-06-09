using InventoryTracker.Domain.ValueObjects;
using InventoryTracker.Domain.ValueObjects.Base;
using InventoryTracker.Domain.ValueObjects.Validators;

namespace InventoryTracker.Domain.ValueObjects
{
    public class ProductName : ValueObject<string>
    {
        public string Value { get; } = default!;

        public ProductName(string name)
            : base(new ProductNameValidator(), name.Trim())
        {
            Value = name.Trim();
        }

        protected ProductName() : base() { }

        public override string ToString() => Value;
    }
}
