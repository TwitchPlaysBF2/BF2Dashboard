using BF2Dashboard.Domain.BattlefieldApi;
using Blazored.LocalStorage;
using Fluxor;

namespace BF2Dashboard.UI.Store.FriendList;

public record FriendListState
{
    public bool IsEmpty => OnlineFriendList?.Count == 0 && OfflineFriendList?.Count == 0;

    public bool IsInitialized { get; init; }

    public bool IsLoading { get; init; }

    public List<FriendModel>? OnlineFriendList { get; set; }

    public List<FriendModel>? OfflineFriendList { get; set; }
}

public class FriendListFeature : Feature<FriendListState>
{
    public override string GetName() => nameof(FriendListFeature);

    protected override FriendListState GetInitialState() => new()
    {
        IsInitialized = false,
        IsLoading = false,
        OnlineFriendList = null,
        OfflineFriendList = null,
    };
}

public class AddFriendAction
{
    public Player Player { get; }

    public AddFriendAction(Player player)
    {
        Player = player;
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

public class SetInitializedFriendListAction
{
}

public class SetLoadingFriendListAction
{
    public bool IsLoading { get; }

    public SetLoadingFriendListAction(bool isLoading)
    {
        IsLoading = isLoading;
    }
}

public class ResolveFriendListAction
{
    public List<string> FriendNameList { get; }

    public ResolveFriendListAction(List<string> friendNameList)
    {
        FriendNameList = friendNameList;
    }
}

public class FriendListEffects
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IState<FullServerListState> _serverListState;
    private readonly IState<FriendListState> _friendListState;

    public FriendListEffects(ILocalStorageService localStorageService, IState<FullServerListState> serverListState, IState<FriendListState> friendListState)
    {
        _localStorageService = localStorageService;
        _serverListState = serverListState;
        _friendListState = friendListState;
    }

    [EffectMethod]
    public async Task AddFriendToPersistence(AddFriendAction action, IDispatcher _)
    {
        var friendNameList = await _localStorageService.GetItemAsync<List<string>>(Commons.FriendListKey)
                             ?? new List<string>();

        friendNameList.Add(action.Player.FullName);
        await _localStorageService.SetItemAsync(Commons.FriendListKey, friendNameList);
    }

    [EffectMethod]
    public async Task AddFriend(AddFriendAction action, IDispatcher dispatcher)
    {
        var newFriend = CreateFriendModels(
                new List<string> {action.Player.FullName},
                _serverListState.Value.ServerList ?? new List<Server>())
            .ToList();
     
        var onlineFriendList = _friendListState.Value.OnlineFriendList ?? new List<FriendModel>();
        var offlineFriendList = _friendListState.Value.OfflineFriendList ?? new List<FriendModel>();
        
        onlineFriendList.AddRange(newFriend.Where(f => f.IsOnline));
        offlineFriendList.AddRange(newFriend.Where(f => !f.IsOnline));

        dispatcher.Dispatch(new SetFriendListAction(onlineFriendList, offlineFriendList));
        dispatcher.Dispatch(new SetLoadingFriendListAction(false));
        dispatcher.Dispatch(new SetInitializedFriendListAction());
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
        dispatcher.Dispatch(new SetLoadingFriendListAction(true));
        var friends = await _localStorageService.GetItemAsync<List<string>>(Commons.FriendListKey)
                      ?? new List<string>();

        dispatcher.Dispatch(new ResolveFriendListAction(friends));
    }

    [EffectMethod]
    public async Task OnResolveFriendList(ResolveFriendListAction action, IDispatcher dispatcher)
    {
        var friendModels = CreateFriendModels(
                action.FriendNameList,
                _serverListState.Value.ServerList ?? new List<Server>())
            .ToList();

        var onlineFriendList = friendModels.Where(m => m.IsOnline)
            .OrderBy(m => m.ServerInfo?.MapName)
            .ThenBy(m => m.ServerInfo?.ServerName)
            .ThenBy(m => m.DisplayName)
            .ToList();

        var offlineFriendList = friendModels.Where(m => !m.IsOnline)
            .OrderBy(m => m.DisplayName)
            .ToList();

        dispatcher.Dispatch(new SetFriendListAction(onlineFriendList, offlineFriendList));
        dispatcher.Dispatch(new SetLoadingFriendListAction(false));
        dispatcher.Dispatch(new SetInitializedFriendListAction());
    }

    private static IEnumerable<FriendModel> CreateFriendModels(List<string> friendNameList, List<Server> serverList)
    {
        foreach (var friendName in friendNameList)
        {
            var wasPlayerFoundOnline = false;
            foreach (var server in serverList)
            {
                var player = server.Players?.FirstOrDefault(p => p.FullName == friendName);
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
        oldState.OnlineFriendList = action.OnlineFriendList;
        oldState.OfflineFriendList = action.OfflineFriendList;
        return oldState;
    }

    [ReducerMethod]
    public FriendListState OnRemoveFriend(FriendListState oldState, RemoveFriendAction action)
    {
        var state = oldState;
        
        var onlineFriendToDelete = oldState.OnlineFriendList?.FirstOrDefault(f => f.DisplayName == action.Name);
        if (onlineFriendToDelete != null)
        {
            state.OnlineFriendList?.Remove(onlineFriendToDelete);
        }

        var offlineFriendToDelete = oldState.OfflineFriendList?.FirstOrDefault(f => f.DisplayName == action.Name);
        if (offlineFriendToDelete != null)
        {
            state.OfflineFriendList?.Remove(offlineFriendToDelete);
        }

        return state;
    }

    [ReducerMethod]
    public static FriendListState OnSetLoading(FriendListState oldState, SetLoadingFriendListAction action)
    {
        return oldState with
        {
            IsLoading = action.IsLoading
        };
    }

    [ReducerMethod(typeof(SetInitializedFriendListAction))]
    public static FriendListState OnSetInitialized(FriendListState state)
    {
        return state with
        {
            IsInitialized = true
        };
    }
}