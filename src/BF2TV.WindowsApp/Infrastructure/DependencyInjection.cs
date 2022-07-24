using System.Reflection;
using BF2TV.Frontend.Infrastructure;
using BF2TV.WindowsApp.Infrastructure.Tray;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BF2TV.WindowsApp.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterWinFormsServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<IEnvironment, WinFormsEnvironment>();
        services.AddSingleton<TrayService>();
    }
}