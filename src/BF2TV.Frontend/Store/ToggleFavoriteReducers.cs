using BF2TV.Domain.BattlefieldApi;
using Fluxor;

namespace BF2TV.Frontend.Store;

public class ToggleFavoriteReducers
{
    [ReducerMethod]
    public FavoriteServerListState UpdateFavoriteServerList(FavoriteServerListState oldState, ToggleFavoriteAction action)
    {
        var newList = oldState.ServerList ?? new List<Server>();
        
        if (action.Server.IsPinned)
            newList.Remove(action.Server);
        else
            newList.Add(action.Server);

        var reorderedServerList = newList.OrderByDescending(s => s.NumPlayersWithoutBots).ToList();

        var newState = new FavoriteServerListState(reorderedServerList);
        return newState;
    }

    [ReducerMethod]
    public FullServerListState OnToggleFullServerListItem(FullServerListState oldState, ToggleFavoriteAction action)
    {
        if (oldState.ServerList == null)
            throw new InvalidOperationException("Couldn't update a server of an empty server list");

        var server = oldState.ServerList.First(s => s.Guid == action.Server.Guid);
        server.IsPinned = !server.IsPinned;

        return oldState;
    }
}