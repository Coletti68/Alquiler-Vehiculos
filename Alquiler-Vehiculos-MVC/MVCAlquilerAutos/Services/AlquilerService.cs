using MVCAlquilerAutos.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MVCAlquilerAutos.Services
{
    public class AlquilerService : IAlquilerService
    {
        private readonly HttpClient _httpClient;

        public AlquilerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:5001/api/Alquiler/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Alquiler>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("");
            if (!response.IsSuccessStatusCode) return [];

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Alquiler>>(content);
        }

        public async Task<Alquiler> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Alquiler>(content);
        }

        public async Task<bool> CreateAsync(Alquiler alquiler)
        {
            var json = JsonConvert.SerializeObject(alquiler);
            var response = await _httpClient.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, Alquiler alquiler)
        {
            var json = JsonConvert.SerializeObject(alquiler);
            var response = await _httpClient.PutAsync($"{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> FinalizarVencidosAsync()
        {
            var response = await _httpClient.PutAsync("finalizar/vencidos", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> FinalizarAsync(int id)
        {
            var response = await _httpClient.PutAsync($"../alquilar/finalizar/{id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CancelarAsync(int id)
        {
            var response = await _httpClient.PutAsync($"../alquilar/cancelar{id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Alquiler>> GetByEstadoAsync(string estado)
        {
            var response = await _httpClient.GetAsync($"status/{estado}");
            if (!response.IsSuccessStatusCode) return [];

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Alquiler>>(content);
        }

        public async Task<IEnumerable<Alquiler>> GetDetalladoAsync()
        {
            var response = await _httpClient.GetAsync("detallado");
            if (!response.IsSuccessStatusCode) return [];

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Alquiler>>(content);
        }

        public async Task<IEnumerable<Alquiler>> GetHabilitadosAsync()
        {
            var response = await _httpClient.GetAsync("enables");
            if (!response.IsSuccessStatusCode) return [];

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Alquiler>>(content);
        }

        public async Task<IEnumerable<Alquiler>> GetResumenPorUsuarioAsync(int usuarioId)
        {
            var response = await _httpClient.GetAsync($"usuario/{usuarioId}/resumen");
            if (!response.IsSuccessStatusCode) return [];

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Alquiler>>(content);
        }

        public async Task<string> GetNumeradorPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"numerador/{id}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<Alquiler>> GetByRangoFechasAsync(DateTime desde, DateTime hasta)
        {
            var response = await _httpClient.GetAsync($"rango?desde={desde:yyyy-MM-dd}&hasta={hasta:yyyy-MM-dd}");
            if (!response.IsSuccessStatusCode) return [];

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Alquiler>>(content);
        }
    }
}
