using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Commands;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Infrastructure;
using Blazored.LocalStorage;
using Fluxor;
using MediatR;

namespace BF2TV.Frontend.Store;

public class InitializeServerListsAction
{
}

public class InitializeServerListsEffect
{
    private readonly IServerListService _serverListService;
    private readonly ILocalStorageService _localStorageService;
    private readonly IEnvironment _environment;
    private readonly IMediator _mediator;

    public InitializeServerListsEffect(
        IServerListService serverListService, 
        ILocalStorageService localStorageService,
        IEnvironment environment,
        IMediator mediator)
    {
        _serverListService = serverListService;
        _localStorageService = localStorageService;
        _environment = environment;
        _mediator = mediator;
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
            ?.ToList()
            ?? new List<Server>();

        dispatcher.Dispatch(new SetFavoriteServerListAction(favorites));

        if (_environment.IsApp())
            await _mediator.Send(new RaiseFavoriteServerListToApp(favorites));
    }
}