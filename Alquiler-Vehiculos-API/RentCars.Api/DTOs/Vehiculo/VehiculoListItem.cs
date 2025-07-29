namespace RentCars.Api.DTOs.Vehiculo
{
    public class VehiculoListItem
    {
        public int VehiculoId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Estado { get; set; }
        public string Color { get; set; }
        public decimal PrecioPorDia { get; set; }
    }
}
