namespace RentCars.Api.Models
{
    public class Alquiler
    {
        public int AlquilerId { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public bool AceptoTerminos { get; set; } = false;

        public ICollection<Pago> Pagos { get; set; }
        public ICollection<Multa> Multas { get; set; }

    }
}

