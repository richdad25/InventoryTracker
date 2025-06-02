namespace InventoryTracker.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// Исключение при невалидной денежной сумме
    /// </summary>
    internal class MoneyAmountInvalidException(string message, string paramName, decimal value)
        : ArgumentException(message, paramName)
    {
        public decimal InvalidValue { get; } = value;
    }
}