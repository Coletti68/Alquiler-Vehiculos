using System.ComponentModel.DataAnnotations;

namespace RentCars.Api.DTOs.Auth
{
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        public string NuevaPassword { get; set; }  = string.Empty;
    }
}