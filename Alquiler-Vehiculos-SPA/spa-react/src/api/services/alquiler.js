export const crearAlquiler = async (alquiler) => {
  const res = await fetch("/api/alquiler", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(alquiler)
  });

  if (!res.ok) {
    const errorData = await res.json();
    console.error("🚨 Error del backend:", errorData);
    throw new Error("Error al crear el alquiler");
  }

  return await res.json();
};