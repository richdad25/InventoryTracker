namespace InventoryTracker.Domain.ValueObjects.Exceptions
{
    /// <summary>
    /// Исключение при пустой строке или null
    /// </summary>
    internal class ArgumentNullOrWhiteSpaceException(string paramName, string message)
        : ArgumentNullException(paramName, message);
}