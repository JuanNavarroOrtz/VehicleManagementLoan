using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace VehicleManagementLoan.Infrastructure.Persistence;

/// <summary>
/// Interfaz para servicios que se encargan de la inicialización y población de datos iniciales (Seed Data).
/// Define el contrato para métodos que preparan la base de datos antes de su uso en producción o desarrollo.
/// </summary>
public interface IApplicationDbInitializer
{
    /// <summary>
    /// Inicializa y configura la base de datos.
    /// </summary>
    /// <param name="environment">Entorno actual que puede influir en el proceso de inicialización.</param>
    Task InitializeAsync(IHostEnvironment environment);
}
