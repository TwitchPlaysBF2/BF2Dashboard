using System.Runtime.Serialization;
using System.Text.Json;
using System.Web;
using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Repositories;

/// <summary>
///     https://bflist.io/api-endpoints/v2/battlefield-2/
/// </summary>
public class ServerListRepository : HttpRepositoryBase
{
    public static async Task<List<Server>> QueryServerList()
    {
        var servers = new List<Server>();

        var cursor = string.Empty;
        var after = string.Empty;
        for (var i = 1;; i++)
        {
            Console.WriteLine($"Querying servers, page {i}");
            var pagedResponse = await QueryServersForPage(cursor, after);
            servers.AddRange(pagedResponse.Servers);

            if (pagedResponse.IsLastPage || pagedResponse.IsEmpty)
                break;

            cursor = pagedResponse.Cursor;
            after = pagedResponse.Servers.Last().IpAndPort;
        }

        var uniqueServers = servers
            .DistinctBy(s => s.Name)
            .ToList();

        Console.WriteLine($"Received {uniqueServers.Count} unique servers in total");
        return uniqueServers;
    }

    private static async Task<PagedServerListResponse> QueryServersForPage(string? cursor, string? after)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        
        // Use maximum page size, https://bflist.io/api-endpoints/v2/#pagination-page-size
        query.Set("perPage", "100");

        // Add pagination parameters if given
        if (!string.IsNullOrEmpty(cursor) && !string.IsNullOrEmpty(after))
        {
            query.Set("cursor", cursor);
            query.Set("after", after);
        }
        
        var endpoint = new UriBuilder(Constants.ApiBaseUrl);
        endpoint.Path += "/servers";
        endpoint.Query = query.ToString();
        
        try
        {
            var response = await HttpClient.GetAsync(endpoint.Uri);
            var json = await response.Content.ReadAsStringAsync();
            var servers = JsonSerializer.Deserialize<PagedServerListResponse>(json, JsonOptions)
                          ?? throw new SerializationException(json);

            return servers;
        }
        catch
        {
            Console.WriteLine($"Failed querying endpoint {endpoint.Uri}");
            throw;
        }
    }
}