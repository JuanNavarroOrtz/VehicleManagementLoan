using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Common.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Client>> GetAllAsync(CancellationToken cancellationToken = default);
}
