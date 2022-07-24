namespace BF2TV.WindowsApp.Infrastructure.Tray;

public class TrayService : IDisposable
{
    private NotifyIcon? _trayIcon;

    public void Initialize()
    {
        _trayIcon = new NotifyIcon
        {
            Icon = new Icon("Resources/favicon.ico"),
            Text = $@"BF2.TV v{VersionProvider.GetAppVersion()}",
            ContextMenuStrip = CreateMenu(),
            Visible = true,
        };
    }

    private ContextMenuStrip CreateMenu()
    {
        return new ContextMenuStrip
        {
            Items =
            {
                new ToolStripMenuItem("Exit", null, OnClickExit, "Exit")
            },
        };
    }

    private void OnClickExit(object? sender, EventArgs e)
    {
        Dispose();
        Application.Exit();
    }

    public void Dispose()
    {
        _trayIcon?.Dispose();
        _trayIcon = null;
    }
}