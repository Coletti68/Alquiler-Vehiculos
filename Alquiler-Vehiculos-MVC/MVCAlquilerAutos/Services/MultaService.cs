using System.Net.Http;
using System.Net.Http.Json;
using MVCAlquilerAutos.Models;

namespace MVCAlquilerAutos.Services
{
    public class MultaService
    {
        private readonly HttpClient _httpClient;

        public MultaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Multa>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/multas");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Multa>>();
        }

        public async Task<bool> AsignarMultaAsync(Multa multa)
        {
            var response = await _httpClient.PostAsJsonAsync("api/multas", multa);
            return response.IsSuccessStatusCode;
        }
    }
}