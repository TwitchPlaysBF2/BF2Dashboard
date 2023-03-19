using BF2TV.Domain.Models.Alerts;

namespace BF2TV.Domain.Services;

public class ConditionStatusTracker : IConditionStatusTracker
{
    private readonly List<ConditionStatusId> _statusHistory = new();

    public bool TrackUnlessAlreadyExists(IConditionStatus status)
    {
        if (_statusHistory.Contains(status.Id))
            return true;
        
        _statusHistory.Add(status.Id);
        return false;
    }
}