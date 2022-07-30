using BF2TV.BlazorWasm.Services;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Infrastructure;

namespace BF2TV.BlazorWasm.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterBlazorWasmServices(this IServiceCollection services)
    {
        services.AddTransient<IEnvironment, BlazorWasmEnvironment>();
        services.AddTransient<IActivePlayerLookupService, WebBasedActivePlayerLookupService>();
        services.AddTransient<IGameLauncher, WebBasedGameLauncher>();
    }
}