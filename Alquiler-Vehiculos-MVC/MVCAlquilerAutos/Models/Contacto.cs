public class Contacto
{
    public int ContactoId { get; set; }
    public int UsuarioId { get; set; }
    public string Email { get; set; }
    public string Motivo { get; set; }
    public string Mensaje { get; set; }
    public DateTime Fecha { get; set; }
    public bool Respondido { get; set; }
}
