using System.Runtime.Serialization;
using System.Text.Json;
using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.Domain.Repositories;

/// <summary>
///     https://bflist.io/api-endpoints/battlefield-2/
/// </summary>
public class ServerListRepository : HttpRepositoryBase
{
    public static async Task<IReadOnlyList<Server>> GetAllServers()
    {
        var servers = new List<Server>();

        for (var i = 1;; i++)
        {
            Console.WriteLine($"Querying servers, page {i}");
            var pagedResponse = await GetServerListForPage(i);
            servers.AddRange(pagedResponse.Servers);

            if (pagedResponse.IsLastPage || pagedResponse.IsEmpty)
                break;

            await Task.Delay(50); // we don't want to bomb the API with too many requests at once
        }

        var uniqueServers = servers
            .DistinctBy(s => s.Name)
            .ToList();

        Console.WriteLine($"Received {uniqueServers.Count} unique servers in total");
        return uniqueServers;
    }

    private static async Task<PagedServerListResponse> GetServerListForPage(int pageNumber)
    {
        var response = await HttpClient.GetAsync($"{Constants.ApiBaseUrl}/servers/{pageNumber}?perPage=100");
        var json = await response.Content.ReadAsStringAsync();
        var servers = JsonSerializer.Deserialize<List<Server>>(json, JsonOptions)
                      ?? throw new SerializationException(json);

        var maximumKnownPages = response.Headers.GetValues("X-Total-Pages").FirstOrDefault();

        return new PagedServerListResponse(servers, pageNumber, maximumKnownPages);
    }
}