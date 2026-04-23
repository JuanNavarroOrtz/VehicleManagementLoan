using VehicleManagementLoan.Domain.Common;

namespace VehicleManagementLoan.Domain.Entities;

public class Bill : AuditableEntity
{
    public int Id { get; set; }
    public int LoanId { get; set; }
    public DateTime BillDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public int BillStatusId { get; set; }
}
