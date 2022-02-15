using BF2Dashboard.Domain.BattlefieldApi;
using Blazored.LocalStorage;

namespace BF2Dashboard.Domain.Services;

public class ServerHandlingService
{
    private readonly string _commonPersistenceKey = "pinnedservers";
    private readonly ILocalStorageService _localStorageService;
    private readonly ServerCachingService _serverCachingService;
    public List<Server> PinnedServers = new();
    public List<Server> AllServers = new();

    public ServerHandlingService(ILocalStorageService localStorageService, ServerCachingService serverCachingService)
    {
        _localStorageService = localStorageService;
        _serverCachingService = serverCachingService;
    }

    public async Task TogglePin(string serverGuid)
    {
        var pinnedServer = PinnedServers.FirstOrDefault(HasSameGuid);
        if (pinnedServer != null)
        {
            pinnedServer.IsPinned = !pinnedServer.IsPinned;
            PinnedServers.Remove(pinnedServer);
            AllServers.Add(pinnedServer);
        }

        var nonPinnedServer = AllServers.FirstOrDefault(HasSameGuid);
        if (nonPinnedServer != null)
        {
            nonPinnedServer.IsPinned = !nonPinnedServer.IsPinned;
            PinnedServers.Add(nonPinnedServer);
            AllServers.Remove(nonPinnedServer);
        }

        await _localStorageService.SetItemAsync(nameof(PinnedServers), PinnedServers);

        bool HasSameGuid(Server server) =>
            string.Equals(server.Guid, serverGuid, StringComparison.CurrentCultureIgnoreCase);
    }

    public async Task Initialize()
    {
        var result = await _localStorageService.GetItemAsync<List<Server>>(_commonPersistenceKey);
        if (result != null)
        {
            _serverCachingService.SaveToCache(result);
            PinnedServers = result;
        }

        AllServers = await ServerListService.GetServerList();
        _serverCachingService.SaveToCache(AllServers);

        UpdateOrRemovePinnedServerInstances(AllServers);
    }

    private void UpdateOrRemovePinnedServerInstances(IReadOnlyCollection<Server> freshlyUpdatedServerInstances)
    {
        for (var i = 0; i < PinnedServers!.Count; i++)
        {
            var updatedPinnedServerInstance =
                freshlyUpdatedServerInstances.FirstOrDefault(s => s.Guid == PinnedServers[i].Guid);
            if (updatedPinnedServerInstance != null)
            {
                PinnedServers[i] = updatedPinnedServerInstance;
                PinnedServers[i].IsPinned = true;
                AllServers!.Remove(
                    updatedPinnedServerInstance); // avoiding duplication since serverGuid gets 'moved' up
            }
            else
            {
                PinnedServers.Remove(PinnedServers[i]); // serverGuid is gone, we don't render old instances
            }
        }
    }
}