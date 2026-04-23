using Microsoft.AspNetCore.Mvc;
using VehicleManagementLoan.Application.Loans.CreateLoan;
using VehicleManagementLoan.Application.Loans.GetLoans;
using VehicleManagementLoan.Application.Loans.GetLoansByDateRange;
using VehicleManagementLoan.Application.Loans.GetLoansByStatus;
using Microsoft.EntityFrameworkCore;

namespace VehicleManagementLoan.API.Controllers;

/// <summary>
/// Controlador principal para la gestión de préstamos de vehículos.
/// Implementa operaciones de lectura (Queries) y escritura (Commands) respetando la separación de CQRS.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly CreateLoanHandler _handler;
    private readonly GetLoansHandler _getLoansHandler;
    private readonly GetLoansByDateRangeHandler _getByDateHandler;
    private readonly GetLoansByStatusHandler _getByStatusHandler;

    /// <summary>
    /// Inyecta los Handlers necesarios para las operaciones de lectura y escritura.
    /// </summary>
    public LoansController(
        CreateLoanHandler handler, 
        GetLoansHandler getLoansHandler, 
        GetLoansByDateRangeHandler getByDateHandler,
        GetLoansByStatusHandler getByStatusHandler)
    {
        _handler = handler;
        _getLoansHandler = getLoansHandler;
        _getByDateHandler = getByDateHandler;
        _getByStatusHandler = getByStatusHandler;
    }

    /// <summary>
    /// Crea un nuevo préstamo. Actúa como el receptor de un Command en CQRS.
    /// Valida disponibilidades, tarifas y crea las asociaciones.
    /// </summary>
    /// <param name="command">Comando con los datos para generar el préstamo.</param>
    /// <returns>Retorna 201 Created con el objeto Loan recién materializado.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLoanCommand command)
    {
        var loan = await _handler.HandleAsync(command);
        return CreatedAtAction(null, new { id = loan.Id }, loan);
    }

    /// <summary>
    /// Obtiene el listado general de préstamos con sus datos asociados (vehículo, cliente, etc).
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var loanDtos = await _getLoansHandler.HandleAsync(cancellationToken);
        return Ok(loanDtos);
    }

    /// <summary>
    /// Consulta especializada para obtener préstamos filtrados por un rango de fechas de inicio.
    /// </summary>
    /// <param name="from">Fecha de inicio del rango.</param>
    /// <param name="to">Fecha de fin del rango.</param>
    /// <returns>Colección de DTOs con la información resumida de los préstamos.</returns>
    [HttpGet("byDate")]
    public async Task<IActionResult> GetByDateRange(DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        var loanDtos = await _getByDateHandler.HandleAsync(from, to, cancellationToken);
        return Ok(loanDtos);
    }

    /// <summary>
    /// Consulta especializada para obtener préstamos según su estado (ej. Activos, Finalizados).
    /// </summary>
    /// <param name="statusId">ID del estado en el catálogo de LoanStatuses.</param>
    /// <returns>Colección de DTOs filtrados.</returns>
    [HttpGet("byStatus/{statusId}")]
    public async Task<IActionResult> GetByStatus(int statusId, CancellationToken cancellationToken)
    {
        var loanDtos = await _getByStatusHandler.HandleAsync(statusId, cancellationToken);
        return Ok(loanDtos);
    }
}
