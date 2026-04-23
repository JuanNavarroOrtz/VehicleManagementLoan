using Microsoft.EntityFrameworkCore;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Application.Common.Dtos;
using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Infrastructure.Persistence;
using System.Text.RegularExpressions;

namespace VehicleManagementLoan.Infrastructure.Repositories;

/// <summary>
/// Repositorio de préstamos. Implementa las consultas LINQ sobre EF Core para la entidad Loan.
/// Incluye la generación automática del consecutivo con formato LN-XXXX al registrar un préstamo nuevo.
/// </summary>
public sealed class LoanRepository(ApplicationDbContext context) : ILoanRepository
{
    /// <summary>Obtiene un préstamo por su identificador primario.</summary>
    public Task<Loan?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Loans.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
    {
        // Generar Consecutive automático si no viene proporcionado
        if (string.IsNullOrWhiteSpace(loan.Consecutive))
        {
            // Formato: LN-0001
            const string prefix = "LN-";
            // Obtener el mayor consecutivo existente con prefijo LN-
            var last = await context.Loans
                .Where(l => l.Consecutive != null && l.Consecutive.StartsWith(prefix))
                .OrderByDescending(l => l.Consecutive)
                .Select(l => l.Consecutive)
                .FirstOrDefaultAsync(cancellationToken);

            var nextNumber = 1;
            if (!string.IsNullOrEmpty(last))
            {
                var m = Regex.Match(last, @"\\d+$");
                if (m.Success && int.TryParse(m.Value, out var n))
                {
                    nextNumber = n + 1;
                }
            }

            loan.Consecutive = prefix + nextNumber.ToString("D4");
        }

        await context.Loans.AddAsync(loan, cancellationToken);
    }

    /// <summary>Retorna todos los préstamos registrados en el sistema.</summary>
    public async Task<IReadOnlyCollection<Loan>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Loans.ToListAsync(cancellationToken);
    }

    /// <summary>Filtra préstamos por rango de fechas de inicio.</summary>
    public async Task<IReadOnlyCollection<Loan>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return await context.Loans
            .Where(l => l.StartDate >= from && l.StartDate <= to)
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna todos los préstamos como DTOs resumidos con datos desnormalizados (placa, cliente, estado).</summary>
    public async Task<IReadOnlyCollection<LoanSummaryDto>> GetSummariesAsync(CancellationToken cancellationToken = default)
    {
        return await context.Loans
            .Select(l => new LoanSummaryDto
            {
                Id = l.Id,
                Consecutive = l.Consecutive,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                VehicleId = l.VehicleId,
                VehiclePlate = context.Vehicles.Where(v => v.Id == l.VehicleId).Select(v => v.Plate).FirstOrDefault(),
                ClientId = l.ClientId,
                ClientFullName = context.Clients.Where(c => c.Id == l.ClientId).Select(c => c.FirstName + " " + c.LastName).FirstOrDefault(),
                ClientLoanCount = context.Loans.Count(x => x.ClientId == l.ClientId),
                StatusId = l.StatusId,
                StatusName = context.Catalogs.Where(x => x.Id == l.StatusId).Select(z => z.Name).FirstOrDefault(),
            })
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna DTOs resumidos de préstamos filtrados por rango de fechas de inicio.</summary>
    public async Task<IReadOnlyCollection<LoanSummaryDto>> GetSummariesByDateRangeAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return await context.Loans
            .Where(l => l.StartDate >= from && l.StartDate <= to)
            .Select(l => new LoanSummaryDto
            {
                Id = l.Id,
                Consecutive = l.Consecutive,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                VehicleId = l.VehicleId,
                VehiclePlate = context.Vehicles.Where(v => v.Id == l.VehicleId).Select(v => v.Plate).FirstOrDefault(),
                ClientId = l.ClientId,
                ClientFullName = context.Clients.Where(c => c.Id == l.ClientId).Select(c => c.FirstName + " " + c.LastName).FirstOrDefault(),
                ClientLoanCount = context.Loans.Count(x => x.ClientId == l.ClientId),
                StatusId = l.StatusId,
                StatusName = context.Catalogs.Where(x => x.Id == l.StatusId).Select(z => z.Name).FirstOrDefault()
            })
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna DTOs resumidos de préstamos filtrados por estado.</summary>
    public async Task<IReadOnlyCollection<LoanSummaryDto>> GetSummariesByStatusAsync(int statusId, CancellationToken cancellationToken = default)
    {
        return await context.Loans
            .Where(l => l.StatusId == statusId)
            .Select(l => new LoanSummaryDto
            {
                Id = l.Id,
                Consecutive = l.Consecutive,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                VehicleId = l.VehicleId,
                VehiclePlate = context.Vehicles.Where(v => v.Id == l.VehicleId).Select(v => v.Plate).FirstOrDefault(),
                ClientId = l.ClientId,
                ClientFullName = context.Clients.Where(c => c.Id == l.ClientId).Select(c => c.FirstName + " " + c.LastName).FirstOrDefault(),
                ClientLoanCount = context.Loans.Count(x => x.ClientId == l.ClientId),
                StatusId = l.StatusId,
                StatusName = context.Catalogs.Where(x => x.Id == l.StatusId).Select(z => z.Name).FirstOrDefault()
            })
            .ToListAsync(cancellationToken);
    }

    /// <summary>Retorna la colección de entidades Loan filtradas por estado.</summary>
    public async Task<IReadOnlyCollection<Loan>> GetByStatusAsync(int statusId, CancellationToken cancellationToken = default)
    {
        return await context.Loans.Where(l => l.StatusId == statusId).ToListAsync(cancellationToken);
    }
}
