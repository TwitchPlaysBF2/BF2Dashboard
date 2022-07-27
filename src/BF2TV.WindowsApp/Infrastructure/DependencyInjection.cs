using System.Reflection;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Infrastructure;
using BF2TV.WindowsApp.Infrastructure.Tray;
using BF2TV.WindowsApp.Services;
using BF2TV.WindowsApp.Services.GameDataLookup;
using BF2TV.WindowsApp.Services.GameDataLookup.Battlefield2.Files;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BF2TV.WindowsApp.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterWinFormsServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<IEnvironment, WinFormsEnvironment>();
        services.AddTransient<IGameLauncher, JoinmeUriProtocolGameLauncher>();
        services.AddSingleton<GlobalConfigFile>();
        services.AddTransient<IActivePlayerLookupService, AppBasedActivePlayerLookupService>();
        services.AddSingleton<TrayService>();
    }
}