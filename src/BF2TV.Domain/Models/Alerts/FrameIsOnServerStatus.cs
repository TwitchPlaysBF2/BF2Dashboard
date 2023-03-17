namespace BF2TV.Domain.Models.Alerts;

public record FrameIsOnServerStatus(
        DateTime WhenUtc,
        string PlayerName,
        ServerInfoModel ServerInfo)
    : IConditionStatus
{
    public ConditionStatusId Id => ConditionStatusId.Create(this);
}