using System.Diagnostics;
using System.Reflection;

namespace BF2TV.WindowsApp.Infrastructure;

public static class VersionProvider
{
    public static string GetAppVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        return versionInfo?.FileVersion
               ?? throw new InvalidOperationException("Couldn't resolve app version");
    }
}