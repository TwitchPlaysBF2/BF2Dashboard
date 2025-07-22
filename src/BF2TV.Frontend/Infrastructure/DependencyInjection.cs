using System.Reflection;
using BF2TV.Domain.DiscordApi;
using BF2TV.Domain.Models.Alerts;
using BF2TV.Domain.Repositories;
using BF2TV.Domain.Services;
using BF2TV.Frontend.Services;
using BF2TV.Frontend.Services.Alerts;
using Blazored.LocalStorage;
using Fluxor;
using Majorsoft.Blazor.Components.CssEvents;
using Majorsoft.Blazor.Components.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace BF2TV.Frontend.Infrastructure;

public static class DependencyInjection
{
    public static void RegisterSharedServices(this IServiceCollection services)
    {
        services.AddBlazoredLocalStorage();
        services.AddCssEvents();
        services.AddNotifications();
        services.AddScoped<IServerListService, ServerListService>();
        services.AddScoped<IAlertGenerationService, AlertGenerationService>();
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
        services.AddScoped<IJsonRepository<FriendIsOnServerCondition>, JsonRepository<FriendIsOnServerCondition>>();
        services.AddScoped<IJsonRepository<AlertSettings>, JsonRepository<AlertSettings>>();
        services.AddScoped<IAlertSettingsService, AlertSettingsService>();
        services.AddScoped<IPeriodicRefresher, PeriodicRefresher>();
    }
}