// API_BASE_URL configurado previamente en index.js
import { API_BASE_URL } from './index';

/**
 * Servicio centralizado para manejar las llamadas a la API de backend.
 * Utiliza fetch nativo para mantener la ligereza del proyecto.
 */
const apiService = {
  // --- VEHÍCULOS ---
  /** Obtiene la lista de todos los vehículos */
  getVehicles: async () => {
    const response = await fetch(`${API_BASE_URL}/vehicles`);
    if (!response.ok) throw new Error('Error al obtener vehículos');
    return await response.json();
  },

  // --- CLIENTES ---
  /** Obtiene la lista de todos los clientes */
  getClients: async () => {
    const response = await fetch(`${API_BASE_URL}/clients`);
    if (!response.ok) throw new Error('Error al obtener clientes');
    return await response.json();
  },

  // --- TARIFAS ---
  /** Obtiene la lista de todas las tarifas */
  getFees: async () => {
    const response = await fetch(`${API_BASE_URL}/fees`);
    if (!response.ok) throw new Error('Error al obtener tarifas');
    return await response.json();
  },

  // --- PRÉSTAMOS ---
  /** Obtiene la lista de préstamos */
  getLoans: async () => {
    const response = await fetch(`${API_BASE_URL}/loans`);
    if (!response.ok) throw new Error('Error al obtener préstamos');
    return await response.json();
  },

  /** Obtiene la lista de préstamos por rango de fechas */
  getLoansByDate: async (from, to) => {
    const response = await fetch(`${API_BASE_URL}/loans/byDate?from=${from}&to=${to}`);
    if (!response.ok) throw new Error('Error al obtener préstamos por fecha');
    return await response.json();
  },

  /** Crea un nuevo préstamo */
  createLoan: async (loanData) => {
    const response = await fetch(`${API_BASE_URL}/loans`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(loanData)
    });
    if (!response.ok) throw new Error('Error al crear el préstamo');
    return await response.json();
  },

  // --- MANTENIMIENTO ---
  /** Obtiene la lista de mantenimientos */
  getMaintenances: async () => {
    const response = await fetch(`${API_BASE_URL}/maintenance`);
    if (!response.ok) throw new Error('Error al obtener mantenimientos');
    return await response.json();
  },

  /** Obtiene la lista de mantenimientos por rango de fechas */
  getMaintenancesByDate: async (from, to) => {
    const response = await fetch(`${API_BASE_URL}/maintenance/byDate?from=${from}&to=${to}`);
    if (!response.ok) throw new Error('Error al obtener mantenimientos por fecha');
    return await response.json();
  },

  /** Registra un nuevo registro de mantenimiento */
  registerMaintenance: async (maintenanceData) => {
    const response = await fetch(`${API_BASE_URL}/maintenance`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(maintenanceData)
    });
    if (!response.ok) throw new Error('Error al registrar mantenimiento');
    return await response.json();
  },

  // --- FACTURACIÓN ---
  /** Genera una factura para un préstamo */
  generateBill: async (billData) => {
    const response = await fetch(`${API_BASE_URL}/bills`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(billData)
    });
    if (!response.ok) throw new Error('Error al generar la factura');
    return await response.json();
  },

  // --- CATÁLOGOS ---
  /** Obtiene tipos de trabajo de mantenimiento */
  getWorkTypes: async () => {
    const response = await fetch(`${API_BASE_URL}/worktypes`);
    if (!response.ok) throw new Error('Error al obtener tipos de trabajo');
    return await response.json();
  },

  /** Obtiene estados de préstamo desde el catálogo */
  getLoanStatuses: async () => {
    const response = await fetch(`${API_BASE_URL}/catalogs/loanStatuses`);
    if (!response.ok) throw new Error('Error al obtener estados de préstamo');
    return await response.json();
  },

  /** Obtiene estados de vehículos desde el catálogo */
  getVehicleStatuses: async () => {
    const response = await fetch(`${API_BASE_URL}/catalogs/vehicleStatuses`);
    if (!response.ok) throw new Error('Error al obtener estados de vehículo');
    return await response.json();
  },

  /** Obtiene tipos de contexto de mantenimiento (Normal/Asociado) */
  getMaintenanceContextTypes: async () => {
    const response = await fetch(`${API_BASE_URL}/catalogs/maintenanceContextTypes`);
    if (!response.ok) throw new Error('Error al obtener contextos de mantenimiento');
    return await response.json();
  }
};

export default apiService;
