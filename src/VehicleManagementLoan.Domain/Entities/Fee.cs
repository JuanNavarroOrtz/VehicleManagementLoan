using VehicleManagementLoan.Domain.Common;

namespace VehicleManagementLoan.Domain.Entities;

public class Fee : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsActive { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }

    public bool IsEffectiveOn(DateTime date)
    {
        if (!IsActive)
        {
            return false;
        }

        var targetDate = date.Date;
        var startsOnOrBefore = EffectiveFrom.Date <= targetDate;
        var endsOnOrAfter = !EffectiveTo.HasValue || EffectiveTo.GetValueOrDefault().Date >= targetDate;

        return startsOnOrBefore && endsOnOrAfter;
    }

    public void EnsureIsEffectiveOn(DateTime date)
    {
        if (!IsEffectiveOn(date))
        {
            throw new DomainException("La tarifa no está vigente para la fecha solicitada.");
        }
    }
}
