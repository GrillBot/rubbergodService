using System.Text.Json.Serialization;

namespace RubbergodService.Data.Models.DirectApi;

public class DirectApiCommand
{
    [JsonPropertyName("method")]
    public string Method { get; set; } = null!;

    [JsonPropertyName("parameters")]
    public Dictionary<string, object> Parameters { get; set; }

    public DirectApiCommand()
    {
        Parameters = new Dictionary<string, object>();
    }
}
