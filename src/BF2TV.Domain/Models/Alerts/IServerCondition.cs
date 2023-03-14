using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Models.Alerts;

public interface IServerCondition
{
    ConditionId ConditionId { get; }
    bool IsFulfilled(Server server, out IAlert? resultingAlert);
}