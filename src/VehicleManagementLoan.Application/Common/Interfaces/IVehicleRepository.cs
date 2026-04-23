using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Application.Common.Dto;

namespace VehicleManagementLoan.Application.Common.Interfaces;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
    Task<VehicleDto?> GetDtoByIdAsync(int id, CancellationToken cancellationToken = default);
}
