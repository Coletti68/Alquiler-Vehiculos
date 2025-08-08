export const verificarDniExistente = async (dni) => {
  const response = await fetch(`/api/usuario/existe-dni/${dni}`);
  if (!response.ok) throw new Error('Error al verificar el DNI');
  const data = await response.json();
  return data.existe;
};