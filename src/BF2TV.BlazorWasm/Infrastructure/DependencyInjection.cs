using BF2TV.Frontend.Infrastructure;

namespace BF2TV.BlazorWasm.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterBlazorWasmServices(this IServiceCollection services)
    {
        services.AddTransient<IEnvironment, BlazorWasmEnvironment>();
    }
}