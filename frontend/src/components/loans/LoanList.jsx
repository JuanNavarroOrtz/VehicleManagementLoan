import React from 'react';
import { STATUS_COLORS, LOAN_STATUS } from '../../constants';

/**
 * Componente que renderiza la lista de préstamos con sus filtros de fecha.
 * 
 * @param {Object} props - Propiedades del componente
 * @param {Array} props.loans - Lista de préstamos a mostrar
 * @param {Object} props.dateFilter - Estado de los filtros por fecha (from, to)
 * @param {Function} props.setDateFilter - Función para actualizar el estado del filtro
 * @param {Function} props.handleFilterLoans - Función para aplicar el filtrado
 */
const LoanList = ({ loans, dateFilter, setDateFilter, handleFilterLoans }) => {
  return (
    <div style={{ marginTop: '40px' }}>
      <div className="product-list-header">
        <span style={{ fontSize: '16px', fontWeight: '600' }}>Lista General de Préstamos</span>
        <span className="product-count">{loans.length} préstamos</span>
      </div>

      {/* Sección de filtros por fecha */}
      <div className="form-row" style={{ marginBottom: '16px', alignItems: 'flex-end', background: 'var(--bg-secondary)', padding: '12px', borderRadius: 'var(--radius-sm)' }}>
        <div className="form-group" style={{ flex: 'none', width: '200px' }}>
          <label>Desde Fecha</label>
          <input type="date" value={dateFilter.from} onChange={e => setDateFilter(prev => ({ ...prev, from: e.target.value }))} />
        </div>
        <div className="form-group" style={{ flex: 'none', width: '200px' }}>
          <label>Hasta Fecha</label>
          <input type="date" value={dateFilter.to} onChange={e => setDateFilter(prev => ({ ...prev, to: e.target.value }))} />
        </div>
        <div className="form-group" style={{ flex: 'none' }}>
          <button type="button" onClick={handleFilterLoans} className="btn btn-secondary">
            🔍 Filtrar
          </button>
        </div>
      </div>

      {/* Tabla de préstamos */}
      <div style={{ overflowX: 'auto' }}>
        <table className="product-table">
          <thead>
            <tr>
              <th>Número de Préstamo</th>
              <th>Cliente</th>
              <th>Vehículo</th>
              <th>Estado del Préstamo</th>
              <th>Fecha de Inicio</th>
            </tr>
          </thead>
          <tbody>
            {loans.length === 0 ? (
              <tr>
                <td colSpan="5" style={{ textAlign: 'center', padding: '20px' }}>
                  <div className="empty-state">
                    <p>No hay préstamos registrados</p>
                  </div>
                </td>
              </tr>
            ) : (
              loans.map(loan => (
                <tr key={loan.id}>
                  <td className="product-name">{loan.consecutive || `LN-${loan.id}`}</td>
                  <td>{loan.clientFullName}</td>
                  <td>{loan.vehiclePlate}</td>
                  <td>
                    <span style={{
                      padding: '4px 8px',
                      borderRadius: '12px',
                      fontSize: '12px',
                      backgroundColor: loan.statusName === LOAN_STATUS.ACTIVO ? STATUS_COLORS.SUCCESS.BG : STATUS_COLORS.WARNING.BG,
                      color: loan.statusName === LOAN_STATUS.ACTIVO ? STATUS_COLORS.SUCCESS.TEXT : STATUS_COLORS.WARNING.TEXT
                    }}>
                      {loan.statusName}
                    </span>
                  </td>
                  <td>{new Date(loan.startDate).toLocaleDateString()}</td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default LoanList;
