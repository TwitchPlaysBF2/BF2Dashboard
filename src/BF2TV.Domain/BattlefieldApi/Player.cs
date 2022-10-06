using System;
using System.Text.Json.Serialization;

namespace BF2TV.Domain.BattlefieldApi;

#pragma warning disable CS8618
[Serializable]
public class Player
{
    public bool IsHuman => Aibot != true && (Ping.GetValueOrDefault() > 0 || Score.GetValueOrDefault() != 0 ||
                                             Kills.GetValueOrDefault() != 0 || Deaths.GetValueOrDefault() != 0);

    public bool IsFriend { get; set; } = false;

    public string FullName => Tag + " " + Name;

    // Long names of player may be cut off if used in combination with a clan tag/prefix,
    // meaning linking to the cut-off name would lead to a 404 => link to profile based on pid instead
    public string ProfileUrl => $"https://www.bf2hub.com/stats/{Pid}";
    
    [JsonPropertyName("pid")]
    public int? Pid { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("tag")]
    public string Tag { get; set; }

    [JsonPropertyName("score")]
    public int? Score { get; set; }

    [JsonPropertyName("kills")]
    public int? Kills { get; set; }

    [JsonPropertyName("deaths")]
    public int? Deaths { get; set; }

    [JsonPropertyName("ping")]
    public int? Ping { get; set; }

    [JsonPropertyName("team")]
    public int? Team { get; set; }

    [JsonPropertyName("teamLabel")]
    public string TeamLabel { get; set; }

    [JsonPropertyName("aibot")]
    public bool? Aibot { get; set; }
}
#pragma warning restore CS8618