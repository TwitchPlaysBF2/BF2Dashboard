using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.UI.Store;

public class FullServerListState
{
    public List<Server>? ServerList { get; }

    public FullServerListState(List<Server>? serverList)
    {
        ServerList = serverList;
    }
}