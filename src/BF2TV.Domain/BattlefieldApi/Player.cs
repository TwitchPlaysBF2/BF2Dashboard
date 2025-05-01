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

    // When players join online server with local profiles, they receive a pid of 0.
    // It makes no sense to link to them, so only provide a ProfileUrl for players with a non-zero pid.
    public string? ProfileUrl => Pid > 0 ? $"https://playerpath.link/p/{Pid}" : null;
    
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