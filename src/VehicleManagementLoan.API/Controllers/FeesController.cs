using Microsoft.AspNetCore.Mvc;
using VehicleManagementLoan.Application.Fees.GetFeeById;
using VehicleManagementLoan.Application.Fees.GetFees;

namespace VehicleManagementLoan.API.Controllers;

/// <summary>
/// Controlador para el recurso Fees (Tarifas).
/// Diseñado bajo principios REST, delega las consultas de lectura (Queries) a sus respectivos Handlers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FeesController : ControllerBase
{
    private readonly GetFeesHandler _handler;
    private readonly GetFeeByIdHandler _getByIdHandler;

    /// <summary>
    /// Constructor con inyección de dependencias para los manejadores de consultas (Query Handlers).
    /// </summary>
    public FeesController(GetFeesHandler handler, GetFeeByIdHandler getByIdHandler)
    {
        _handler = handler;
        _getByIdHandler = getByIdHandler;
    }

    /// <summary>
    /// Obtiene una tarifa específica mediante su identificador.
    /// </summary>
    /// <param name="id">Identificador primario de la tarifa.</param>
    /// <returns>Retorna 200 OK con los datos de la tarifa o 404 NotFound si no existe en la base de datos.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var f = await _getByIdHandler.HandleAsync(id);
        if (f == null) return NotFound();
        return Ok(f);
    }

    /// <summary>
    /// Obtiene el listado completo de tarifas configuradas en el sistema.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación para peticiones asíncronas.</param>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await _handler.HandleAsync(cancellationToken);
        return Ok(items);
    }
}
