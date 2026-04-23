CREAR PRÉSTAMO
- Validar que el vehículo existe
- Validar que el vehículo está disponible
- Validar que el cliente existe y está activo
- Validar que la tarifa es válida
- Crear préstamo
- Cambiar estado del vehículo a Prestado

REGISTRAR MANTENIMIENTO
- Validar vehículo
- Determinar si asociado o normal
- Obtener WorkType
- Establecer IsBillable desde WorkType
- Guardar mantenimiento

GENERAR FACTURA
- Flujo plus
- Obtener préstamo
- Obtener registros de mantenimiento asociados relacionados con el préstamo según reglas de facturación
- Calcular subtotal
- Aplicar impuesto
- Guardar Factura

