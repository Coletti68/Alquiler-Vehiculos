using System.ComponentModel.DataAnnotations;

namespace RentCars.Api.DTOs.Vehiculo
{
    public class VehiculoCreateRequest
    {
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Range(1900, 2100)]
        public int Anio { get; set; }
        [Required]
        public string Placa { get; set; }
        public string Color { get; set; }
        public string Tipo { get; set; }
        [Range(0, 10000)]
        public decimal PrecioPorDia { get; set; }
        public string Estado { get; set; } = "Disponible";
    }
}
