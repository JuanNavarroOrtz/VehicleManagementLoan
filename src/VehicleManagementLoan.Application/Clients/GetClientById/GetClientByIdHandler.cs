using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Clients.GetClientById;

public sealed class GetClientByIdHandler
{
    private readonly IClientRepository _repo;

    public GetClientByIdHandler(IClientRepository repo)
    {
        _repo = repo;
    }

    public Task<Client?> HandleAsync(int id, CancellationToken cancellationToken = default)
    {
        return _repo.GetByIdAsync(id, cancellationToken);
    }
}
