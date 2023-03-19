using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Store;
using Fluxor;

namespace BF2TV.Frontend.Services.Alerts;

public class AlertGenerationService : IAlertGenerationService
{
    private readonly IDispatcher _dispatcher;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IState<AlertStore.State> _alertState;
    private readonly IConditionStatusTracker _conditionStatusTracker;

    public AlertGenerationService(IDispatcher dispatcher,
        IDateTimeProvider dateTimeProvider,
        IState<AlertStore.State> alertState,
        IConditionStatusTracker conditionStatusTracker)
    {
        _dispatcher = dispatcher;
        _dateTimeProvider = dateTimeProvider;
        _alertState = alertState;
        _conditionStatusTracker = conditionStatusTracker;
    }

    public void Generate(List<Server> fullServerList)
    {
        foreach (var server in fullServerList)
        {
            ForServer(server);
        }
    }

    private void ForServer(Server server)
    {
        foreach (var condition in _alertState.Value.FriendIsOnServerConditions)
        {
            ForCondition(server, condition);
        }
    }

    private void ForCondition(Server server, FriendIsOnServerCondition condition)
    {
        if (!condition.IsEnabled)
            return;

        if (!condition.IsFulfilled(_dateTimeProvider, server, out var result))
            return;

        if (_conditionStatusTracker.TrackUnlessAlreadyExists(result))
            return;

        _dispatcher.Dispatch(new AlertStore.Actions.SendAlert(result));
    }
}