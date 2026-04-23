# Resumen de VehicleManagementLoan

## Objetivo

La solución implementa una API .NET 10 con:

- Arquitectura Limpia (Clean Architecture)
- Patrón CQRS simplificado (Handlers por caso de uso)
- Patrón Repositorio e Interfaz de Repositorio
- Unidad de Trabajo (Unit of Work)
- Reglas de negocio claras para préstamos, mantenimiento y facturación

Casos de uso implementados:

- Crear un préstamo (`CreateLoanHandler`)
- Registrar un mantenimiento normal o asociado a préstamo (`CreateMaintenanceHandler`)
- Generar una factura a partir de los mantenimientos facturables (`GenerateBillHandler`)
- Consultas por filtros: fecha, estado, ID, catálogos

## Capas

### Dominio

Contendrá las entidades de negocio y reglas de dominio.
El modelo de datos detallado está centralizado en:

- `Documentación/Steps/Entidades.cs`

El resumen legible del modelo está centralizado en:

- `Documentación/Diccionario/index.md`

### Aplicación

Contendrá los casos de uso y contratos:

- repositorios por agregado
- `IUnitOfWork`
- `IDateTimeProvider`
- `IUserContext`

Casos de uso implementados:

- `CreateLoanHandler` — crea un préstamo, valida vehículo, cliente y tarifa
- `CreateMaintenanceHandler` — registra mantenimiento normal o asociado a préstamo
- `GenerateBillHandler` — genera factura consolidando mantenimientos facturables
- `GetLoansHandler`, `GetLoansByDateRangeHandler`, `GetLoansByStatusHandler` — consultas de préstamos
- `GetMaintenanceHandler`, `GetMaintenanceByDateRangeHandler`, `GetMaintenanceByStatusHandler` — consultas de mantenimiento
- `GetVehicleByIdHandler`, `GetVehiclesHandler` — consultas de vehículos
- `GetClientsHandler`, `GetClientByIdHandler` — consultas de clientes
- `GetFeesHandler`, `GetFeeByIdHandler` — consultas de tarifas
- `GetWorkTypesHandler`, `GetWorkTypeByIdHandler` — consultas de tipos de trabajo
- `CatalogHandler` — consultas de catálogos por código padre o código específico

### Infraestructura

Contendrá:

- EF Core
- repositorios concretos
- `UnitOfWork`
- configuración de entidades
- migraciones
- carga manual de catálogos en la base de datos

Notas:

- los catálogos no tendrán endpoints CRUD en la API
- los datos de catálogos se insertarán manualmente en la base de datos

### API

Expone endpoints HTTP para:

- Crear préstamos (`POST /api/loans`)
- Registrar mantenimientos (`POST /api/maintenance`)
- Generar facturas (`POST /api/bills`)
- Consultar préstamos, mantenimientos, vehículos, clientes, tarifas, tipos de trabajo y catálogos

Todos los controladores delegan la lógica únicamente a Handlers de la capa de Application.
No existe inyección directa de repositorios en los controladores.

## Alcance del Negocio

- crear préstamo
- registrar mantenimiento normal
- registrar mantenimiento asociado
- generar factura como plus

El flujo de negocio detallado está centralizado en:

- `Documentación/Steps/CasosUso.txt`
