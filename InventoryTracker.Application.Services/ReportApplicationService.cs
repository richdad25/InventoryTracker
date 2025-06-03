using InventoryTracker.Application.Models.Report;
using InventoryTracker.Application.Services.Abstractions;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Repositories.Abstractions;
using AutoMapper;

namespace InventoryTracker.Application.Services
{
    public class ReportApplicationService(
        IInventoryReportRepository reportRepository,
        IMapper mapper) : IReportApplicationService
    {
        public async Task<InventoryReportModel> GenerateInventoryReportAsync(CancellationToken cancellationToken)
        {
            var report = await reportRepository.GenerateReportAsync(cancellationToken);
            return mapper.Map<InventoryReportModel>(report);
        }
    }
}