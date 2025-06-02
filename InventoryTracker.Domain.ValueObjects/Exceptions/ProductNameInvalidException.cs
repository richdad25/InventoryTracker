namespace InventoryTracker.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// Исключение при невалидном названии товара
    /// </summary>
    internal class ProductNameInvalidException(string message, string paramName, string value)
        : ArgumentException(message, paramName)
    {
        public string InvalidValue { get; } = value;
    }
}
