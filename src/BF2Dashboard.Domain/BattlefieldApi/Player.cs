using System.Text.Json.Serialization;

namespace BF2Dashboard.Domain.BattlefieldApi;

#pragma warning disable CS8618
[Serializable]
public class Player
{
    [JsonPropertyName("pid")]
    public int Pid { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("tag")]
    public string Tag { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("kills")]
    public int Kills { get; set; }

    [JsonPropertyName("deaths")]
    public int Deaths { get; set; }

    [JsonPropertyName("ping")]
    public int Ping { get; set; }

    [JsonPropertyName("team")]
    public int Team { get; set; }

    [JsonPropertyName("teamLabel")]
    public string TeamLabel { get; set; }

    [JsonPropertyName("aibot")]
    public bool Aibot { get; set; }
}
#pragma warning restore CS8618