namespace BF2TV.Domain.Models.Alerts;

public record FrameIsOnServerStatus(
        DateTime WhenUtc,
        string PlayerName,
        ServerInfoModel ServerInfo)
    : IConditionStatus
{
    public ConditionStatusId Id => ConditionStatusId.Create(this);
    public string AlertTitle => "BF2.TV Friend Alert";
    public string AlertText => $"{PlayerName} joined: \n{ServerInfo.ServerName}";
}