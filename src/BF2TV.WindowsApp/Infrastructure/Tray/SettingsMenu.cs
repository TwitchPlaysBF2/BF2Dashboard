namespace BF2TV.WindowsApp.Infrastructure.Tray;

public class SettingsMenu : IDisposable
{
    private ToolStripMenuItem? _menuInstance;

    public SettingsMenu()
    {
        
    }
    
    public ToolStripMenuItem Create()
    {
        Dispose();
        _menuInstance = new ToolStripMenuItem("Settings");

        const string title = "Start BF2.TV with Windows";
        var item = new ToolStripMenuItem(title, null, (_, _) => OnChangeSetting(), title);
        item.CheckOnClick = true;
        item.Checked = true;
        _menuInstance.DropDown.Items.Add(item);

        return _menuInstance;
    }
    private void OnChangeSetting()
    {
        // throw new NotImplementedException();
    }

    public void Dispose()
    {
        _menuInstance?.Dispose();
    }
}