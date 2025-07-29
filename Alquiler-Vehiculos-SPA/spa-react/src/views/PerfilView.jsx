import { useEffect, useState } from 'react';

export default function PerfilView() {
  const [usuario, setUsuario] = useState(null);
  const [alquileres, setAlquileres] = useState([]);
  const [multas, setMultas] = useState([]);

  const formatoFecha = (fechaISO) => {
    if (!fechaISO) return '—';
    return new Date(fechaISO).toLocaleDateString('es-AR');
  };

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (!storedUser) return;

    const user = JSON.parse(storedUser);
    if (!user?.id) return;

    setUsuario(user);

    fetch(`/api/alquiler/usuario/${user.id}`)
      .then((res) => res.ok ? res.json() : [])
      .then(setAlquileres)
      .catch((err) => console.error('Error al cargar alquileres:', err));

    fetch(`/api/multas/usuario/${user.id}`)
      .then((res) => res.ok ? res.json() : [])
      .then(setMultas)
      .catch((err) => console.error('Error al cargar multas:', err));
  }, []);

  if (!usuario) {
    return (
      <div className="container py-5">
        <h2>👤 Mi perfil</h2>
        <p className="text-muted">Cargando datos del usuario...</p>
      </div>
    );
  }

  return (
    <div className="container py-5">
      <h2 className="mb-4">👤 Mi perfil</h2>

      <div className="mb-4 p-4 bg-white shadow rounded">
        <h4 className="mb-3">📄 Datos personales</h4>
        {/* <p><strong>Nombre:</strong> {usuario.nombre || 'No disponible'}</p> */}
        <p><strong>Email:</strong> {usuario.email || 'No disponible'}</p>
      </div>

      <div className="mb-5">
        <h4 className="mb-3">🧾 Historial de alquileres</h4>
        {alquileres.length > 0 ? (
          <ul className="list-group">
            {alquileres.map((a, index) => (
              <li key={index} className="list-group-item">
                <div className="d-flex justify-content-between align-items-center">
                  <div>
                    {/*<strong>🚗 {a.vehiculoMarca} {a.vehiculoModelo}</strong><br />*/}
                    📆 {formatoFecha(a.fechaInicio)} — {formatoFecha(a.fechaFin)}<br />
                    📌 Estado:{' '}
                    <span className={`badge bg-${a.estado === 'Activo' ? 'success' : 'secondary'} text-light`}>
                      {a.estado || 'Desconocido'}
                    </span>
                  </div>
                  <span className="badge bg-warning text-dark fs-6">
                    {a.precioTotal != null ? `$${a.precioTotal.toLocaleString('es-AR')}` : '—'}
                  </span>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-muted">No tenés alquileres registrados.</p>
        )}
      </div>

      <div>
        <h4 className="mb-3">🚫 Multas registradas</h4>
        {multas.length > 0 ? (
          <ul className="list-group">
            {multas.map((m, index) => (
              <li key={index} className="list-group-item text-danger">
                {m.descripcion} — Fecha: {formatoFecha(m.fecha)}
              </li>
            ))}
          </ul>
        ) : (
          <p className="text-muted">Sin multas por el momento 🎉</p>
        )}
      </div>
    </div>
  );
}