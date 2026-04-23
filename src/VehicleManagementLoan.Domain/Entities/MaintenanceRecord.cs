using VehicleManagementLoan.Domain.Common;

namespace VehicleManagementLoan.Domain.Entities;

public class MaintenanceRecord : AuditableEntity
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int? LoanId { get; set; }
    public int MaintenanceContextTypeId { get; set; }
    public int WorkTypeId { get; set; }
    public decimal Kilometers { get; set; }
    public decimal Cost { get; set; }
    public bool IsBillable { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string? Description { get; set; }
    public int StatusId { get; set; }
    // Consecutivo para identificar el mantenimiento (por ejemplo "MT-0001")
    public string? Consecutive { get; set; }

    public bool IsAssociated()
    {
        return LoanId.HasValue;
    }

    public bool IsNormal()
    {
        return !IsAssociated();
    }

    public void EnsureAssociatedLoanConsistency(bool shouldBeAssociated)
    {
        var isAssociated = IsAssociated();

        if (shouldBeAssociated && !isAssociated)
        {
            throw new DomainException("El mantenimiento asociado debe incluir un préstamo.");
        }

        if (!shouldBeAssociated && isAssociated)
        {
            throw new DomainException("El mantenimiento normal no puede incluir un préstamo.");
        }
    }

    public void ApplyBillableBehavior(bool isBillable)
    {
        IsBillable = isBillable;
    }
}
