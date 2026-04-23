# Progreso del Proyecto

## 2026-04-19

### Tarea 1 - Modelado de Entidades

Estado: `Completado`

Trabajo completado:

- Definido `AuditableEntity`
- Definido `Catalog`
- Definidas entidades de negocio base
- Definido `Bill`
- Organizado `Domain` en `Common` y `Entities`
- Alineada la documentación alrededor de `Documentación/Steps/Entidades.cs`

Verificación:

- `VehicleManagementLoan.Domain` compiló exitosamente

### Tarea 2 - Reglas de Negocio del Dominio

Estado: `Completado`

Trabajo completado:

- Agregado `DomainException`
- Agregada validación de cliente activo
- Agregada validación de disponibilidad de vehículo
- Agregado comportamiento de cambio de estado de vehículo
- Agregada validez de tarifa por fecha
- Agregada validación de cierre de préstamo
- Agregado comportamiento de mantenimiento normal vs asociado
- Agregado comportamiento facturable de mantenimiento

Verificación:

- `VehicleManagementLoan.Domain` compiló exitosamente después de reglas de negocio

### Tarea 3 - Capa de Aplicación

Estado: `Completado`

Trabajo completado:

- Agregados contratos de repositorio
- Agregado `IUnitOfWork`
- Agregado `ApplicationLayerException`
- Agregado `CreateLoanCommand` y `CreateLoanHandler`
- Agregado `RegisterMaintenanceCommand`, modelo de resultado y manejador
- Agregado `GenerateBillCommand` y manejador

Verificación:

- `VehicleManagementLoan.Application` compiló exitosamente

### Tarea 4 - Capa de Infraestructura

Estado: `Completado`

Trabajo planificado:

- Agregar paquetes EF Core
- Crear `DbContext`
- Crear configuraciones de entidades
- Implementar repositorios
- Implementar `UnitOfWork`
- Registrar servicios en DI
- Preparar migraciones y conexión a base de datos

Progreso actual:

- Agregadas referencias de paquetes EF Core
- Creado `ApplicationDbContext`
- Creadas configuraciones de entidades base
- Implementados repositorios EF
- Implementado `EfUnitOfWork`
- Registrados servicios de infraestructura en DI
- Agregada cadena de conexión base de SQL Server

Verificación:

- `VehicleManagementLoan.API` compiló exitosamente con Infraestructura integrada

Notas:

- Compilación completada con advertencias de vulnerabilidades de NuGet relacionadas con `System.Security.Cryptography.Xml` 9.0.0 provenientes de dependencias transitivas

### Ajuste - Simplificación del Modelo de Facturación

Estado: `Completado`

Trabajo completado:

- Removido `AdditionalCharge` de `Domain`
- Removido repositorio y configuración EF de `AdditionalCharge`
- Cambiado flujo de facturación para usar entradas `MaintenanceRecord` facturables asociadas directamente
- Actualizada documentación para reflejar el nuevo modelo de facturación

Verificación:

- `VehicleManagementLoan.API` compiló exitosamente después de remover `AdditionalCharge`

### Alineación de Documentación

Estado: `Completado`

Trabajo completado:

- Confirmado `Documentación/Steps/Entidades.cs` es la fuente de verdad para el modelo
- Actualizado diccionario de documentación para seguir `Entidades.cs`
- Cambiado el archivo de diccionario a formato Markdown para mejor legibilidad en VS Code
- Actualizado resumen de arquitectura para reflejar el objetivo real del proyecto
- Actualizados casos de uso para reflejar el objetivo actual:
  - crear préstamo
  - registrar mantenimiento normal
  - registrar mantenimiento asociado
  - generar factura como plus

Próximo enfoque funcional:

- continuar el proyecto basado en `Entidades.cs`

### Alineación de Código con Entities.cs

Estado: `Completado`

Trabajo completado:

- alinear entidades de `Domain` con `Documentación/Steps/Entidades.cs`
- alinear modelo de persistencia de `Infrastructure` con la misma fuente
- agregado `User` y `Employee`
- cambiado `Client.City` a `Client.CityId`
- removidos campos no presentes en `Entidades.cs`
- mantenido facturación como flujo plus sin cambiar `Entidades.cs`
- cambiado formato de diccionario de `.txt` a `.md`

Verificación:

- `VehicleManagementLoan.API` compiló exitosamente después de la alineación

### Mejora del diccionario

Estado: `Completado`

Trabajo completado:

- Reescrito el diccionario para explicar el propósito de cada campo
- Eliminadas listas de campos poco útiles
- Cambiado el contenido a un formato descriptivo orientado al negocio

### Convención de idioma

Estado: `Completado`

Trabajo completado:

- Mensajes de excepción en español
- Mantener nombres técnicos y estructura del código en inglés
- Registros de catálogos en español según el uso esperado

### Configuración de conexión local

Estado: `Registrado`

Decisión:

- No almacenar credenciales reales en `appsettings.json`
- Usar `dotnet user-secrets` para desarrollo local
- Usar variables de entorno o un almacén seguro fuera del entorno de desarrollo

Comandos recomendados:

Inicializar user secrets en el proyecto API (desde la carpeta del proyecto o usando --project):

```powershell
cd C:\Users\jcnx2\Documents\Projects\Net\Project\VehicleManagementLoan\src\VehicleManagementLoan.API
dotnet user-secrets init
```

Crear o establecer la cadena de conexión:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True"
```

Listar secretos almacenados:

```powershell
dotnet user-secrets list --project "C:\Users\jcnx2\Documents\Projects\Net\Project\VehicleManagementLoan\src\VehicleManagementLoan.API\VehicleManagementLoan.API.csproj"
```

Nota importante:

- `DependencyInjection.cs` ya lee `ConnectionStrings:DefaultConnection`.
- Cambiar el valor del secreto es suficiente; no se requiere cambio de código.

### EF Core Foreign Keys

Status: `Completed`

Completed work:

- made important foreign keys explicit in Fluent API
- added foreign key constraint names
- configured `DeleteBehavior.Restrict` on critical relationships

Change Brand string field to fk
- Its more funtional handled a catalog for these field 

## 2026-04-20

### Task - Use Cases Review

Status: `Recorded`

Observed:

- Application layer: CreateLoanHandler, RegisterMaintenanceHandler and GenerateBillHandler implemented and aligned with Entities.cs.
- Infrastructure: ApplicationDbContext, entity configurations, repositories and EfUnitOfWork are present and registered in DI.
- API: minimal setup (Program.cs with controllers and Swagger) but no controllers/endpoints found for the three use cases.
- DTOs/commands exist in Application but there is no API model binding/validation layer (DTOs, FluentValidation or similar) and no explicit mapping (AutoMapper or manual mappers).
- Migrations: no migrations folder found; database provisioning/migrations automation is not present.
- Tests: no unit or integration tests detected.
- Error handling & logging: no global error handling middleware or structured logging configuration found.

Risks / Notes:

- Without controllers or endpoints the API is not callable; with a short deadline, implementing API entrypoints is priority.
- Lack of migrations blocks easy DB creation for integration tests and deployments.

Recommended priority (short deadline):

1. Implement API endpoints (Controllers or Minimal API) for CreateLoan, RegisterMaintenance and GenerateBill so backend can be exercised end-to-end.
2. Add simple DTOs and request validation (params -> Application commands). Implement mapping (manual or AutoMapper).
3. Configure EF Core migrations and create an initial migration; add instructions to README and use-secrets guidance already in Progress.md.
4. Add basic global error handling middleware that converts ApplicationLayerException into structured HTTP responses (Spanish messages allowed in payloads as agreed).
5. Add simple integration tests later (after migrations) and unit tests for handlers if time allows.

Next action proposal: implement item 1 (API endpoints) now so you can test flows quickly. Confirm which task to start and I will update this file with progress.

### 2026-04-20 - API Endpoints Implementation

Status: `In Progress`

Completed work:

- Implemented LoansController POST /api/loans that maps CreateLoanDto to CreateLoanCommand and calls CreateLoanHandler.
- Implemented MaintenanceController POST /api/maintenance that maps RegisterMaintenanceDto to RegisterMaintenanceCommand and calls RegisterMaintenanceHandler.
- Implemented BillsController POST /api/bills that maps GenerateBillDto to GenerateBillCommand and calls GenerateBillHandler.
- Registered application handlers in Infrastructure DI so controllers can resolve them.

- Registered repository implementations in DI (VehicleRepository, LoanRepository, BillRepository, ClientRepository, FeeRepository, MaintenanceRecordRepository, WorkTypeRepository) so handlers can resolve dependencies.

Next steps:

- Add minimal request validation and model comments (English) in controllers.
- Test endpoint flows locally (requires DB connection/migrations).

### 2026-04-21 - Endpoints añadidos (GET necesarios para casos de uso)

Estado: `En progreso - endpoints básicos añadidos`

Trabajo realizado:

- Añadidos controladores y endpoints básicos para soportar los casos de uso solicitados:
  - VehiclesController: GET /api/vehicles y GET /api/vehicles/{id} (placeholder para listado)
  - ClientsController: GET /api/clients y GET /api/clients/{id}
  - FeesController: GET /api/fees y GET /api/fees/{id}
  - WorkTypesController: GET /api/worktypes y GET /api/worktypes/{id}
  - CatalogsController: GET /api/catalogs/loan-statuses, /vehicle-statuses y /maintenance-context-types
  - LoansController y MaintenanceController: ya existían los POST; se añadieron controladores de lectura básicos para Vehicles/Clients/Fees/WorkTypes/Catalogs

Notas:

- Los endpoints de listado devuelven actualmente colecciones vacías cuando no existe un método de listado implementado en los repositorios. Esto permite integrar el frontend y completar los handlers sin romper contratos.
- Se registraron los repositorios en DI para que los controladores puedan resolver dependencias.

Próximos pasos recomendados:

- Implementar métodos de listado en repositorios (GetAllAsync) y devolver datos reales.
- Añadir validación de entrada y mapeo DTO -> Command en controladores para seguridad y claridad.
- Añadir pruebas de integración para flujos end-to-end.


got it

## 2026-04-21 - Ejecutar migraciones y preparar herramientas

Estado: `Completado`

Trabajo realizado:

- Verificada la herramienta dotnet-ef (versión 10.0.6).
- Añadido/ajustado el paquete Microsoft.EntityFrameworkCore a la versión 10.0.6 en el proyecto Infrastructure para evitar conflictos de versión.
- Añadido Microsoft.EntityFrameworkCore.Design al proyecto Infrastructure cuando fue necesario.
- Creada la migración InitialCreate en el proyecto Infrastructure (ApplicationDbContext).
- Aplicadas las migraciones a la base de datos usando el proyecto API como startup (se actualizaron las tablas y la tabla __EFMigrationsHistory).

Comandos ejecutados (resumen):

```powershell
dotnet ef --version
dotnet add src\VehicleManagementLoan.Infrastructure package Microsoft.EntityFrameworkCore -v 10.0.6
dotnet add src\VehicleManagementLoan.Infrastructure package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate --project src\VehicleManagementLoan.Infrastructure --startup-project src\VehicleManagementLoan.API --output-dir Persistence\Migrations --context ApplicationDbContext
dotnet ef database update --project src\VehicleManagementLoan.Infrastructure --startup-project src\VehicleManagementLoan.API --context ApplicationDbContext
```

Notas:

- Si usas user-secrets para la cadena de conexión, asegúrate de tener el secreto correctamente configurado y que ASPNETCORE_ENVIRONMENT esté en Development cuando corresponda.
- Las migraciones fueron aplicadas con éxito; revisa la BD VehicleManagementLoanDB en el servidor configurado para confirmar las tablas y datos iniciales.

### 2026-04-21 - Refactor: controladores delegan y repositorios listados

Estado: `Completado`

Trabajo realizado:

- Movidos y actualizados controladores para que sólo reciban parámetros y deleguen a la capa de Infrastructure/Application:
  - VehiclesController: ahora usa IVehicleRepository.GetAllAsync y GetByIdAsync.
  - ClientsController: ahora usa IClientRepository.GetAllAsync y GetByIdAsync.
  - FeesController: ahora usa IFeeRepository.GetAllAsync y GetByIdAsync.
  - WorkTypesController: ahora usa IWorkTypeRepository.GetAllAsync y GetByIdAsync.
- Añadidos métodos GetAllAsync en los contratos de repositorio (Application) y sus implementaciones EF (Infrastructure).
- Registrados los repositorios en DI (DependencyInjection) para resolución por los controladores.

Notas:

- La lógica de filtrado/negocio queda fuera de los controladores; la próxima tarea será implementar filtros en Application o en queries de Infrastructure.
- Se actualizó el código y la documentación de progreso automáticamente.
 La lógica de filtrado/negocio queda fuera de los controladores; la próxima tarea será implementar filtros en Application o en queries de Infrastructure.
 Se actualizó el código y la documentación de progreso automáticamente.

### 2026-04-22 - Endpoints: listados y filtros por fecha/estado; Consecutivos

Estado: `En progreso`

Trabajo realizado:

- Agregados cuatro endpoints nuevos:
  - LoansController: GET /api/loans (listado), GET /api/loans/by-date (filtrar por rango from/to), GET /api/loans/by-status/{statusId} (filtrar por estado).
  - MaintenanceController: GET /api/maintenance (listado), GET /api/maintenance/by-date (filtrar por rango), GET /api/maintenance/by-status/{statusId}.

- Añadidos métodos en repositorios para soportar consultas:
  - ILoanRepository / LoanRepository: GetAllAsync, GetByDateRangeAsync, GetByStatusAsync.
  - IMaintenanceRecordRepository / MaintenanceRecordRepository: GetAllAsync, GetByDateRangeAsync, GetByStatusAsync.

- Agregados campos Consecutive a las entidades Loan y MaintenanceRecord y configurados en Fluent API (MaxLength 50).

Notas:

- Los endpoints reciben parámetros (from/to/statusId) y delegan la lógica a los repositorios; ningún filtro de negocio vive en los controladores.
- Próximo paso: inicializar Consecutive para registros existentes (seed) y añadir validación en handlers para generar consecutivos al crear nuevas entidades.

### 2026-04-23 - Consecutivos automáticos en repositorios

Estado: `Completado`

Trabajo realizado:

- Implementado generación automática de campo Consecutive al guardar entidades:
  - LoanRepository.AddAsync genera valores como "LN-0001", "LN-0002", ... si el campo no es provisto.
  - MaintenanceRecordRepository.AddAsync genera valores como "MT-0001", "MT-0002", ... si el campo no es provisto.

Notas:

- La generación busca el último consecutivo con el prefijo correspondiente en la base de datos y genera el siguiente número de forma secuencial (padding D4).
- Próximo paso: ejecutar migración para agregar las columnas y sembrar 10 clientes de prueba; luego validar end-to-end.


## 2026-04-21 - Desarrollo del Frontend y Limpieza de Placeholder

Estado: `Completado`

Trabajo realizado:

- Limpieza del proyecto: Eliminadas todas las referencias a "Products" (vistas, componentes y controladores heredados).
- Implementado `apiService.js`: Servicio ligero que utiliza `fetch` nativo para conectar con la API de .NET (Vehículos, Clientes, Préstamos, Mantenimiento, Facturación).
- Refactorización de `App.jsx`: Implementado sistema de navegación por pestañas (Tabs) basado en `useState` para una experiencia ligera y rápida.
- Implementada Vista de Préstamos (`Loans.jsx`):
  - Formulario dinámico para creación de préstamos (Caso de Uso 1).
  - Carga automática de catálogos de vehículos, clientes y tarifas al montar el componente.
- Implementada Vista de Mantenimiento (`Maintenance.jsx`):
  - Registro de mantenimientos normales y asociados a préstamos (Caso de Uso 2).
  - Filtrado reactivo de préstamos según el vehículo seleccionado.
- Implementada Vista de Facturación (`Billing.jsx`):
  - Selección de préstamos y generación de facturas (Caso de Uso 3 - Flujo Plus).
  - Visualización de resultados de facturación (subtotales, impuestos, total).
- Mejora Estética y UX:
  - Mantenimiento del sistema de diseño (Dark Mode, Glassmorphism, Flexbox).
  - Adaptación de `index.css` para soportar la navegación y los nuevos formularios.
- Corrección de Errores:
  - Corregido `ReferenceError: Greenland is not defined` causado por texto accidental al final de los archivos de vista.
- Automatización de Puerto:
  - Configurado Proxy en `vite.config.js` para redireccionar `/api` al puerto 5071 (ajustado según la ejecución real de .NET).
  - Actualizada `API_BASE_URL` en el frontend a una ruta relativa para permitir detección automática del puerto/host del backend en desarrollo.

Notas:

- Todo el código del frontend ha sido comentado en español siguiendo la solicitud del usuario.
- El proyecto mantiene una arquitectura sencilla sin librerías de UI pesadas para garantizar la máxima ligereza.
- Se requiere que el backend esté ejecutándose en `http://localhost:5045/api` para el correcto funcionamiento de las peticiones.

## 2026-04-22 - Mostrar nombres en Vehicles API (Brand/Type/Status)

Estado: `Completado`

Trabajo realizado:

- Añadidas propiedades de navegación en la entidad Vehicle (Brand, VehicleType, Status) para permitir proyecciones basadas en relaciones a Catalog.
- Creado VehicleDto en la capa Application (Id, Plate, Vin, Model, Year, BrandName, VehicleTypeName, StatusName).
- Añadido método GetDtoByIdAsync en la interfaz IVehicleRepository y su implementación en VehicleRepository que proyecta a VehicleDto usando relaciones.
- Actualizado VehicleConfiguration para mapear las propiedades de navegación a las claves foráneas existentes.
- Modificado VehiclesController para devolver VehicleDto en GET /api/vehicles/{id} en lugar de la entidad completa con FK ids.

Notas técnicas:

- No fue necesaria ninguna migración para estos cambios (solo se añadieron propiedades de navegación y DTOs).
- La proyección usa las relaciones a Catalog para recuperar Name en una sola consulta EF Core.
- Recomendación: añadir constantes/enum para los códigos de catálogo y usar DTOs adicionales para listados paginados.

Comandos y comprobaciones realizadas:

```powershell
dotnet build
```

La solución compila correctamente tras los cambios.

## 2026-04-22 - Listas generales, filtros por fecha y select de préstamos

Estado: `Completado`

Trabajo realizado:

- Agregado método `getMaintenances` y métodos `getLoansByDate`, `getMaintenancesByDate` en `apiService.js` en el frontend.
- Modificada la vista `Loans.jsx` (Préstamos) para agregar una tabla (lista general) con la lista de préstamos.
- Agregado select para consultar préstamos en la pantalla de préstamos.
- Modificada la vista `Maintenance.jsx` (Mantenimiento) para agregar una tabla (lista general) con la lista de mantenimientos.
- Implementado filtro por rango de fechas (Desde / Hasta) en las vistas de Préstamos y Mantenimientos, conectado con los endpoints `byDate` de la API.

Notas:

- Las listas se recargan automáticamente al registrar un nuevo préstamo o mantenimiento.
- Los filtros por fecha usan las opciones nativas del input tipo "date" y permiten delimitar correctamente las búsquedas.
