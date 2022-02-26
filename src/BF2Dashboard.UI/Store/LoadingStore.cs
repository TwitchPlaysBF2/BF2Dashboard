using Fluxor;

namespace BF2Dashboard.UI.Store;

public record LoadingState
{
    public bool IsLoading { get; init; }
}

public class LoadingReducers
{
    [ReducerMethod(typeof(LoadingBeginAction))]
    public LoadingState OnStart(IDispatcher dispatcher)
    {
        return new LoadingState
        {
            IsLoading = true,
        };
    }

    [ReducerMethod(typeof(LoadingEndAction))]
    public LoadingState OnStop(IDispatcher dispatcher)
    {
        return new LoadingState
        {
            IsLoading = false,
        };
    }
}

public class LoadingFeature : Feature<LoadingState>
{
    public override string GetName() => nameof(LoadingFeature);

    protected override LoadingState GetInitialState() => new() { IsLoading = false };
}

public class LoadingBeginAction
{
}

public class LoadingEndAction
{
}