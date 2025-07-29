namespace RentCars.Api.Models
{ 
    public class Empleado
    {
        public int EmpleadoId { get; set; }
        public string Nombre_Completo { get; set; } = String.Empty;
        public string Cargo { get; set; } = String.Empty;
        public string DNI { get; set; } = String.Empty;
        public string Telefono { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Sucursal { get; set; } = String.Empty;
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }

        public ICollection<Alquiler> Alquileres { get; set; }

    }
}

