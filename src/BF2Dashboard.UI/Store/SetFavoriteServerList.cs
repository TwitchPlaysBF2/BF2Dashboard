using BF2Dashboard.Domain.BattlefieldApi;
using Fluxor;

namespace BF2Dashboard.UI.Store;

public class SetFavoriteServerListAction
{
    public List<Server> Servers { get; }

    public SetFavoriteServerListAction(List<Server> servers)
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