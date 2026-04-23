using VehicleManagementLoan.Domain.Common;

namespace VehicleManagementLoan.Domain.Entities;

public class Loan : AuditableEntity
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Deposit { get; set; }
    public int FeeId { get; set; }
    public int StatusId { get; set; }
    // Consecutivo para identificar el préstamo (por ejemplo "LN-0001")
    public string? Consecutive { get; set; }

    public bool IsClosed()
    {
        return EndDate.HasValue;
    }

    public void Close(DateTime endDate, int closedStatusId)
    {
        if (IsClosed())
        {
            throw new DomainException("El préstamo ya está cerrado.");
        }

        if (endDate.Date < StartDate.Date)
        {
            throw new DomainException("La fecha de finalización del préstamo no puede ser anterior a la fecha de inicio.");
        }

        EndDate = endDate;
        StatusId = closedStatusId;
    }
}
