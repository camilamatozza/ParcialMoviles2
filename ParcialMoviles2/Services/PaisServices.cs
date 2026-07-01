using System.Text.Json;
using PaisesApp.Models;

namespace PaisesApp.Services;

public class PaisService : IPaisService
{
    private readonly HttpClient _httpClient;

    public PaisService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer rc_live_272f2726a9a74b6e9c6f079dff3990a2");
    }

    public async Task<List<Pais>> GetPaisesAsync()
    {
        var response = await _httpClient.GetAsync(
            "https://api.restcountries.com/countries/v5?limit=100&response_fields=names.common,capitals,region,population");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Error del servidor: {(int)response.StatusCode}");

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);
        var objects = doc.RootElement
                         .GetProperty("data")
                         .GetProperty("objects");

        var paises = new List<Pais>();

        foreach (var item in objects.EnumerateArray())
        {
            string? common = null;
            if (item.TryGetProperty("names", out var names) &&
                names.TryGetProperty("common", out var commonProp))
                common = commonProp.GetString();

            string? capital = null;
            if (item.TryGetProperty("capitals", out var caps) &&
                caps.ValueKind == JsonValueKind.Array)
            {
                var first = caps.EnumerateArray().FirstOrDefault();
                if (first.ValueKind == JsonValueKind.Object &&
                    first.TryGetProperty("name", out var capName))
                    capital = capName.GetString();
            }

            string? region = null;
            if (item.TryGetProperty("region", out var reg))
                region = reg.GetString();

            long population = 0;
            if (item.TryGetProperty("population", out var pop))
                population = pop.GetInt64();

            paises.Add(new Pais
            {
                Name = new NombrePais { Common = common },
                Capital = capital != null ? new List<string> { capital } : null,
                Region = region,
                Population = population
            });
        }

        return paises.OrderBy(p => p.NombreComun).ToList();
    }
}