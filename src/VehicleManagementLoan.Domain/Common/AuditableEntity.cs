namespace VehicleManagementLoan.Domain.Common;

public abstract class AuditableEntity
{
    public int CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? ModifiedByUserId { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
