using AutoUpdaterDotNET;

namespace BF2TV.WindowsApp.Infrastructure;

public static class UpdateService
{
    public static void PromptForUpdateIfThereIsOneAvailable()
    {
        AutoUpdater.AppTitle = "BF2.TV";
        AutoUpdater.ShowSkipButton = false;
        AutoUpdater.Start("https://raw.githubusercontent.com/TwitchPlaysBF2/BF2Dashboard/main/build/AutoUpdater.xml");
    }
}