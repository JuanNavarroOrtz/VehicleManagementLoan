using Microsoft.EntityFrameworkCore;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Infrastructure.Persistence;

namespace VehicleManagementLoan.Infrastructure.Repositories;

/// <summary>
/// Repositorio de tipos de trabajo. Implementa las operaciones de lectura de la entidad WorkType sobre EF Core.
/// </summary>
public sealed class WorkTypeRepository(ApplicationDbContext context) : IWorkTypeRepository
{
    public Task<WorkType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.WorkTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<WorkType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.WorkTypes.ToListAsync(cancellationToken);
    }
}
