using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Models.Alerts;

public class FriendIsOnServerCondition : IServerCondition
{
    public string ConditionIdentifier => FriendIdentity;
    public IAlert ResultingAlert { get; private set; }
    public string FriendIdentity { get; }

    public FriendIsOnServerCondition(string friendIdentity)
    {
        FriendIdentity = friendIdentity;
    }

    public bool IsFulfilled(Server server)
    {
        if (server?.Players == null)
            return false;

        var isPlayingOnServer = server.Players.Any(x => x.FullName == FriendIdentity);
        if (!isPlayingOnServer) 
            return false;

        ResultingAlert = new FriendCameOnlineAlert(FriendIdentity, ServerInfoModel.FromServer(server), DateTime.UtcNow);
        return isPlayingOnServer;
    }
}