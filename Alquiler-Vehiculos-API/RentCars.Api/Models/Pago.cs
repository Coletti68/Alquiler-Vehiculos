namespace RentCars.Api.Models
{
    public class Pago
    {
        public int PagoId { get; set; }
        public int AlquilerId { get; set; }
        public Alquiler Alquiler { get; set; } 
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = String.Empty;
        public string? CodigoReferencia { get; set; }
        public DateTime? FechaRegistro { get; set; } = DateTime.Now;
        public string? Observaciones { get; set; }
    }
}