using Blazored.LocalStorage;
using Fluxor;

namespace BF2TV.Frontend.Store;

public class ToggleFavoriteEffects
{
    private readonly ILocalStorageService _localStorageService;

    public ToggleFavoriteEffects(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }
    
    [EffectMethod]
    public async Task PersistFavoriteServerList(ToggleFavoriteAction action, IDispatcher dispatcher)
    {
        var favoriteServerIds = await _localStorageService.GetItemAsync<List<string>>(Commons.FavoriteServerListKey)
                                ?? new List<string>();

        var shouldAdd = action.Server.IsPinned; 
        switch (shouldAdd)
        {
            case true:
                favoriteServerIds.Add(action.Server.Guid);
                break;
            case false:
                favoriteServerIds.Remove(action.Server.Guid);
                break;
        }
        
        await _localStorageService.SetItemAsync(Commons.FavoriteServerListKey, favoriteServerIds);
    }
}