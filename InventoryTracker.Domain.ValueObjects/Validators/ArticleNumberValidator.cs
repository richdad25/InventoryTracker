using InventoryTracker.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;

namespace InventoryTracker.Domain.ValueObjects.Validators
{
    public class ArticleNumberValidator : IValidator<string>
    {
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "Артикул не может быть пустым");

            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\-]{3,20}$"))
                throw new ArgumentException("Артикул должен быть 3-20 символов (буквы, цифры, дефисы)", nameof(value));
        }
    }
}
