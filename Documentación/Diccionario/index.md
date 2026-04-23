# Diccionario de Datos - VehicleManagementLoan

## AuditableEntity

Campos base compartidos por todas las entidades principales.

| Campo | Significado | Notas |
|---|---|---|
| `CreatedByUserId` | Usuario que creó el registro | Campo de auditoría |
| `CreatedOn` | Fecha y hora cuando se creó el registro | Campo de auditoría |
| `ModifiedByUserId` | Usuario que modificó por última vez el registro | Nulable porque un registro puede nunca editarse |
| `ModifiedOn` | Fecha y hora de la última modificación | Nulable porque un registro puede nunca editarse |

## User

Representa una cuenta de inicio de sesión del sistema.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único del usuario | Clave primaria |
| `Username` | Nombre de inicio de sesión usado para acceder al sistema | Debería ser único en la práctica |
| `Password` | Contraseña o valor de credencial almacenado | Más tarde esto debería manejarse de forma segura |
| `Email` | Dirección de correo electrónico del usuario | Opcional |
| `IsActive` | Indica si la cuenta aún puede usarse | Útil para habilitar/deshabilitar acceso |

## Employee

Representa un empleado interno relacionado con un usuario del sistema.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único del empleado | Clave primaria |
| `FirstName` | Nombre del empleado | Requerido |
| `SecondName` | Segundo nombre del empleado | Opcional |
| `LastName` | Apellido del empleado | Requerido |
| `SecondLastName` | Segundo apellido del empleado | Opcional |
| `Address` | Dirección del hogar o de contacto del empleado | Requerido |
| `CityId` | Ciudad donde pertenece o reside el empleado | Viene de `Catalog` |
| `IsActive` | Indica si el empleado está activo | Estado de negocio |
| `UserId` | Cuenta de inicio de sesión relacionada | Referencia `User.Id` |

## Catalog

Tabla genérica usada para datos de catálogo manual.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único de la fila del catálogo | Clave primaria |
| `Code` | Código interno para el valor del catálogo | Ejemplo: `ACTIVE`, `MANAGUA`, `SUV` |
| `Code` | Código numérico interno para el valor del catálogo | Ejemplo: `100` (Ciudades), `101` (Managua) |
| `Name` | Nombre amigable mostrado a los usuarios | Ejemplo: `Active`, `Managua`, `SUV` |
| `Description` | Explicación extra para el valor del catálogo | Opcional |
| `ParentCatalogId` | Fila padre cuando el catálogo es jerárquico | Opcional |
| `IsActive` | Indica si ese valor de catálogo aún puede usarse | Útil para desactivar valores antiguos |

## Client

Representa el cliente que recibe el préstamo del vehículo.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único del cliente | Clave primaria |
| `FirstName` | Nombre del cliente | Requerido |
| `SecondName` | Segundo nombre del cliente | Opcional |
| `LastName` | Apellido del cliente | Requerido |
| `SecondLastName` | Segundo apellido del cliente | Opcional |
| `Address` | Dirección del cliente | Requerido |
| `CityId` | Ciudad del cliente | Viene de `Catalog` |
| `BusinessName` | Nombre legal del negocio | Opcional, principalmente para clientes empresariales |
| `CommercialName` | Nombre comercial usado públicamente | Opcional |
| `Email` | Dirección de correo electrónico del cliente | Opcional |
| `IdentificationNumber` | Número de documento de identidad o fiscal | Importante para unicidad y trazabilidad legal |
| `Phone` | Número de teléfono del cliente | Requerido |
| `ClientTypeId` | Tipo de cliente | Viene de `Catalog`, por ejemplo natural o jurídico |
| `IsActive` | Indica si el cliente aún puede operar en el sistema | Estado de negocio |

## Vehicle

Representa un vehículo disponible para préstamos y mantenimiento.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único del vehículo | Clave primaria |
| `Plate` | Número de placa del vehículo | Usualmente único |
| `Vin` | VIN del vehículo | Usualmente único |
| `BrandId` | marca del vehículo | Viene de `Catalog`, por ejemplo Toyota, Honda |
| `Model` | Modelo del vehículo | Ejemplo: Corolla, CR-V |
| `VehicleTypeId` | Tipo de vehículo | Viene de `Catalog`, por ejemplo SUV, sedán, motocicleta |
| `Year` | Año de fabricación | Año numérico |
| `StatusId` | Estado actual del vehículo | Viene de `Catalog`, por ejemplo activo, mantenimiento, inactivo |

## Fee

Representa la configuración de precio usada al crear un préstamo.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único de la tarifa | Clave primaria |
| `Name` | Nombre de la tarifa | Ejemplo: tarifa diaria, tarifa semanal |
| `Amount` | Monto monetario de la tarifa | Usado en cálculos de negocio |
| `IsActive` | Indica si la tarifa aún puede usarse | Estado de negocio |
| `EffectiveFrom` | Fecha desde la cual la tarifa es válida | Usada para validar préstamos |
| `EffectiveTo` | Fecha hasta la cual la tarifa es válida | Opcional |

## WorkType

Tipo de trabajo realizado en un proceso de mantenimiento.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único del tipo de trabajo | Clave primaria |
| `Name` | Nombre del tipo de trabajo | Ejemplo: Mantenimiento, Reparación, Accidente |
| `IsBillable` | Indica si este trabajo es facturable | Usado al registrar mantenimiento |
| `IsActive` | Indica si el tipo de trabajo está disponible | Estado de negocio |

## Loan

Representa el acto de prestar un vehículo a un cliente.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único del préstamo | Clave primaria |
| `VehicleId` | Vehículo prestado | Referencia a `Vehicle.Id` |
| `ClientId` | Cliente receptor del préstamo | Referencia a `Client.Id` |
| `StartDate` | Fecha de inicio del préstamo | Requerido |
| `EndDate` | Fecha de fin del préstamo | Opcional hasta la devolución del vehículo |
| `Deposit` | Monto de depósito de seguridad | Valor monetario |
| `FeeId` | Tarifa aplicada al préstamo | Referencia a `Fee.Id` |
| `StatusId` | Estado actual del préstamo | Proviene de `Catalog` |

## MaintenanceRecord

Representa un evento de mantenimiento registrado para un vehículo.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único del registro de mantenimiento | Clave primaria |
| `VehicleId` | Vehículo que recibió el mantenimiento | Referencia a `Vehicle.Id` |
| `LoanId` | Préstamo relacionado cuando el mantenimiento es asociado | Opcional. Nulo significa mantenimiento normal |
| `MaintenanceContextTypeId` | Indica si el mantenimiento es normal o asociado | Proviene de `Catalog` |
| `WorkTypeId` | Tipo de trabajo realizado | Referencia a `WorkType.Id` |
| `Kilometers` | Kilometraje del vehículo al momento del mantenimiento | Ayuda a rastrear el desgaste y los tiempos de servicio |
| `Cost` | Costo monetario del mantenimiento | Usado para lógica de costos o facturación |
| `IsBillable` | Indica si este mantenimiento puede ser facturado | Proviene de `WorkType`, puede ajustarse por reglas de negocio |
| `MaintenanceDate` | Fecha en que ocurrió el mantenimiento | Requerido |
| `Description` | Descripción libre del mantenimiento realizado | Opcional |
| `StatusId` | Estado actual del mantenimiento | Proviene de `Catalog`, por ejemplo: pendiente, completado, cancelado |
| `Consecutive` | Código secuencial autogenerado del mantenimiento | Formato MT-XXXX, generado en la capa de Infraestructura |

## Bill

Representa un documento de facturación generado a partir de un préstamo con mantenimientos facturables.

| Campo | Significado | Notas |
|---|---|---|
| `Id` | Identificador único de la factura | Clave primaria |
| `LoanId` | Préstamo al que pertenece la factura | Referencia a `Loan.Id` |
| `BillDate` | Fecha en que se generó la factura | Requerido |
| `Subtotal` | Monto antes de impuestos | Valor monetario |
| `Tax` | Monto del impuesto aplicado | Valor monetario |
| `Total` | Monto final después de impuestos | Valor monetario |
| `BillStatusId` | Estado actual de la factura | Proviene de `Catalog`, por ejemplo: pendiente, pagada, anulada |

## Catálogos utilizados en el sistema

Los siguientes catálogos son requeridos por las entidades del dominio y se cargan mediante datos iniciales (Seed Data):

- Ciudades
- Tipos de cliente (natural, jurídico)
- Tipos de vehículo (SUV, sedán, motocicleta)
- Estados de vehículo (activo, en préstamo, en mantenimiento, inactivo)
- Estados de préstamo (activo, finalizado, anulado)
- Tipos de contexto de mantenimiento (normal, asociado a préstamo)
- Estados de mantenimiento (pendiente, completado, anulado)
- Estados de factura (pendiente, pagada, anulada)
- Marcas de vehículo (Toyota, Honda, etc.)

## Flujo de negocio

### Crear Préstamo (`CreateLoanHandler`)

1. Validar que el vehículo existe y está en estado activo
2. Validar que el cliente existe y está activo
3. Validar que la tarifa existe y es vigente a la fecha de inicio
4. Cambiar el estado del vehículo a "En Préstamo"
5. Crear el registro de préstamo con estado activo
6. Persistir todos los cambios de forma transaccional

### Registrar Mantenimiento (`CreateMaintenanceHandler`)

1. Validar que el vehículo existe
2. Si el mantenimiento está asociado a un préstamo, validar que el préstamo existe
3. Validar que el tipo de trabajo existe y está activo
4. Establecer `IsBillable` según el tipo de trabajo
5. Validar la consistencia entre el tipo de contexto y la presencia de un préstamo
6. Persistir el registro con estado pendiente y consecutivo MT-XXXX autogenerado

### Generar Factura (`GenerateBillHandler`)

1. Obtener el préstamo al que pertenece la factura
2. Obtener todos los mantenimientos facturables asociados a ese préstamo
3. Calcular subtotal sumando el costo de cada mantenimiento facturable
4. Aplicar el porcentaje de impuesto para obtener el total
5. Persistir la factura

### Consultas adicionales implementadas

- Obtener préstamos por rango de fechas (`GetLoansByDateRangeHandler`)
- Obtener préstamos por estado (`GetLoansByStatusHandler`)
- Obtener mantenimientos por rango de fechas (`GetMaintenanceByDateRangeHandler`)
- Obtener mantenimientos por estado (`GetMaintenanceByStatusHandler`)
- Obtener vehículo por ID con DTO enriquecido (`GetVehicleByIdHandler`)
- Obtener todos los vehículos (`GetVehiclesHandler`)
