using Microsoft.EntityFrameworkCore;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Infrastructure.Persistence;

namespace VehicleManagementLoan.Infrastructure.Repositories;

/// <summary>
/// Repositorio de clientes. Implementa las operaciones de lectura de la entidad Client sobre EF Core.
/// </summary>
public sealed class ClientRepository(ApplicationDbContext context) : IClientRepository
{
    public Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Clients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Client>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Clients.ToListAsync(cancellationToken);
    }
}
