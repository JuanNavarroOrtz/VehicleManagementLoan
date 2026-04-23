using VehicleManagementLoan.Domain.Common;

namespace VehicleManagementLoan.Domain.Entities;

public class Employee : AuditableEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? SecondName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? SecondLastName { get; set; }
    public string Address { get; set; } = string.Empty;
    public int CityId { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }
}
