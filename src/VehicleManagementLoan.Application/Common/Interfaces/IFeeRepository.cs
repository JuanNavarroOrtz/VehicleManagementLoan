using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Common.Interfaces;

public interface IFeeRepository
{
    Task<Fee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Fee>> GetAllAsync(CancellationToken cancellationToken = default);
}
