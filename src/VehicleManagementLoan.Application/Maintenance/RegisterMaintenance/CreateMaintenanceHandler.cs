using VehicleManagementLoan.Application.Common;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Constants;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Maintenance.RegisterMaintenance;

/// <summary>
/// Caso de uso responsable del registro de un nuevo evento de mantenimiento.
/// Procesa tanto mantenimientos rutinarios como aquellos asociados a préstamos activos.
/// </summary>
public sealed class CreateMaintenanceHandler(
    IVehicleRepository vehicleRepository,
    ILoanRepository loanRepository,
    IWorkTypeRepository workTypeRepository,
    IMaintenanceRecordRepository maintenanceRecordRepository,
    ICatalogRepository catalogRepository,
    IUnitOfWork unitOfWork)
{
    /// <summary>
    /// Valida la existencia de las entidades relacionadas (vehículo, préstamo, tipo de trabajo)
    /// y asegura que se cumplan las reglas del dominio (como la consistencia del préstamo asociado 
    /// y si el trabajo es facturable) antes de persistir el registro.
    /// </summary>
    /// <param name="command">Estructura de datos que contiene toda la información del mantenimiento a registrar.</param>
    /// <param name="cancellationToken">Token para cancelación asíncrona.</param>
    /// <returns>Resultado envolviendo la entidad del registro de mantenimiento recién persistida.</returns>
    public async Task<CreateMaintenanceResult> HandleAsync(
        CreateMaintenanceCommand command,
        CancellationToken cancellationToken = default)
    {
        int _VehicleId = command.VehicleId ?? 0;
        

        if (command.LoanId.HasValue)
        {
            var loan = await loanRepository.GetByIdAsync(command.LoanId.Value, cancellationToken);
            if (loan is null)
            {
                throw new ApplicationLayerException("No se encontró el préstamo asociado.");
            }
            _VehicleId = loan.VehicleId; // Asegura que el mantenimiento se asocie al vehículo correcto del préstamo
        }

        var vehicle = await vehicleRepository.GetByIdAsync(_VehicleId, cancellationToken);
        if (vehicle is null)
        {
            throw new ApplicationLayerException("No se encontró el vehículo.");
        }

        var workType = await workTypeRepository.GetByIdAsync(command.WorkTypeId, cancellationToken);
        if (workType is null)
        {
            throw new ApplicationLayerException("No se encontró el tipo de trabajo.");
        }

        if (!workType.IsActive)
        {
            throw new ApplicationLayerException("El tipo de trabajo está inactivo.");
        }
        var maintenancePedingStatus = await catalogRepository.GetCatalogsByCodeAsync(MaintenanceStatusCodes.Pending, cancellationToken);

        var maintenanceRecord = new MaintenanceRecord
        {
            VehicleId = _VehicleId,
            LoanId = command.LoanId,
            MaintenanceContextTypeId = command.MaintenanceContextTypeId,
            WorkTypeId = command.WorkTypeId,
            Kilometers = command.Kilometers,
            Cost = command.Cost,
            MaintenanceDate = command.MaintenanceDate,
            Description = command.Description,
            StatusId = maintenancePedingStatus?.Id ?? 0
        };

        maintenanceRecord.ApplyBillableBehavior(workType.IsBillable);
        maintenanceRecord.EnsureAssociatedLoanConsistency(command.LoanId.HasValue);

        await maintenanceRecordRepository.AddAsync(maintenanceRecord, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateMaintenanceResult
        {
            MaintenanceRecord = maintenanceRecord
        };
    }
}
