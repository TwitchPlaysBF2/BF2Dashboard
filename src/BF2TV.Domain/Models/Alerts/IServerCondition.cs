using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Services;

namespace BF2TV.Domain.Models.Alerts;

public interface IServerCondition
{
    ConditionId Id { get; }
    bool IsFulfilled(IDateTimeProvider dateTimeProvider, Server server, out IConditionStatus result);
}