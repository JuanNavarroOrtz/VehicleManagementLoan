using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Application.Common.Dtos;

namespace VehicleManagementLoan.Application.Common.Interfaces;

public interface IMaintenanceRecordRepository
{
    Task AddAsync(MaintenanceRecord maintenanceRecord, CancellationToken cancellationToken = default);
    Task<MaintenanceRecord?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MaintenanceRecord>> GetBillableByLoanIdAsync(int loanId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MaintenanceRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MaintenanceRecord>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MaintenanceRecord>> GetByStatusAsync(int statusId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MaintenanceSummaryDto>> GetSummariesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MaintenanceSummaryDto>> GetSummariesByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<MaintenanceSummaryDto>> GetSummariesByStatusAsync(int statusId, CancellationToken cancellationToken = default);
}
