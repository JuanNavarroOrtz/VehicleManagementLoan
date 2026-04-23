
# VehicleManagementLoan

API REST en .NET 10 para gestión de préstamos de vehículos, mantenimientos y facturación.

Descripción breve
- Clean Architecture: Domain, Application, Infrastructure, API.
- EF Core + SQL Server para persistencia.

Requisitos
- .NET 10 SDK
- SQL Server accesible desde tu entorno (local o contenedor)
- dotnet-ef (herramienta de CLI) — instalar si no está:
  dotnet tool install --global dotnet-ef

Estructura principal
```
VehicleManagementLoan/
├── src/
│   ├── VehicleManagementLoan.Domain/
│   ├── VehicleManagementLoan.Application/
│   ├── VehicleManagementLoan.Infrastructure/
│   └── VehicleManagementLoan.API/
├── Documentación/
└── VehicleManagementLoan.sln
```

Pasos para ejecutar localmente (recomendado)
1) Clonar y restaurar:
   git clone <url>
   cd VehicleManagementLoan
   dotnet restore

2) Configurar la cadena de conexión (API project):
   - Usar User Secrets en desarrollo:
     cd src\VehicleManagementLoan.API
     dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=VehicleManagementLoanDB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True"

3) Aplicar migraciones y/o crear nuevas migraciones
   - IMPORTANTE: EF Tools necesita poder crear el DbContext en tiempo de diseño. La forma recomendada es indicar el proyecto "startup" (API) que configura DI y la cadena de conexión.

   - Listar migraciones existentes:
     $env:ASPNETCORE_ENVIRONMENT = 'Development'
     dotnet ef migrations list --project src\VehicleManagementLoan.Infrastructure --startup-project src\VehicleManagementLoan.API

   - Crear una nueva migración (cuando cambias el modelo):
     $env:ASPNETCORE_ENVIRONMENT = 'Development'
     dotnet ef migrations add NombreMigracion --project src\VehicleManagementLoan.Infrastructure --startup-project src\VehicleManagementLoan.API --context ApplicationDbContext

   - Aplicar migraciones a la base de datos:
     dotnet ef database update --project src\VehicleManagementLoan.Infrastructure --startup-project src\VehicleManagementLoan.API --context ApplicationDbContext

   - Alternativa: si NO quieres usar --startup-project puedes agregar un DesignTimeDbContextFactory en el proyecto Infrastructure. Esto permite que EF cree el DbContext en diseño sin depender de la API.

4) Ejecutar la API (seed en Development)
   - El initializer carga seed JSON desde `src/VehicleManagementLoan.Infrastructure/SeedData/initialData.json` cuando ASPNETCORE_ENVIRONMENT == Development.
     En PowerShell:
     $env:ASPNETCORE_ENVIRONMENT = 'Development'
     dotnet run --project src\VehicleManagementLoan.API

   - Al arrancar, ApplicationDbInitializer aplicará migraciones pendientes y cargará datos desde los JSON (si no existen en la BD).

Verificación en BD
- Revisa las tablas creadas y los datos seed (Users, Catalogs, Fees, WorkTypes, Vehicles, Clients, Employees).
- También puedes consultar __EFMigrationsHistory para ver migraciones aplicadas.

Errores comunes y soluciones
- "Unable to resolve service for type 'DbContextOptions...'": ejecuta EF con --startup-project apuntando a VehicleManagementLoan.API o agrega DesignTimeDbContextFactory.
- "file locked" (MSB3021): detén el proceso de depuración en Visual Studio (Stop), cierra la app y reconstruye.
- Si usas User Secrets, asegúrate de ejecutar comandos en el entorno Development o pasar --startup-project para que se carguen.

Seed y datos
- Seed inicial ahora se lee desde src/VehicleManagementLoan.Infrastructure/SeedData/initialData.json y/o catalogs.json.
- El Initializer inserta datos solo si no existen para evitar duplicados.

Swagger
- Al ejecutar la API, Swagger disponible en /swagger (p. ej. https://localhost:5071/swagger)

