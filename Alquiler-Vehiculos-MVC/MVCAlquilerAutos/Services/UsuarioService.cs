using System.Net.Http;
using System.Net.Http.Json;
using MVCAlquilerAutos.Models;

namespace MVCAlquilerAutos.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            var usuarios = await _httpClient.GetFromJsonAsync<List<Usuario>>("api/usuario");
            return usuarios ?? new List<Usuario>();
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"api/usuario/{id}");
        }

        public async Task<Usuario?> GetUsuarioConAlquileresYMultasAsync(int id)
        {
         
            return await _httpClient.GetFromJsonAsync<Usuario>($"api/usuario/detalles/{id}");
        }

        public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/usuario/{usuario.UsuarioId}", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> MarcarMultaComoPagadaAsync(int multaId)
        {
            
            var response = await _httpClient.PutAsync($"api/multas/marcarpagada/{multaId}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
