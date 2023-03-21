using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Repositories;

namespace BF2TV.Domain.Services;

public class ConditionStatusTracker : IConditionStatusTracker
{
    private readonly IJsonRepository<ConditionStatusId> _repository;

    public ConditionStatusTracker(IJsonRepository<ConditionStatusId> repository)
    {
        _repository = repository;
    }

    public bool TrackUnlessAlreadyExists(IConditionStatus status)
    {
        var trackedIds = _repository.GetAll().Result;
        if (trackedIds.Contains(status.Id))
            return true;

        _repository.Add(status.Id);
        return false;
    }
}