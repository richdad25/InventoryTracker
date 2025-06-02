
using InventoryTracker.Domain.ValueObjects.Base;
using InventoryTracker.Domain.ValueObjects.Validators;

namespace InventoryTracker.Domain.ValueObjects
{
    public class ProductName(string name)
        : ValueObject<string>(new ProductNameValidator(), name.Trim());
}