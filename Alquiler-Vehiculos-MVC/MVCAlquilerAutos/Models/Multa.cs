using System;
using System.ComponentModel.DataAnnotations;

namespace MVCAlquilerAutos.Models
{
    public class Multa
    {
        public int MultaId { get; set; }

        [Required(ErrorMessage = "El alquiler es obligatorio.")]
        public int AlquilerId { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser mayor o igual a cero.")]
        public decimal Monto { get; set; }

        [Display(Name = "Fecha de Multa")]
        public DateTime FechaMulta { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; } = string.Empty; 

        [Required(ErrorMessage = "El tipo de multa es obligatorio.")]
        public string Tipo { get; set; } = string.Empty;

        public Alquiler? Alquiler { get; set; }
    }
}
