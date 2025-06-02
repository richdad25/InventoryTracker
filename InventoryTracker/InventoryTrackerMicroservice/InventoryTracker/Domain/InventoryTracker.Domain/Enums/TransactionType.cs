namespace InventoryTracker.Domain.Enums
{
    /// <summary>
    /// Тип операции с инвентарем
    /// </summary>
    public enum TransactionType
    {
        /// <summary> Поступление товара </summary>
        Incoming,

        /// <summary> Списание (продажа/утилизация) </summary>
        Outgoing,

        /// <summary> Корректировка количества </summary>
        Adjustment,

        /// <summary> Списание просроченного товара </summary>
        WriteOff
    }
}