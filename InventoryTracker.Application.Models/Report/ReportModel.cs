using InventoryTracker.Application.Models.Base;
using InventoryTracker.Domain.Enums;

namespace InventoryTracker.Application.Models.Report
{
    public record InventoryReportModel(
        Guid Id,
        ReportType Type,
        DateTime GeneratedDate,
        string Content) : IModel<Guid>;
}