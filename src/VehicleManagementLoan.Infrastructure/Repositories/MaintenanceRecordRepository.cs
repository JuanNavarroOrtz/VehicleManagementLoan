using Microsoft.EntityFrameworkCore;
using VehicleManagementLoan.Application.Common.Dtos;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Infrastructure.Persistence;
using System.Text.RegularExpressions;
namespace VehicleManagementLoan.Infrastructure.Repositories;

/// <summary>
/// Repositorio de registros de mantenimiento. Implementa las consultas LINQ sobre EF Core para la entidad MaintenanceRecord.
/// Incluye la generación automática del consecutivo con formato MT-XXXX al registrar un mantenimiento nuevo.
/// </summary>
public sealed class MaintenanceRecordRepository(ApplicationDbContext context) : IMaintenanceRecordRepository
{
    public async Task AddAsync(MaintenanceRecord maintenanceRecord, CancellationToken cancellationToken = default)
    {
        // Generar Consecutive automático si no se proporcionó
        if (string.IsNullOrWhiteSpace(maintenanceRecord.Consecutive))
        {
            //buscar el ultimo consecutive y generar el siguiente            
            const string prefix = "MT-";
            var last = await context.MaintenanceRecords
                .Where(m => m.Consecutive != null && m.Consecutive.StartsWith(prefix))
                .OrderByDescending(m => m.Consecutive)
                .Select(m => m.Consecutive)
                .FirstOrDefaultAsync(cancellationToken);

            var nextNumber = 1;
            //analiza si hay un numero detras del prefijo MT-
            if (!string.IsNullOrEmpty(last))
            {
                //busca el ultimo numero despues del prefijo MT- y le suma 1 para generar el siguiente consecutivo
                var m = Regex.Match(last, @"\d+$");
                if (m.Success && int.TryParse(m.Value, out var n))
                {
                    nextNumber = n + 1;
                }
            }

            maintenanceRecord.Consecutive = prefix + nextNumber.ToString("D4");
        }

        await context.MaintenanceRecords.AddAsync(maintenanceRecord, cancellationToken);
    }

    /// <summary>Obtiene un registro de mantenimiento por su identificador primario.</summary>
    public Task<MaintenanceRecord?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.MaintenanceRecords.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>Retorna todos los mantenimientos facturables asociados a un préstamo específico.</summary>
    public async Task<IReadOnlyCollection<MaintenanceRecord>> GetBillableByLoanIdAsync(
        int loanId,
        CancellationToken cancellationToken = default)
    {
        return await context.MaintenanceRecords
            .Where(x => x.LoanId == loanId && x.IsBillable)
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna todos los registros de mantenimiento del sistema.</summary>
    public async Task<IReadOnlyCollection<MaintenanceRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.MaintenanceRecords.ToListAsync(cancellationToken);
    }

    /// <summary>Filtra mantenimientos por rango de fechas de ejecución.</summary>
    public async Task<IReadOnlyCollection<MaintenanceRecord>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return await context.MaintenanceRecords
            .Where(m => m.MaintenanceDate >= from && m.MaintenanceDate <= to)
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna todos los mantenimientos como DTOs con datos desnormalizados (vehículo, tipo de trabajo, estado).</summary>
    public async Task<IReadOnlyCollection<MaintenanceSummaryDto>> GetSummariesAsync(CancellationToken cancellationToken = default)
    {
        return await context.MaintenanceRecords
            .Select(m => new MaintenanceSummaryDto
            {
                Id = m.Id,
                Consecutive = m.Consecutive,
                MaintenanceDate = m.MaintenanceDate,
                VehicleId = m.VehicleId,
                VehiclePlate = context.Vehicles.Where(v => v.Id == m.VehicleId).Select(v => v.Plate).FirstOrDefault(),
                LoanId = m.LoanId,
                MaintenanceContextName = context.Catalogs.Where(c => c.Id == m.MaintenanceContextTypeId).Select(c => c.Name).FirstOrDefault(),
                WorkTypeId = m.WorkTypeId,
                WorkTypeName = context.WorkTypes.Where(w => w.Id == m.WorkTypeId).Select(w => w.Name).FirstOrDefault(),
                StatusId = m.StatusId,
                StatusName = context.Catalogs.Where(c => c.Id == m.StatusId).Select(c => c.Name).FirstOrDefault()
            })
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna DTOs de mantenimientos filtrados por rango de fechas.</summary>
    public async Task<IReadOnlyCollection<MaintenanceSummaryDto>> GetSummariesByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return await context.MaintenanceRecords
            .Where(m => m.MaintenanceDate >= from && m.MaintenanceDate <= to)
            .Select(m => new MaintenanceSummaryDto
            {
                Id = m.Id,
                Consecutive = m.Consecutive,
                MaintenanceDate = m.MaintenanceDate,
                VehicleId = m.VehicleId,
                VehiclePlate = context.Vehicles.Where(v => v.Id == m.VehicleId).Select(v => v.Plate).FirstOrDefault(),
                LoanId = m.LoanId,
                MaintenanceContextName = context.Catalogs.Where(c => c.Id == m.MaintenanceContextTypeId).Select(c => c.Name).FirstOrDefault(),
                WorkTypeId = m.WorkTypeId,
                WorkTypeName = context.WorkTypes.Where(w => w.Id == m.WorkTypeId).Select(w => w.Name).FirstOrDefault(),
                StatusId = m.StatusId,
                StatusName = context.Catalogs.Where(c => c.Id == m.StatusId).Select(c => c.Name).FirstOrDefault()
            })
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna DTOs de mantenimientos filtrados por estado.</summary>
    public async Task<IReadOnlyCollection<MaintenanceSummaryDto>> GetSummariesByStatusAsync(int statusId, CancellationToken cancellationToken = default)
    {
        return await context.MaintenanceRecords
            .Where(m => m.StatusId == statusId)
            .Select(m => new MaintenanceSummaryDto
            {
                Id = m.Id,
                Consecutive = m.Consecutive,
                MaintenanceDate = m.MaintenanceDate,
                VehicleId = m.VehicleId,
                VehiclePlate = context.Vehicles.Where(v => v.Id == m.VehicleId).Select(v => v.Plate).FirstOrDefault(),
                LoanId = m.LoanId,
                MaintenanceContextName = context.Catalogs.Where(c => c.Id == m.MaintenanceContextTypeId).Select(c => c.Name).FirstOrDefault(),
                WorkTypeId = m.WorkTypeId,
                WorkTypeName = context.WorkTypes.Where(w => w.Id == m.WorkTypeId).Select(w => w.Name).FirstOrDefault(),
                StatusId = m.StatusId
            })
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna la colección de entidades MaintenanceRecord filtradas por estado.</summary>
    public async Task<IReadOnlyCollection<MaintenanceRecord>> GetByStatusAsync(int statusId, CancellationToken cancellationToken = default)
    {
        return await context.MaintenanceRecords.Where(m => m.StatusId == statusId).ToListAsync(cancellationToken);
    }
}
