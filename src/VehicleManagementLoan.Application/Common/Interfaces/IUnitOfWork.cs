namespace VehicleManagementLoan.Application.Common.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IBillRepository BillRepository { get; }
    IClientRepository ClientRepository { get; }    
    ILoanRepository LoanRepository { get; }
    IVehicleRepository VehicleRepository { get; }
    IMaintenanceRecordRepository  MaintenanceRecordRepository { get; }
    IWorkTypeRepository WorkTypeRepository { get; } 
    IFeeRepository FeeRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
