using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BF2TV.Domain.GeoApi;

namespace BF2TV.Domain.BattlefieldApi;

#pragma warning disable CS8618
[Serializable]
public class Server
{
    public bool IsPinned { get; set; }

    public int NumPlayersWithoutBots => Players?.Count(p => p.IsHuman) ?? 0;

    public string IpAndPort => Ip + ":" + Port;

    public string MapNameAndSize => $"{MapName} (x{MapSize})";

    public GeoLocation? GeoLocation { get; set; }

    [JsonPropertyName("guid")]
    public string Guid { get; set; }

    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    [JsonPropertyName("port")]
    public float? Port { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("numPlayers")]
    private float? NumPlayers { get; set; }

    [JsonPropertyName("maxPlayers")]
    public float? MaxPlayers { get; set; }

    [JsonPropertyName("mapName")]
    public string MapName { get; set; }

    [JsonPropertyName("mapSize")]
    public float MapSize { get; set; }

    [JsonPropertyName("password")]
    public bool? Password { get; set; }

    [JsonPropertyName("gameType")]
    public string GameType { get; set; }

    [JsonPropertyName("gameVersion")]
    public string GameVersion { get; set; }

    [JsonPropertyName("gameVariant")]
    public string GameVariant { get; set; }

    [JsonPropertyName("timelimit")]
    public float? Timelimit { get; set; }

    [JsonPropertyName("roundsPerMap")]
    public float? RoundsPerMap { get; set; }

    [JsonPropertyName("ranked")]
    public bool? Ranked { get; set; }

    [JsonPropertyName("anticheat")]
    public bool? Anticheat { get; set; }

    [JsonPropertyName("battlerecorder")]
    public bool? Battlerecorder { get; set; }

    [JsonPropertyName("demoIndex")]
    public string DemoIndex { get; set; }

    [JsonPropertyName("demoDownload")]
    public string DemoDownload { get; set; }

    [JsonPropertyName("voip")]
    public bool? Voip { get; set; }

    [JsonPropertyName("autobalance")]
    public bool? Autobalance { get; set; }

    [JsonPropertyName("friendlyfire")]
    public bool? Friendlyfire { get; set; }

    [JsonPropertyName("tkmode")]
    public string Tkmode { get; set; }

    [JsonPropertyName("startdelay")]
    public float? Startdelay { get; set; }

    [JsonPropertyName("spawntime")]
    public float? Spawntime { get; set; }

    [JsonPropertyName("sponsorText")]
    public string SponsorText { get; set; }

    [JsonPropertyName("sponsorLogoUrl")]
    public string SponsorLogoUrl { get; set; }

    [JsonPropertyName("communityLogoUrl")]
    public string CommunityLogoUrl { get; set; }

    [JsonPropertyName("scorelimit")]
    public float? Scorelimit { get; set; }

    [JsonPropertyName("ticketratio")]
    public float? Ticketratio { get; set; }

    [JsonPropertyName("teamratio")]
    public float? Teamratio { get; set; }

    [JsonPropertyName("team1")]
    public string Team1 { get; set; }

    [JsonPropertyName("team2")]
    public string Team2 { get; set; }

    [JsonPropertyName("pure")]
    public bool? Pure { get; set; }

    [JsonPropertyName("globalUnlocks")]
    public bool? GlobalUnlocks { get; set; }

    [JsonPropertyName("reservedSlots")]
    public float? ReservedSlots { get; set; }

    [JsonPropertyName("dedicated")]
    public bool? Dedicated { get; set; }

    [JsonPropertyName("os")]
    public string Os { get; set; }

    [JsonPropertyName("bots")]
    public bool? Bots { get; set; }

    [JsonPropertyName("fps")]
    public float? Fps { get; set; }

    [JsonPropertyName("plasma")]
    public bool? Plasma { get; set; }

    [JsonPropertyName("coopBotRatio")]
    public float? CoopBotRatio { get; set; }

    [JsonPropertyName("coopBotCount")]
    public float? CoopBotCount { get; set; }

    [JsonPropertyName("coopBotDiff")]
    public float? CoopBotDiff { get; set; }

    [JsonPropertyName("noVehicles")]
    public bool? NoVehicles { get; set; }

    [JsonPropertyName("joinLink")]
    public string? JoinLink { get; set; }

    [JsonPropertyName("joinLinkWeb")]
    public string? JoinLinkWeb { get; set; }

    [JsonPropertyName("teams")]
    public List<Team>? Teams { get; set; }

    [JsonPropertyName("players")]
    public List<Player>? Players { get; set; }
}
#pragma warning restore CS8618