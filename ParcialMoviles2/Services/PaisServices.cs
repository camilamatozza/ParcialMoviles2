using System.Net.Http.Json;
using PaisesApp.Models;

namespace PaisesApp.Services
{
    public class PaisService
    {
        private readonly HttpClient _httpClient;

        public PaisService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Pais>> GetPaisesAsync()
        {
            var response = await _httpClient
                .GetAsync("https://restcountries.com/v3.1/region/americas");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"Error del servidor: {(int)response.StatusCode}");

            var paises = await response.Content
                .ReadFromJsonAsync<List<Pais>>();

            return paises?.OrderBy(p => p.NombreComun).ToList()
                ?? new List<Pais>();
        }
    }
}