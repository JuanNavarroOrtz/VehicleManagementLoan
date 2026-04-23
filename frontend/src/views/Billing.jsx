import React, { useState, useEffect } from 'react';
import apiService from '../api/apiService';
import BillingForm from '../components/billing/BillingForm';

/**
 * Vista principal para la generación de facturas.
 * Gestiona el estado de los préstamos facturables y el proceso de generación.
 */
const Billing = () => {
  // --- ESTADOS ---
  const [loans, setLoans] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [processing, setProcessing] = useState(false);

  const [formData, setFormData] = useState({
    loanId: '',
    issueDate: new Date().toISOString().split('T')[0],
    billingStatusId: 1 // ID inicial (ej. Pagada o Emitida)
  });

  // --- CARGA INICIAL ---
  useEffect(() => {
    const loadLoans = async () => {
      setLoading(true);
      try {
        const data = await apiService.getLoans();
        setLoans(data);
      } catch (err) {
        setError('Error al cargar la lista de préstamos.');
      } finally {
        setLoading(false);
      }
    };
    loadLoans();
  }, []);

  // --- MANEJADORES ---
  
  /** Maneja los cambios en los campos del formulario */
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  /** Envía la solicitud de generación de factura a la API */
  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);
    setProcessing(true);

    if (!formData.loanId) {
      setError('Debe seleccionar un préstamo para generar la factura.');
      setProcessing(false);
      return;
    }

    try {
      const payload = {
        ...formData,
        loanId: parseInt(formData.loanId),
        issueDate: new Date(formData.issueDate).toISOString()
      };

      const bill = await apiService.generateBill(payload);
      
      alert(`✅ Factura generada con éxito!\nID de Factura: ${bill.id}\nSubtotal: $${bill.subtotal}\nImpuesto: $${bill.tax}\nTotal: $${bill.total}`);
      
      // Limpiar selección después de éxito
      setFormData(prev => ({ ...prev, loanId: '' }));
    } catch (err) {
      console.error('Error al generar factura:', err);
      setError('Hubo un error al generar la factura. Asegúrese de que el préstamo no tenga facturas previas si el sistema lo prohíbe.');
    } finally {
      setProcessing(false);
    }
  };

  // Pantalla de carga
  if (loading) return <div className="loading"><div className="spinner"></div> Cargando préstamos...</div>;

  return (
    <div className="card">
      {/* Componente del Formulario de Facturación */}
      <BillingForm 
        loans={loans}
        formData={formData}
        processing={processing}
        handleChange={handleChange}
        handleSubmit={handleSubmit}
        error={error}
      />
    </div>
  );
};

export default Billing;
