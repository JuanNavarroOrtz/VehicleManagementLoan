import React, { useState, useEffect } from 'react';
import apiService from '../api/apiService';
import LoanForm from '../components/loans/LoanForm';
import LoanList from '../components/loans/LoanList';

/**
 * Vista principal para gestionar los préstamos de vehículos.
 * Actúa como contenedor de estado y lógica para los componentes LoanForm y LoanList.
 */
const Loans = () => {
  // --- ESTADOS DE DATOS (Catálogos) ---
  const [vehicles, setVehicles] = useState([]);
  const [clients, setClients] = useState([]);
  const [fees, setFees] = useState([]);
  const [loans, setLoans] = useState([]);
  const today = new Date().toISOString().split('T')[0];
  const [dateFilter, setDateFilter] = useState({ from: today, to: today });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // --- ESTADO DEL FORMULARIO ---
  const [formData, setFormData] = useState({
    vehicleId: '',
    clientId: '',
    startDate: new Date().toISOString().split('T')[0],
    deposit: '',
    feeId: ''
  });

  // --- CARGA INICIAL DE DATOS ---
  useEffect(() => {
    const loadData = async () => {
      setLoading(true);
      try {
        // Ejecutamos las peticiones en paralelo para mayor eficiencia
        const [vehiclesData, clientsData, feesData, loansData] = await Promise.all([
          apiService.getVehicles(),
          apiService.getClients(),
          apiService.getFees(),
          apiService.getLoans()
        ]);

        setVehicles(vehiclesData);
        setClients(clientsData);
        setFees(feesData);
        setLoans(loansData);
      } catch (err) {
        console.error('Error al cargar datos:', err);
        setError('No se pudieron cargar los catálogos. Verifique que la API esté corriendo.');
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  // --- MANEJADORES DE EVENTOS ---

  /** Maneja los cambios en los campos del formulario */
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  /** Maneja el envío del formulario para crear un préstamo */
  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);

    // Validación simple
    if (!formData.vehicleId || !formData.clientId || !formData.feeId || !formData.deposit) {
      setError('Por favor, complete todos los campos obligatorios.');
      return;
    }

    try {
      const payload = {
        ...formData,
        vehicleId: parseInt(formData.vehicleId),
        clientId: parseInt(formData.clientId),
        feeId: parseInt(formData.feeId),
        deposit: parseFloat(formData.deposit),
        startDate: new Date(formData.startDate).toISOString()
      };

      await apiService.createLoan(payload);
      alert('✅ Préstamo creado exitosamente.');

      // Limpiar formulario excepto fechas/estados
      setFormData(prev => ({
        ...prev,
        vehicleId: '',
        clientId: '',
        deposit: '',
        feeId: ''
      }));

      // Recargar vehículos y préstamos para reflejar el cambio de estado
      const [updatedVehicles, updatedLoans] = await Promise.all([
        apiService.getVehicles(),
        apiService.getLoans()
      ]);
      setVehicles(updatedVehicles);
      setLoans(updatedLoans);
      setDateFilter({ from: today, to: today });

    } catch (err) {
      console.error('Error al crear préstamo:', err);
      setError('Error al procesar el préstamo. Verifique los datos o la disponibilidad del vehículo.');
    }
  };

  /** Maneja el filtrado de préstamos por rango de fechas */
  const handleFilterLoans = async () => {
    try {
      if (dateFilter.from && dateFilter.to) {
        const data = await apiService.getLoansByDate(dateFilter.from, dateFilter.to);
        setLoans(data);
      } else {
        const data = await apiService.getLoans();
        setLoans(data);
      }
    } catch (err) {
      console.error('Error al filtrar préstamos:', err);
    }
  };

  // Pantalla de carga
  if (loading) return <div className="loading"><div className="spinner"></div> Cargando datos...</div>;

  return (
    <div className="card">
      {/* Componente del Formulario */}
      <LoanForm
        vehicles={vehicles}
        clients={clients}
        fees={fees}
        formData={formData}
        handleChange={handleChange}
        handleSubmit={handleSubmit}
        error={error}
      />

      {/* Componente de la Lista */}
      <LoanList
        loans={loans}
        dateFilter={dateFilter}
        setDateFilter={setDateFilter}
        handleFilterLoans={handleFilterLoans}
      />
    </div>
  );
};

export default Loans;
