namespace RentCars.Api.Dtos.Contacto
{
    public class ContactoRequestDto
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}
