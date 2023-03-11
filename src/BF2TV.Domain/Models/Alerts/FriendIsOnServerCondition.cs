using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Models.Alerts;

public class FriendIsOnServerCondition : IServerCondition
{
    public string FullPlayerName { get; }
    public string ConditionIdentifier => FullPlayerName;

    public FriendIsOnServerCondition(string fullPlayerName)
    {
        FullPlayerName = fullPlayerName;
    }

    public bool IsFulfilled(Server server)
    {
        if (server?.Players == null)
            return false;

        var isPlayingOnServer = server.Players.Any(x => x.FullName == FullPlayerName);
        return isPlayingOnServer;
    }
}