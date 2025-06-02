using InventoryTracker.Domain.ValueObjects.Base;
using InventoryTracker.Domain.ValueObjects.Validators;

namespace InventoryTracker.Domain.ValueObjects
{
    public class Money(decimal amount)
        : ValueObject<decimal>(new MoneyAmountValidator(), decimal.Round(amount, 2))
    {
        public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
        public static Money operator -(Money a, Money b) => new(a.Value - b.Value);
    }
}
