using System;

namespace VehicleManagementLoan.Application.Common.Dtos;

public sealed class LoanSummaryDto
{
    public int Id { get; init; }
    public string? Consecutive { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int VehicleId { get; init; }
    public string? VehiclePlate { get; init; }
    public int ClientId { get; init; }
    public string? ClientFullName { get; init; }
    public int ClientLoanCount { get; init; }
    public int StatusId { get; init; }
    public string? StatusName { get; init; }
}
