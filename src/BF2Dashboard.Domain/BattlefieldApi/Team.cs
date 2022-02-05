using System.Text.Json.Serialization;

namespace BF2Dashboard.Domain.BattlefieldApi;

#pragma warning disable CS8618
[Serializable]
public class Team
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; }
}
#pragma warning restore CS8618