using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Models.Alerts;

public class FriendIsOnServerCondition : IServerCondition, IEquatable<FriendIsOnServerCondition>
{
    public ConditionId ConditionId => ConditionId.Create(this);
    public string FriendIdentity { get; }

    public FriendIsOnServerCondition(string friendIdentity)
    {
        FriendIdentity = friendIdentity;
    }

    public bool IsFulfilled(Server server, out IAlert? resultingAlert)
    {
        resultingAlert = null;

        if (server?.Players == null)
            return false;

        var isPlayingOnServer = server.Players.Any(x => x.FullName == FriendIdentity);
        if (!isPlayingOnServer)
            return false;

        resultingAlert = new FriendCameOnlineAlert(FriendIdentity, ServerInfoModel.FromServer(server), DateTime.UtcNow);
        return true;
    }

    public bool Equals(FriendIsOnServerCondition? other) => other?.ConditionId.Id == ConditionId.Id;
}