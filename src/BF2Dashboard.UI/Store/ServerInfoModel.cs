﻿using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.UI.Store;

public class ServerInfoModel
{
    public string ServerName { get; private init; }

    public string MapName { get; private init; }

    public int CurrentPlayerCountWithoutBots { get; private init; }

    public int MaxPlayerCount { get; private init; }

    public string JoinLink { get; private init; }

    public static ServerInfoModel FromServer(Server server)
    {
        return new ServerInfoModel
        {
            ServerName = server.Name,
            MapName = server.MapName,
            CurrentPlayerCountWithoutBots = server.NumPlayersWithoutBots,
            MaxPlayerCount = (int?)server.MaxPlayers ?? 0,
            JoinLink = server.JoinLink,
        };
    }
}