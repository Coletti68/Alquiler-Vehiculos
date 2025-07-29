import { useState, useEffect } from 'react';
import { enviarContacto } from '../api/services/contacto';

export default function ContactoView() {
  const [motivo, setMotivo] = useState('');
  const [mensaje, setMensaje] = useState('');
  const [email, setEmail] = useState('');
  const [enviado, setEnviado] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (enviado || error) {
      const timer = setTimeout(() => {
        setEnviado(false);
        setError(null);
      }, 4000);
      return () => clearTimeout(timer);
    }
  }, [enviado, error]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);

    const usuario = JSON.parse(localStorage.getItem('user') || '{}');
    const usuarioId = usuario?.id;

    if (!usuarioId) {
      setError('❌ No se encontró usuario. Iniciá sesión nuevamente.');
      return;
    }

    if (!motivo.trim() || !mensaje.trim() || !email.trim()) {
      setError('Por favor completá todos los campos.');
      return;
    }

    try {
      await enviarContacto({ usuarioId, email, motivo, mensaje });
      setEnviado(true);
      setMotivo('');
      setMensaje('');
      setEmail('');
    } catch (err) {
      console.error(err);
      setError('❌ Error al enviar el contacto. Intente de nuevo.');
    }
  };

  return (
    <section className="container pt-4 pb-5">
      <h2 className="text-center mb-4 text-white">Contacto</h2>

      <form
        onSubmit={handleSubmit}
        className="bg-light p-4 rounded shadow mx-auto"
        style={{ maxWidth: '720px', marginTop: '1rem' }}
      >
        <div className="mb-3">
          <label className="form-label fw-semibold">Email</label>
          <input
            type="email"
            className="form-control"
            placeholder="tucorreo@ejemplo.com"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label fw-semibold">Motivo</label>
          <select
            className="form-select"
            value={motivo}
            onChange={(e) => setMotivo(e.target.value)}
            required
          >
            <option value="">Seleccione una opción</option>
            <option>Consulta</option>
            <option>Queja</option>
            <option>Sugerencia</option>
            <option>Problema técnico</option>
            <option>Otro</option>
          </select>
        </div>

        <div className="mb-3">
          <label className="form-label fw-semibold">Mensaje</label>
          <textarea
            className="form-control"
            rows="5"
            placeholder="Escribí tu mensaje aquí..."
            style={{ maxHeight: '200px', overflowY: 'auto' }}
            value={mensaje}
            onChange={(e) => setMensaje(e.target.value)}
            required
          />
        </div>

        <button className="btn btn-primary w-100 fw-semibold" type="submit">
          ✉️ Enviar mensaje
        </button>

        {enviado && (
          <div className="alert alert-success mt-4 rounded">
            ✅ ¡Gracias por tu mensaje! Te responderemos pronto.
          </div>
        )}
        {error && (
          <div className="alert alert-danger mt-4 rounded">
            {error}
          </div>
        )}
      </form>
    </section>
  );
}