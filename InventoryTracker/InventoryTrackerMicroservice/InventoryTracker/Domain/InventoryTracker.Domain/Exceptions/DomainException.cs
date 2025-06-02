﻿namespace InventoryTracker.Domain.Exceptions
{
    /// <summary>
    /// Базовое исключение для доменного слоя
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception inner) : base(message, inner) { }
    }
}