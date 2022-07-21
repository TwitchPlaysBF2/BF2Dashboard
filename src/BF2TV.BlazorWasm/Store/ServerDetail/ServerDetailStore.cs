using BF2TV.Domain.BattlefieldApi;
using BF2TV.Domain.GeoApi;
using BF2TV.Domain.Repositories;
using BF2TV.BlazorWasm.Store.FriendList;
using Fluxor;

namespace BF2TV.BlazorWasm.Store.ServerDetail;

public record ServerDetailState
{
    public Server? LoadedServer { get; init; }

    public bool IsLoading { get; init; }
}

public record ServerGeoLocationState
{
    public GeoLocation? GeoLocation { get; set; }
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
    public ServerDetailState OnServerLoaded(ServerDetailState oldState, SetLoadedServerAction action)
    {
        return new ServerDetailState
        {
            LoadedServer = action.LoadedServer,
            IsLoading = false,
        };
    }

    [ReducerMethod]
    public ServerGeoLocationState OnGeoLocationLoaded(ServerGeoLocationState oldState, SetServerGeoLocationAction action)
    {
        return new ServerGeoLocationState
        {
            GeoLocation = action.GeoLocation,
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

    [EffectMethod]
    public async Task LoadServerGeoLocation(SetLoadedServerAction action, IDispatcher dispatcher)
    {
        var geoLocation = await ServerDetailRepository.GetServerGeoLocation(action.LoadedServer.Ip);
        dispatcher.Dispatch(new SetServerGeoLocationAction {GeoLocation = geoLocation});
    }
}

public class ServerDetailFeature : Feature<ServerDetailState>
{
    public override string GetName() => nameof(ServerDetailFeature);

    protected override ServerDetailState GetInitialState() => new() {IsLoading = false};
}

public class ServerGeoLocationFeature : Feature<ServerGeoLocationState>
{
    public override string GetName() => nameof(ServerGeoLocationState);

    protected override ServerGeoLocationState GetInitialState() => new();
}

public class BeginLoadingServerDetailAction
{
    public string IpAndPort { get; set; }
}

public class SetLoadedServerAction
{
    public Server LoadedServer { get; set; }
}

public class SetServerGeoLocationAction
{
    public GeoLocation? GeoLocation { get; set; }
}