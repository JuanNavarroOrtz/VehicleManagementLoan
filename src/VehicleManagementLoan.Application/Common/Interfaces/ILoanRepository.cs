using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Application.Common.Dtos;

namespace VehicleManagementLoan.Application.Common.Interfaces;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Loan loan, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Loan>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Loan>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Loan>> GetByStatusAsync(int statusId, CancellationToken cancellationToken = default);
    // Proyecciones para listados con datos relacionados
    Task<IReadOnlyCollection<LoanSummaryDto>> GetSummariesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<LoanSummaryDto>> GetSummariesByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<LoanSummaryDto>> GetSummariesByStatusAsync(int statusId, CancellationToken cancellationToken = default);
}
