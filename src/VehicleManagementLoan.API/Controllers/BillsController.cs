using Microsoft.AspNetCore.Mvc;
using VehicleManagementLoan.Application.Billing.GenerateBill;

namespace VehicleManagementLoan.API.Controllers;

/// <summary>
/// Controlador para el módulo de facturación (Bills).
/// Actúa como la capa de Presentación/API en Clean Architecture, exponiendo endpoints REST.
/// Delega toda la lógica de negocio a la capa de Aplicación a través de Handlers (patrón CQRS/Mediator simplificado).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BillsController : ControllerBase
{
    private readonly GenerateBillHandler _handler;

    /// <summary>
    /// Inyección de dependencias para el handler del caso de uso GenerateBill.
    /// </summary>
    /// <param name="handler">Handler de la capa de aplicación encargado de procesar la generación de la factura.</param>
    public BillsController(GenerateBillHandler handler)
    {
        _handler = handler;
    }

    /// <summary>
    /// Genera una nueva factura basada en los datos proporcionados (comando).
    /// </summary>
    /// <param name="dto">Data Transfer Object (DTO) que actúa como comando de entrada.</param>
    /// <returns>Retorna 201 Created con la factura generada, cumpliendo con los estándares RESTful.</returns>
    [HttpPost]
    public async Task<IActionResult> Generate([FromBody] GenerateBillCommand dto)
    {
        var bill = await _handler.HandleAsync(dto);
        // Retorna 201 CreatedAtAction, proporcionando el identificador del recurso recién creado.
        return CreatedAtAction(null, new { id = bill.Id }, bill);
    }
}
