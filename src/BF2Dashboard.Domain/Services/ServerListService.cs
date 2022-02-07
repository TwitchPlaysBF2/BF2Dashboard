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