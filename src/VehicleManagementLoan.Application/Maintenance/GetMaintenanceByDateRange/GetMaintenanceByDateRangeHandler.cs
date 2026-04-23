using VehicleManagementLoan.Application.Common.Dtos;
using VehicleManagementLoan.Application.Common.Interfaces;

namespace VehicleManagementLoan.Application.Maintenance.GetMaintenanceByDateRange;

public sealed class GetMaintenanceByDateRangeHandler
{
    private readonly IMaintenanceRecordRepository _repo;

    public GetMaintenanceByDateRangeHandler(IMaintenanceRecordRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<MaintenanceSummaryDto>> HandleAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return _repo.GetSummariesByDateRangeAsync(from, to, cancellationToken);
    }
}
