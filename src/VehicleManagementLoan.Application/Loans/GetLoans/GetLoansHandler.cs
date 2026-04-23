using VehicleManagementLoan.Application.Common.Dtos;
using VehicleManagementLoan.Application.Common.Interfaces;

namespace VehicleManagementLoan.Application.Loans.GetLoans;

public sealed class GetLoansHandler
{
    private readonly ILoanRepository _repo;

    public GetLoansHandler(ILoanRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<LoanSummaryDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _repo.GetSummariesAsync(cancellationToken);
    }
}
