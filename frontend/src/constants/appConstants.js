/**
 * Constantes generales de la aplicación para evitar "magic strings"
 */

// Estados de Préstamos
export const LOAN_STATUS = {
  ACTIVO: 'Activo',
  FINALIZADO: 'Finalizado',
  PENDIENTE: 'Pendiente',
};

// Estados de Mantenimiento
export const MAINTENANCE_STATUS = {
  COMPLETADO: 'Completado',
  PENDIENTE: 'Pendiente',
};

// Contextos de Mantenimiento
export const MAINTENANCE_CONTEXT = {
  NORMAL: 'Normal',
  ASOCIADO: 'Asociado',
};

// IDs de Catálogos (Basados en la base de datos)
export const CATALOG_IDS = {
  MAINTENANCE_CONTEXT_ASOCIADO: 1,
  MAINTENANCE_CONTEXT_NORMAL: 2,
};
