using BF2Dashboard.Domain.BattlefieldApi;
using BF2Dashboard.Domain.Repositories;

namespace BF2Dashboard.Domain.Services;

public static class ServerListService
{
    public static async Task<List<Server>> GetServerList()
    {
        try
        {
            // TODO Try and cache this somehow for session (mainly to prevent F5 bombs but it should be fetch-able again after 20s)
            var serverList = await ServerListRepository.QueryServerList();

            var result = serverList
                .Where(s => s.NumPlayersWithoutBots > 0)
                .OrderByDescending(s => s.NumPlayersWithoutBots)
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