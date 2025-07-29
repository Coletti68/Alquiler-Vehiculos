import { useState } from 'react';

export default function OlvidePasswordView() {
  const [email, setEmail] = useState('');
  const [enviado, setEnviado] = useState(false);
  const [error, setError] = useState('');
  const [linkGenerado, setLinkGenerado] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setEnviado(false);
    setLinkGenerado('');

    const res = await fetch('/api/auth/recuperar-password', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email })
    });

    if (res.ok) {
      const data = await res.json();
      setEnviado(true);
      setLinkGenerado(data.link); 
    } else {
      const data = await res.json();
      setError(data?.mensaje || 'No se pudo generar el enlace.');
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

        {enviado && (
          <div className="alert alert-success">
            ✅ Link generado con éxito.
          </div>
        )}

        {error && (
          <div className="alert alert-danger">{error}</div>
        )}

        <button type="submit" className="btn btn-primary">
          Enviar enlace
        </button>

        {linkGenerado && (
  <div className="alert alert-info mt-3">
    <strong>Link generado:</strong><br />
    <a href={linkGenerado} target="_blank" rel="noopener noreferrer">
      {linkGenerado}
    </a>

    {!linkGenerado.startsWith('http://localhost:5173') && (
      <div className="text-danger mt-2">
        ⚠️ Atención: el link no apunta al frontend (debería comenzar con <code>http://localhost:5173</code>)
      </div>
        )}
       </div>
        )}
      </form>
    </div>
  );
}
