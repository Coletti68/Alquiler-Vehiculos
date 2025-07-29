namespace RentCars.Api.DTOs.Vehiculo
{
    public class VehiculoListadoDTO
    {
        public int VehiculoId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string Color { get; set; }
        public decimal PrecioPorDia { get; set; }
        public string Estado { get; set; }
    }
}
