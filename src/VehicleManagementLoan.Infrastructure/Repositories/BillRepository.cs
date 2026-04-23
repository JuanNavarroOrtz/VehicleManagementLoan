using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Infrastructure.Persistence;

namespace VehicleManagementLoan.Infrastructure.Repositories;

/// <summary>
/// Implementación del repositorio para operaciones CRUD en la entidad Bill.
/// </summary>
public sealed class BillRepository(ApplicationDbContext context) : IBillRepository
{
    public async Task AddAsync(Bill bill, CancellationToken cancellationToken = default)
    {
        await context.Bills.AddAsync(bill, cancellationToken);
    }
}
