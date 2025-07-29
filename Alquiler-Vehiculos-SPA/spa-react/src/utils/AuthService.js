export const isAuthenticated = () => !!localStorage.getItem('token');

export const cerrarSesion = () => {
  localStorage.removeItem('token');
  localStorage.removeItem('usuarioId');
};