namespace RentCars.Api.DTOs.Multa
{
    public class MultaResponse
    {
        public int MultaId { get; set; }
        public int AlquilerId { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaMulta { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }

    }
}
