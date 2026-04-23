using Microsoft.AspNetCore.Mvc;
using VehicleManagementLoan.Application.Vehicles.GetVehicleById;
using VehicleManagementLoan.Application.Vehicles.GetVehicles;

namespace VehicleManagementLoan.API.Controllers;

/// <summary>
/// Controlador principal para los activos físicos (Vehículos).
/// Permite su consulta detallada o la visualización de un catálogo general.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly GetVehicleByIdHandler _getByIdHandler;
    private readonly GetVehiclesHandler _getVehiclesHandler;

    /// <summary>
    /// Inyecta los Handlers necesarios para las consultas de vehículos.
    /// </summary>
    public VehiclesController(GetVehicleByIdHandler getByIdHandler, GetVehiclesHandler getVehiclesHandler)
    {
        _getByIdHandler = getByIdHandler;
        _getVehiclesHandler = getVehiclesHandler;
    }

    /// <summary>
    /// Recupera la vista detallada de un vehículo por su identificador.
    /// Utiliza un Handler de la capa de Application para ensamblar un DTO complejo.
    /// </summary>
    /// <param name="id">Clave primaria del vehículo.</param>
    /// <returns>Retorna 200 OK con el DTO enriquecido (nombres en lugar de IDs foráneos) o 404 NotFound.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Devolver DTO con nombres legibles en lugar de FK ids a través del handler de aplicación
        var dto = await _getByIdHandler.HandleAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// Obtiene el catálogo completo de vehículos sin paginación.
    /// Apto solo para volúmenes de datos controlados.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await _getVehiclesHandler.HandleAsync(cancellationToken);
        return Ok(items);
    }
}
