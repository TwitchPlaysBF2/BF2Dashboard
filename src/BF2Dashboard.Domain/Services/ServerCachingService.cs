using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.Domain.Services;

public class ServerCachingService
{
    public void SaveToCache(List<Server> servers)
    {
        CachedServers = servers;
    }

    public Server? GetFromCache(string? serverGuid)
    {
        if (serverGuid == null)
            return null;

        return CachedServers.FirstOrDefault(HasSameGuid);

        bool HasSameGuid(Server server) =>
            string.Equals(server.Guid, serverGuid, StringComparison.CurrentCultureIgnoreCase);
    }

    private List<Server> CachedServers { get; set; } = new();
}