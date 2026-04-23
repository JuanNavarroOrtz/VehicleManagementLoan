namespace VehicleManagementLoan.Application.Billing.GenerateBill;

public record GenerateBillCommand(
    int LoanId,
    DateTime BillDate,
    decimal TaxPercentage,
    int BillStatusId
);
