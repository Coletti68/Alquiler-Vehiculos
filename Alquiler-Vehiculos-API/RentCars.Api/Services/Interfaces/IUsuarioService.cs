using RentCars.Api.DTOs.Alquiler;

namespace RentCars.Api.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id); 
        Task<Usuario> CreateAsync(Usuario usuario); 
        Task<Usuario> UpdateAsync(int id, Usuario usuario); 
        Task<bool> DesactivarAsync(int id); 
        Task<IEnumerable<AlquilerResponse>> GetHistorialPorUsuarioAsync(int id);
        Task<Usuario?> GetUsuarioConAlquileresYMultasAsync(int id);
        Task<bool> DniExisteAsync(string dni);
        Task<bool> EmailExisteAsync(string email);
    }
}
