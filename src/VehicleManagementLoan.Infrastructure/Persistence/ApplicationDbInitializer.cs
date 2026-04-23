using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VehicleManagementLoan.Domain.Constants;
using VehicleManagementLoan.Domain.Entities;

namespace VehicleManagementLoan.Infrastructure.Persistence;

/// <summary>
/// Clase encargada de la inicialización y población de datos iniciales (Seed Data) en la base de datos.
/// Utilizada principalmente en entornos de desarrollo para configurar el estado base del sistema.
/// </summary>
public class ApplicationDbInitializer : IApplicationDbInitializer
{
    private readonly ApplicationDbContext _db;
    public ApplicationDbInitializer(ApplicationDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    private record SeedCatalog(int Code, int? ParentCode, string Name);
    private record SeedUser(string Username, string Password, string Email, bool IsActive);
    private record SeedFee(string Name, decimal Amount, bool IsActive, DateTime EffectiveFrom, DateTime? EffectiveTo);
    private record SeedWorkType(string Name, bool IsBillable, bool IsActive);
    private record SeedVehicle(string Plate, string Vin, int BrandCode, string Model, int VehicleTypeCode, int Year, int StatusCode);
    private record SeedClient(string FirstName, string? SecondName, string? LastName, string? SecondLastName, string? BusinessName, string? CommercialName, string? Address, int CityCode, int ClientTypeCode, string? IdentificationNumber, string? Phone, string? Email, string? CreatedByUsername);
    private record SeedEmployee(string FirstName, string LastName, string Address, int CityCode, bool IsActive, string Username);
    private class RootSeed
    {
        public List<SeedUser>? Users { get; set; }
        public List<SeedCatalog>? Catalogs { get; set; }
        public List<SeedFee>? Fees { get; set; }
        public List<SeedWorkType>? WorkTypes { get; set; }
        public List<SeedVehicle>? Vehicles { get; set; }
        public List<SeedClient>? Clients { get; set; }
        public List<SeedEmployee>? Employees { get; set; }
    }

    public async Task InitializeAsync(IHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
            return;

        await _db.Database.MigrateAsync();

        // Carga el archivo de inicialización de datos (seed) desde la carpeta de salida
        var seedPath = Path.Combine(AppContext.BaseDirectory, "SeedData", "initialData.json");
        if (!File.Exists(seedPath))
        {
            // Alternativa: usar catalogs.json si initialData.json no está presente
            seedPath = Path.Combine(AppContext.BaseDirectory, "SeedData", "catalogs.json");
            if (!File.Exists(seedPath))
                return; // No hay datos para inicializar
        }

        var json = await File.ReadAllTextAsync(seedPath);
        var root = JsonSerializer.Deserialize<RootSeed>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var now = DateTime.UtcNow;

        // Inicialización de Usuarios
        if (root?.Users?.Any() == true && !_db.Users.Any())
        {
            var users = root.Users.Select(u => new User { Username = u.Username, Password = u.Password, Email = u.Email, IsActive = u.IsActive }).ToList();
            _db.Users.AddRange(users);
            await _db.SaveChangesAsync();
        }

        // Asegurar que el usuario administrador existe
        var adminUser = _db.Users.FirstOrDefault(u => u.Username == "Admin");
        if (adminUser == null)
        {
            adminUser = new User { Username = "Admin", Password = "123", Email = "Admin@local", IsActive = true };
            _db.Users.Add(adminUser);
            await _db.SaveChangesAsync();
        }

        // Catálogos: se crean primero los padres y luego los hijos usando Code/ParentCode
        if (root?.Catalogs?.Any() == true && !_db.Catalogs.Any())
        {
            var catalogSeeds = root.Catalogs;
            // Crear catálogos padre primero (ParentCode == null)
            var parentSeeds = catalogSeeds.Where(c => c.ParentCode == null).ToList();
            foreach (var p in parentSeeds)
            {
                _db.Catalogs.Add(new Catalog { Code = p.Code, Name = p.Name, IsActive = true, CreatedByUserId = adminUser.Id, CreatedOn = now });
            }
            await _db.SaveChangesAsync();

            // Crear catálogos hijos
            var childSeeds = catalogSeeds.Where(c => c.ParentCode != null).ToList();
            foreach (var c in childSeeds)
            {
                var parent = _db.Catalogs.FirstOrDefault(x => x.Code == c.ParentCode!.Value);
                if (parent != null)
                {
                    _db.Catalogs.Add(new Catalog { Code = c.Code, ParentCatalogId = parent.Id, Name = c.Name, IsActive = true, CreatedByUserId = adminUser.Id, CreatedOn = now });
                }
            }
            await _db.SaveChangesAsync();
        }

        // Tarifas
        if (root?.Fees?.Any() == true && !_db.Fees.Any())
        {
            foreach (var f in root.Fees)
            {
                _db.Fees.Add(new Fee { Name = f.Name, Amount = f.Amount, IsActive = f.IsActive, EffectiveFrom = f.EffectiveFrom, EffectiveTo = f.EffectiveTo, CreatedByUserId = adminUser.Id, CreatedOn = now });
            }
            await _db.SaveChangesAsync();
        }

        // Tipos de trabajo
        if (root?.WorkTypes?.Any() == true && !_db.WorkTypes.Any())
        {
            foreach (var wt in root.WorkTypes)
            {
                _db.WorkTypes.Add(new WorkType { Name = wt.Name, IsBillable = wt.IsBillable, IsActive = wt.IsActive, CreatedByUserId = adminUser.Id, CreatedOn = now });
            }
            await _db.SaveChangesAsync();
        }

        // Vehículos
        if (root?.Vehicles?.Any() == true && !_db.Vehicles.Any())
        {
            foreach (var v in root.Vehicles)
            {
                var brand = _db.Catalogs.FirstOrDefault(c => c.Code == v.BrandCode);
                var vtype = _db.Catalogs.FirstOrDefault(c => c.Code == v.VehicleTypeCode);
                var status = _db.Catalogs.FirstOrDefault(c => c.Code == v.StatusCode);
                if (brand != null && vtype != null && status != null)
                {
                    _db.Vehicles.Add(new Vehicle { Plate = v.Plate, Vin = v.Vin, BrandId = brand.Id, Model = v.Model, VehicleTypeId = vtype.Id, Year = v.Year, StatusId = status.Id, CreatedByUserId = adminUser.Id, CreatedOn = now });
                }
            }
            await _db.SaveChangesAsync();
        }

        // Clientes
        if (root?.Clients?.Any() == true && !_db.Clients.Any())
        {
            foreach (var c in root.Clients)
            {
                var creator = _db.Users.FirstOrDefault(u => u.Username == c.CreatedByUsername) ?? adminUser;
                var city = _db.Catalogs.FirstOrDefault(cat => cat.Code == c.CityCode);
                var clientType = _db.Catalogs.FirstOrDefault(cat => cat.Code == c.ClientTypeCode);
                if (city != null && clientType != null)
                {
                    var client = new Client
                    {
                        FirstName = c.FirstName ?? string.Empty,
                        SecondName = c.SecondName,
                        LastName = c.LastName ?? string.Empty,
                        SecondLastName = c.SecondLastName,
                        BusinessName = c.BusinessName,
                        CommercialName = c.CommercialName,
                        Address = c.Address ?? string.Empty,
                        CityId = city.Id,
                        ClientTypeId = clientType.Id,
                        IdentificationNumber = c.IdentificationNumber ?? string.Empty,
                        Phone = c.Phone ?? string.Empty,
                        Email = c.Email,
                        IsActive = true,
                        CreatedByUserId = creator.Id,
                        CreatedOn = now
                    };
                    _db.Clients.Add(client);
                }
            }
            await _db.SaveChangesAsync();
        }

        // Empleados
        if (root?.Employees?.Any() == true && !_db.Employees.Any())
        {
            foreach (var e in root.Employees)
            {
                var user = _db.Users.FirstOrDefault(u => u.Username == e.Username);
                var city = _db.Catalogs.FirstOrDefault(c => c.Code == e.CityCode);
                if (user != null && city != null)
                {
                    _db.Employees.Add(new Employee { FirstName = e.FirstName, LastName = e.LastName, Address = e.Address, CityId = city.Id, IsActive = e.IsActive, UserId = user.Id, CreatedByUserId = adminUser.Id, CreatedOn = now });
                }
            }
            await _db.SaveChangesAsync();
        }

        // Asegurar que los estados de 'Anulado' existan para préstamos, mantenimientos y facturas
        var loanStatusParentExisting = _db.Catalogs.FirstOrDefault(c => c.Code == 500);
        if (loanStatusParentExisting != null && !_db.Catalogs.Any(c => c.Code == 503))
        {
            _db.Catalogs.Add(new Catalog { Code = 503, ParentCatalogId = loanStatusParentExisting.Id, Name = "Anulado", IsActive = true, CreatedByUserId = adminUser.Id, CreatedOn = DateTime.UtcNow });
            await _db.SaveChangesAsync();
        }

        var maintenanceStatusParentExisting = _db.Catalogs.FirstOrDefault(c => c.Code == 700);
        if (maintenanceStatusParentExisting != null && !_db.Catalogs.Any(c => c.Code == 703))
        {
            _db.Catalogs.Add(new Catalog { Code = 703, ParentCatalogId = maintenanceStatusParentExisting.Id, Name = "Anulado", IsActive = true, CreatedByUserId = adminUser.Id, CreatedOn = DateTime.UtcNow });
            await _db.SaveChangesAsync();
        }

        var billStatusParentExisting = _db.Catalogs.FirstOrDefault(c => c.Code == 800);
        if (billStatusParentExisting != null && !_db.Catalogs.Any(c => c.Code == 803))
        {
            _db.Catalogs.Add(new Catalog { Code = 803, ParentCatalogId = billStatusParentExisting.Id, Name = "Anulado", IsActive = true, CreatedByUserId = adminUser.Id, CreatedOn = DateTime.UtcNow });
            await _db.SaveChangesAsync();
        }
    }
}
