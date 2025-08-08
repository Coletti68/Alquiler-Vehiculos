using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using MVCAlquilerAutos.Models;

public class ContactoService
{
    private readonly HttpClient _httpClient;

    public ContactoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Contacto>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("api/contacto");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Contacto>>(json);
    }

    public async Task<Contacto?> MarcarComoRespondidoAsync(int id)
    {
        var response = await _httpClient.PutAsync($"api/contacto/responder/{id}", null);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Contacto>(json);
    }
}

