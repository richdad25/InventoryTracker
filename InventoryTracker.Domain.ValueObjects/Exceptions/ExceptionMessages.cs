namespace InventoryTracker.Domain.ValueObjects.Exceptions
{
    internal static class ExceptionMessages
    {
        // Общие ошибки (из примера)
        public const string VALIDATOR_MUST_BE_SPECIFIED = "Validator must be specified for type";

        // Для товаров
        public const string PRODUCT_NAME_EMPTY = "Название товара не может быть пустым";
        public const string PRODUCT_NAME_TOO_SHORT = "Название товара должно содержать минимум 2 символа";
        public const string PRODUCT_NAME_TOO_LONG = "Название товара не может превышать 100 символов";

        // Для артикулов
        public const string ARTICLE_NUMBER_EMPTY = "Артикул не может быть пустым";
        public const string ARTICLE_NUMBER_INVALID = "Артикул должен содержать 3-20 символов (a-z, 0-9, -)";

        // Для денег
        public const string MONEY_AMOUNT_NON_POSITIVE = "Сумма должна быть положительной";
        public const string MONEY_AMOUNT_INVALID_DECIMALS = "Сумма не может иметь больше 2 знаков после запятой";
    }
}
