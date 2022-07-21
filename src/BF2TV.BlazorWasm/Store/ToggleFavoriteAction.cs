using BF2TV.Domain.BattlefieldApi;

namespace BF2TV.BlazorWasm.Store;

public class ToggleFavoriteAction
{
    public Server Server { get; }

    public ToggleFavoriteAction(Server server)
    {
        Server = server;
    }
}