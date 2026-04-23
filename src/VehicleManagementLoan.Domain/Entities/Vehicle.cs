using VehicleManagementLoan.Domain.Common;

namespace VehicleManagementLoan.Domain.Entities;

public class Vehicle : AuditableEntity
{
    public int Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public int BrandId { get; set; }
    public string Model { get; set; } = string.Empty;
    public int VehicleTypeId { get; set; }
    public int Year { get; set; }
    public int StatusId { get; set; }

    // Navegación a catálogos para mostrar nombres en proyecciones
    public Catalog? Brand { get; set; }
    public Catalog? VehicleType { get; set; }
    public Catalog? Status { get; set; }

    public bool IsAvailable(int availableStatusId)
    {
        return StatusId == availableStatusId;
    }

    public void MarkAsLoaned(int availableStatusId, int loanedStatusId)
    {
        if (!IsAvailable(availableStatusId))
        {
            throw new DomainException("El vehículo no está disponible para préstamo.");
        }

        StatusId = loanedStatusId;
    }

    public void MarkAsAvailable(int availableStatusId)
    {
        StatusId = availableStatusId;
    }
}
