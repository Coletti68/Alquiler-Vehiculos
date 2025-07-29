namespace RentCars.Api.Dtos.Contacto
{
    public class ContactoResponseDto
    {
        public int ContactoId { get; set; }
        public int UsuarioId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public bool Respondido { get; set; }
    }
}