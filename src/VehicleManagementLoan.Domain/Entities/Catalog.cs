using VehicleManagementLoan.Domain.Common;

namespace VehicleManagementLoan.Domain.Entities;

public class Catalog : AuditableEntity
{
    public int Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentCatalogId { get; set; }
    public bool IsActive { get; set; }
}
