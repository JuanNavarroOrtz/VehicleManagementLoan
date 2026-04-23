using Microsoft.AspNetCore.Mvc;
using VehicleManagementLoan.Application.Maintenance.RegisterMaintenance;
using VehicleManagementLoan.Application.Maintenance.GetMaintenance;
using VehicleManagementLoan.Application.Maintenance.GetMaintenanceByDateRange;
using VehicleManagementLoan.Application.Maintenance.GetMaintenanceByStatus;

namespace VehicleManagementLoan.API.Controllers;

/// <summary>
/// Controlador para el registro y consulta de mantenimientos de vehículos.
/// Maneja operaciones tanto para mantenimientos regulares como los asociados a préstamos activos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MaintenanceController : ControllerBase
{
    private readonly CreateMaintenanceHandler _handler;
    
    private readonly GetMaintenanceHandler _getMaintenanceHandler;
    private readonly GetMaintenanceByDateRangeHandler _getByDateHandler;
    private readonly GetMaintenanceByStatusHandler _getByStatusHandler;

    /// <summary>
    /// Inyecta los Handlers necesarios para las operaciones de lectura y escritura.
    /// </summary>
    public MaintenanceController(
        CreateMaintenanceHandler handler, 
        GetMaintenanceHandler getMaintenanceHandler,
        GetMaintenanceByDateRangeHandler getByDateHandler,
        GetMaintenanceByStatusHandler getByStatusHandler)
    {
        _handler = handler;
        _getMaintenanceHandler = getMaintenanceHandler;
        _getByDateHandler = getByDateHandler;
        _getByStatusHandler = getByStatusHandler;
    }

    /// <summary>
    /// Registra un nuevo evento de mantenimiento.
    /// Delega la lógica de negocio (validación de contexto, actualización de estados de vehículo) a la capa Application.
    /// </summary>
    /// <param name="command">Datos estructurados del mantenimiento a crear.</param>
    /// <returns>Retorna 201 Created junto con la entidad persistida.</returns>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateMaintenanceCommand command)
    {        
        var result = await _handler.HandleAsync(command);

        return CreatedAtAction(null, new { id = result.MaintenanceRecord.Id }, result.MaintenanceRecord);
    }

    /// <summary>
    /// Obtiene todos los mantenimientos registrados, formateados como resúmenes (DTOs).
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var dtos = await _getMaintenanceHandler.HandleAsync(cancellationToken);
        return Ok(dtos);
    }

    /// <summary>
    /// Consulta mantenimientos filtrados por un rango de fechas.
    /// Útil para la generación de reportes y vistas principales.
    /// </summary>
    /// <param name="from">Fecha inicio.</param>
    /// <param name="to">Fecha fin.</param>
    [HttpGet("byDate")]
    public async Task<IActionResult> GetByDateRange(DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        var dtos = await _getByDateHandler.HandleAsync(from, to, cancellationToken);
        return Ok(dtos);
    }

    /// <summary>
    /// Obtiene mantenimientos filtrados por el identificador de su estado.
    /// </summary>
    /// <param name="statusId">ID del catálogo asociado al estado de la orden de mantenimiento.</param>
    [HttpGet("byStatus/{statusId}")]
    public async Task<IActionResult> GetByStatus(int statusId, CancellationToken cancellationToken)
    {
        var items = await _getByStatusHandler.HandleAsync(statusId, cancellationToken);
        return Ok(items);
    }
}
