using BF2Dashboard.Domain.BattlefieldApi;
using Blazored.LocalStorage;
using Fluxor;

namespace BF2Dashboard.UI.Store.FriendList;

public class FriendListState
{
    public bool IsLoadedAndHasNoFriendsYet => OnlineFriendList != null && OfflineFriendList != null &&
                                              OnlineFriendList.Count == 0 && OfflineFriendList.Count == 0;

    public List<FriendModel>? OnlineFriendList { get; }

    public List<FriendModel>? OfflineFriendList { get; }

    public FriendListState(List<FriendModel>? onlineFriendList, List<FriendModel>? offlineFriendList)
    {
        OnlineFriendList = onlineFriendList;
        OfflineFriendList = offlineFriendList;
    }
}

public class FriendListFeature : Feature<FriendListState>
{
    public override string GetName() => nameof(FriendListFeature);

    protected override FriendListState GetInitialState() => new(null, null);
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
    public List<FriendModel> OnlineFriendList { get; }

    public List<FriendModel> OfflineFriendList { get; }

    public SetFriendListAction(List<FriendModel> onlineFriendList, List<FriendModel> offlineFriendList)
    {
        OnlineFriendList = onlineFriendList;
        OfflineFriendList = offlineFriendList;
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
        var friendModels = CreateFriendModels(action.FriendNameList, action.ServerList).ToList();
        var onlineFriendList = friendModels.Where(m => m.IsOnline)
            .OrderBy(m => m.ServerInfo?.MapName)
            .ThenBy(m => m.ServerInfo?.ServerName)
            .ThenBy(m => m.DisplayName)
            .ToList();

        var offlineFriendList = friendModels.Where(m => !m.IsOnline).OrderBy(m => m.DisplayName).ToList();

        dispatcher.Dispatch(new SetFriendListAction(onlineFriendList, offlineFriendList));
    }

    private static IEnumerable<FriendModel> CreateFriendModels(List<string> friendNameList, List<Server> serverList)
    {
        foreach (var friendName in friendNameList)
        {
            var wasPlayerFoundOnline = false;
            foreach (var server in serverList)
            {
                var player = server.Players.FirstOrDefault(p => p.FullName == friendName);
                if (player != null)
                {
                    wasPlayerFoundOnline = true;
                    player.IsFriend = true; // sets UI
                    yield return FriendModel.CreateOnlineFriend(player, server);
                    break;
                }
            }

            if (!wasPlayerFoundOnline)
                yield return FriendModel.CreateOfflineFriend(friendName);
        }
    }
}

public class FriendListReducers
{
    [ReducerMethod]
    public FriendListState OnSetFriendList(FriendListState oldState, SetFriendListAction action)
    {
        return new FriendListState(action.OnlineFriendList, action.OfflineFriendList);
    }
}