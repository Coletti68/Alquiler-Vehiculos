using System.ComponentModel.DataAnnotations;

namespace MVCAlquilerAutos.Models
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        public string Modelo { get; set; } = string.Empty;

        [Range(1900, 2100, ErrorMessage = "El año debe estar entre 1900 y 2100.")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "La placa es obligatoria.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "El color es obligatorio.")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        public string Tipo { get; set; } = string.Empty;

        [Range(0, 999999, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal PrecioPorDia { get; set; }

        public string Estado { get; set; } = string.Empty;

        public string ImagenUrl { get; set; } = string.Empty;

    }
}
