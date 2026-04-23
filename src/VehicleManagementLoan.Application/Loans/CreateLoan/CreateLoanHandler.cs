using VehicleManagementLoan.Application.Common;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Constants;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Loans.CreateLoan;

/// <summary>
/// Caso de uso para la creación de un nuevo préstamo de vehículo.
/// Orquesta la validación de reglas de negocio y la persistencia de datos asociados al préstamo.
/// </summary>
public sealed class CreateLoanHandler(
    IVehicleRepository vehicleRepository,
    IClientRepository clientRepository,
    IFeeRepository feeRepository,
    ILoanRepository loanRepository,
    ICatalogRepository catalogRepository,
    IUnitOfWork unitOfWork)
{
    /// <summary>
    /// Ejecuta la lógica para registrar un préstamo, verificando la disponibilidad del vehículo,
    /// la validez del cliente y la tarifa aplicable.
    /// Actualiza el estado del vehículo a 'En Préstamo' y persiste los cambios transaccionalmente.
    /// </summary>
    /// <param name="command">Datos requeridos para la creación del préstamo.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>La entidad de préstamo generada.</returns>
    public async Task<Loan> HandleAsync(CreateLoanCommand command, CancellationToken cancellationToken = default)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(command.VehicleId, cancellationToken);
        if (vehicle is null)
        {
            throw new ApplicationLayerException("No se encontró el vehículo.");
        }

        var client = await clientRepository.GetByIdAsync(command.ClientId, cancellationToken);
        if (client is null)
        {
            throw new ApplicationLayerException("No se encontró el cliente.");
        }

        var fee = await feeRepository.GetByIdAsync(command.FeeId, cancellationToken);
        if (fee is null)
        {
            throw new ApplicationLayerException("No se encontró la tarifa.");
        }
       
        var vehicleLoanedStatus = await catalogRepository.GetCatalogsByCodeAsync(VehicleStatusCodes.Loaned, cancellationToken);      
        var vehicleActiveStatus = await catalogRepository.GetCatalogsByCodeAsync(VehicleStatusCodes.Active, cancellationToken);
        var loanActiveStatus = await catalogRepository.GetCatalogsByCodeAsync(LoanStatusCodes.Active, cancellationToken);

        client.EnsureIsActive();
        fee.EnsureIsEffectiveOn(command.StartDate);
        vehicle.MarkAsLoaned(vehicleActiveStatus?.Id ?? 0, vehicleLoanedStatus?.Id ?? 0);

        var loan = new Loan
        {
            VehicleId = command.VehicleId,
            ClientId = command.ClientId,
            StartDate = command.StartDate,
            Deposit = command.Deposit,
            FeeId = command.FeeId,
            StatusId = loanActiveStatus?.Id ?? 0,
            CreatedOn = DateTime.UtcNow
        };

        await loanRepository.AddAsync(loan, cancellationToken);
        await vehicleRepository.UpdateAsync(vehicle, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return loan;
    }
}
