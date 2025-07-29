using RentCars.Api.Models;
using System.ComponentModel.DataAnnotations;
public class Usuario
{
    public int UsuarioId { get; set; }
    [Required]
    public string Nombre_Completo { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    public string? DNI { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Telefono { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }  = string.Empty;
    public string? Pais { get; set; }
    public string? Direccion { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public string Rol { get; set; } = "Usuario";
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpira { get; set; }
    public bool Activo { get; set; } = true;
    public ICollection<Alquiler>? Alquileres { get; set; }
    public ICollection<Contacto>? Contactos { get; set; }
}