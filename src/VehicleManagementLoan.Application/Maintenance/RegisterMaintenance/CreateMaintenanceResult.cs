using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Maintenance.RegisterMaintenance;

public sealed class CreateMaintenanceResult
{
    public required MaintenanceRecord MaintenanceRecord { get; init; }
}
