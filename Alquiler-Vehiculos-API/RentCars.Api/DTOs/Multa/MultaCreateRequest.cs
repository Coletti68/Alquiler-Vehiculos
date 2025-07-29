namespace RentCars.Api.DTOs.Multa
{
    public class MultaCreateRequest
    {
        public int AlquilerId { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaMulta { get; set; } = DateTime.Today;
        public string Estado { get; set; }
        public string Tipo { get; set; }

    }
}
