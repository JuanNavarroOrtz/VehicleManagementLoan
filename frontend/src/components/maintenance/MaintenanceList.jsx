import React from 'react';
import { STATUS_COLORS, MAINTENANCE_STATUS } from '../../constants';

/**
 * Componente que renderiza la lista de mantenimientos registrados con filtros.
 * 
 * @param {Object} props - Propiedades del componente
 * @param {Array} props.maintenances - Lista de mantenimientos
 * @param {Object} props.dateFilter - Estado de filtros por fecha
 * @param {Function} props.setDateFilter - Función para actualizar filtros
 * @param {Function} props.handleFilterMaintenances - Función para aplicar filtros
 */
const MaintenanceList = ({ maintenances, dateFilter, setDateFilter, handleFilterMaintenances }) => {
  return (
    <div style={{ marginTop: '40px' }}>
      <div className="product-list-header">
        <span style={{ fontSize: '16px', fontWeight: '600' }}>Lista General de Mantenimientos</span>
        <span className="product-count">{maintenances.length} registros</span>
      </div>

      {/* Filtro por fecha */}
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
          <button type="button" onClick={handleFilterMaintenances} className="btn btn-secondary">
            🔍 Filtrar
          </button>
        </div>
      </div>

      <div style={{ overflowX: 'auto' }}>
        <table className="product-table">
          <thead>
            <tr>
              <th>Número de Mantenimiento</th>
              <th>Estado</th>
              <th>Vehículo</th>
              <th>Tipo de Préstamo (Contexto)</th>
            </tr>
          </thead>
          <tbody>
            {maintenances.length === 0 ? (
              <tr>
                <td colSpan="4" style={{ textAlign: 'center', padding: '20px' }}>
                  <div className="empty-state">
                    <p>No hay mantenimientos registrados</p>
                  </div>
                </td>
              </tr>
            ) : (
              maintenances.map(m => (
                <tr key={m.id}>
                  <td className="product-name">{m.consecutive || `MT-${m.id}`}</td>
                  <td>
                    <span style={{
                      padding: '4px 8px',
                      borderRadius: '12px',
                      fontSize: '12px',
                      backgroundColor: m.statusName === MAINTENANCE_STATUS.COMPLETADO ? STATUS_COLORS.SUCCESS.BG : STATUS_COLORS.WARNING.BG,
                      color: m.statusName === MAINTENANCE_STATUS.COMPLETADO ? STATUS_COLORS.SUCCESS.TEXT : STATUS_COLORS.WARNING.TEXT
                    }}>
                      {m.statusName}
                    </span>
                  </td>
                  <td>{m.vehiclePlate}</td>
                  <td>
                    <span style={{
                      padding: '4px 8px',
                      borderRadius: '12px',
                      fontSize: '12px',
                      backgroundColor: 'rgba(108, 92, 231, 0.1)',
                      color: 'var(--accent)',
                      border: '1px solid rgba(108, 92, 231, 0.2)'
                    }}>
                      {m.maintenanceContextName}
                    </span>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default MaintenanceList;
