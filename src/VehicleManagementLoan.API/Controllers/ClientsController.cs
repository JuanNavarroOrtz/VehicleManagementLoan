using Microsoft.AspNetCore.Mvc;
using VehicleManagementLoan.Application.Clients.GetClientById;
using VehicleManagementLoan.Application.Clients.GetClients;

namespace VehicleManagementLoan.API.Controllers;

/// <summary>
/// Controlador para el recurso Clients (Clientes).
/// Mantiene la capa de API lo más delgada posible, delegando la lógica de consulta
/// a los Handlers de la capa de Application.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly GetClientsHandler _handler;
    private readonly GetClientByIdHandler _getByIdHandler;

    /// <summary>
    /// Constructor con inyección de dependencias.
    /// Define explícitamente los casos de uso (Handlers) que soporta el controlador.
    /// </summary>
    public ClientsController(GetClientsHandler handler, GetClientByIdHandler getByIdHandler)
    {
        _handler = handler;
        _getByIdHandler = getByIdHandler;
    }

    /// <summary>
    /// Obtiene los detalles de un cliente por su identificador único.
    /// </summary>
    /// <param name="id">Identificador primario del cliente.</param>
    /// <returns>Retorna 200 OK con el DTO del cliente si se encuentra; de lo contrario 404 NotFound.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var c = await _getByIdHandler.HandleAsync(id);
        if (c == null) return NotFound();
        return Ok(c);
    }

    /// <summary>
    /// Retorna una colección con todos los clientes del sistema.
    /// </summary>
    /// <param name="cancellationToken">Token para propagar la cancelación de la solicitud HTTP asíncrona hacia la base de datos.</param>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await _handler.HandleAsync(cancellationToken);
        return Ok(items);
    }
}
