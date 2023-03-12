using System.Collections.Immutable;
using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Models.Alerts;
using Fluxor;

namespace BF2TV.Frontend.Store;

// ReSharper disable ClassNeverInstantiated.Global
public class AlertStore
{
    [FeatureState]
    public class State
    {
        public IImmutableList<IAlert> AlertHistory { get; }
        public IImmutableList<IServerCondition> ActiveServerConditions { get; }

        public State()
        {
            AlertHistory = ImmutableList.Create<IAlert>();
            ActiveServerConditions = ImmutableList.Create<IServerCondition>();
        }

        public State(IImmutableList<IServerCondition> activeServerConditions, IImmutableList<IAlert> alertHistory)
        {
            AlertHistory = alertHistory;
            ActiveServerConditions = activeServerConditions;
        }
    }

    public class Actions
    {
        public record AddCondition(IServerCondition ServerCondition);

        public record RemoveCondition(IServerCondition ServerCondition);

        public record RunAlertGeneration(List<Server> FullServerList);

        public record SendAlert(IAlert Alert);
    }

    public class Reducers
    {
        [ReducerMethod]
        public State Reduce(State oldState, Actions.AddCondition action)
        {
            var conditions = oldState.ActiveServerConditions.Add(action.ServerCondition);
            return new State(conditions, oldState.AlertHistory);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.RemoveCondition action)
        {
            var conditions = oldState.ActiveServerConditions.Remove(action.ServerCondition);
            return new State(conditions, oldState.AlertHistory);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.SendAlert action)
        {
            var alertHistory = oldState.AlertHistory.Add(action.Alert);
            return new State(oldState.ActiveServerConditions, alertHistory);
        }
    }

    public class Effects
    {
        private readonly IState<State> _alertState;

        public Effects(IState<State> alertState)
        {
            _alertState = alertState;
        }

        [EffectMethod]
        public async Task Handle(Actions.RunAlertGeneration action, IDispatcher dispatcher)
        {
            await Task.Run(() =>
            {
                foreach (var server in action.FullServerList)
                {
                    foreach (var condition in _alertState.Value.ActiveServerConditions)
                    {
                        if (condition.IsFulfilled(server))
                        {
                            dispatcher.Dispatch(new Actions.SendAlert(condition.ResultingAlert));
                        }
                    }
                }
            });
        }
    }
}