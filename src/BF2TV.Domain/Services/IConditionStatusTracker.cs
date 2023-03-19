using BF2TV.Domain.Models.Alerts;

namespace BF2TV.Domain.Services;

public interface IConditionStatusTracker
{
    bool TrackUnlessAlreadyExists(IConditionStatus status);
}