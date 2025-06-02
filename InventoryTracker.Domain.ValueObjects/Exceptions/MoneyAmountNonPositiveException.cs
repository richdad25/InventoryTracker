namespace InventoryTracker.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// Исключение при неположительной сумме денег
    /// </summary>
    internal class MoneyAmountNonPositiveException(string paramName, decimal value)
        : MoneyAmountInvalidException(ExceptionMessages.MONEY_AMOUNT_NON_POSITIVE, paramName, value);
}
