using BF2TV.Domain.Commands;
using BF2TV.WindowsApp.Infrastructure.Tray;
using JetBrains.Annotations;
using MediatR;

namespace BF2TV.WindowsApp.Handlers;

[UsedImplicitly]
public class RaiseFavoriteServersToAppHandler : RequestHandler<RaiseFavoriteServerListToApp>
{
    private readonly TrayService _trayService;

    public RaiseFavoriteServersToAppHandler(TrayService trayService)
    {
        _trayService = trayService;
    }

    protected override void Handle(RaiseFavoriteServerListToApp request)
    {
        var serversToShowInApp = request.ServerList
            .Where(s => s.NumPlayersWithoutBots > 0);

        _trayService.SetFavorites(serversToShowInApp.ToArray());
    }
}