import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { iniciarSesion } from '../api/auth/login';
import { Link } from 'react-router-dom';

export default function LoginView() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [mostrarModalInicio, setMostrarModalInicio] = useState(false);
  const [mostrarModalInactivo, setMostrarModalInactivo] = useState(false);
  const [mostrarModalError, setMostrarModalError] = useState(false);
  const [mensajeError, setMensajeError] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const datos = await iniciarSesion(email, password);

     localStorage.setItem("user", JSON.stringify({
       id: datos.usuarioId,
       nombre: datos.NombreCompleto, 
       email: datos.email 
     }));

      setMostrarModalInicio(true);
    } catch (error) {
      const status = error?.response?.status;
      const mensaje = error?.response?.data?.mensaje || '❌ Error inesperado al iniciar sesión.';

      if (status === 403 && mensaje.toLowerCase().includes('inactiva')) {
        setMensajeError(mensaje);
        setMostrarModalInactivo(true);
      } else if (status === 401 && mensaje.toLowerCase().includes('credenciales')) {
        setMensajeError('Correo o contraseña incorrectos. Verificá tus datos.');
        setMostrarModalError(true);
      } else {
        setMensajeError('Ocurrió un error inesperado. Contactá con soporte si el problema persiste.');
        setMostrarModalError(true);
      }
    }
  };

  return (
    <>
      <section className="py-5" style={{ backgroundColor: '#ffffff' }}>
        <div className="container">
          <div className="auth-container mx-auto p-4 p-md-5 rounded shadow" style={{ maxWidth: '500px', backgroundColor: '#ffffff' }}>
            <h2 className="text-center mb-4 neon-text-b">Inicie sesión</h2>
            <form onSubmit={handleSubmit}>
              <div className="mb-3">
                <label className="form-label">Correo Electrónico</label>
                <input
                  type="email"
                  className="form-control neon-input-light"
                  required
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Contraseña</label>
                <div className="position-relative">
                  <input
                    type="password"
                    className="form-control pe-5"
                    required
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                  />
                  <button
                    type="button"
                    className="btn btn-sm btn-link position-absolute top-50 end-0 translate-middle-y me-2 p-0"
                    tabIndex="-1"
                  >
                    <i className="bi bi-eye-fill fs-5 text-primary"></i>
                  </button>
                </div>
              </div>

             <Link
                to="/olvide-password"
                className="btn btn-outline-secondary d-block mb-3"
                >
                ¿Olvidaste tu contraseña?
             </Link>

              <div className="d-grid mt-4">
                <button type="submit" className="btn neon-btn-black">
                  Iniciar Sesión
                </button>
              </div>
            </form>
          </div>
        </div>
      </section>

{mostrarModalInicio && (
  <div
    className="modal fade show d-block"
    tabIndex="-1"
    style={{
      backgroundColor: 'rgba(0, 0, 0, 0.5)',
      backdropFilter: 'blur(3px)',
      zIndex: 1055
    }}
  >
    <div className="modal-dialog modal-dialog-centered">
      <div className="modal-content border-0 shadow-lg rounded-4">
        <div
          className="modal-header text-white rounded-top-4"
          style={{ backgroundColor: '#3498db' }} 
        >
          <h5 className="modal-title"> ¡Inicio exitoso!</h5>
          <button
            type="button"
            className="btn-close btn-close-white"
            onClick={() => setMostrarModalInicio(false)}
          ></button>
        </div>
        <div className="modal-body text-center">
          <p className="fs-5 mb-3">Bienvenido/a</p>
          <p className="text-muted">¿Querés ver los vehículos disponibles y comenzar tu alquiler?</p>
        </div>
        <div className="modal-footer justify-content-center">
          <button
            className="btn px-4 rounded-pill me-2"
            style={{
              backgroundColor: '#00ffff',
              color: '#fff',
              border: 'none'
            }}
            onClick={() => {
              setMostrarModalInicio(false);
              navigate('/vehiculos');
            }}
          >
            Ver Flota 🚗
          </button>
          <button
            className="btn btn-outline-light text-dark border-secondary px-4 rounded-pill"
            onClick={() => {
              setMostrarModalInicio(false);
              navigate('/');
            }}
          >
            Ir al Inicio
          </button>
        </div>
      </div>
    </div>
  </div>
)}

      {mostrarModalInactivo && (
        <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content border border-danger">
              <div className="modal-header">
                <h5 className="modal-title text-danger">🚫 Cuenta Inactiva</h5>
                <button type="button" className="btn-close" onClick={() => setMostrarModalInactivo(false)}></button>
              </div>
              <div className="modal-body">
                <p>{mensajeError}</p>
              </div>
              <div className="modal-footer">
                <button className="btn btn-outline-secondary" onClick={() => setMostrarModalInactivo(false)}>
                  Cerrar
                </button>
              </div>
            </div>
          </div>
        </div>
      )}

      {mostrarModalError && (
        <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content border border-warning">
              <div className="modal-header">
                <h5 className="modal-title text-warning">⚠️ Error</h5>
                <button type="button" className="btn-close" onClick={() => setMostrarModalError(false)}></button>
              </div>
              <div className="modal-body">
                <p>{mensajeError}</p>
              </div>
              <div className="modal-footer">
                <button className="btn btn-outline-secondary" onClick={() => setMostrarModalError(false)}>
                  Cerrar
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
}