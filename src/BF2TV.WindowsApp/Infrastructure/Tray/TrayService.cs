namespace BF2TV.WindowsApp.Infrastructure.Tray;

public class TrayService : IDisposable
{
    private NotifyIcon _trayIcon = null!;
    private BlazorViewForm _form = null!;

    public void Initialize(BlazorViewForm form)
    {
        _form = form ?? throw new ArgumentNullException(paramName: nameof(form));
        _trayIcon = new NotifyIcon
        {
            Icon = new Icon("Resources/favicon.ico"),
            Text = $@"BF2.TV v{VersionProvider.GetAppVersion()}",
            ContextMenuStrip = CreateMenu(),
            Visible = true,
        };
        
        _trayIcon.MouseClick += OnTrayClick;
        _trayIcon.DoubleClick += (sender, _)
            => OnTrayClick(sender, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
    }

    private ContextMenuStrip CreateMenu()
    {
        return new ContextMenuStrip
        {
            Items =
            {
                new ToolStripMenuItem("Exit", null, OnClickExit, "Exit"),
            },
        };
    }

    private void OnTrayClick(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) 
            return;

        _form.Show();
        _form.WindowState = _form.CachedWindowStateBeforeMinimizing ?? FormWindowState.Maximized;
        _form.Activate();
    }

    private void OnClickExit(object? sender, EventArgs e)
    {
        Dispose();
        Application.Exit();
    }

    public void Dispose() => _trayIcon.Dispose();
}