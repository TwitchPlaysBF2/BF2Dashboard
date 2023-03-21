﻿using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Services;
using BF2TV.WindowsApp.Infrastructure.Tray;

namespace BF2TV.WindowsApp.Services;

public class WindowsAlertService : IAlertService
{
    private readonly TrayService _trayService;

    public WindowsAlertService(TrayService trayService)
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
                ToolTipIcon.Info);
        });
    }
}