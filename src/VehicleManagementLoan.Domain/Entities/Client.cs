using VehicleManagementLoan.Domain.Common;

namespace VehicleManagementLoan.Domain.Entities;

public class Client : AuditableEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? SecondName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? SecondLastName { get; set; }
    public string Address { get; set; } = string.Empty;
    public int CityId { get; set; }
    public string? BusinessName { get; set; }
    public string? CommercialName { get; set; }
    public string? Email { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int ClientTypeId { get; set; }
    public bool IsActive { get; set; }

    public void EnsureIsActive()
    {
        if (!IsActive)
        {
            throw new DomainException("El cliente está inactivo.");
        }
    }
}
