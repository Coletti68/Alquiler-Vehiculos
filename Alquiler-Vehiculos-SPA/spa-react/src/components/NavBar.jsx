import { NavLink, useNavigate } from 'react-router-dom';
import { isAuthenticated, cerrarSesion } from '../utils/authService';

export default function NavBar() {
  const navigate = useNavigate();
  const user = JSON.parse(localStorage.getItem('user'));

  return (
    <nav className="navbar navbar-expand-lg navbar-dark py-3 shadow-sm neon-navbar fixed-top">
      <div className="container">
        <NavLink className="navbar-brand fw-bold fs-4 neon-text" to="/">RentCars</NavLink>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav ms-auto align-items-center gap-2">
            <li className="nav-item">
              <NavLink className="nav-link fw-semibold neon-link" to="/">Inicio</NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link fw-semibold neon-link" to="/vehiculos">Flota</NavLink>
            </li>

            {isAuthenticated() && (
              <>
                <li className="nav-item">
                  <NavLink className="nav-link fw-semibold neon-link" to="/contacto">Contacto</NavLink>
                </li>
                <li className="nav-item d-flex align-items-center text-white">
                  <NavLink className="nav-link fw-semibold neon-link" to="/perfil">👤 Mi Perfil</NavLink>
                </li>
              </>
            )}

            {!isAuthenticated() && (
              <>
                <li className="nav-item">
                  <NavLink className="nav-link fw-semibold neon-link" to="/login">Iniciar Sesión</NavLink>
                </li>
                <li className="nav-item">
                  <NavLink className="nav-link fw-semibold neon-link" to="/registro">Registrarse</NavLink>
                </li>
              </>
            )}

            {isAuthenticated() && (
              <li className="nav-item">
                <button
                  className="btn neon-btn-rojo ms-2"
                  onClick={() => {
                    cerrarSesion();
                    navigate('/');
                  }}
                >
                  Cerrar Sesión
                </button>
              </li>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
}