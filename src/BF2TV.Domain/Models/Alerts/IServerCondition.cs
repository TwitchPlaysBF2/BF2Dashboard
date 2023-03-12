using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Models.Alerts;

public interface IServerCondition
{
    ConditionId ConditionId { get; }
    IAlert ResultingAlert { get; }
    bool IsFulfilled(Server server);
}