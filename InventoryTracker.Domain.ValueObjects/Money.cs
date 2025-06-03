using InventoryTracker.Domain.ValueObjects.Base;
using InventoryTracker.Domain.ValueObjects.Validators;

namespace InventoryTracker.Domain.ValueObjects
{
    public class Money : ValueObject<decimal>
    {
        public decimal Value { get; } = default!;

        public Money(decimal amount)
            : base(new MoneyAmountValidator(), decimal.Round(amount, 2))
        {
            Value = decimal.Round(amount, 2);
        }

        protected Money() : base() { }

        public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
        public static Money operator -(Money a, Money b) => new(a.Value - b.Value);

        public override string ToString() => Value.ToString("C");
    }
}