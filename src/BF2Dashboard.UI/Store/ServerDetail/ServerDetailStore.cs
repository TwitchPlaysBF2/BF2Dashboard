using BF2Dashboard.Domain.BattlefieldApi;
using BF2Dashboard.Domain.Repositories;
using BF2Dashboard.UI.Store.FriendList;
using Fluxor;

namespace BF2Dashboard.UI.Store.ServerDetail;

public record ServerDetailState
{
    public Server? LoadedServer { get; init; }

    public bool IsLoading { get; init; }
}

public class ServerDetailReducers
{
    [ReducerMethod(typeof(BeginLoadingServerDetailAction))]
    public ServerDetailState OnStart(ServerDetailState oldState)
    {
        return new ServerDetailState
        {
            IsLoading = true,
        };
    }

    [ReducerMethod]
    public ServerDetailState OnStop(ServerDetailState oldState, SetLoadedServerAction action)
    {
        return new ServerDetailState
        {
            LoadedServer = action.LoadedServer,
            IsLoading = false,
        };
    }
}

public class ServerDetailEffects
{
    private readonly IState<FriendListState> _friendListState;

    public ServerDetailEffects(IState<FriendListState> friendListState)
    {
        _friendListState = friendListState;
    }

    [EffectMethod]
    public async Task LoadServerDetails(BeginLoadingServerDetailAction action, IDispatcher dispatcher)
    {
        var server = await ServerDetailRepository.GetServerDetailForIpAndPort(action.IpAndPort);
        dispatcher.Dispatch(new SetLoadedServerAction {LoadedServer = server});
    }
}

public class ServerDetailFeature : Feature<ServerDetailState>
{
    public override string GetName() => nameof(ServerDetailFeature);

    protected override ServerDetailState GetInitialState() => new() {IsLoading = false};
}

public class BeginLoadingServerDetailAction
{
    public string IpAndPort { get; set; }
}

public class SetLoadedServerAction
{
    public Server LoadedServer { get; set; }
}