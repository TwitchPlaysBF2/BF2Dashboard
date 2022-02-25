using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.UI.Store.FriendList;

public class FriendModel
{
    public Player Player { get; private init; }

    public ServerInfoModel ServerInfo { get; private init; }

    public static FriendModel Create(Player player, Server server)
    {
        return new FriendModel()
        {
            Player = player,
            ServerInfo = ServerInfoModel.FromServer(server),
        };
    }
}