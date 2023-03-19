using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Repositories;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Services.Alerts;
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
        public IImmutableList<IConditionStatus> AlertHistory { get; }
        public IImmutableList<FriendIsOnServerCondition> FriendIsOnServerConditions { get; }
        public bool IsLoaded { get; }

        public State()
        {
            AlertHistory = ImmutableList.Create<IConditionStatus>();
            FriendIsOnServerConditions = ImmutableList.Create<FriendIsOnServerCondition>();
            IsLoaded = false;
        }

        public State(
            IImmutableList<IConditionStatus> alertHistory,
            IImmutableList<FriendIsOnServerCondition> friendIsOnServerConditions,
            bool isLoaded)
        {
            AlertHistory = alertHistory;
            FriendIsOnServerConditions = friendIsOnServerConditions;
            IsLoaded = isLoaded;
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

            public record SetEnabledState(FriendIsOnServerCondition Condition, bool NewEnabledState);
        }

        public record RunAlertGeneration(List<Server> FullServerList);

        public record SendAlert(IConditionStatus ConditionStatus);
    }

    public class Reducers
    {
        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.FinishLoading action)
        {
            return new State(oldState.AlertHistory, action.Conditions, isLoaded: true);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.Add action)
        {
            var conditions = oldState.FriendIsOnServerConditions.Insert(0, action.Condition);
            return new State(oldState.AlertHistory, conditions, oldState.IsLoaded);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.Remove action)
        {
            var conditions = oldState.FriendIsOnServerConditions.Remove(action.Condition);
            return new State(oldState.AlertHistory, conditions, oldState.IsLoaded);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.SetEnabledState action)
        {
            var condition = oldState.FriendIsOnServerConditions.FirstOrDefault(x => x.Id.Id == action.Condition.Id.Id);
            if (condition == null)
                return oldState;

            condition.IsEnabled = action.NewEnabledState;
            return new State(oldState.AlertHistory, oldState.FriendIsOnServerConditions, oldState.IsLoaded);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.SendAlert action)
        {
            var alertHistory = oldState.AlertHistory.Insert(0, action.ConditionStatus);
            return new State(alertHistory, oldState.FriendIsOnServerConditions, oldState.IsLoaded);
        }
    }

    public class Effects
    {
        private readonly IAlertGenerationService _alertGenerationService;
        private readonly IJsonRepository<FriendIsOnServerCondition> _jsonRepository;

        public Effects(
            IAlertGenerationService alertGenerationService,
            IJsonRepository<FriendIsOnServerCondition> jsonRepository)
        {
            _alertGenerationService = alertGenerationService;
            _jsonRepository = jsonRepository;
        }

        [EffectMethod]
        public async Task Handle(Actions.RunAlertGeneration action, IDispatcher dispatcher)
        {
            await Task.Run(() => _alertGenerationService.Generate(action.FullServerList));
        }

        [EffectMethod]
        public async Task Handle(Actions.FriendIsOnServerConditions.StartLoading action, IDispatcher dispatcher)
        {
            var conditions = await _jsonRepository.GetAll();
            conditions.Reverse();
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

        [EffectMethod]
        public async Task Handle(Actions.FriendIsOnServerConditions.SetEnabledState action, IDispatcher dispatcher)
        {
            await _jsonRepository.Remove(action.Condition);
            action.Condition.IsEnabled = action.NewEnabledState;
            await _jsonRepository.Add(action.Condition);
        }
    }
}