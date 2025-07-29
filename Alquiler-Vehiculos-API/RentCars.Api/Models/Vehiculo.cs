namespace RentCars.Api.Models
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; }
        public string Marca { get; set; } = String.Empty;
        public string Modelo { get; set; } = String.Empty;
        public int Anio { get; set; }
        public string Placa { get; set; } = String.Empty;
        public string Color { get; set; } = String.Empty;
        public string Tipo { get; set; } = String.Empty;
        public decimal PrecioPorDia { get; set; }
        public string Estado { get; set; } = String.Empty;
        public ICollection<Alquiler> Alquileres { get; set; }
    }
}
