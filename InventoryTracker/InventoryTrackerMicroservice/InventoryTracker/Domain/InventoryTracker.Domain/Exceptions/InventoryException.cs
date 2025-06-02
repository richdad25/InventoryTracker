using System;

namespace InventoryService.Domain.Exceptions
{
    public class InventoryException : Exception
    {
        public InventoryException(string message) : base(message)
        {
        }

        public InventoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}