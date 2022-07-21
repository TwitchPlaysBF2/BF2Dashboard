using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Repositories;

namespace BF2TV.Domain.Services;

public class ServerListService : IServerListService
{
    public async Task<List<Server>> GetServerList()
    {
        try
        {
            // TODO Try and cache this somehow for session (mainly to prevent F5 bombs but it should be fetch-able again after 20s)
            var serverList = await ServerListRepository.QueryServerList();

            var result = serverList
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