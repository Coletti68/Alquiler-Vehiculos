namespace RentCars.Api.DTOs.Multa
{
    public class MultaUpdateRequest
    {
        public string? Descripcion { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? FechaMulta { get; set; }
        public string Estado { get; set; }
        public string? Tipo { get; set; }
    }
}
