using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Fees.GetFeeById;

public sealed class GetFeeByIdHandler
{
    private readonly IFeeRepository _repo;

    public GetFeeByIdHandler(IFeeRepository repo)
    {
        _repo = repo;
    }

    public Task<Fee?> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        return _repo.GetByIdAsync(id, cancellationToken);
    }
}
