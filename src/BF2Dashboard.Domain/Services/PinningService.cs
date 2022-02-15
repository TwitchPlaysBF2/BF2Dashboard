using BF2Dashboard.Domain.BattlefieldApi;
using Blazored.LocalStorage;

namespace BF2Dashboard.Domain.Services;

public class PinningService
{
    private readonly ServerCachingService _serverCachingService;
    private readonly ILocalStorageService _browserStorage;

    public PinningService(ServerCachingService serverCachingService, ILocalStorageService browserStorage)
    {
        _serverCachingService = serverCachingService;
        _browserStorage = browserStorage;
    }

    public async Task PinServer(string id)
    {
        var serverFromCache = _serverCachingService.GetFromCache(id);
        if (serverFromCache != null)
            serverFromCache.IsPinned = !serverFromCache.IsPinned;
        
        var result = await _browserStorage.GetItemAsync<List<Server>>("_pinnedServers");
        var server = result?.FirstOrDefault(s => s.Guid == serverFromCache?.Guid);
        if (server != null)
        {
            server.IsPinned = !server.IsPinned;
            await _browserStorage.SetItemAsync("_pinnedServers", result);
        }
    }
}