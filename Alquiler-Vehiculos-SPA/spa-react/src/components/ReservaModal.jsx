import { useState } from "react";

export default function ReservaModal({ vehiculo, onClose, onConfirm }) {
  const [fechaInicio, setFechaInicio] = useState("");
  const [fechaFin, setFechaFin] = useState("");
  const [aceptaTerminos, setAceptaTerminos] = useState(false);

  const hoy = new Date().toISOString().split("T")[0]; // formato YYYY-MM-DD

  const handleConfirmar = () => {
    if (fechaInicio && fechaFin && aceptaTerminos) {
      onConfirm({
        vehiculoId: vehiculo.vehiculoId,
        fechaInicio,
        fechaFin,
        aceptoTerminos: true
      });
    }
  };

  return (
    <div className="modal show d-block fade" tabIndex="-1" role="dialog">
      <div className="modal-dialog modal-lg">
        <div className="modal-content">

          <div className="modal-header">
            <h5 className="modal-title">Reservar Vehículo</h5>
            <button type="button" className="btn-close" onClick={onClose}></button>
          </div>

          <div className="modal-body">
            <form>
              <div className="mb-3">
                <label className="form-label fw-bold">Vehículo</label>
                <div className="form-control-plaintext">
                  {vehiculo?.marca} {vehiculo?.modelo}
                </div>
              </div>

              <div className="mb-3">
                <label className="form-label">Fecha de Inicio</label>
                <input
                  type="date"
                  className="form-control"
                  value={fechaInicio}
                  onChange={(e) => setFechaInicio(e.target.value)}
                  min={hoy}
                  required
                />
              </div>

              <div className="mb-3">
                <label className="form-label">Fecha de Fin</label>
                <input
                  type="date"
                  className="form-control"
                  value={fechaFin}
                  onChange={(e) => setFechaFin(e.target.value)}
                  min={fechaInicio || hoy}
                  required
                />
              </div>

              <div className="alert alert-warning small" role="alert">
                Al confirmar el alquiler usted acepta que es responsable por el uso del vehículo,
                deberá devolverlo limpio y con combustible. La empresa no se hace responsable por objetos olvidados.
                En caso de que se le registre una multa, tendrá 10 días hábiles para abonarla desde la fecha de la infracción;
                de lo contrario, la multa pasará al estado <strong>Atrasado</strong> y se sumará un recargo del <strong>20%</strong> al monto original.
                <br /><br />
                <strong>⚠️ Todas las multas deben abonarse de forma presencial en nuestra sucursal.</strong>
              </div>

              <div className="form-check mb-3">
                <input
                  className="form-check-input"
                  type="checkbox"
                  id="terminosCheck"
                  checked={aceptaTerminos}
                  onChange={(e) => setAceptaTerminos(e.target.checked)}
                />
                <label className="form-check-label" htmlFor="terminosCheck">
                  Acepto los términos y condiciones
                </label>
              </div>
            </form>
          </div>

          <div className="modal-footer">
            <button className="btn btn-secondary" onClick={onClose}>
              Cerrar
            </button>
            <button
              className="btn btn-primary"
              onClick={handleConfirmar}
              disabled={!fechaInicio || !fechaFin || !aceptaTerminos}
            >
              Confirmar reserva
            </button>
          </div>

        </div>
      </div>
    </div>
  );
}