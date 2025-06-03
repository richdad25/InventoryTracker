using InventoryTracker.Application.Models.Report;
using System.Threading.Tasks;

namespace InventoryTracker.Application.Services.Abstractions
{
    public interface IReportApplicationService
    {
        Task<InventoryReportModel> GenerateInventoryReportAsync(CancellationToken cancellationToken);
    }
}