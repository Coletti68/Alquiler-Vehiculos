namespace RentCars.Api.DTOs.Empleado
{
    public class EmpleadoResponse
    {
        public int EmpleadoId { get; set; }
        public string Nombre_Completo { get; set; }
        public string Cargo { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Sucursal { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }
    }
}
