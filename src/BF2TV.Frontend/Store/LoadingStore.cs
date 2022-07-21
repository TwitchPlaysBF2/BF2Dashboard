using Fluxor;

namespace BF2TV.Frontend.Store;

public record LoadingState
{
    public bool IsLoading { get; init; }
}

public class LoadingReducers
{
    [ReducerMethod(typeof(LoadingBeginAction))]
    public LoadingState OnStart(LoadingState oldState)
    {
        return new LoadingState
        {
            IsLoading = true,
        };
    }

    [ReducerMethod(typeof(LoadingEndAction))]
    public LoadingState OnStop(LoadingState oldState)
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