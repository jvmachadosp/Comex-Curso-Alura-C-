using System.Text.Json.Serialization;

namespace Comex;

public class ProdutoApiResponse
{
    [JsonPropertyName("title")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Preco { get; set; }

    [JsonPropertyName("description")]
    public string Descricao { get; set; } = string.Empty;
}
