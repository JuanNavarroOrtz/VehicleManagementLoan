using VehicleManagementLoan.Application.Common.Dtos;
using VehicleManagementLoan.Application.Common.Interfaces;

namespace VehicleManagementLoan.Application.Loans.GetLoansByStatus;

public sealed class GetLoansByStatusHandler
{
    private readonly ILoanRepository _repo;

    public GetLoansByStatusHandler(ILoanRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<LoanSummaryDto>> HandleAsync(int statusId, CancellationToken cancellationToken = default)
    {
        return _repo.GetSummariesByStatusAsync(statusId, cancellationToken);
    }
}
