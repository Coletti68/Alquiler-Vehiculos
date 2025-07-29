namespace RentCars.Api.DTOs.Alquiler
{
    public class AlquilerDTO
    {
        public int AlquilerId { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreVehiculo { get; set; }
        public string NombreEmpleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }
}
