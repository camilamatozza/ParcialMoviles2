using System.Text.Json.Serialization;

namespace PaisesApp.Models
{
    public class Pais
    {
        [JsonPropertyName("name")]
        public NombrePais? Name { get; set; }

        [JsonPropertyName("capital")]
        public List<string>? Capital { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("population")]
        public long Population { get; set; }

        public string NombreComun => Name?.Common ?? "Sin nombre";
        public string CapitalTexto => Capital?.FirstOrDefault() ?? "Sin capital";
        public string PoblacionTexto => $"{Population:N0} habitantes";
    }

    public class NombrePais
    {
        [JsonPropertyName("common")]
        public string? Common { get; set; }
    }
}