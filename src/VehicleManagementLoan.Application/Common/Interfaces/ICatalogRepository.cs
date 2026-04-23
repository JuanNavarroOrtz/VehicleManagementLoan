using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Common.Interfaces
{
    public interface ICatalogRepository
    {
        Task<List<Catalog>> GetCatalogsByParentCode(int parentCode, CancellationToken cancellationToken = default);
        Task<Catalog?> GetCatalogsByCodeAsync(int code, CancellationToken cancellationToken = default);
    }
}
