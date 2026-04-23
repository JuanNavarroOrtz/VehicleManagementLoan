import React, { useState, useEffect } from 'react';
import apiService from '../api/apiService';
import { MAINTENANCE_CONTEXT } from '../constants';
import MaintenanceForm from '../components/maintenance/MaintenanceForm';
import MaintenanceList from '../components/maintenance/MaintenanceList';

/**
 * Vista principal para el registro y gestión de mantenimientos.
 * Centraliza el estado de vehículos, tipos de trabajo y mantenimientos registrados.
 */
const Maintenance = () => {
  // --- ESTADOS ---
  const [vehicles, setVehicles] = useState([]);
  const [workTypes, setWorkTypes] = useState([]);
  const [contextTypes, setContextTypes] = useState([]);
  const [loans, setLoans] = useState([]); // Préstamos para el vehículo seleccionado
  const [maintenances, setMaintenances] = useState([]); // Lista de mantenimientos
  const today = new Date().toISOString().split('T')[0];
  const [dateFilter, setDateFilter] = useState({ from: today, to: today });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [loanType, setLoanType] = useState(''); // 'Asociado' o 'Normal'

  const [formData, setFormData] = useState({
    vehicleId: '',
    loanId: '',
    maintenanceContextTypeId: '',
    workTypeId: '',
    kilometers: '',
    cost: '',
    maintenanceDate: new Date().toISOString().split('T')[0],
    description: ''
  });

  // --- CARGA INICIAL ---
  useEffect(() => {
    const loadCatalogs = async () => {
      setLoading(true);
      try {
        const [v, wt, ct, mt] = await Promise.all([
          apiService.getVehicles(),
          apiService.getWorkTypes(),
          apiService.getMaintenanceContextTypes(),
          apiService.getMaintenances()
        ]);
        setVehicles(v);
        setWorkTypes(wt);
        setContextTypes(ct);
        setMaintenances(mt);
      } catch (err) {
        setError('Error al cargar catálogos de mantenimiento.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    loadCatalogs();
  }, []);

  // --- EFECTO: Cargar préstamos cuando cambia el vehículo y el contexto es "Asociado" ---
  useEffect(() => {
    const fetchLoansForVehicle = async () => {
      if (loanType === MAINTENANCE_CONTEXT.ASOCIADO) {
        try {
          // Buscamos todos los préstamos
          const allLoans = await apiService.getLoans();
          setLoans(allLoans);
        } catch (err) {
          console.error('Error al cargar préstamos:', err);
        }
      } else {
        setLoans([]);
      }
    };
    fetchLoansForVehicle();
  }, [loanType]);

  // --- MANEJADORES ---

  /** Maneja cambios en los campos del formulario */
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  /** Envía el registro de mantenimiento a la API */
  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);

    try {
      const payload = {
        ...formData,
        vehicleId: parseInt(formData.vehicleId),
        loanId: formData.loanId ? parseInt(formData.loanId) : null,
        maintenanceContextTypeId: parseInt(formData.maintenanceContextTypeId),
        workTypeId: parseInt(formData.workTypeId),
        kilometers: parseFloat(formData.kilometers),
        cost: parseFloat(formData.cost),
        maintenanceDate: new Date(formData.maintenanceDate).toISOString()
      };

      await apiService.registerMaintenance(payload);
      alert('✅ Mantenimiento registrado correctamente.');

      // Reset del formulario
      setFormData(prev => ({
        ...prev,
        vehicleId: '',
        loanId: '',
        workTypeId: '',
        kilometers: '',
        cost: '',
        description: ''
      }));

      // Recargar la lista de mantenimientos
      const updatedMaintenances = await apiService.getMaintenances();
      setMaintenances(updatedMaintenances);
      setDateFilter({ from: today, to: today });

    } catch (err) {
      setError('Error al registrar el mantenimiento.');
      console.error('Error al registrar mantenimiento:', err);
    }
  };

  /** Filtra la lista de mantenimientos por fecha */
  const handleFilterMaintenances = async () => {
    try {
      if (dateFilter.from && dateFilter.to) {
        const data = await apiService.getMaintenancesByDate(dateFilter.from, dateFilter.to);
        setMaintenances(data);
      } else {
        const data = await apiService.getMaintenances();
        setMaintenances(data);
      }
    } catch (err) {
      console.error('Error al filtrar mantenimientos:', err);
    }
  };

  // Pantalla de carga
  if (loading) return <div className="loading"><div className="spinner"></div> Cargando catálogos...</div>;

  return (
    <div className="card">
      {/* Componente del Formulario */}
      <MaintenanceForm
        vehicles={vehicles}
        workTypes={workTypes}
        contextTypes={contextTypes}
        loans={loans}
        formData={formData}
        loanType={loanType}
        setLoanType={setLoanType}
        handleChange={handleChange}
        handleSubmit={handleSubmit}
        error={error}
      />

      {/* Componente de la Lista */}
      <MaintenanceList
        maintenances={maintenances}
        dateFilter={dateFilter}
        setDateFilter={setDateFilter}
        handleFilterMaintenances={handleFilterMaintenances}
      />
    </div>
  );
};

export default Maintenance;
