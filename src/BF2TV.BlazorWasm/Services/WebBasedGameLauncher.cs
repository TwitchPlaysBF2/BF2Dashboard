using BF2TV.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace BF2TV.BlazorWasm.Services;

public class WebBasedGameLauncher : IGameLauncher
{
    private readonly NavigationManager _navigationManager;

    public WebBasedGameLauncher(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }
    
    public void LaunchGame(string args)
    {
        _navigationManager.NavigateTo(args);
        Thread.Sleep(200);
        _navigationManager.NavigateTo("app-only");
    }
}