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
        public bool? AreAllAlertsEnabled { get; }

        public State()
        {
            AlertHistory = ImmutableList.Create<IConditionStatus>();
            FriendIsOnServerConditions = ImmutableList.Create<FriendIsOnServerCondition>();
            IsLoaded = false;
            AreAllAlertsEnabled = null;
        }

        public State(
            IImmutableList<IConditionStatus> alertHistory,
            IImmutableList<FriendIsOnServerCondition> friendIsOnServerConditions,
            bool isLoaded,
            bool? areAllAlertsEnabled)
        {
            AlertHistory = alertHistory;
            FriendIsOnServerConditions = friendIsOnServerConditions;
            IsLoaded = isLoaded;
            AreAllAlertsEnabled = areAllAlertsEnabled;
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

        public record ShowAlert(IConditionStatus ConditionStatus);

        public record Notify(IConditionStatus ConditionStatus);

        public class AreAllAlertsEnabled
        {
            public record StartLoadingFromSettings;

            public record FinishLoadingFromSettings(bool AreAllAlertsEnabled);

            public record Toggle(bool AreAllAlertsEnabled);
        }
    }

    public class Reducers
    {
        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.FinishLoading action)
        {
            return new State(oldState.AlertHistory, action.Conditions, isLoaded: true, oldState.AreAllAlertsEnabled);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.Add action)
        {
            var conditions = oldState.FriendIsOnServerConditions.Insert(0, action.Condition);
            return new State(oldState.AlertHistory, conditions, oldState.IsLoaded, oldState.AreAllAlertsEnabled);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.Remove action)
        {
            var conditions = oldState.FriendIsOnServerConditions.Remove(action.Condition);
            return new State(oldState.AlertHistory, conditions, oldState.IsLoaded, oldState.AreAllAlertsEnabled);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.FriendIsOnServerConditions.SetEnabledState action)
        {
            var condition = oldState.FriendIsOnServerConditions.FirstOrDefault(x => x.Id.Id == action.Condition.Id.Id);
            if (condition == null)
                return oldState;

            condition.IsEnabled = action.NewEnabledState;
            return new State(oldState.AlertHistory, oldState.FriendIsOnServerConditions, oldState.IsLoaded,
                oldState.AreAllAlertsEnabled);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.ShowAlert action)
        {
            var alertHistory = oldState.AlertHistory.Insert(0, action.ConditionStatus);
            return new State(alertHistory, oldState.FriendIsOnServerConditions, oldState.IsLoaded,
                oldState.AreAllAlertsEnabled);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.AreAllAlertsEnabled.Toggle action)
        {
            return new State(oldState.AlertHistory, oldState.FriendIsOnServerConditions, oldState.IsLoaded,
                action.AreAllAlertsEnabled);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.AreAllAlertsEnabled.FinishLoadingFromSettings action)
        {
            return new State(oldState.AlertHistory, oldState.FriendIsOnServerConditions, oldState.IsLoaded,
                action.AreAllAlertsEnabled);
        }
    }

    public class Effects
    {
        private readonly IAlertGenerationService _alertGenerationService;
        private readonly INotificationService _notificationService;
        private readonly IJsonRepository<FriendIsOnServerCondition> _jsonRepository;
        private readonly IAlertSettingsService _alertSettingsService;

        public Effects(
            IAlertGenerationService alertGenerationService,
            INotificationService notificationService,
            IJsonRepository<FriendIsOnServerCondition> jsonRepository,
            IAlertSettingsService alertSettingsService)
        {
            _alertGenerationService = alertGenerationService;
            _notificationService = notificationService;
            _jsonRepository = jsonRepository;
            _alertSettingsService = alertSettingsService;
        }

        [EffectMethod]
        public async Task Handle(Actions.RunAlertGeneration action, IDispatcher dispatcher)
        {
            var shouldGenerate = await _alertSettingsService.GetAreAllAlertsEnabled();
            if (!shouldGenerate)
                return;

            await Task.Run(() => _alertGenerationService.Generate(action.FullServerList));
        }

        [EffectMethod]
        public async Task Handle(Actions.Notify action, IDispatcher dispatcher)
        {
            await _notificationService.NotifyAsync(action.ConditionStatus);
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
            await Task.Run(() => _notificationService.RequestPermissions());
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
            await Task.Run(() => _notificationService.RequestPermissions());
        }

        [EffectMethod]
        public async Task Handle(Actions.AreAllAlertsEnabled.Toggle action, IDispatcher dispatcher)
        {
            await _alertSettingsService.SetAreAllAlertsEnabled(action.AreAllAlertsEnabled);
            await Task.Run(() => _notificationService.RequestPermissions());
        }

        [EffectMethod]
        public async Task Handle(Actions.AreAllAlertsEnabled.StartLoadingFromSettings action, IDispatcher dispatcher)
        {
            var result = await _alertSettingsService.GetAreAllAlertsEnabled();
            dispatcher.Dispatch(new Actions.AreAllAlertsEnabled.FinishLoadingFromSettings(result));
        }
    }
}