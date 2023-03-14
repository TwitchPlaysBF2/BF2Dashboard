using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Repositories;
using Fluxor;

namespace BF2TV.Frontend.Store;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class AlertStore
{
    [FeatureState]
    public class State
    {
        public IImmutableList<IAlert> AlertHistory { get; }
        public IImmutableList<FriendIsOnServerCondition> FriendIsOnServerConditions { get; }

        public State()
        {
            AlertHistory = ImmutableList.Create<IAlert>();
            FriendIsOnServerConditions = ImmutableList.Create<FriendIsOnServerCondition>();
        }

        public State(
            IImmutableList<IAlert> alertHistory,
            IImmutableList<FriendIsOnServerCondition> friendIsOnServerConditions)
        {
            AlertHistory = alertHistory;
            FriendIsOnServerConditions = friendIsOnServerConditions;
        }
    }

    public class Actions
    {
        public class FriendIsOnServerConditions
        {
            public record StartLoading;

            public record FinishLoading(IImmutableList<FriendIsOnServerCondition> Conditions);

            public record Add(FriendIsOnServerCondition Condition);

            public record Remove(FriendIsOnServerCondition Condition);
        }

        public record RunAlertGeneration(List<Server> FullServerList);

        public record SendAlert(IAlert Alert);
    }

    public class Reducers
    {
        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.FinishLoading action)
        {
            return new State(oldState.AlertHistory, action.Conditions);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.Add action)
        {
            var conditions = oldState.FriendIsOnServerConditions.Add(action.Condition);
            return new State(oldState.AlertHistory, conditions);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.Remove action)
        {
            var conditions = oldState.FriendIsOnServerConditions.Remove(action.Condition);
            return new State(oldState.AlertHistory, conditions);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.SendAlert action)
        {
            var alertHistory = oldState.AlertHistory.Add(action.Alert);
            return new State(alertHistory, oldState.FriendIsOnServerConditions);
        }
    }

    public class Effects
    {
        private readonly IState<State> _alertState;
        private readonly IJsonRepository<FriendIsOnServerCondition> _jsonRepository;

        public Effects(IState<State> alertState, IJsonRepository<FriendIsOnServerCondition> jsonRepository)
        {
            _alertState = alertState;
            _jsonRepository = jsonRepository;
        }

        [EffectMethod]
        public async Task Handle(Actions.RunAlertGeneration action, IDispatcher dispatcher)
        {
            await Task.Run(() =>
            {
                foreach (var server in action.FullServerList)
                {
                    foreach (var condition in _alertState.Value.FriendIsOnServerConditions)
                    {
                        if (condition.IsFulfilled(server, out var resultingAlert))
                        {
                            dispatcher.Dispatch(new Actions.SendAlert(resultingAlert!));
                        }
                    }
                }
            });
        }

        [EffectMethod]
        public async Task Handle(Actions.FriendIsOnServerConditions.StartLoading action, IDispatcher dispatcher)
        {
            var conditions = await _jsonRepository.GetAll();
            dispatcher.Dispatch(new Actions.FriendIsOnServerConditions.FinishLoading(conditions.ToImmutableList()));
        }

        [EffectMethod]
        public async Task Handle(Actions.FriendIsOnServerConditions.Add action, IDispatcher dispatcher)
        {
            await _jsonRepository.Add(action.Condition);
        }

        [EffectMethod]
        public async Task Handle(Actions.FriendIsOnServerConditions.Remove action, IDispatcher dispatcher)
        {
            await _jsonRepository.Remove(action.Condition);
        }
    }
}