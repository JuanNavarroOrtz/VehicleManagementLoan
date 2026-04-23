public abstract class AuditableEntity
{
    public int CreatedByUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? ModifiedByUserId { get; set; }
    public DateTime? ModifiedOn { get; set; }
}

public class User : AuditableEntity
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Email { get; set; }
    public bool IsActive { get; set; }
}

public class Employee : AuditableEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? SecondName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? SecondLastName { get; set; }
    public string Address { get; set; } = string.Empty;
    public int CityId { get; set; } // Managua, León, Granada, etc. --- tabla de catálogo
    public bool IsActive { get; set; }
    public int UserId { get; set; } // tabla Usuario
}

public class Catalog : AuditableEntity
{
    public int Id { get; set; }
    // Código numérico que identifica el catálogo (ej. 100 = Ciudades)
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentCatalogId { get; set; } // Para datos jerárquicos
    public bool IsActive { get; set; }
}

public class Client : AuditableEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? SecondName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? SecondLastName { get; set; }
    public string Address { get; set; } = string.Empty;
    public int CityId { get; set; } // Managua, León, Granada, etc. --- tabla de catálogo
    public string? BusinessName { get; set; }
    public string? CommercialName { get; set; }
    public string? Email { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int ClientTypeId { get; set; } // Natural, Jurídico --- tabla de catálogo
    public bool IsActive { get; set; }
}

public class Vehicle : AuditableEntity
{
    public int Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public string BrandId { get; set; } = string.Empty; // Toyota, Nissan, Honda, etc. --- tabla de catálogo
    public string Model { get; set; } = string.Empty;
    public int VehicleTypeId { get; set; } // Auto Sedán, Auto SUV, Auto hatchback, Moto, Camioneta, etc. --- tabla de catálogo
    public int Year { get; set; }
    public int StatusId { get; set; } // Activo, Inactivo, Mantenimiento, Cancelado, baja temporal --- tabla de catálogo
}

public class Fee : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Por Día, Por Semana, Por Mes
    public decimal Amount { get; set; }
    public bool IsActive { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
}

public class WorkType : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Mantenimiento, Reparación, Accidente, etc.
    public bool IsBillable { get; set; }
    public bool IsActive { get; set; }
}

public class Loan : AuditableEntity
{
    public int Id { get; set; }
    public int VehicleId { get; set; } // tabla Vehículo
    public int ClientId { get; set; } // tabla Cliente
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Deposit { get; set; }
    public int FeeId { get; set; } // Por Día, Por Semana, Por Mes --- tabla Tarifa
    public int StatusId { get; set; } // Activo, Inactivo, Cancelado --- tabla de catálogo
}

public class MaintenanceRecord : AuditableEntity
{
    public int Id { get; set; }
    public int VehicleId { get; set; } // tabla Vehículo
    public int? LoanId { get; set; } // tabla Préstamo
    public int MaintenanceContextTypeId { get; set; } // Normal, Asociado --- tabla de catálogo
    public int WorkTypeId { get; set; } // Mantenimiento, Reparación, Accidente, etc. --- tabla WorkType
    public decimal Kilometers { get; set; }
    public decimal Cost { get; set; }
    public bool IsBillable { get; set; } // de la tabla WorkType y campo editable
    public DateTime MaintenanceDate { get; set; }
    public string? Description { get; set; }
    public int StatusId { get; set; } // Realizado, Pendiente, Cancelado --- tabla de catálogo
}

public class Bill : AuditableEntity
{
    public int Id { get; set; }
    public int LoanId { get; set; } // tabla Préstamo
    public DateTime BillDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public int BillStatusId { get; set; } // Pagada, Pendiente, Cancelada --- tabla de catálogo
}
