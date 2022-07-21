using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Frontend.Store;

public class FavoriteServerListState
{
    public List<Server>? ServerList { get; }

    public FavoriteServerListState(List<Server>? serverList)
    {
        ServerList = serverList;
    }
}