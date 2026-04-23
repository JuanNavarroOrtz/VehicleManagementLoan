using Microsoft.AspNetCore.Mvc;
using VehicleManagementLoan.Application.Catalogs;
using VehicleManagementLoan.Domain.Constants;

namespace VehicleManagementLoan.API.Controllers;

/// <summary>
/// Controlador para la gestión de catálogos paramétricos (tipos, estados, etc.).
/// Proveedor centralizado de datos maestros para las listas desplegables del frontend.
/// Evita la necesidad de hardcodear IDs en la capa de presentación.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CatalogsController : ControllerBase
{
    private readonly CatalogHandler _handler;

    /// <summary>
    /// Inyección del handler de catálogos.
    /// Este handler encapsula la lógica para recuperar sub-elementos de un código padre.
    /// </summary>
    public CatalogsController(CatalogHandler handler)
    {
        _handler = handler;
    }

    /// <summary>
    /// Obtiene los estados disponibles para un préstamo (ej. Pendiente, Activo, Finalizado).
    /// </summary>
    /// <returns>Lista de entidades de catálogo correspondientes a LoanStatuses.</returns>
    [HttpGet("loanStatuses")]
    public async Task<IActionResult> GetLoanStatuses()
    {
        // Accede al código constante en la capa de Dominio para evitar "magic strings"
        var items = await _handler.GetCatalogsByParentCode(CatalogCodes.LoanStatuses);
        return Ok(items);
    }

    /// <summary>
    /// Obtiene los estados disponibles para un vehículo (ej. Disponible, En Préstamo, En Mantenimiento).
    /// </summary>
    [HttpGet("vehicleStatuses")]
    public async Task<IActionResult> GetVehicleStatuses()
    {
        var items = await _handler.GetCatalogsByParentCode(CatalogCodes.VehicleStatuses);
        return Ok(items);
    }

    /// <summary>
    /// Obtiene los contextos de mantenimiento (ej. Normal, Asociado a préstamo).
    /// </summary>
    [HttpGet("maintenanceContextTypes")]
    public async Task<IActionResult> GetMaintenanceContexts()
    {
        var items = await _handler.GetCatalogsByParentCode(CatalogCodes.MaintenanceContexts);
        return Ok(items);
    }
}
