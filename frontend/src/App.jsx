import React, { useState } from 'react'
import './App.css'
import Loans from './views/Loans'
import Maintenance from './views/Maintenance'
import Billing from './views/Billing'

/**
 * Componente principal de la aplicación.
 * Gestiona la navegación simple entre las diferentes vistas de negocio
 * utilizando un estado local (activeTab).
 */
function App() {
  // Estado para controlar qué pestaña/vista está activa
  const [activeTab, setActiveTab] = useState('loans');

  return (
    <div className="app-container">
      {/* Cabecera principal */}
      <header className="app-header">
        <h1>🚗 Gestión de Vehículos </h1>
        <p>Sistema de Gestión de Préstamos y Mantenimiento</p>
      </header>

      {/* Menú de navegación simple (Tabs) */}
      <nav className="app-nav">
        <button
          className={`nav-btn ${activeTab === 'loans' ? 'active' : ''}`}
          onClick={() => setActiveTab('loans')}
        >
          📋 Préstamos
        </button>
        <button
          className={`nav-btn ${activeTab === 'maintenance' ? 'active' : ''}`}
          onClick={() => setActiveTab('maintenance')}
        >
          🔧 Mantenimiento
        </button>
        <button
          className={`nav-btn ${activeTab === 'billing' ? 'active' : ''}`}
          onClick={() => setActiveTab('billing')}
        >
          💰 Facturación
        </button>
      </nav>

      {/* Contenido dinámico según la pestaña seleccionada */}
      <main className="app-content">
        {activeTab === 'loans' && <Loans />}
        {activeTab === 'maintenance' && <Maintenance />}
        {activeTab === 'billing' && <Billing />}
      </main>

      {/* Pie de página simple */}
      <footer style={{ marginTop: '40px', textAlign: 'center', opacity: 0.5, fontSize: '12px' }}>
        &copy; 2026 Vehículos Propietarios
      </footer>
    </div>
  )
}

export default App
