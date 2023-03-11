using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.Domain.Models.Alerts;

public interface IServerCondition
{
    string ConditionIdentifier { get; }
    bool IsFulfilled(Server server);
}