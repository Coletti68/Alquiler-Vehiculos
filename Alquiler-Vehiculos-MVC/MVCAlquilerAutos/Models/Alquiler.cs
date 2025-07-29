using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAlquilerAutos.Models
{
    public class Alquiler
    {
        public int AlquilerId { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El vehículo es obligatorio.")]
        public int VehiculoId { get; set; }

        [Required(ErrorMessage = "El empleado es obligatorio.")]
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [Display(Name = "Fecha de Fin")]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Fecha de Devolución")]
        public DateTime? FechaDevolucion { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser mayor a cero.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; } = string.Empty;

        [Display(Name = "Aceptó Términos")]
        public bool AceptoTerminos { get; set; }

        public Usuario? Usuario { get; set; }

        public List<Multa>? Multas { get; set; }

        [Display(Name = "Usuario")]
        public string? NombreUsuario { get; set; }

        [Display(Name = "Vehículo")]
        public string? NombreVehiculo { get; set; }

        [Display(Name = "Empleado")]
        public string? NombreEmpleado { get; set; }
    }
}

