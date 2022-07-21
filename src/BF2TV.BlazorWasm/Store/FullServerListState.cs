using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.BlazorWasm.Store;

public class FullServerListState
{
    public List<Server>? ServerList { get; }

    public FullServerListState(List<Server>? serverList)
    {
        ServerList = serverList;
    }
}