export default function VehicleCard({ vehiculo, onAlquilar }) {
  console.log('Vehiculo recibido:', vehiculo);

  const coloresMap = {
    Banchisa: "banchisa",
    Gris: "gris",
    Negro: "negro",
    Plata: "plata",
    Rojo: "rojo"
  };

  const color = vehiculo.color || vehiculo.Color || "default";
  const nombreImagen = coloresMap[color] || "default";
  const precio = vehiculo.precioPorDia || vehiculo.precio || 0;

  return (
    <div className="col-md-4 mb-4">
      <div className="vehicle-card p-3 rounded shadow text-white text-center">
        <img
          src={`/ImgAutos/${nombreImagen}.png`}
          alt={`Vehículo color ${color}`}
          className="img-fluid mb-3 vehicle-img"
        />

        <h5>{vehiculo.marca} {vehiculo.modelo} ({vehiculo.anio})</h5>

        <p className="mb-1"><strong>Placa:</strong> {vehiculo.placa}</p>
        <p className="mb-1"><strong>Color:</strong> {color}</p>
        <p className="mb-1"><strong>Precio por día:</strong> ARS {precio}</p>
        <p className="mb-2">
          🛈 <strong>Estado:</strong>{" "}
          <span className={`badge bg-${vehiculo.estado === "Disponible" ? "success" : "secondary"}`}>
            {vehiculo.estado}
          </span>
        </p>

        <button
          className="btn neon-btn mt-2"
          onClick={() => onAlquilar(vehiculo)}
          disabled={vehiculo.estado !== "Disponible"}
        >
          Alquilar
        </button>
      </div>
    </div>
  );
}