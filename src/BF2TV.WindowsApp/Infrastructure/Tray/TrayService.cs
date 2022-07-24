using BF2TV.Domain.BattlefieldApi;
using BF2TV.WindowsApp.Commands;
using MediatR;

namespace BF2TV.WindowsApp.Infrastructure.Tray;

public class TrayService : IDisposable
{
    private readonly IMediator _mediator;
    private NotifyIcon _trayIcon = null!;
    private BlazorViewForm _form = null!;
    private readonly ToolStripMenuItem _trayItemFavorites = new("Join Favorite Server");

    public TrayService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void Initialize(BlazorViewForm form)
    {
        _form = form ?? throw new ArgumentNullException(nameof(form));
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

    public void SetFavorites(params Server[] serverList)
    {
        _trayItemFavorites.DropDownItems.Clear();
        if (serverList.Length == 0)
        {
            _trayItemFavorites.DropDown.Items.Add(new ToolStripMenuItem("No servers available"));
            return;
        }

        foreach (var server in serverList)
        {
            var title = $"[{server.NumPlayersWithoutBots}/{server.MaxPlayers}] {server.Name}";
            var item = new ToolStripMenuItem(title, null, (_, _) => OnJoinServer(server), title);
            item.ToolTipText = $@"Map: {server.MapName}";
            _trayItemFavorites.DropDown.Items.Add(item);
        }
    }

    private ContextMenuStrip CreateMenu()
    {
        SetFavorites();

        return new ContextMenuStrip
        {
            Items =
            {
                _trayItemFavorites,
                new ToolStripSeparator(),
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

    private void OnJoinServer(Server server)
    {
        var args = server.JoinLink;

        _mediator.Send(new GameLaunchCommand(args));
    }

    private void OnClickExit(object? sender, EventArgs e)
    {
        Dispose();
        Application.Exit();
    }

    public void Dispose() => _trayIcon.Dispose();
}