using VehicleManagementLoan.Application.Common.Dtos;
using VehicleManagementLoan.Application.Common.Interfaces;

namespace VehicleManagementLoan.Application.Maintenance.GetMaintenanceByStatus;

public sealed class GetMaintenanceByStatusHandler
{
    private readonly IMaintenanceRecordRepository _repo;

    public GetMaintenanceByStatusHandler(IMaintenanceRecordRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<MaintenanceSummaryDto>> HandleAsync(int statusId, CancellationToken cancellationToken = default)
    {
        return _repo.GetSummariesByStatusAsync(statusId, cancellationToken);
    }
}
