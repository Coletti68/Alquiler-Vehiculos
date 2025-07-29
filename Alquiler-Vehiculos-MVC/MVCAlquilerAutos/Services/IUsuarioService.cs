using MVCAlquilerAutos.Models;

namespace MVCAlquilerAutos.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllUsuariosAsync();
        Task<Usuario?> GetUsuarioByIdAsync(int id);
        Task<Usuario?> GetUsuarioConAlquileresYMultasAsync(int id);
        Task<bool> UpdateUsuarioAsync(Usuario usuario);
        Task<bool> MarcarMultaComoPagadaAsync(int multaId);
    }
}
