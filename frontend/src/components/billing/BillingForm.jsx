import React from 'react';

/**
 * Componente que renderiza el formulario para la generación de facturas.
 * 
 * @param {Object} props - Propiedades del componente
 * @param {Array} props.loans - Lista de préstamos disponibles para facturar
 * @param {Object} props.formData - Datos actuales del formulario
 * @param {boolean} props.processing - Estado de procesamiento (cargando)
 * @param {Function} props.handleChange - Manejador de cambios en inputs
 * @param {Function} props.handleSubmit - Manejador de envío de formulario
 * @param {string} props.error - Mensaje de error
 */
const BillingForm = ({ loans, formData, processing, handleChange, handleSubmit, error }) => {
  return (
    <>
      <div className="card-title">
        <span className="icon">💰</span> Generación de Factura
      </div>

      {error && <div style={{ color: 'var(--danger)', marginBottom: '16px' }}>⚠️ {error}</div>}

      <form onSubmit={handleSubmit} className="product-form">
        <p style={{ fontSize: '14px', color: 'var(--text-secondary)', marginBottom: '20px' }}>
          Seleccione un préstamo activo o finalizado para generar la factura correspondiente, incluyendo días de préstamo y mantenimientos facturables asociados.
        </p>

        <div className="form-group" style={{ marginBottom: '16px' }}>
          <label>Préstamo a Facturar</label>
          <select 
            name="loanId" 
            value={formData.loanId} 
            onChange={handleChange} 
            required
            style={{ backgroundColor: 'var(--bg-input)', color: 'var(--text-primary)', border: '1.5px solid var(--border)', borderRadius: 'var(--radius-sm)', padding: '12px' }}
          >
            <option value="">Seleccione el préstamo</option>
            {loans.map(l => (
              <option key={l.id} value={l.id}>
                Préstamo #{l.id} - ID Vehículo: {l.vehicleId} - Depósito: ${l.deposit}
              </option>
            ))}
          </select>
        </div>

        <div className="form-row">
          <div className="form-group">
            <label>Fecha de Emisión</label>
            <input type="date" name="issueDate" value={formData.issueDate} onChange={handleChange} required />
          </div>
          
          {/* Campo informativo (Moneda) */}
          <div className="form-group" style={{ opacity: 0.5 }}>
            <label>Moneda</label>
            <input type="text" value="USD ($)" disabled />
          </div>
        </div>

        <div className="form-actions" style={{ marginTop: '20px' }}>
          <button type="submit" className="btn btn-primary" disabled={processing}>
            {processing ? '⚙️ Generando...' : '📜 Generar Factura Final'}
          </button>
        </div>
      </form>
      
      {/* Nota informativa sobre el cálculo de la factura */}
      <div style={{ marginTop: '32px', padding: '16px', border: '1px dashed var(--border)', borderRadius: 'var(--radius-md)', background: 'rgba(255,255,255,0.02)' }}>
         <h4 style={{ fontSize: '14px', marginBottom: '8px' }}>📌 Nota sobre el Flujo Plus</h4>
         <p style={{ fontSize: '13px', color: 'var(--text-muted)' }}>
           La factura se calcula automáticamente sumando el subtotal por días del préstamo y agregando cualquier registro de mantenimiento marcado como "Facturable" vinculado al préstamo seleccionado.
         </p>
      </div>
    </>
  );
};

export default BillingForm;
