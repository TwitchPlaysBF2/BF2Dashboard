using BF2TV.Domain.BattlefieldApi;
using Fluxor;

namespace BF2TV.Frontend.Store;

public class SetFavoriteServerListAction
{
    public List<Server>? Servers { get; }

    public SetFavoriteServerListAction(List<Server>? servers)
    {
        Servers = servers;
    }
}

public class SetFavoriteServerListReducer
{
    [ReducerMethod]
    public FavoriteServerListState Reduce(FavoriteServerListState oldState, SetFavoriteServerListAction action)
    {
        return new FavoriteServerListState(action.Servers);
    }
}