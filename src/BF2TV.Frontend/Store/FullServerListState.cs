using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Frontend.Store;

public class FullServerListState
{
    public List<Server>? ServerList { get; }

    public FullServerListState(List<Server>? serverList)
    {
        ServerList = serverList;
    }
}