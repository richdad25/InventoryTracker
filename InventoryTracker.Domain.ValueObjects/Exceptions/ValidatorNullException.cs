namespace InventoryTracker.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// Исключение при отсутствии валидатора для ValueObject
    /// </summary>
    internal class ValidatorNullException(string typeName, string message)
        : ArgumentNullException(nameof(typeName), message);
}