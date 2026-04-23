using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Common.Interfaces;

public interface IWorkTypeRepository
{
    Task<WorkType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<WorkType>> GetAllAsync(CancellationToken cancellationToken = default);
}
