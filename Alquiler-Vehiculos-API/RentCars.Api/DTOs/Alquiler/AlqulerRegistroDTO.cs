public class AlquilerRegistroDTO
{
    public int UsuarioId { get; set; }
    public int VehiculoId { get; set; }
    public int EmpleadoId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Estado { get; set; } = "Reservado"; 
    public bool AceptoTerminos { get; set; } = false;
}