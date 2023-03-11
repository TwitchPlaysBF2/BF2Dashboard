using System.Collections.Immutable;
using BF2TV.Domain.Models.Alerts;
using Fluxor;

namespace BF2TV.Frontend.Store.Alerts;

// ReSharper disable ClassNeverInstantiated.Global
public class AlertStore
{
    [FeatureState]
    public class State
    {
        public IImmutableList<IServerCondition> ServerConditions { get; }

        public State()
        {
            ServerConditions = ImmutableList.Create<IServerCondition>();
        }

        public State(IImmutableList<IServerCondition> serverConditions)
        {
            ServerConditions = serverConditions;
        }
    }

    public class Actions
    {
        public record AddNewAlert(IServerCondition ServerCondition);

        public record RemoveAlert(IServerCondition ServerCondition);
    }

    public class Reducers
    {
        [ReducerMethod]
        public State Reduce(State oldState, Actions.AddNewAlert action)
        {
            var conditions = oldState.ServerConditions.Add(action.ServerCondition);
            return new State(conditions);
        }

        [ReducerMethod]
        public State Reduce(State oldState, Actions.RemoveAlert action)
        {
            var conditions = oldState.ServerConditions.Remove(action.ServerCondition);
            return new State(conditions);
        }
    }

    public class Effects
    {
    }
}