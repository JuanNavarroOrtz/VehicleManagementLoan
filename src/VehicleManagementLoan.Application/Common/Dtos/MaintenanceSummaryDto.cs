using System;

namespace VehicleManagementLoan.Application.Common.Dtos;

public sealed class MaintenanceSummaryDto
{
    public int Id { get; init; }
    public string? Consecutive { get; init; }
    public DateTime MaintenanceDate { get; init; }
    public int VehicleId { get; init; }
    public string? VehiclePlate { get; init; }
    public int? LoanId { get; init; }
    public string? MaintenanceContextName { get; init; }
    public int WorkTypeId { get; init; }
    public string? WorkTypeName { get; init; }
    public int StatusId { get; init; }
    public string? StatusName { get; init; }
}
