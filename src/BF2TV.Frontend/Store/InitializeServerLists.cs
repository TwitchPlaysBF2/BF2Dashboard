﻿using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Services;
using Blazored.LocalStorage;
using Fluxor;

namespace BF2TV.Frontend.Store;

public class InitializeServerListsAction
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

    [EffectMethod(actionType: typeof(InitializeServerListsAction))]
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