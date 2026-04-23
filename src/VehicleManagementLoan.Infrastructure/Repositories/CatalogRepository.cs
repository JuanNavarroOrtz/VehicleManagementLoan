using Microsoft.EntityFrameworkCore;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Infrastructure.Persistence;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio para operaciones CRUD en la entidad Catalog.
    /// </summary>
    public class CatalogRepository(ApplicationDbContext context) : ICatalogRepository
    {       

        /// <summary>
        /// Obtiene una lista de catálogos filtrados por código padre.
        /// </summary>
        /// <param name="parentCode">Código del catálogo padre para filtrar.</param>
        /// <param name="cancellationToken">Token para la cancelación de la operación.</param>
        /// <returns>Lista de catálogos filtrados.</returns>
        public async Task<List<Catalog>> GetCatalogsByParentCode(int parentCode, CancellationToken cancellationToken = default)
        {
             var items = await context.Catalogs.Where(c => c.ParentCatalogId != null && c.Code >= parentCode && c.Code < parentCode + 100).ToListAsync(cancellationToken);
            return items;
        }

         public async Task<Catalog?> GetCatalogsByCodeAsync(int code, CancellationToken cancellationToken = default)
        {
            var item = await context.Catalogs.FirstOrDefaultAsync(c => c.Code == code, cancellationToken);
            return item;
        }
    }
}
