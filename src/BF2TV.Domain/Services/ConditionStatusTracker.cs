using BF2TV.Domain.Models.Alerts;

namespace BF2TV.Domain.Services;

public class ConditionStatusTracker : IConditionStatusTracker
{
    private readonly List<ConditionStatusId> _statusHistory = new();

    public bool IsNewStatus(IConditionStatus status)
    {
        if (_statusHistory.Contains(status.Id))
            return false;
        
        _statusHistory.Add(status.Id);
        return true;
    }
}