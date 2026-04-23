import React from 'react';

/**
 * Componente que renderiza el formulario para la creación de un nuevo préstamo.
 * 
 * @param {Object} props - Propiedades del componente
 * @param {Array} props.vehicles - Lista de vehículos disponibles
 * @param {Array} props.clients - Lista de clientes registrados
 * @param {Array} props.fees - Lista de tarifas disponibles
 * @param {Object} props.formData - Estado actual de los datos del formulario
 * @param {Function} props.handleChange - Manejador para los cambios en los inputs
 * @param {Function} props.handleSubmit - Manejador para el envío del formulario
 * @param {string} props.error - Mensaje de error si existe
 */
const LoanForm = ({ vehicles, clients, fees, formData, handleChange, handleSubmit, error }) => {
  return (
    <>
      <div className="card-title">
        <span className="icon">📋</span> Nuevo Préstamo
      </div>

      {error && <div style={{ color: 'var(--danger)', marginBottom: '16px', fontSize: '14px' }}>⚠️ {error}</div>}

      <form onSubmit={handleSubmit} className="product-form">
        <div className="form-row">
          {/* Selección de Vehículo */}
          <div className="form-group">
            <label>Vehículo</label>
            <select
              name="vehicleId"
              value={formData.vehicleId}
              onChange={handleChange}
              className="form-group input"
              style={{ backgroundColor: 'var(--bg-input)', color: 'var(--text-primary)', border: '1.5px solid var(--border)', borderRadius: 'var(--radius-sm)', padding: '12px' }}
              required
            >
              <option value="">Seleccione un vehículo</option>
              {vehicles.map(v => (
                <option key={v.id} value={v.id}>
                  {v.model} ({v.plate})
                </option>
              ))}
            </select>
          </div>

          {/* Selección de Cliente */}
          <div className="form-group">
            <label>Cliente</label>
            <select
              name="clientId"
              value={formData.clientId}
              onChange={handleChange}
              style={{ backgroundColor: 'var(--bg-input)', color: 'var(--text-primary)', border: '1.5px solid var(--border)', borderRadius: 'var(--radius-sm)', padding: '12px' }}
              required
            >
              <option value="">Seleccione un cliente</option>
              {clients.map(c =>
                c.comercialName ?? c.businessName ?
                  <option key={c.id} value={c.id}>
                    {c.identificationNumber} - {c.comercialName ?? c.businessName}
                  </option>
                  :
                  <option key={c.id} value={c.id}>
                    {c.identificationNumber} - {c.firstName} {c.lastName}
                  </option>
              )}
            </select>
          </div>
        </div>

        <div className="form-row">
          {/* Fecha de Inicio */}
          <div className="form-group">
            <label>Fecha de Inicio</label>
            <input
              type="date"
              name="startDate"
              value={formData.startDate}
              onChange={handleChange}
              required
            />
          </div>

          {/* Depósito */}
          <div className="form-group">
            <label>Depósito Inicial ($)</label>
            <input
              type="number"
              name="deposit"
              placeholder="0.00"
              step="0.01"
              value={formData.deposit}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        <div className="form-row">
          {/* Tarifa / Fee */}
          <div className="form-group">
            <label>Tarifa (Fee)</label>
            <select
              name="feeId"
              value={formData.feeId}
              onChange={handleChange}
              style={{ backgroundColor: 'var(--bg-input)', color: 'var(--text-primary)', border: '1.5px solid var(--border)', borderRadius: 'var(--radius-sm)', padding: '12px' }}
              required
            >
              <option value="">Seleccione una tarifa</option>
              {fees.map(f => (
                <option key={f.id} value={f.id}>
                  {f.name} - ${f.amount}
                </option>
              ))}
            </select>
          </div>
        </div>

        <div className="form-actions">
          <button type="submit" className="btn btn-primary">
            🚀 Crear Préstamo
          </button>
        </div>
      </form>
    </>
  );
};

export default LoanForm;
