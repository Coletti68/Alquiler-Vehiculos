export const crearPago = async (pago) => {
  const response = await fetch("/api/pago", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(pago)
  });

  if (!response.ok) throw new Error("Error al crear el pago");

  return await response.json();
};