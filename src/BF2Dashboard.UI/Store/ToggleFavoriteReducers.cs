using BF2Dashboard.Domain.BattlefieldApi;
using Fluxor;

namespace BF2Dashboard.UI.Store;

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

        var newState = new FavoriteServerListState(newList);
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