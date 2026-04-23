using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Application.Common.Interfaces;

namespace VehicleManagementLoan.Application.Vehicles.GetVehicles;

/// <summary>
/// Caso de uso para consultar el catálogo completo de vehículos.
/// Separa la responsabilidad de lectura (Query) de la capa de API.
/// </summary>
public sealed class GetVehiclesHandler
{
    private readonly IVehicleRepository _vehicleRepository;

    public GetVehiclesHandler(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    /// <summary>
    /// Recupera la colección completa de vehículos disponibles en el sistema.
    /// </summary>
    /// <param name="cancellationToken">Token para la cancelación de la solicitud asíncrona.</param>
    /// <returns>Colección de sólo lectura con las entidades de los vehículos.</returns>
    public Task<IReadOnlyCollection<Vehicle>> HandleAsync(CancellationToken cancellationToken = default)
    {
        // Delega la consulta al repositorio, manteniendo el controlador limpio
        return _vehicleRepository.GetAllAsync(cancellationToken);
    }
}
