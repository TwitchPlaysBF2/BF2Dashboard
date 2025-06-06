using System.Text.Json.Serialization;

namespace BF2TV.Domain.BattlefieldApi;

#pragma warning disable CS8618
[Serializable]
public class PagedServerListResponse
{
    [JsonPropertyName("servers")]
    public List<Server> Servers { get; set; }

    [JsonPropertyName("cursor")]
    public string Cursor { get; set; }
    
    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }

    public bool IsEmpty => Servers.Count == 0;

    public bool IsLastPage => !HasMore;
}
#pragma warning restore CS8618