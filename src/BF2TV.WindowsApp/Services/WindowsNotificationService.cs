using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Services;
using BF2TV.WindowsApp.Infrastructure.Tray;

namespace BF2TV.WindowsApp.Services;

public class WindowsNotificationService : INotificationService
{
    private readonly TrayService _trayService;

    public WindowsNotificationService(TrayService trayService)
    {
        _trayService = trayService;
    }

    public async Task NotifyAsync(IConditionStatus status)
    {
        await Task.Run(() =>
        {
            _trayService.TrayIcon.ShowBalloonTip(5,
                "",
                status.AlertText,
                ToolTipIcon.None);
        });
    }

    public Task RequestPermissions() => Task.CompletedTask;
}