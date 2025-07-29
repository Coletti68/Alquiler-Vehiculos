namespace MVCAlquilerAutos.Models
{
    public class HistorialAlquiler
    {
        public int AlquilerId { get; set; }
        public string Vehiculo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }
        public Alquiler Alquiler { get; set; }

    }
}
