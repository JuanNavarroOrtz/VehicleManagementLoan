using VehicleManagementLoan.Application.Common.Dto;
using VehicleManagementLoan.Application.Common.Interfaces;

namespace VehicleManagementLoan.Application.Vehicles.GetVehicleById;

public sealed class GetVehicleByIdHandler
{
    private readonly IVehicleRepository _vehicleRepository;

    public GetVehicleByIdHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public Task<VehicleDto?> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        return _vehicleRepository.GetDtoByIdAsync(id, cancellationToken);
    }
}
