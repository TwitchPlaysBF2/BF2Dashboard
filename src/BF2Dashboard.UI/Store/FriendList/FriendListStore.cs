using BF2Dashboard.Domain.BattlefieldApi;
using Blazored.LocalStorage;
using Fluxor;

namespace BF2Dashboard.UI.Store.FriendList;

public class FriendListState
{
    public List<FriendModel>? OnlineFriendList { get; }

    public FriendListState(List<FriendModel>? onlineFriendList)
    {
        OnlineFriendList = onlineFriendList;
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

    public List<Server> ServerList { get; }

    public ResolvePidListIntoFriendListAction(List<int> pidList, List<Server> serverList)
    {
        PidList = pidList;
        ServerList = serverList;
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

    [EffectMethod]
    public async Task InitializeFriendList(SetFullServerListAction action, IDispatcher dispatcher)
    {
        var pidList = await _localStorageService.GetItemAsync<List<int>>(Commons.FriendListKey);
        dispatcher.Dispatch(new ResolvePidListIntoFriendListAction(pidList, action.ServerList ?? new List<Server>()));
    }

    [EffectMethod]
    public async Task OnResolvePidList(ResolvePidListIntoFriendListAction action, IDispatcher dispatcher)
    {
        var friendList = ResolveFriendList(action.PidList, action.ServerList).ToList();
        dispatcher.Dispatch(new SetFriendListAction(friendList));

        IEnumerable<FriendModel> ResolveFriendList(List<int> pidList, List<Server> serverList)
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
}

public class FriendListReducers
{
    [ReducerMethod]
    public FriendListState OnSetFriendList(FriendListState oldState, SetFriendListAction action)
    {
        return new FriendListState(action.FriendList);
    }
}