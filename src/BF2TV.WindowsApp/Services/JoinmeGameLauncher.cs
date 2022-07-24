using System.Diagnostics;
using BF2TV.Domain.Services;
using Microsoft.Win32;

namespace BF2TV.WindowsApp.Services;

/// <summary>
/// Thanks to @cetteup for providing his game launcher.
/// https://joinme.click/
/// https://github.com/cetteup/joinme.click-launcher
/// </summary>
public class JoinmeGameLauncher : IGameLauncher
{
    public void LaunchGame(string args)
    {
        var filePath = GetPathToLauncherExe();
        Process.Start(filePath, args);
    }

    private string GetPathToLauncherExe()
    {
        var value = Registry.GetValue(@"HKEY_CLASSES_ROOT\bf2\shell\open\command", "", null);

        if (value is string launcherExePath)
            return ReplaceUnneededArguments(launcherExePath);

        throw new InvalidOperationException(
            "Game launcher is not properly configured. Please re-install BF2.TV or contact @TwitchPlaysBF2 about this problem!");

        // The registry value looks like this: 
        // "C:\Program Files (x86)\BF2.TV\_external\joinme.click-launcher.exe" "%1"
        // We don't need the last part: "%1"
        // So I just remove it (I know it's ugly)
        static string ReplaceUnneededArguments(string exePath) => exePath.Replace(" \"%1\"", "");
    }
}