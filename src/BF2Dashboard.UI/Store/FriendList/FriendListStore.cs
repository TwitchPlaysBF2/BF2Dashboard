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
    public string Name { get; }

    public AddFriendAction(string name)
    {
        Name = name;
    }
}

public class RemoveFriendAction
{
    public string Name { get; }

    public RemoveFriendAction(string name)
    {
        Name = name;
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

public class ResolveFriendListAction
{
    public List<string> FriendNameList { get; }

    public List<Server> ServerList { get; }

    public ResolveFriendListAction(List<string> friendNameList, List<Server> serverList)
    {
        FriendNameList = friendNameList;
        ServerList = serverList;
    }
}

public class FriendListEffects
{
    private readonly ILocalStorageService _localStorageService;

    public FriendListEffects(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task AddFriendToPersistence(AddFriendAction action, IDispatcher _)
    {
        var friendNameList = await _localStorageService.GetItemAsync<List<string>>(Commons.FriendListKey)
                      ?? new List<string>();

        friendNameList.Add(action.Name);
        await _localStorageService.SetItemAsync(Commons.FriendListKey, friendNameList);
    }

    [EffectMethod]
    public async Task RemoveFriendFromPersistence(RemoveFriendAction action, IDispatcher _)
    {
        var friendNameList = await _localStorageService.GetItemAsync<List<string>>(Commons.FriendListKey);
        friendNameList.Remove(action.Name);
        await _localStorageService.SetItemAsync(Commons.FriendListKey, friendNameList);
    }

    [EffectMethod]
    public async Task InitializeFriendList(SetFullServerListAction action, IDispatcher dispatcher)
    {
        var friends = await _localStorageService.GetItemAsync<List<string>>(Commons.FriendListKey)
                      ?? new List<string>();

        dispatcher.Dispatch(new ResolveFriendListAction(friends, action.ServerList ?? new List<Server>()));
    }

    [EffectMethod]
    public async Task OnResolveFriendList(ResolveFriendListAction action, IDispatcher dispatcher)
    {
        var friendList = ResolveFriendList(action.FriendNameList, action.ServerList).ToList();
        dispatcher.Dispatch(new SetFriendListAction(friendList));

        IEnumerable<FriendModel> ResolveFriendList(List<string> friendNameList, List<Server> serverList)
        {
            foreach (var friendName in friendNameList)
            {
                foreach (var server in serverList)
                {
                    var player = server.Players.FirstOrDefault(p => p.Name == friendName);
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