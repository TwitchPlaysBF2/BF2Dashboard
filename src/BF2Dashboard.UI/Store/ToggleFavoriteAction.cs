using BF2Dashboard.Domain.BattlefieldApi;

namespace BF2Dashboard.UI.Store;

public class ToggleFavoriteAction
{
    public Server Server { get; }

    public ToggleFavoriteAction(Server server)
    {
        Server = server;
    }
}