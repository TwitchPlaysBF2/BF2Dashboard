using BF2Dashboard.Domain.BattlefieldApi;
using BF2Dashboard.Domain.Services;
using Blazored.LocalStorage;
using Fluxor;

namespace BF2Dashboard.UI.Store;

public class LoadServerListAction
{
}

public class InitializeServerListsEffect
{
    private readonly IServerListService _serverListService;
    private readonly ILocalStorageService _localStorageService;

    public InitializeServerListsEffect(IServerListService serverListService, ILocalStorageService localStorageService)
    {
        _serverListService = serverListService;
        _localStorageService = localStorageService;
    }

    [EffectMethod(actionType: typeof(LoadServerListAction))]
    public async Task InitializeServerLists(IDispatcher dispatcher)
    {
        var fullServerList = await _serverListService.GetServerList();
        dispatcher.Dispatch(new SetFullServerListAction(fullServerList));

        var favoriteServerIds = await _localStorageService.GetItemAsync<List<string>>(Commons.FavoriteServerListKey);
        var favorites = fullServerList?
            .Where(s => favoriteServerIds?.Contains(s.Guid) == true)
            ?.Select(s =>
            {
                s.IsPinned = true;
                return s;
            })
            ?.ToList();

        dispatcher.Dispatch(new SetFavoriteServerListAction(favorites ?? new List<Server>()));
    }
}