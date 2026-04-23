using VehicleManagementLoan.Application.Common.Dtos;
using VehicleManagementLoan.Application.Common.Interfaces;

namespace VehicleManagementLoan.Application.Loans.GetLoansByDateRange;

public sealed class GetLoansByDateRangeHandler
{
    private readonly ILoanRepository _repo;

    public GetLoansByDateRangeHandler(ILoanRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<LoanSummaryDto>> HandleAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return _repo.GetSummariesByDateRangeAsync(from, to, cancellationToken);
    }
}
