namespace InventoryTracker.Domain.Exceptions
{
    /// <summary>
    /// Исключение при отсутствии товара
    /// </summary>
    public class ProductNotFoundException : DomainException
    {
        public Guid ProductId { get; }

        public ProductNotFoundException(Guid productId)
            : base($"Товар с ID {productId} не найден.")
        {
            ProductId = productId;
        }
    }
}