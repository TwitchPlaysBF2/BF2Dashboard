using BF2Dashboard.Domain.BattlefieldApi;
using Blazored.LocalStorage;
using Fluxor;

namespace BF2Dashboard.UI.Store.FriendList;

public class FriendListState
{
    public List<FriendModel>? FriendList { get; }

    public FriendListState(List<FriendModel>? friendList)
    {
        FriendList = friendList;
    }
}

public class FriendListFeature : Feature<FriendListState>
{
    public override string GetName() => nameof(FriendListFeature);

    protected override FriendListState GetInitialState() => new(null);
}

public class AddFriendAction
{
    public int Pid { get; }

    public AddFriendAction(int pid)
    {
        Pid = pid;
    }
}

public class RemoveFriendAction
{
    public int Pid { get; }

    public RemoveFriendAction(int pid)
    {
        Pid = pid;
    }
}

public class SetFriendListAction
{
    public List<FriendModel> FriendList { get; }

    public SetFriendListAction(List<FriendModel> friendList)
    {
        FriendList = friendList;
    }
}

public class InitializeFriendListAction
{
}

public class ResolvePidListIntoFriendListAction
{
    public List<int> PidList { get; }

    public ResolvePidListIntoFriendListAction(List<int> pidList)
    {
        PidList = pidList;
    }
}

public class FriendListEffects
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IState<FullServerListState> _serverList;

    public FriendListEffects(ILocalStorageService localStorageService, IState<FullServerListState> serverList)
    {
        _localStorageService = localStorageService;
        _serverList = serverList;
    }

    [EffectMethod]
    public async Task AddFriendToPersistence(AddFriendAction action, IDispatcher _)
    {
        var pidList = await _localStorageService.GetItemAsync<List<int>>(Commons.FriendListKey)
                      ?? new List<int>();

        pidList.Add(action.Pid);
        await _localStorageService.SetItemAsync(Commons.FriendListKey, pidList);
    }

    [EffectMethod]
    public async Task RemoveFriendFromPersistence(RemoveFriendAction action, IDispatcher _)
    {
        var pidList = await _localStorageService.GetItemAsync<List<int>>(Commons.FriendListKey);
        pidList.Remove(action.Pid);
        await _localStorageService.SetItemAsync(Commons.FriendListKey, pidList);
    }

    [EffectMethod(typeof(InitializeFriendListAction))]
    public async Task OnInitializeFriendList(IDispatcher dispatcher)
    {
        var pidList = await _localStorageService.GetItemAsync<List<int>>(Commons.FriendListKey);
        dispatcher.Dispatch(new ResolvePidListIntoFriendListAction(pidList));
    }

    [EffectMethod]
    public async Task OnResolvePidList(ResolvePidListIntoFriendListAction action, IDispatcher dispatcher)
    {
        var friendList = ResolveFromPidList(action.PidList);
        dispatcher.Dispatch(new SetFriendListAction(friendList));
    }

    private List<FriendModel> ResolveFromPidList(List<int> pidList)
    {
        var serverList = _serverList.Value.ServerList;
        if (serverList == null)
            return new List<FriendModel>();

        var friendList = ResolveFriendList(pidList, serverList).ToList();
        return friendList;
    }

    private static IEnumerable<FriendModel> ResolveFriendList(List<int> pidList, List<Server> serverList)
    {
        foreach (var friendPid in pidList)
        {
            foreach (var server in serverList)
            {
                var player = server.Players.FirstOrDefault(p => p.Pid == friendPid);
                if (player != null)
                {
                    yield return FriendModel.Create(player, server);
                }
            }
        }
    }
}

public class FriendListReducers
{
    [ReducerMethod]
    public FriendListState OnSetFriendList(FriendListState oldState, SetFriendListAction action)
    {
        return new FriendListState(action.FriendList);
    }
}