using VehicleManagementLoan.Application.Common.Dtos;
using VehicleManagementLoan.Application.Common.Interfaces;

namespace VehicleManagementLoan.Application.Maintenance.GetMaintenance;

public sealed class GetMaintenanceHandler
{
    private readonly IMaintenanceRecordRepository _repo;

    public GetMaintenanceHandler(IMaintenanceRecordRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<MaintenanceSummaryDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _repo.GetSummariesAsync(cancellationToken);
    }
}
