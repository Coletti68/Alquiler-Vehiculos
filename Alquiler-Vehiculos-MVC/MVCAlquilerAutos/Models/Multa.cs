
using System;
using System.ComponentModel.DataAnnotations;

namespace MVCAlquilerAutos.Models
{
    public class Multa
    {
        public int MultaId { get; set; }

        [Required]
        public int AlquilerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        [Range(0, 999999.99)]
        public decimal Monto { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaMulta { get; set; } = DateTime.Today;

        [Required]
        public string Estado { get; set; }

        [Required]
        public string Tipo { get; set; }
        public Alquiler? Alquiler { get; set; }
    }
}
