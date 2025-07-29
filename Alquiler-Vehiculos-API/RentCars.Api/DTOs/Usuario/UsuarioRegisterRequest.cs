using System.ComponentModel.DataAnnotations;

namespace RentCars.Api.DTOs.Usuario
{
    public class UsuarioRegisterRequest
    {
        [Required]
        public string NombreCompleto { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string DNI { get; set; }

        public string? FechaNacimiento { get; set; }

        public string? Telefono { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Pais { get; set; }

        public string? Direccion { get; set; }

        public bool Activo { get; set; } = true;


    }
}
