using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Frontend.Store.FriendList;

public class FriendModel
{
    public bool IsOnline { get; set; }

    public string DisplayName { get; private init; }

    public string ProfileUrl => FindProfileUrl();

    public override string ToString() => DisplayName;

    public Player? Player { get; private init; }

    public ServerInfoModel? ServerInfo { get; private init; }

    public static FriendModel CreateOnlineFriend(Player player, Server server)
    {
        return new FriendModel()
        {
            IsOnline = true,
            DisplayName = player.FullName,
            Player = player,
            ServerInfo = ServerInfoModel.FromServer(server),
        };
    }

    public static FriendModel CreateOfflineFriend(string displayName)
    {
        return new FriendModel()
        {
            IsOnline = false,
            DisplayName = displayName,
            Player = null,
            ServerInfo = null,
        };
    }

    private FriendModel()
    {
    }

    private string FindProfileUrl()
    {
        return Player?.ProfileUrl ?? $"https://www.bf2hub.com/player/{PlayerNameWithoutPrefix()}";

        // TODO: Resolve persisted friend name (without prefix), once friendlist-persistence is is more than just 1x string
        string PlayerNameWithoutPrefix()
        {
            // We need the playername without prefix. As of now, Player instance isn't loaded for offline friends.
            // (Quick & dirty, until the terrible friendlist persistence is done better)
            // (iirc, I ran into performance issues persisiting more than 1x string in local browser storage)
            return DisplayName.Trim().Split(' ').LastOrDefault() ?? DisplayName;
        }
    }
}