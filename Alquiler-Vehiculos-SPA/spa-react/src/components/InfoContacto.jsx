export default function InfoContacto() {
  return (
    <section className="bg-light py-5" id="contacto">
      <div className="container">
        <div className="row align-items-stretch">
          {/* Información de contacto */}
          <div className="col-md-6 mb-4">
            <div className="p-4 bg-white rounded shadow h-100">
              <h3 className="mb-4 text-primary">Información de Contacto</h3>
              <p><i className="fas fa-phone me-2 text-secondary"></i> Teléfono: <strong>3777-885466</strong></p>
              <p><i className="fas fa-envelope me-2 text-secondary"></i> Email: <strong>RentCarsgoya@gmail.com</strong></p>
              <p><i className="fas fa-map-marker-alt me-2 text-secondary"></i> Dirección: <strong>Av. Sarmiento 500, Goya, Corrientes</strong></p>
            </div>
          </div>

          {/* Mapa */}
          <div className="col-md-6 mb-4">
            <div className="rounded overflow-hidden shadow h-100">
              <iframe
                title="Ubicación en Goya"
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d55921.86195869541!2d-59.29071647431638!3d-29.1379489!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x944dc888185d7af7%3A0x4e2a005882f78f0c!2sAv.%20Sarmiento%20500%2C%20Goya%2C%20Corrientes!5e0!3m2!1ses-419!2sar!4v1652276518000!5m2!1ses-419!2sar"
                width="100%"
                height="100%"
                style={{ border: 0, minHeight: '300px' }}
                allowFullScreen=""
                loading="lazy"
                referrerPolicy="no-referrer-when-downgrade"
              ></iframe>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}