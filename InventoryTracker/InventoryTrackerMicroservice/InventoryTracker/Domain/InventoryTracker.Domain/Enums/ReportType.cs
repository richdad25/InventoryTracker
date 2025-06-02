namespace InventoryTracker.Domain.Enums
{
    /// <summary>
    /// Тип отчета
    /// </summary>
    public enum ReportType
    {
        /// <summary> Остатки на складах </summary>
        InventoryLevels,

        /// <summary> Статистика продаж </summary>
        SalesStatistics,

        /// <summary> История движений товара </summary>
        MovementHistory,

        /// <summary> Просроченные товары </summary>
        ExpiredProducts
    }
}