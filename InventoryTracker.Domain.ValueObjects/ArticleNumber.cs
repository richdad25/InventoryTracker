using InventoryTracker.Domain.ValueObjects.Base;
using InventoryTracker.Domain.ValueObjects.Validators;

namespace InventoryTracker.Domain.ValueObjects
{
    public class ArticleNumber : ValueObject<string>
    {
        public string Value { get; } = default!;

        public ArticleNumber(string number)
            : base(new ArticleNumberValidator(), number.Trim())
        {
            Value = number.Trim();
        }

        protected ArticleNumber() : base() { }

        public override string ToString() => Value;
    }
}