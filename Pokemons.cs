using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

namespace Pokemon;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

public class ListaPokemon
{
    [JsonPropertyName("count")]
    public int cantidad { get; set; }

    [JsonPropertyName("next")]
    public string proximo { get; set; }

    [JsonPropertyName("previous")]
    public object anterior { get; set; }

    [JsonPropertyName("results")]
    public List<PokemonIndividual> listaPokemones { get; set; }
}
public class PokemonIndividual
{
    [JsonPropertyName("name")]
    public string nombre { get; set; }

    [JsonPropertyName("url")]
    public string url { get; set; }
}

