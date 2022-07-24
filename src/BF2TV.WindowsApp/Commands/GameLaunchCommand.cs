using MediatR;

namespace BF2TV.WindowsApp.Commands;

public class GameLaunchCommand : IRequest
{
    public string? Arguments { get; }

    public GameLaunchCommand(string? arguments)
    {
        Arguments = arguments;
    }
}