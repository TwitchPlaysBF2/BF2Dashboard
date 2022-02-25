using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.UI.Store;

public class ServerInfoModel
{
    public string Name { get; private init; }

    public string Map { get; private init; }

    public int NumPlayersWithoutBots { get; private init; }

    public int MaxPlayers { get; private init; }

    public static ServerInfoModel FromServer(Server server)
    {
        return new ServerInfoModel
        {
            Name = server.Name,
            Map = server.MapName,
            NumPlayersWithoutBots = server.NumPlayersWithoutBots,
            MaxPlayers = server.MaxPlayers,
        };
    }
}