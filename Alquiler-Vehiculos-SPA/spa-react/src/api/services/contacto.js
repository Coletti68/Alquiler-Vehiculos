export const enviarContacto = async (data) => {
  const response = await fetch("/api/contacto", {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    const errorData = await response.json();
    console.error("🚨 Error al enviar contacto:", errorData);
    throw new Error(errorData.error || 'Error al enviar el contacto');
  }

  return await response.json();
};