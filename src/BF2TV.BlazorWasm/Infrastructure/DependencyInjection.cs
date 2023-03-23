using BF2TV.BlazorWasm.Services;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Infrastructure;
using BF2TV.Frontend.Services.Alerts;

namespace BF2TV.BlazorWasm.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterBlazorWasmServices(this IServiceCollection services)
    {
        services.AddTransient<IEnvironment, BlazorWasmEnvironment>();
        services.AddTransient<IActivePlayerLookupService, WebBasedActivePlayerLookupService>();
        services.AddTransient<INotificationService, BrowserNotificationService>();
    }
}