using InventoryTracker.Domain.ValueObjects.Base;
using InventoryTracker.Domain.ValueObjects.Exceptions;

namespace InventoryTracker.Domain.ValueObjects.Validators
{
    public class ProductNameValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), "Название товара не может быть пустым");

            if (value.Length < 2)
                throw new ArgumentException("Название товара должно содержать минимум 2 символа", nameof(value));

            if (value.Length > 100)
                throw new ArgumentException("Название товара не может превышать 100 символов", nameof(value));
        }
    }
}