using BF2TV.Domain.BattlefieldApi;
using Fluxor;

namespace BF2TV.BlazorWasm.Store;

public class SetFullServerListAction
{
    public List<Server>? ServerList { get; }

    public SetFullServerListAction(List<Server>? serverList)
    {
        ServerList = serverList;
    }
}

public class SetFullServerListReducer
{
    [ReducerMethod]
    public FullServerListState SetFullServerList(FullServerListState oldState, SetFullServerListAction action)
    {
        return new FullServerListState(action.ServerList);
    }
}