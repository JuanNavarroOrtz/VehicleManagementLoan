namespace VehicleManagementLoan.Application.Common.Dto;

public sealed class VehicleDto
{
    public int Id { get; init; }
    public string Plate { get; init; } = string.Empty;
    public string Vin { get; init; } = string.Empty;
    public string BrandName { get; init; } = string.Empty;
    public string VehicleTypeName { get; init; } = string.Empty;
    public string StatusName { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
}
