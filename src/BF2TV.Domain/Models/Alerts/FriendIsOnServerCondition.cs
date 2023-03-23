using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Services;

namespace BF2TV.Domain.Models.Alerts;

public class FriendIsOnServerCondition : IServerCondition, IEquatable<FriendIsOnServerCondition>
{
    public ConditionId Id => ConditionId.Create(this);
    public string FriendIdentity { get; }
    public bool IsEnabled { get; set; } = true;

    public FriendIsOnServerCondition(string friendIdentity)
    {
        FriendIdentity = friendIdentity;
    }

    public bool IsFulfilled(IDateTimeProvider dateTimeProvider, Server server, out IConditionStatus result)
    {
        result = null!;

        if (server?.Players == null)
            return false;

        var isPlayingOnServer = server.Players.Any(x => x.FullName == FriendIdentity);
        if (!isPlayingOnServer)
            return false;

        result = new FrameIsOnServerStatus(dateTimeProvider.NowUtc, FriendIdentity, ServerInfoModel.FromServer(server));
        return true;
    }

    public bool Equals(FriendIsOnServerCondition? other) => other?.Id.Id == Id.Id;
}