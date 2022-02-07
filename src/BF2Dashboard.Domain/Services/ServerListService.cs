using BF2Dashboard.Domain.BattlefieldApi;
using BF2Dashboard.Domain.Repositories;

namespace BF2Dashboard.Domain.Services;

public static class ServerListService
{
    private static IReadOnlyList<Server>? CachedServerList { get; set; }

    public static async Task<IReadOnlyList<Server>> GetServerList()
    {
        try
        {
            // TODO I'M pretty sure there's a bug here caching this across multiple sessions.
            // TODO Try fixing with the DI refactoring by setting a scope per session (main goal is to prevent F5 bombs from querying the API)
            // TODO investigate maybe there's a cache time I could set somehow. So that it gets cached for 20s but then expires and gets fetched again
            if (CachedServerList == null)
                CachedServerList = await ServerListRepository.QueryServerList();

            var result = CachedServerList
                .Where(s => s.NumPlayers > 0)
                .OrderByDescending(s => s.NumPlayers)
                .ToList();

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<Server>();
        }
    }
}