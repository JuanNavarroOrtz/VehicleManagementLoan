using Microsoft.EntityFrameworkCore;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Application.Common.Dto;
using VehicleManagementLoan.Domain.Entities;
using VehicleManagementLoan.Infrastructure.Persistence;

namespace VehicleManagementLoan.Infrastructure.Repositories;

/// <summary>
/// Repositorio de vehículos. Implementa consultas sobre EF Core para la entidad Vehicle.
/// Expone tanto consultas de entidades como de DTOs enriquecidos (con nombres de catálogos).
/// </summary>
public sealed class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
{
    /// <summary>Obtiene un vehículo por su identificador primario.</summary>
    public Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Vehicles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>Retorna todos los vehículos registrados en el sistema.</summary>
    public async Task<IReadOnlyCollection<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Vehicles.ToListAsync(cancellationToken);
    }

    /// <summary>Actualiza los datos de un vehículo existente en el contexto de EF Core.</summary>
    public Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        context.Vehicles.Update(vehicle);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Proyecta la entidad Vehicle a un DTO enriquecido con los nombres de marca, tipo y estado
    /// obtenidos de los catálogos relacionados mediante navegación de EF Core.
    /// </summary>
    public async Task<VehicleDto?> GetDtoByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await context.Vehicles
            .Where(v => v.Id == id)
            .Select(v => new VehicleDto
            {
                Id = v.Id,
                Plate = v.Plate,
                Vin = v.Vin,
                Model = v.Model,
                Year = v.Year,
                BrandName = v.Brand != null ? v.Brand.Name : string.Empty,
                VehicleTypeName = v.VehicleType != null ? v.VehicleType.Name : string.Empty,
                StatusName = v.Status != null ? v.Status.Name : string.Empty
            })
            .FirstOrDefaultAsync(cancellationToken);

        return item;
    }
}
