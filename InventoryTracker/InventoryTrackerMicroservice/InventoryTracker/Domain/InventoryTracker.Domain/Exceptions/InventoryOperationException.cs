namespace InventoryTracker.Domain.Exceptions
{
    /// <summary>
    /// Исключение при операциях с инвентарем
    /// </summary>
    public class InventoryOperationException : DomainException
    {
        public InventoryOperationException() { }
        public InventoryOperationException(string message) : base(message) { }
        public InventoryOperationException(string message, Exception inner) : base(message, inner) { }
    }
}