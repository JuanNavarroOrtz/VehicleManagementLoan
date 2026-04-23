using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Infrastructure.Persistence;
using VehicleManagementLoan.Infrastructure.Repositories;

namespace VehicleManagementLoan.Infrastructure.UnitOfWork;

/// <summary>
/// Implementación concreta del patrón Unit of Work sobre Entity Framework Core.
/// Centraliza el acceso a los repositorios y expone el método transaccional SaveChangesAsync.
/// </summary>
public sealed class EfUnitOfWork : IUnitOfWork 
{
    private readonly ApplicationDbContext _context;
   
    public IBillRepository BillRepository {get; private set;}

    public IClientRepository ClientRepository {get; private set;} 

    public ILoanRepository LoanRepository {get; private set;}   
    
    public IVehicleRepository VehicleRepository {get; private set;}

    public IMaintenanceRecordRepository MaintenanceRecordRepository {get; private set;}

    public IWorkTypeRepository WorkTypeRepository {get; private set;}

    public IFeeRepository FeeRepository {get; private set;} 


    public EfUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        BillRepository = new BillRepository(_context);
        ClientRepository = new ClientRepository(_context);
        LoanRepository = new LoanRepository(_context);
        VehicleRepository = new VehicleRepository(_context);
        MaintenanceRecordRepository = new MaintenanceRecordRepository(_context);
        WorkTypeRepository = new WorkTypeRepository(_context);
        FeeRepository = new FeeRepository(_context);
    }

    public ValueTask DisposeAsync()
    {
        _context.Dispose();
        return ValueTask.CompletedTask;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
