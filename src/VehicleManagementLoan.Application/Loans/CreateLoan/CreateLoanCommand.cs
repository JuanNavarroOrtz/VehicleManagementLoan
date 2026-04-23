namespace VehicleManagementLoan.Application.Loans.CreateLoan;

public sealed class CreateLoanCommand
{
    public int VehicleId { get; init; }
    public int ClientId { get; init; }
    public DateTime StartDate { get; init; }
    public decimal Deposit { get; init; }
    public int FeeId { get; init; }
    public int LoanStatusId { get; init; }
    public int VehicleAvailableStatusId { get; init; }
    public int VehicleLoanedStatusId { get; init; }
}
