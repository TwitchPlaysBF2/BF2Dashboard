using BF2TV.Domain.Services;
using BF2TV.WindowsApp.Commands;
using JetBrains.Annotations;
using MediatR;

namespace BF2TV.WindowsApp.Handlers;

[UsedImplicitly]
public class GameLaunchHandler : RequestHandler<GameLaunchCommand>
{
    private readonly IGameLauncher _gameLauncher;

    public GameLaunchHandler(IGameLauncher gameLauncher)
    {
        _gameLauncher = gameLauncher;
    }

    protected override void Handle(GameLaunchCommand request)
    {
        if (request.Arguments == null)
        {
            MessageBox.Show(
                @"Couldn't join server.\n\nEither something went wrong, or the server is running an unsupported modification.",
                @"Couldn't join server",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        _gameLauncher.LaunchGame(request.Arguments);
    }
}