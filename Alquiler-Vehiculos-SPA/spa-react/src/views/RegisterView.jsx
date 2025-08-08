import { useState } from 'react';
import { registrarUsuario } from '../api/auth/register';
import { useNavigate } from 'react-router-dom';
import { verificarDniExistente } from '../api/auth/verificarDNIexist';
import {verificarEmailExistente} from '../api/auth/verificarEMAILexist';

export default function RegisterView() {
  const [pais, setPais] = useState('');
  const [nombre, setNombre] = useState('');
  const [password, setPassword] = useState('');
  const [dni, setDni] = useState('');
  const [fechaNacimiento, setFechaNacimiento] = useState('');
  const [telefono, setTelefono] = useState('');
  const [email, setEmail] = useState('');
  const [direccion, setDireccion] = useState('');
  const [mostrarModalRegistro, setMostrarModalRegistro] = useState(false);
  const [mostrarModalError, setMostrarModalError] = useState(false);
  const [mensajeError, setMensajeError] = useState('');
  const navigate = useNavigate();

  const reglasDni = {
    Argentina: 8,
    Brasil: 11,
    Paraguay: 6,
    Chile: 9,
  };

  const calcularEdad = (fecha) => {
    const hoy = new Date();
    const nacimiento = new Date(fecha);
    let edad = hoy.getFullYear() - nacimiento.getFullYear();
    const mes = hoy.getMonth() - nacimiento.getMonth();
    if (mes < 0 || (mes === 0 && hoy.getDate() < nacimiento.getDate())) edad--;
    return edad;
  };

  const handleRegister = async (e) => {
    e.preventDefault();

    if (reglasDni[pais]) {
      const longitudEsperada = reglasDni[pais];
      if (!dni || dni.length !== longitudEsperada) {
        setMensajeError(`El DNI debe tener ${longitudEsperada} dígitos para ${pais}`);
        setMostrarModalError(true);
        return;
      }
    }

    if (!fechaNacimiento || calcularEdad(fechaNacimiento) < 18) {
      setMensajeError('Debés tener al menos 18 años para registrarte.');
      setMostrarModalError(true);
      return;
    }

    const existeDNI = await verificarDniExistente(dni);
    if (existeDNI) {
      setMensajeError('Ya existe un usuario registrado con ese DNI.');
      setMostrarModalError(true);
      return;
    }

    const existeEmail = await verificarEmailExistente(email);
    if (existeEmail) {
      setMensajeError('Ya existe un usuario registrado con ese Email.');
      setMostrarModalError(true);
      return;
    }

    const usuario = {
      pais,
      nombre,
      password,
      dni,
      fechaNacimiento,
      telefono,
      email,
      direccion,
    };

    try {
      await registrarUsuario(usuario);
      setMostrarModalRegistro(true);
    } catch (error) {
      const mensaje = error?.response?.data?.mensaje || 'Ocurrió un error al registrar la cuenta.';
      setMensajeError(mensaje);
      setMostrarModalError(true);
    }
  };

  return (
    <>
      <section className="py-5" style={{ background: 'linear-gradient(135deg, #ffffff, #ffffff)' }}>
        <div className="container">
          <div
            className="auth-container mx-auto p-4 p-md-5 rounded shadow-lg text-white neon-glow"
            style={{ maxWidth: '600px' }}
          >
            <h2 className="text-center mb-4 neon-text-b">Crear Cuenta</h2>
            <form onSubmit={handleRegister}>
              <div className="mb-3">
                <label className="form-label">País</label>
                <select className="form-select neon-input" value={pais} onChange={(e) => setPais(e.target.value)}>
                  <option value="">Seleccione su país</option>
                  <option value="Argentina">Argentina</option>
                  <option value="Brasil">Brasil</option>
                  <option value="Paraguay">Paraguay</option>
                  <option value="Chile">Chile</option>
                </select>
              </div>

              <div className="mb-3">
                <label className="form-label">Nombre completo *</label>
                <input
                  type="text"
                  className="form-control neon-input"
                  required
                  value={nombre}
                  onChange={(e) => setNombre(e.target.value)}
                />
              </div>

              <div className="mb-3">
                <label className="form-label">Contraseña *</label>
                <input
                  type="password"
                  className="form-control neon-input"
                  placeholder="Mínimo 6 caracteres"
                  required
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>

              <div className="mb-3">
                <label className="form-label">DNI</label>
                <input
                  type="text"
                  className="form-control neon-input"
                  placeholder="Ingrese su documento"
                  maxLength={pais && reglasDni[pais] ? reglasDni[pais] : 20}
                  value={dni}
                  onChange={(e) => {
                    const soloNumeros = e.target.value.replace(/\D/g, '');
                    setDni(soloNumeros);
                  }}
                />
                {reglasDni[pais] && (
                  <small className="form-text text-secondary">
                    El DNI debe tener {reglasDni[pais]} dígitos para {pais}.
                  </small>
                )}
              </div>

              <div className="mb-3">
                <label className="form-label">Fecha de nacimiento</label>
                <input
                  type="date"
                  className="form-control neon-input"
                  value={fechaNacimiento}
                  onChange={(e) => setFechaNacimiento(e.target.value)}
                />
              </div>

              <div className="mb-3">
                <label className="form-label">Teléfono</label>
                <input
                  type="tel"
                  className="form-control neon-input"
                  value={telefono}
                  onChange={(e) => setTelefono(e.target.value)}
                />
              </div>

              <div className="mb-3">
                <label className="form-label">Correo electrónico *</label>
                <input
                  type="email"
                  className="form-control neon-input"
                  required
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </div>

              <div className="mb-3">
                <label className="form-label">Dirección</label>
                <input
                  type="text"
                  className="form-control neon-input"
                  value={direccion}
                  onChange={(e) => setDireccion(e.target.value)}
                />
              </div>

              <div className="d-grid mt-4">
                <button type="submit" className="btn neon-btn-black">
                  Registrarse
                </button>
              </div>
            </form>
          </div>
        </div>
      </section>

      {/* 🟢 Modal de confirmación post-registro */}
      {mostrarModalRegistro && (
        <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content border border-success">
              <div className="modal-header">
                <h5 className="modal-title text-success">✅ Registro Exitoso</h5>
                <button className="btn-close" onClick={() => setMostrarModalRegistro(false)}></button>
              </div>
              <div className="modal-body">
                <p>¡Tu cuenta fue creada correctamente! ¿Querés iniciar sesión ahora?</p>
              </div>
              <div className="modal-footer">
                <button className="btn btn-outline-success" onClick={() => navigate('/login')}>
                  Iniciar Sesión
                </button>
                <button className="btn btn-outline-secondary" onClick={() => navigate('/')}>
                  Volver al Inicio
                </button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* ⚠️ Modal de error visual */}
      {mostrarModalError && (
        <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content border border-warning">
              <div className="modal-header">
                <h5 className="modal-title text-warning">⚠️ Registro fallido</h5>
                <button className="btn-close" onClick={() => setMostrarModalError(false)}></button>
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