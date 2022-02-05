using System.Text.Json.Serialization;

namespace BF2Dashboard.Domain.BattlefieldApi;

#pragma warning disable CS8618
[Serializable]
public class LiveStats
{
    [JsonPropertyName("servers")]
    public int Servers { get; set; }

    [JsonPropertyName("players")]
    public int Players { get; set; }
}
#pragma warning restore CS8618