export default function Footer() {
  return (
    <footer>
      <div className="container">
        <div className="row">
          <div className="col-md-6">
            <h4>CarRentalEasy</h4>
            <p>El mejor servicio de alquiler de autos en Goya, Corrientes.</p>
          </div>
          <div className="col-md-6 text-md-end">
            <p><i className="fas fa-phone me-2"></i> 3777-885466</p>
            <p><i className="fas fa-envelope me-2"></i> masinfoalquilergoya.com</p>
          </div>
        </div>
        <div className="row mt-3">
          <div className="col-12 text-center">
            <p>&copy; 2025 CarRentalEasy. All rights reserved.</p>
          </div>
        </div>
      </div>
    </footer>
  );
}