namespace RentCars.Api.DTOs.Pago
{
    public class PagoResponse
    {
        public int PagoId { get; set; }
        public int AlquilerId { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? CodigoReferencia { get; set; }  
        public string? Observaciones { get; set; }     
    }

}