namespace InventoryTracker.Domain.Exceptions
{
    /// <summary>
    /// Исключение при недопустимом количестве товара
    /// </summary>
    public class InvalidQuantityException : InventoryOperationException
    {
        public int RequestedQuantity { get; }
        public int AvailableQuantity { get; }

        public InvalidQuantityException(int requested, int available)
            : base($"Запрошено {requested} единиц, доступно {available}.")
        {
            RequestedQuantity = requested;
            AvailableQuantity = available;
        }
    }
}