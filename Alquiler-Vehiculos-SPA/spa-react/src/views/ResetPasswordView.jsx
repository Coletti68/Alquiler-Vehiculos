import { useEffect, useState } from 'react';

export default function ResetPasswordView() {
  const [nuevaPassword, setNuevaPassword] = useState('');
  const [email, setEmail] = useState('');
  const [token, setToken] = useState('');
  const [estado, setEstado] = useState(null); 
  const [cargando, setCargando] = useState(false);

  useEffect(() => {
    const params = new URLSearchParams(window.location.search);
    setEmail(params.get('email') || '');
    setToken(decodeURIComponent(params.get('token') || ''));
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setEstado(null);
    setCargando(true);

    try {
      const res = await fetch('/api/auth/resetear-password', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, token, nuevaPassword })
      });

      setEstado(res.ok ? 'ok' : 'error');
    } catch (err) {
      setEstado('error');
    } finally {
      setCargando(false);
    }
  };

  return (
    <div className="container py-5">
      <h2 className="mb-4">🔁 Restablecer contraseña</h2>

      <form onSubmit={handleSubmit} className="bg-white shadow rounded p-4">
        <div className="mb-3">
          <label className="form-label">Nueva contraseña</label>
          <input
            type="password"
            className="form-control"
            value={nuevaPassword}
            onChange={(e) => setNuevaPassword(e.target.value)}
            required
          />
        </div>

        <button type="submit" className="btn btn-success" disabled={cargando}>
          {cargando ? 'Actualizando...' : 'Confirmar nueva contraseña'}
        </button>

        {estado === 'ok' && (
          <div className="alert alert-success mt-3">
            🎉 ¡Contraseña actualizada con éxito! Ya podés iniciar sesión.
          </div>
        )}

        {estado === 'error' && (
          <div className="alert alert-danger mt-3">
            ❌ El token es inválido o expiró. Solicitá uno nuevo desde "¿Olvidaste tu contraseña?"
          </div>
        )}
      </form>
    </div>
  );
}