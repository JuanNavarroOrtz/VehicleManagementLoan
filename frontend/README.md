# Frontend - Gestión de Vehículos

Este proyecto es la interfaz de usuario para el sistema de Gestión de Préstamos y Mantenimiento de Vehículos. Está construido con **React** y **Vite**.

## Características Principales
- **Gestión de Préstamos**: Registro y consulta de préstamos de vehículos.
- **Mantenimiento**: Registro de mantenimientos normales y asociados a préstamos activos.

## Estructura del Proyecto
```
frontend/
├── src/
│   ├── api/          # Servicios para llamadas a la API de backend
│   ├── components/   # Componentes modulares (Formularios y Listas)
│   ├── constants/    # Centralización de colores y nombres de estados
│   ├── views/        # Vistas principales (Loans, Maintenance, Billing)
│   └── App.jsx       # Componente raíz y navegación por pestañas
└── index.html
```

## Requisitos
- [Node.js](https://nodejs.org/) (versión 18 o superior recomendada)
- [npm](https://www.npmjs.com/) (incluido con Node.js)

## Cómo Ejecutar Localmente

1. **Instalar dependencias**:
   Desde la carpeta `frontend`, ejecuta:
   ```bash
   npm install
   ```

2. **Configurar la API**:
   Asegúrate de que el backend esté corriendo. La URL base se configura en `src/api/index.js`.

3. **Iniciar el servidor de desarrollo**:
   ```bash
   npm run dev
   ```
   La aplicación estará disponible por defecto en `http://localhost:5173`.

## Scripts Disponibles
- `npm run dev`: Inicia el servidor de desarrollo.
- `npm run build`: Crea la versión de producción en la carpeta `dist`.
- `npm run preview`: Previsualiza la versión de producción localmente.
