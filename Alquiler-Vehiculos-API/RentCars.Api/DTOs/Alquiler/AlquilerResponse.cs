namespace RentCars.Api.DTOs.Alquiler
{ 
 public class AlquilerResponse
 {
    public int AlquilerId { get; set; }
    public int UsuarioId { get; set; }
    public int VehiculoId { get; set; }
    public int EmpleadoId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = string.Empty;
    public bool AceptoTerminos { get; set; } = false;
    public string Vehiculo { get; internal set; }
    }
}
