import { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Asegurate de tener esto si usás npm

export default function OlvidePasswordView() {
  const [email, setEmail] = useState('');
  const [enviado, setEnviado] = useState(false);
  const [error, setError] = useState('');
  const [cargando, setCargando] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setEnviado(false);
    setCargando(true);

    try {
      const res = await fetch('/api/auth/recuperar-password', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email })
      });

      const data = await res.json();

      if (res.ok) {
        setEnviado(true);
      } else {
        setError(data?.mensaje || 'No se pudo generar el enlace.');
      }
    } catch (err) {
      setError('Error de conexión. Intentá nuevamente.');
    } finally {
      setCargando(false);
    }
  };

  return (
    <div className="container py-5">
      <h2 className="mb-4">🔐 Recuperar contraseña</h2>

      <form onSubmit={handleSubmit} className="bg-white shadow rounded p-4">
        <div className="mb-3">
          <label className="form-label">Email registrado</label>
          <input
            type="email"
            className="form-control"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>

        <button
          type="submit"
          className="btn btn-primary d-flex align-items-center justify-content-center gap-2"
          disabled={cargando}
          style={{ minWidth: '180px' }}
        >
          {cargando ? (
            <>
              <span
                className="spinner-border spinner-border-sm text-light"
                role="status"
                aria-hidden="true"
              ></span>
              <span>Enviando...</span>
            </>
          ) : (
            'Enviar enlace'
          )}
        </button>

        {enviado && (
          <div className="alert alert-success mt-3 fade show" role="alert">
            ✅ Link generado con éxito. Revisá tu correo para continuar.
          </div>
        )}

        {error && (
          <div className="alert alert-danger mt-3 fade show" role="alert">
            ❌ {error}
          </div>
        )}
      </form>
    </div>
  );
}