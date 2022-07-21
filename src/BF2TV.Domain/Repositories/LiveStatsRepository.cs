using System.Runtime.Serialization;
using System.Text.Json;
using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Repositories;

/// <summary>
///     https://bflist.io/api-endpoints/battlefield-2/
/// </summary>
public class LiveStatsRepository : HttpRepositoryBase
{
    public static async Task<LiveStats> GetLiveStats()
    {
        var respsonse = await HttpClient.GetAsync($"{Constants.ApiBaseUrl}/livestats");
        var json = await respsonse.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<LiveStats>(json, JsonOptions)
                     ?? throw new SerializationException(json);

        return result;
    }
}