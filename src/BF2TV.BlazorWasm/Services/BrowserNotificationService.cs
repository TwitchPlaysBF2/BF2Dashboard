using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Services.Alerts;
using Majorsoft.Blazor.Components.Notifications;

namespace BF2TV.BlazorWasm.Services;

public class BrowserNotificationService : INotificationService
{
    private readonly IHtmlNotificationService _notificationService;

    public BrowserNotificationService(IHtmlNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task NotifyAsync(IConditionStatus status)
    {
        await RequestPermissions();

        var options = new HtmlNotificationOptions(status.AlertTitle)
        {
            Body = status.AlertText,
            // TODO: Render map img instead of BF2.TV Icon https://github.com/TwitchPlaysBF2/BF2Dashboard/issues/48
            // Icon = "https://www.bf2hub.com/home/images/favorite/map_dalian_plant.jpg",
            Icon = "https://bf2.tv/_content/BF2TV.Frontend/img/logo.ico",
            Vibrate = new[] {100},
        };

        await _notificationService.ShowsAsync(options);
    }

    public async Task RequestPermissions()
    {
        await _notificationService.RequestPermissionAsync(null!);
    }
}