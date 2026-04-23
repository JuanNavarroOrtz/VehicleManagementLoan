using VehicleManagementLoan.Application.Common;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Billing.GenerateBill;

/// <summary>
/// Caso de uso encargado de la generación de facturas asociadas a préstamos.
/// Consolida los costos de los mantenimientos facturables y aplica los impuestos correspondientes.
/// </summary>
public sealed class GenerateBillHandler(
    IUnitOfWork unitOfWork)
{
    /// <summary>
    /// Procesa la creación de una factura para un préstamo específico.
    /// Recupera los mantenimientos facturables, calcula subtotales e impuestos, y persiste la factura.
    /// </summary>
    /// <param name="command">Datos requeridos para generar la factura, incluyendo porcentajes de impuestos.</param>
    /// <param name="cancellationToken">Token para la cancelación asíncrona.</param>
    /// <returns>La entidad Bill generada con sus totales calculados.</returns>
    public async Task<Bill> HandleAsync(GenerateBillCommand command, CancellationToken cancellationToken = default)
    {
        var loan = await unitOfWork.LoanRepository.GetByIdAsync(command.LoanId, cancellationToken);
        if (loan is null)
        {
            throw new ApplicationLayerException("No se encontró el préstamo.");
        }

        var maintenances = await unitOfWork.MaintenanceRecordRepository.GetBillableByLoanIdAsync(command.LoanId, cancellationToken);
        if (maintenances.Count == 0)
        {
            throw new ApplicationLayerException("No existen mantenimientos facturables para este préstamo.");
        }

        var subtotal = maintenances.Sum(maintenance => maintenance.Cost);
        var tax = subtotal * command.TaxPercentage;
        var total = subtotal + tax;

        var bill = new Bill
        {
            LoanId = command.LoanId,
            BillDate = command.BillDate,
            Subtotal = subtotal,
            Tax = tax,
            Total = total,
            BillStatusId = command.BillStatusId
        };

        await unitOfWork.BillRepository.AddAsync(bill, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);   

        return bill;
    }
}
