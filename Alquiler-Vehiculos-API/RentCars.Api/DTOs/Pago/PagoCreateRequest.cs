namespace RentCars.Api.DTOs.Pago
{
    public class PagoCreateRequest
    {
        public int AlquilerId { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
    }
}
