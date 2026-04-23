using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Fees.GetFees;

public sealed class GetFeesHandler
{
    private readonly IFeeRepository _repo;

    public GetFeesHandler(IFeeRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<Fee>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _repo.GetAllAsync(cancellationToken);
    }
}
