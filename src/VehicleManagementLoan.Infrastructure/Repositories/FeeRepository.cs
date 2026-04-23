using Microsoft.EntityFrameworkCore;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Infrastructure.Persistence;

namespace VehicleManagementLoan.Infrastructure.Repositories;

/// <summary>
/// Repositorio de tarifas. Implementa las operaciones de lectura de la entidad Fee sobre EF Core.
/// </summary>
public sealed class FeeRepository(ApplicationDbContext context) : IFeeRepository
{
    public Task<Fee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Fees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Fee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Fees.ToListAsync(cancellationToken);
    }
}
