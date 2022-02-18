using BF2Dashboard.Domain.BattlefieldApi;
using Blazored.LocalStorage;

namespace BF2Dashboard.Domain.Services;

public class ServerHandlingService
{
    private readonly string _commonPersistenceKey = "pinnedservers";
    private readonly ILocalStorageService _localStorageService;
    private readonly ServerCachingService _serverCachingService;
    public List<Server> FavoriteServers = new();
    public List<Server> AllServers = new();
    public event Action OnToggle;

    public ServerHandlingService(ILocalStorageService localStorageService, ServerCachingService serverCachingService)
    {
        _localStorageService = localStorageService;
        _serverCachingService = serverCachingService;
    }

    public async Task TogglePin(string serverGuid)
    {
        var pinnedServer = FavoriteServers.FirstOrDefault(HasSameGuid);
        if (pinnedServer != null)
        {
            pinnedServer.IsPinned = !pinnedServer.IsPinned;
            FavoriteServers.Remove(pinnedServer);
            AllServers.Add(pinnedServer);
        }

        var nonPinnedServer = AllServers.FirstOrDefault(HasSameGuid);
        if (nonPinnedServer != null)
        {
            nonPinnedServer.IsPinned = !nonPinnedServer.IsPinned;
            FavoriteServers.Add(nonPinnedServer);
            // AllServers.Remove(nonPinnedServer);
        }

        OnToggle();
        
        await _localStorageService.SetItemAsync(nameof(FavoriteServers), FavoriteServers);

        bool HasSameGuid(Server server) =>
            string.Equals(server.Guid, serverGuid, StringComparison.CurrentCultureIgnoreCase);
    }

    public async Task Initialize()
    {
        if (HasAlreadyBeenInitialized())
            return;
        
        var result = await _localStorageService.GetItemAsync<List<Server>>(_commonPersistenceKey);
        if (result != null)
        {
            _serverCachingService.SaveToCache(result);
            FavoriteServers = result;
        }

        AllServers = await ServerListService.GetServerList();
        _serverCachingService.SaveToCache(AllServers);
        UpdateOrRemovePinnedServerInstances(AllServers);

        bool HasAlreadyBeenInitialized() => !AllServers.Any() && !FavoriteServers.Any();
    }

    private void UpdateOrRemovePinnedServerInstances(IReadOnlyCollection<Server> freshlyUpdatedServerInstances)
    {
        for (var i = 0; i < FavoriteServers!.Count; i++)
        {
            var updatedPinnedServerInstance =
                freshlyUpdatedServerInstances.FirstOrDefault(s => s.Guid == FavoriteServers[i].Guid);
            if (updatedPinnedServerInstance != null)
            {
                FavoriteServers[i] = updatedPinnedServerInstance;
                FavoriteServers[i].IsPinned = true;
                // AllServers!.Remove(
                //     updatedPinnedServerInstance); // avoiding duplication since serverGuid gets 'moved' up
            }
            else
            {
                // TODO this should only "disable" the server, not delete. Otherwise user will have to re-favorite it everytime it went offline
                FavoriteServers.Remove(FavoriteServers[i]); // serverGuid is gone, we don't render old instances
            }
        }
    }
}