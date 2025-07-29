import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import VehicleCard from '../components/VehicleCard';
import ReservaModal from '../components/ReservaModal';
import PagoModal from '../components/PagoModal';
import { crearAlquiler } from '../api/services/alquiler';
import { crearPago } from '../api/services/pago';
import { isAuthenticated } from '../utils/authService';

export default function VehicleCatalog() {
  const [vehiculos, setVehiculos] = useState([]);
  const [vehiculoSeleccionado, setVehiculoSeleccionado] = useState(null);
  const [mostrarModalAviso, setMostrarModalAviso] = useState(false);
  const [mostrarReserva, setMostrarReserva] = useState(false);
  const [mostrarPago, setMostrarPago] = useState(false);
  const [reservaPendiente, setReservaPendiente] = useState(null);
  const [mostrarModalExito, setMostrarModalExito] = useState(false);
  const [mensajeModalError, setMensajeModalError] = useState(null);
  const [alquileresUsuario, setAlquileresUsuario] = useState([]);
  const [montoTotal, setMontoTotal] = useState(null);
  const navigate = useNavigate();

  const usuarioActual = JSON.parse(localStorage.getItem("user"));
  const usuarioId = usuarioActual?.id;

  useEffect(() => {
    fetch('/api/vehiculos')
      .then(res => res.json())
      .then(setVehiculos);

    if (usuarioId) {
      fetch(`/api/alquileres/usuario/${usuarioId}`)
        .then(res => res.json())
        .then(setAlquileresUsuario)
        .catch(err => console.error("Error cargando alquileres del usuario:", err));
    }
  }, [usuarioId]);

  const cerrarModal = () => setVehiculoSeleccionado(null);

  const handleAlquilar = (vehiculo) => {
    if (!isAuthenticated()) {
      setMostrarModalAviso(true);
      return;
    }

    const tieneActivo = alquileresUsuario.some(a =>
      a.estado === "Activo" || a.estado === "Reservado"
    );

    if (tieneActivo) {
      setMensajeModalError("Ya tenés un alquiler activo o reservado. Cancelalo o esperá a que finalice antes de hacer uno nuevo.");
      return;
    }

    setVehiculoSeleccionado(vehiculo);
    setMostrarReserva(true);
  };

   const handleReservaConfirmada = ({ fechaInicio, fechaFin }) => {
    setReservaPendiente({ fechaInicio, fechaFin });

    const dias = Math.ceil(
     (new Date(fechaFin) - new Date(fechaInicio)) / (1000 * 60 * 60 * 24)
  );

    const precio = vehiculoSeleccionado?.precioPorDia || vehiculoSeleccionado?.precio || 0;
    const total = dias * precio;

    setMontoTotal(total);
    setMostrarReserva(false);
    setMostrarPago(true);
  };

  const handlePagoConfirmado = async (metodo) => {
    try {
      if (!usuarioId) {
        setMensajeModalError("No se pudo obtener el ID del usuario. Iniciá sesión nuevamente.");
        navigate("/login");
        return;
      }

      if (!vehiculoSeleccionado || !reservaPendiente) {
        setMensajeModalError("Faltan datos para procesar el alquiler. Intentá nuevamente.");
        return;
      }

      const vehiculo = vehiculoSeleccionado;
      const { fechaInicio, fechaFin, monto: montoPrecalculado } = reservaPendiente;

      const dias = Math.ceil(
        (new Date(fechaFin) - new Date(fechaInicio)) / (1000 * 60 * 60 * 24)
      );

      const precioPorDia = vehiculo.precioPorDia || vehiculo.precio || 0;
      const monto = montoPrecalculado ?? dias * precioPorDia;

      const alquiler = {
        usuarioId,
        vehiculoId: vehiculo.vehiculoId || vehiculo.id,
        empleadoId: 1,
        fechaInicio,
        fechaFin,
        estado: "Reservado",
        aceptoTerminos: true
      };

      const alquilerCreado = await crearAlquiler(alquiler);

      const pago = {
        alquilerId: alquilerCreado.alquilerId,
        monto,
        fechaPago: new Date().toISOString(),
        metodoPago: metodo
      };

      await crearPago(pago);

      setMostrarModalExito(true);
      setMontoTotal(null);
      setMostrarPago(false);
      setVehiculoSeleccionado(null);
      setReservaPendiente(null);

      fetch('/api/vehiculos')
        .then(res => res.json())
        .then(setVehiculos);
    } catch (error) {
      console.error("⛔ Error detallado:", error);
      let mensaje = "❌ Error al procesar el alquiler";

      if (error.response && error.response.data) {
        mensaje = error.response.data.error || mensaje;
      } else if (error.message) {
        mensaje = error.message;
      }

      setMensajeModalError(mensaje);
    }
  };

  return (
    <section className="py-5" style={{ background: 'linear-gradient(135deg, #0f0f1a, #1b1b3a)' }}>
      <div className="container text-white">
        <h2 className="text-center mb-5 neon-text">Nuestra Flota</h2>
        <div className="row">
          {vehiculos.map(vehiculo => (
            <VehicleCard
              key={vehiculo.id || vehiculo.vehiculoId}
              vehiculo={vehiculo}
              onAlquilar={handleAlquilar}
            />
          ))}
        </div>
      </div>

      {mostrarReserva && vehiculoSeleccionado && (
        <ReservaModal
          vehiculo={vehiculoSeleccionado}
          onClose={() => setMostrarReserva(false)}
          onConfirm={handleReservaConfirmada}
        />
      )}

      {mostrarPago && (
      <PagoModal
      onCancel={() => setMostrarPago(false)}
      onConfirmarPago={handlePagoConfirmado}
      montoAlquiler={montoTotal}
      />
      )}

      {/* 🔒 Modal login requerido */}
      {mostrarModalAviso && (
        <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content border border-danger">
              <div className="modal-header">
                <h5 className="modal-title text-danger fw-bold">🔒 Tenés que iniciar sesión</h5>
                <button type="button" className="btn-close" onClick={() => setMostrarModalAviso(false)}></button>
              </div>
              <div className="modal-body">
                <p>Para realizar un alquiler necesitás estar registrado e iniciar sesión en el sistema.</p>
              </div>
              <div className="modal-footer">
                <button className="btn btn-outline-danger" onClick={() => navigate('/login')}>Iniciar Sesión</button>
                <button className="btn btn-outline-secondary" onClick={() => setMostrarModalAviso(false)}>Cerrar</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* ✅ Modal de éxito */}
      {mostrarModalExito && (
        <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content border border-success">
              <div className="modal-header">
                <h5 className="modal-title text-success">✅ Alquiler confirmado</h5>
                <button type="button" className="btn-close" onClick={() => setMostrarModalExito(false)}></button>
              </div>
              <div className="modal-body">
                <p>El alquiler fue registrado correctamente y el pago fue procesado con éxito.</p>
              </div>
              <div className="modal-footer">
                <button className="btn btn-outline-success" onClick={() => setMostrarModalExito(false)}>
                  Seguir navegando
                </button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* ❌ Modal de error */}
      {mensajeModalError && (
        <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content border border-danger">
              <div className="modal-header">
                <h5 className="modal-title text-danger">❌ No se pudo completar el alquiler</h5>
                <button type="button" className="btn-close" onClick={() => setMensajeModalError(null)}></button>
              </div>
              <div className="modal-body">
                <p>{mensajeModalError}</p>
              </div>
              <div className="modal-footer">
                <button className="btn btn-outline-secondary" onClick={() => setMensajeModalError(null)}>
                  Cerrar
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </section>
  );
}