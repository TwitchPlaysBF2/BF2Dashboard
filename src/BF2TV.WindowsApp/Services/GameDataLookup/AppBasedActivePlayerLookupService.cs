using System.Diagnostics;
using BF2TV.Domain.Services;
using BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2;
using BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2.Files;

namespace BF2TV.WindowsApp.Services.GameDataLookup;

public class AppBasedActivePlayerLookupService : IActivePlayerLookupService
{
    private readonly GlobalConfigFile _globalConfig;

    public AppBasedActivePlayerLookupService(GlobalConfigFile globalConfig)
    {
        _globalConfig = globalConfig;
    }

    public string? GetPlayerName()
    {
        string playerName;
        try
        {
            var activeProfileNumber = _globalConfig.GetCurrentlyActiveProfileNumber();
            var profileConfig = new ProfileConfigFile(activeProfileNumber);
            playerName = profileConfig.GetPlayerName();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Debugger.Break();
            return null;
        }

        return playerName;
    }
}