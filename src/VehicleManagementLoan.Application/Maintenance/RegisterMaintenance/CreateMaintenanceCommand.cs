namespace VehicleManagementLoan.Application.Maintenance.RegisterMaintenance;

public sealed class CreateMaintenanceCommand
{
    public int? VehicleId { get; init; }
    public int? LoanId { get; init; }
    public int MaintenanceContextTypeId { get; init; }
    public int WorkTypeId { get; init; }
    public decimal Kilometers { get; init; }
    public decimal Cost { get; init; }
    public DateTime MaintenanceDate { get; init; }
    public string? Description { get; init; }
    public int MaintenanceStatusId { get; init; }
}
