import { API_URL } from '../apiConfig';

export const registrarUsuario = async (usuario) => {
  try {
    const usuarioBackend = {
      NombreCompleto: usuario.nombre,
      Password: usuario.password,
      DNI: usuario.dni,
      FechaNacimiento: usuario.fechaNacimiento,
      Telefono: usuario.telefono,
      Email: usuario.email,
      Pais: usuario.pais,
      Direccion: usuario.direccion,
    };

    console.log('📤 Enviando al backend:', usuarioBackend);

    const response = await fetch(`${API_URL}/usuario`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(usuarioBackend),
    });

    if (!response.ok) {
      const mensajeError = await response.text();
      console.error('❌ Error del backend:', mensajeError);
      throw new Error(mensajeError);
    }

    const data = await response.json();
    console.log('✅ Usuario creado con éxito:', data);
    return data;

  } catch (error) {
    console.error('🚨 Error en registrarUsuario():', error.message);
    throw error;
  }
};