namespace BF2TV.Domain.Models.Alerts;

public interface IConditionStatus
{
    ConditionStatusId Id { get; }
    DateTime WhenUtc { get; }
    string AlertText { get; }
    string AlertTitle { get; }
}