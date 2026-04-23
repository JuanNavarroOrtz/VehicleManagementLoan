import React from 'react';
import { MAINTENANCE_CONTEXT } from '../../constants';

/**
 * Componente que renderiza el formulario para registrar un mantenimiento.
 * 
 * @param {Object} props - Propiedades del componente
 * @param {Array} props.vehicles - Lista de vehículos
 * @param {Array} props.workTypes - Tipos de trabajo de mantenimiento
 * @param {Array} props.contextTypes - Contextos (Normal/Asociado)
 * @param {Array} props.loans - Préstamos asociados (si aplica)
 * @param {Object} props.formData - Datos actuales del formulario
 * @param {string} props.loanType - Tipo de préstamo seleccionado (texto)
 * @param {Function} props.setLoanType - Función para actualizar el tipo de préstamo
 * @param {Function} props.handleChange - Manejador de cambios en inputs
 * @param {Function} props.handleSubmit - Manejador de envío de formulario
 * @param {string} props.error - Mensaje de error
 */
const MaintenanceForm = ({
  vehicles,
  workTypes,
  contextTypes,
  loans,
  formData,
  loanType,
  setLoanType,
  handleChange,
  handleSubmit,
  error
}) => {
  return (
    <>
      <div className="card-title">
        <span className="icon">🔧</span> Registro de Mantenimiento
      </div>

      {error && <div style={{ color: 'var(--danger)', marginBottom: '16px' }}>⚠️ {error}</div>}

      <form onSubmit={handleSubmit} className="product-form">
        <div className="form-row">
          {/* Contexto (Asociado o Normal) */}
          <div className="form-group">
            <label>Contexto de Mantenimiento</label>
            <select
              name="maintenanceContextTypeId"
              value={formData.maintenanceContextTypeId}
              onChange={e => {
                handleChange(e);
                setLoanType(e.target.options[e.target.selectedIndex].text);
              }}
              required
              style={{ backgroundColor: 'var(--bg-input)', color: 'var(--text-primary)', border: '1.5px solid var(--border)', borderRadius: 'var(--radius-sm)', padding: '12px' }}
            >
              <option value="">Seleccione contexto</option>
              {contextTypes.map(ct => (
                <option key={ct.id} value={ct.id}>{ct.name}</option>
              ))}
            </select>
          </div>
        </div>

        {/* Selección de Vehículo (Solo si es Normal) */}
        {loanType === MAINTENANCE_CONTEXT.NORMAL && (
          <div className="form-group">
            <label>Vehículo</label>
            <select
              name="vehicleId"
              value={formData.vehicleId}
              onChange={handleChange}
              required
              style={{ backgroundColor: 'var(--bg-input)', color: 'var(--text-primary)', border: '1.5px solid var(--border)', borderRadius: 'var(--radius-sm)', padding: '12px' }}
            >
              <option value="">Seleccione vehículo</option>
              {vehicles.map(v => (
                <option key={v.id} value={v.id}>{v.brand} {v.model} ({v.plate})</option>
              ))}
            </select>
          </div>
        )}

        {/* Selección de Préstamo (Solo si es Asociado) */}
        {loanType === MAINTENANCE_CONTEXT.ASOCIADO && (
          <div className="form-group" style={{ marginBottom: '16px' }}>
            <label>Préstamo Relacionado</label>
            <select
              name="loanId"
              value={formData.loanId}
              onChange={handleChange}
              required
              style={{ backgroundColor: 'var(--bg-input)', color: 'var(--text-primary)', border: '1.5px solid var(--border)', borderRadius: 'var(--radius-sm)', padding: '12px' }}
            >
              <option value="">Seleccione el préstamo</option>
              {loans.length === 0 ? (
                <option disabled>No hay préstamos para este vehículo</option>
              ) : (
                loans.map(l => (
                  <option key={l.id} value={l.id}>Préstamo #{l.id} - Inicio: {new Date(l.startDate).toLocaleDateString()}</option>
                ))
              )}
            </select>
          </div>
        )}

        <div className="form-row">
          {/* Tipo de Trabajo */}
          <div className="form-group">
            <label>Tipo de Trabajo</label>
            <select
              name="workTypeId"
              value={formData.workTypeId}
              onChange={handleChange}
              required
              style={{ backgroundColor: 'var(--bg-input)', color: 'var(--text-primary)', border: '1.5px solid var(--border)', borderRadius: 'var(--radius-sm)', padding: '12px' }}
            >
              <option value="">Seleccione trabajo</option>
              {workTypes.map(wt => (
                <option key={wt.id} value={wt.id}>{wt.description} {wt.isBillable ? '(Facturable)' : '(No facturable)'}</option>
              ))}
            </select>
          </div>

          {/* Fecha */}
          <div className="form-group">
            <label>Fecha de Mantenimiento</label>
            <input type="date" name="maintenanceDate" value={formData.maintenanceDate} onChange={handleChange} required />
          </div>
        </div>

        <div className="form-row">
          {/* Kilómetros */}
          <div className="form-group">
            <label>Kilómetros</label>
            <input type="number" name="kilometers" placeholder="0" value={formData.kilometers} onChange={handleChange} required />
          </div>

          {/* Costo */}
          <div className="form-group">
            <label>Costo ($)</label>
            <input type="number" name="cost" step="0.01" placeholder="0.00" value={formData.cost} onChange={handleChange} required />
          </div>
        </div>

        <div className="form-group">
          <label>Descripción / Observaciones</label>
          <textarea name="description" placeholder="Detalles del mantenimiento..." value={formData.description} onChange={handleChange}></textarea>
        </div>

        <div className="form-actions">
          <button type="submit" className="btn btn-primary">💾 Registrar Mantenimiento</button>
        </div>
      </form>
    </>
  );
};

export default MaintenanceForm;
