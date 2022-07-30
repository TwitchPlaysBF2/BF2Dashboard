using System.Diagnostics;
using BF2TV.Domain.Services;

namespace BF2TV.WindowsApp.Services;

/// <summary>
/// Thanks to @cetteup for providing his game launcher.
/// The launcher gets installed within the BF2.TV App setup.
/// The Uri protocol gets registered upon first execution of the launcher (step of setup procedure).
/// https://joinme.click/
/// https://github.com/cetteup/joinme.click-launcher
/// </summary>
public class AppBasedJoinmeGameLauncher : IGameLauncher
{
    public void LaunchGame(string args)
    {
        var uri = new Uri(args);
        if (!uri.IsWellFormedOriginalString())
        {
            MessageBox.Show(
                @"Couldn't join server due to invalid join arguments.\n" +
                @$"The given arguments were: {args}\n" +
                @"Expected format example:  bf2://95.172.92.116:16567",
                @"Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        var psi = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = uri.ToString()
        };

        Process.Start(psi);
    }
}