using BF2Dashboard.Domain.BattlefieldApi;
using Fluxor;

namespace BF2Dashboard.UI.Store;

public class SetFullServerListAction
{
    public List<Server> Servers { get; }

    public SetFullServerListAction(List<Server> servers)
    {
        Servers = servers;
    }
}

public class SetFullServerListReducer
{
    [ReducerMethod]
    public FullServerListState SetFullServerList(FullServerListState oldState, SetFullServerListAction action)
    {
        return new FullServerListState(action.Servers);
    }
}