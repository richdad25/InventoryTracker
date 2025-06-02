using InventoryTracker.Domain.ValueObjects.Base;
using InventoryTracker.Domain.ValueObjects.Exceptions;

namespace InventoryTracker.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Валидатор для денежных сумм.
    /// Проверяет:
    /// 1. Положительность значения.
    /// 2. Не более 2 знаков после запятой.
    /// </summary>
    public class MoneyAmountValidator : IValidator<decimal>
    {
        public void Validate(decimal value)
        {
            // Проверка на положительность
            if (value <= 0)
                throw new MoneyAmountNonPositiveException(nameof(value), value);

            // Проверка на корректность десятичных знаков
            if (HasMoreThanTwoDecimalPlaces(value))
                throw new MoneyAmountDecimalException(nameof(value), value);
        }

        /// <summary>
        /// Проверяет, что у числа не более 2 знаков после запятой.
        /// </summary>
        private bool HasMoreThanTwoDecimalPlaces(decimal value)
        {
            return decimal.Round(value, 2) != value;
        }
    }
}