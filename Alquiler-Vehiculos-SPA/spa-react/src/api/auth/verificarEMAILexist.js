export const verificarEmailExistente = async (email) => {
  const response = await fetch(`/api/usuario/existe-email/${email}`);
  if (!response.ok) throw new Error('Error al verificar el Email');
  const data = await response.json();
  return data.existe;
};