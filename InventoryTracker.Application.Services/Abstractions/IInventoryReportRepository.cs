using InventoryTracker.Domain.Entities;
using System.Threading.Tasks;

namespace InventoryTracker.Domain.Repositories.Abstractions
{
    public interface IInventoryReportRepository
    {
        Task<Report> GenerateReportAsync(CancellationToken cancellationToken);
    }
}