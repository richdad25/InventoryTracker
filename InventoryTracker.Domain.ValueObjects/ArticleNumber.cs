using InventoryTracker.Domain.ValueObjects.Base;
using InventoryTracker.Domain.ValueObjects.Validators;

namespace InventoryTracker.Domain.ValueObjects
{
    public class ArticleNumber(string number)
        : ValueObject<string>(new ArticleNumberValidator(), number.Trim());
}