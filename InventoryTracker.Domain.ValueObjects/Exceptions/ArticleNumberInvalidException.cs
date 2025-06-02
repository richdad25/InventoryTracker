namespace InventoryTracker.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// Исключение при невалидном артикуле
    /// </summary>
    internal class ArticleNumberInvalidException(string message, string paramName, string value)
        : ArgumentException(message, paramName)
    {
        public string InvalidValue { get; } = value;
    }
}