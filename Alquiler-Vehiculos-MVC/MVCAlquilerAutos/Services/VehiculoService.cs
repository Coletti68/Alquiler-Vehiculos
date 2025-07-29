using MVCAlquilerAutos.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MVCAlquilerAutos.Services
{
    public class VehiculoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:5001/api/Vehiculos";

        public VehiculoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Vehiculo>> GetVehiculosAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Vehiculo>>(_baseUrl);
                return response ?? new List<Vehiculo>();
            }
            catch
            {
                return new List<Vehiculo>();
            }
        }


        public async Task<Vehiculo?> GetVehiculoByIdAsync(int id)
        {
            try
            {
                var url = $"{_baseUrl}/{id}";
                return await _httpClient.GetFromJsonAsync<Vehiculo>(url);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateVehiculoAsync(Vehiculo vehiculo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, vehiculo);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateVehiculoAsync(int id, Vehiculo vehiculo)
        {
            try
            {
                var url = $"{_baseUrl}/{id}";
                var response = await _httpClient.PutAsJsonAsync(url, vehiculo);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteVehiculoAsync(int id)
        {
            try
            {
                var url = $"{_baseUrl}/{id}";
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CambiarEstadoVehiculoAsync(int id, string nuevoEstado)
        {
            try
            {
                var url = $"{_baseUrl}/{id}/estado";
                var content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(nuevoEstado),
                        Encoding.UTF8,
                        "application/json"
                    );
                var response = await _httpClient.PatchAsync(url, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Vehiculo>> GetListadoDetalladoAsync()
        {
            try
            {
                var url = $"{_baseUrl}/listado";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return [];

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Vehiculo>>(json) ?? [];
            }
            catch
            {
                return [];
            }
        }

    }
}
