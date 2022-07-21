using BF2TV.Frontend.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BF2TV.WindowsApp.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterWinFormsServices(this IServiceCollection services)
    {
        services.AddTransient<IEnvironment, WinFormsEnvironment>();
    }
}