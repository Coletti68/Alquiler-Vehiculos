namespace RentCars.Api.DTOs.Alquiler
{
    public class FacturaAlquilerDTO
    {
        public int AlquilerId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreVehiculo { get; set; } = string.Empty;
        public string Patente { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal TotalAlquiler { get; set; }
        public List<MultaDTO> Multas { get; set; } = new();
        public List<PagoDTO> Pagos { get; set; } = new();
        public string Estado { get; set; } = string.Empty;
    }
    public class MultaDTO
    {
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime FechaMulta { get; set; }
    }

    public class PagoDTO
    {
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}