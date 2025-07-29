namespace RentCars.Api.DTOs.Auth
{
    public class LoginResponse
    {
            public string Token { get; set; }
            public DateTime Expira { get; set; }
            public int UsuarioId { get; set; } 
            public string NombreCompleto { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;

    }
}
