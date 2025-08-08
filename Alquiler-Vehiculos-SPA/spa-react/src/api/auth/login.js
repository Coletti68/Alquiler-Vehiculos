import { API_URL } from '../apiConfig';

export const iniciarSesion = async (email, password) => {
  const response = await fetch(`${API_URL}/auth/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      Email: email,
      Password: password,
    }),
  });

  if (!response.ok) {
    const errorText = await response.text();
    console.error('❌ Backend respondió con error:', errorText);
    throw new Error(errorText || 'Credenciales incorrectas');
  }

  const data = await response.json();

  // Guardar todo lo necesario en localStorage
  localStorage.setItem('token', data.token);
  localStorage.setItem('user', JSON.stringify({
    id: data.usuarioId,
    nombre: data.nombreCompleto,
    email: data.email
  }));

  return data;
};