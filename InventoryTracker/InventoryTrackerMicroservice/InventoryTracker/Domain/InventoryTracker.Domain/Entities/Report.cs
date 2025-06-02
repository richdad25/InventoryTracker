// InventoryTracker.Domain/Entities/Report.cs
using InventoryTracker.Domain.Entities.Base;

namespace InventoryTracker.Domain.Entities
{
    public enum ReportType
    {
        InventoryLevels,
        SalesStatistics,
        MovementHistory,
        ExpiredProducts
    }

    public class Report : Entity<Guid>
    {
        public ReportType Type { get; private set; }
        public DateTime GeneratedDate { get; private set; }
        public string Content { get; private set; }

        public Report(Guid id, ReportType type, DateTime generatedDate, string content) : base(id)
        {
            Type = type;
            GeneratedDate = generatedDate;
            Content = content;
        }

        protected Report() : base() { }
    }
}