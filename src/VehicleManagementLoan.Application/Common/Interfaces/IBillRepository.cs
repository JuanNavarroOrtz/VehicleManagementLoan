using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Common.Interfaces;

public interface IBillRepository
{
    Task AddAsync(Bill bill, CancellationToken cancellationToken = default);
}
