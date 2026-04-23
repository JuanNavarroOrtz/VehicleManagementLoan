using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Clients.GetClients;

public sealed class GetClientsHandler
{
    private readonly IClientRepository _repo;

    public GetClientsHandler(IClientRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyCollection<Client>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _repo.GetAllAsync(cancellationToken);
    }
}
