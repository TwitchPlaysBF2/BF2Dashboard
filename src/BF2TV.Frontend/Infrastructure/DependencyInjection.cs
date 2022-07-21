using BF2TV.Domain.DiscordApi;
using BF2TV.Domain.Services;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace BF2TV.Frontend.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterSharedServices(this IServiceCollection services)
    {
        services.AddBlazoredLocalStorage();
        services.AddScoped<IServerListService, ServerListService>();
        services.AddFluxor(options =>
        {
            options
                .ScanAssemblies(typeof(DependencyInjection).Assembly)
                .UseRouting()
                .UseReduxDevTools();
        });

        services
            .AddRefitClient<IDiscordRepository>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://d26vco2td5wtt4.cloudfront.net"));
    }
}