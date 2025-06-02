namespace InventoryTracker.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// Исключение при некорректном количестве знаков после запятой
    /// </summary>
    internal class MoneyAmountDecimalException(string paramName, decimal value)
        : MoneyAmountInvalidException(ExceptionMessages.MONEY_AMOUNT_INVALID_DECIMALS, paramName, value);
}