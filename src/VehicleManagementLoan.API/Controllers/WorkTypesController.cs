using Microsoft.AspNetCore.Mvc;
using VehicleManagementLoan.Application.WorkTypes.GetWorkTypeById;
using VehicleManagementLoan.Application.WorkTypes.GetWorkTypes;

namespace VehicleManagementLoan.API.Controllers;

/// <summary>
/// Controlador para gestionar los Tipos de Trabajo de mantenimiento.
/// Proporciona endpoints de solo lectura (Queries) delegando la lógica a los Handlers de Application.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WorkTypesController : ControllerBase
{
    private readonly GetWorkTypesHandler _handler;
    private readonly GetWorkTypeByIdHandler _getByIdHandler;

    /// <summary>
    /// Inyecta las dependencias necesarias para las consultas CQRS relacionadas con WorkTypes.
    /// </summary>
    public WorkTypesController(GetWorkTypesHandler handler, GetWorkTypeByIdHandler getByIdHandler)
    {
        _handler = handler;
        _getByIdHandler = getByIdHandler;
    }

    /// <summary>
    /// Recupera los detalles específicos de un tipo de trabajo de mantenimiento.
    /// </summary>
    /// <param name="id">Clave primaria de la entidad WorkType.</param>
    /// <returns>Retorna 200 OK con el DTO correspondiente o 404 NotFound si el registro no existe.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var wt = await _getByIdHandler.HandleAsync(id);
        if (wt == null) return NotFound();
        return Ok(wt);
    }

    /// <summary>
    /// Consulta el catálogo completo de tipos de trabajos habilitados en el sistema.
    /// Útil para poblar selectores (dropdowns) en el frontend.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelación asíncrona de la solicitud.</param>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await _handler.HandleAsync(cancellationToken);
        return Ok(items);
    }
}
