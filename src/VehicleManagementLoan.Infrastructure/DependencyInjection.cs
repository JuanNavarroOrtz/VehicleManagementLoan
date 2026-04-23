using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehicleManagementLoan.Application.Catalogs;
using VehicleManagementLoan.Application.Clients.GetClients;
using VehicleManagementLoan.Application.Common.Interfaces;
using VehicleManagementLoan.Application.Loans.CreateLoan;
using VehicleManagementLoan.Application.Maintenance.RegisterMaintenance;
using VehicleManagementLoan.Application.Vehicles.GetVehicleById;
using VehicleManagementLoan.Application.Vehicles.GetVehicles;
using VehicleManagementLoan.Application.Loans.GetLoans;
using VehicleManagementLoan.Application.Loans.GetLoansByDateRange;
using VehicleManagementLoan.Application.Loans.GetLoansByStatus;
using VehicleManagementLoan.Application.Maintenance.GetMaintenance;
using VehicleManagementLoan.Application.Maintenance.GetMaintenanceByDateRange;
using VehicleManagementLoan.Application.Maintenance.GetMaintenanceByStatus;
using VehicleManagementLoan.Application.Fees.GetFees;
using VehicleManagementLoan.Application.Fees.GetFeeById;
using VehicleManagementLoan.Application.WorkTypes.GetWorkTypes;
using VehicleManagementLoan.Application.WorkTypes.GetWorkTypeById;
using VehicleManagementLoan.Application.Clients.GetClientById;
using VehicleManagementLoan.Infrastructure.Persistence;
using VehicleManagementLoan.Infrastructure.Repositories;
using VehicleManagementLoan.Infrastructure.UnitOfWork;


namespace VehicleManagementLoan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Obtener la cadena de conexión desde la configuración, lanzando una excepción si no está configurada
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("La cadena de conexión DefaultConnection no está configurada.");

        // Registrar el DbContext con la cadena de conexión y la configuración de migraciones
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name)));

        // Registrar el Unit of Work
        services.AddTransient<IUnitOfWork, EfUnitOfWork>();
        // Registrar el inicializador de base de datos
        services.AddScoped<IApplicationDbInitializer, ApplicationDbInitializer>();

        // Registrar repositorios concretos para lectura simple (Get/ById/List)
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IFeeRepository, FeeRepository>();
        services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();

        // Registrar repositorios y handlers para operaciones más complejas (Create/Update/Delete) que requieren lógica de negocio adicional    
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<CreateLoanHandler>();
        
        // Registrar repositorios y handlers para la capa de aplicacion de Mantenimiento 
        services.AddScoped<IMaintenanceRecordRepository, MaintenanceRecordRepository>();
        services.AddScoped<CreateMaintenanceHandler>(); 

        services.AddScoped<IBillRepository, BillRepository>();
        // Registrar repositorio de catálogos y handlers de la capa de aplicación
        services.AddScoped<ICatalogRepository, CatalogRepository>();
        services.AddScoped<CatalogHandler>();
        
        // Handlers de Application para consultas de vehículos
        services.AddScoped<GetVehicleByIdHandler>();
        services.AddScoped<GetVehiclesHandler>();
        // Handlers para consultas generales
        services.AddScoped<GetClientsHandler>();
        services.AddScoped<GetLoansHandler>();
        services.AddScoped<GetMaintenanceHandler>();
        services.AddScoped<GetFeesHandler>();
        services.AddScoped<GetWorkTypesHandler>();
        services.AddScoped<GetLoansByDateRangeHandler>();
        services.AddScoped<GetLoansByStatusHandler>();
        services.AddScoped<GetMaintenanceByDateRangeHandler>();
        services.AddScoped<GetMaintenanceByStatusHandler>();
        // Registrar handlers para consultas por Id
        services.AddScoped<GetClientByIdHandler>();
        services.AddScoped<GetFeeByIdHandler>();
        services.AddScoped<GetWorkTypeByIdHandler>();


        return services;
    }

    public static async Task SeedDBAsync(this IServiceProvider services)
    {
        // Inicializar BD y datos de desarrollo mediante servicio registrado en Infrastructure
        using (var scope = services.CreateScope())
        {
            // Obtener el entorno y el inicializador de base de datos desde el contenedor de servicios 
            var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
            // El inicializador internamente filtra por entorno, ejecutando solo en desarrollo 
            var initializer = scope.ServiceProvider.GetRequiredService<IApplicationDbInitializer>();
            // Ejecutar de forma asíncrona (await) para evitar bloqueos del hilo principal (deadlocks)
            await initializer.InitializeAsync(env);
        }
    }

}
