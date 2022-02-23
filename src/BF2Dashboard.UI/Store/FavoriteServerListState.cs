using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.UI.Store;

public class FavoriteServerListState
{
    public List<Server>? ServerList { get; }

    public FavoriteServerListState(List<Server>? serverList)
    {
        ServerList = serverList;
    }
}