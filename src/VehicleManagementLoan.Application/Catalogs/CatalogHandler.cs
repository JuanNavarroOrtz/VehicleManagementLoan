
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Application.Catalogs;

public sealed class CatalogHandler(ICatalogRepository catalogRepository)
{
    public async Task<List<Catalog>> GetCatalogsByParentCode(int parentCode, CancellationToken cancellationToken = default)
    {
        var items = await catalogRepository.GetCatalogsByParentCode(parentCode, cancellationToken);
        return items;
    }

    public async Task<Catalog?> GetCatalogsByCode(int code, CancellationToken cancellationToken = default)
    {
        var item = await catalogRepository.GetCatalogsByCodeAsync(code, cancellationToken);
        return item;
    }
}

