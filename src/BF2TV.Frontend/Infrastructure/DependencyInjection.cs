using System.Reflection;
using BF2TV.Domain.DiscordApi;
using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Repositories;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Services;
using Blazored.LocalStorage;
using Fluxor;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace BF2TV.Frontend.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterSharedServices(this IServiceCollection services)
    {
        services.AddBlazoredLocalStorage();
        services.AddScoped<IServerListService, ServerListService>();
        services.AddScoped<IConditionStatusTracker, ConditionStatusTracker>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
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
        
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<DiscordUrlParser>();
        services.AddScoped<IJsonRepository<FriendIsOnServerCondition>, JsonRepository<FriendIsOnServerCondition>>();
        services.AddScoped<IPeriodicRefresher, PeriodicRefresher>();
    }
}