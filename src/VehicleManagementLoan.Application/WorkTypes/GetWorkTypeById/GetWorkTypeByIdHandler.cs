using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.WorkTypes.GetWorkTypeById;

public sealed class GetWorkTypeByIdHandler
{
    private readonly IWorkTypeRepository _repo;

    public GetWorkTypeByIdHandler(IWorkTypeRepository repo)
    {
        _repo = repo;
    }

    public Task<WorkType?> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        return _repo.GetByIdAsync(id, cancellationToken);
    }
}
