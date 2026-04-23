using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.WorkTypes.GetWorkTypes;

public sealed class GetWorkTypesHandler
{
    private readonly IWorkTypeRepository _repo;

    public GetWorkTypesHandler(IWorkTypeRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<WorkType>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _repo.GetAllAsync(cancellationToken);
    }
}
