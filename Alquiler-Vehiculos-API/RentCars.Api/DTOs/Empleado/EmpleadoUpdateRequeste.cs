using System.ComponentModel.DataAnnotations;

namespace RentCars.Api.DTOs.Empleado
{
    public class EmpleadoUpdateRequest
    {
        public string? Nombre_Completo { get; set; }
        public string? Cargo { get; set; }
        public string? DNI { get; set; }
        public string? Telefono { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Sucursal { get; set; }
    }
}
