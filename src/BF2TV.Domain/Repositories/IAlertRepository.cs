using BF2TV.Domain.Models.Alerts;

namespace BF2TV.Domain.Repositories;

public interface IAlertRepository
{
    Task Add(ConditionId conditionId);
    Task Remove(ConditionId conditionId);
}