using BF2TV.Frontend.Infrastructure;

namespace BF2TV.WindowsApp.Infrastructure;

public class WinFormsEnvironment : IEnvironment
{
    public bool IsWeb() => false;
    public bool IsApp() => !IsWeb();
}