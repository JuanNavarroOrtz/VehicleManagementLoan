Setup completo desde cero — VehicleManagementLoan (.NET 10)

1) Crear carpeta raíz del proyecto

Ubícate en tu directorio de trabajo (por ejemplo Documents/Projects/Net) y ejecuta:

```powershell
mkdir VehicleManagementLoan
cd VehicleManagementLoan
```

2) Crear estructura de carpetas

```powershell
mkdir src
mkdir tests
```

3) Crear la solución

```powershell
dotnet new sln -n VehicleManagementLoan --format sln
dir
```

Deberías ver:

- VehicleManagementLoan.sln
- src/
- tests/

4) Crear proyectos dentro de /src

```powershell
cd src
dotnet new classlib -n VehicleManagementLoan.Domain
dotnet new classlib -n VehicleManagementLoan.Application
dotnet new classlib -n VehicleManagementLoan.Infrastructure
dotnet new webapi -n VehicleManagementLoan.API
cd ..
```

5) Agregar proyectos a la solución

```powershell
dotnet sln add src/VehicleManagementLoan.Domain
dotnet sln add src/VehicleManagementLoan.Application
dotnet sln add src/VehicleManagementLoan.Infrastructure
dotnet sln add src/VehicleManagementLoan.API
```

6) Configurar referencias (Clean Architecture)

```powershell
dotnet add src/VehicleManagementLoan.Application reference src/VehicleManagementLoan.Domain
dotnet add src/VehicleManagementLoan.Infrastructure reference src/VehicleManagementLoan.Domain
dotnet add src/VehicleManagementLoan.Infrastructure reference src/VehicleManagementLoan.Application
dotnet add src/VehicleManagementLoan.API reference src/VehicleManagementLoan.Application
dotnet add src/VehicleManagementLoan.API reference src/VehicleManagementLoan.Infrastructure
```

Estructura esperada:

VehicleManagementLoan/
 ├── src/
 │   ├── VehicleManagementLoan.Domain
 │   ├── VehicleManagementLoan.Application
 │   ├── VehicleManagementLoan.Infrastructure
 │   └── VehicleManagementLoan.API
 ├── tests/
 └── VehicleManagementLoan.sln

7) Limpiar template de la API

Eliminar archivo innecesario y dejar Program.cs mínimo:

```powershell
del src\VehicleManagementLoan.API\WeatherForecast.cs
```

Program.cs mínimo:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
```

8) .gitignore

```powershell
dotnet new gitignore
```

9) Instalar Swagger

```powershell
dotnet add package Swashbuckle.AspNetCore
```

10) Verificar que todo compila

```powershell
dotnet build
```

