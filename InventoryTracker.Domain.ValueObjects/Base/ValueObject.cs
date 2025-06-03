using InventoryTracker.Domain.ValueObjects.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace InventoryTracker.Domain.ValueObjects.Base
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>> where T : notnull
    {
        // Основное хранилище значения
        private T _value = default!;

        // Публичное свойство с явной реализацией
        public T Value
        {
            get => _value;
            protected set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Основной конструктор с валидацией
        /// </summary>
        protected ValueObject(IValidator<T> validator, T value)
        {
            if (validator == null)
                throw new ValidatorNullException(
                    GetType().FullName ?? string.Empty,
                    ExceptionMessages.VALIDATOR_MUST_BE_SPECIFIED);

            validator.Validate(value);
            _value = value;
        }

        /// <summary>
        /// Конструктор для EF Core (без валидации)
        /// </summary>
        [ExcludeFromCodeCoverage] // Исключаем из покрытия тестами
        protected ValueObject()
        {
        }

        // Явная реализация ToString
        public override string ToString() => Value.ToString() ?? string.Empty;

        // Реализация равенства
        public override bool Equals(object? obj) => Equals(obj as ValueObject<T>);

        public bool Equals(ValueObject<T>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return GetType() == other.GetType() &&
                   EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);

        public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
            => EqualityComparer<ValueObject<T>>.Default.Equals(left, right);

        public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
            => !(left == right);

        /// <summary>
        /// Неявное преобразование в базовый тип
        /// </summary>
        public static implicit operator T(ValueObject<T> valueObject) => valueObject.Value;
    }
}