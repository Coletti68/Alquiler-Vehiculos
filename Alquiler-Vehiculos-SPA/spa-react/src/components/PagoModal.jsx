import { useState } from "react";

export default function PagoModal({ onConfirmarPago, onCancel, montoAlquiler }) {
  const [metodoSeleccionado, setMetodoSeleccionado] = useState(null);
  const [cargando, setCargando] = useState(false);

  const handlePagar = () => {
    if (!metodoSeleccionado) return;
    setCargando(true);
    setTimeout(() => {
      setCargando(false);
      onConfirmarPago(metodoSeleccionado);
    }, 1500);
  };

  return (
    <div className="modal show d-block fade" tabIndex="-1" role="dialog">
      <div className="modal-dialog">
        <div className="modal-content">

          <div className="modal-header">
            <h5 className="modal-title">Pago</h5>
            <button type="button" className="btn-close" onClick={onCancel}></button>
          </div>

          <div className="modal-body text-center">
            <p className="fw-bold fs-5 mb-3">
              💰 Total del alquiler: <span className="text-success">${montoAlquiler?.toFixed(2)}</span>
            </p>

            <p>Seleccioná tu método de pago:</p>
            <div className="d-flex justify-content-around mb-3">
              {["Tarjeta", "Efectivo", "Transferencia"].map((metodo) => (
                <button
                  key={metodo}
                  className={`btn ${
                    metodoSeleccionado === metodo
                      ? "btn-primary"
                      : "btn-outline-primary"
                  }`}
                  onClick={() => setMetodoSeleccionado(metodo)}
                >
                  {metodo === "Tarjeta" && "💳 "}
                  {metodo === "Efectivo" && "💵 "}
                  {metodo === "Transferencia" && "🧾 "}
                  {metodo}
                </button>
              ))}
            </div>

            <p className="text-muted small">
              Este paso es simulado. En producción conectaríamos un gateway de pago.
            </p>
          </div>

          <div className="modal-footer">
            <button className="btn btn-secondary" onClick={onCancel}>
              Cancelar
            </button>
            <button
              className="btn btn-primary"
              onClick={handlePagar}
              disabled={!metodoSeleccionado || cargando}
            >
              {cargando ? "Procesando..." : "Confirmar pago"}
            </button>
          </div>

        </div>
      </div>
    </div>
  );
}