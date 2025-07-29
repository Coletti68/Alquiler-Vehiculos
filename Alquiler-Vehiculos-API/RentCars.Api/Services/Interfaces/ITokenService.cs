using RentCars.Api.Models;

namespace RentCars.Api.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerarToken(Usuario usuario);
    }
}