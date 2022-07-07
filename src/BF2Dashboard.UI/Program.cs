using BF2Dashboard.Domain.DiscordApi;
using BF2Dashboard.Domain.Services;
using BF2Dashboard.UI;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IServerListService, ServerListService>();
builder.Services.AddFluxor(options =>
{
    options
        .ScanAssemblies(typeof(Program).Assembly)
        .UseRouting()
        .UseReduxDevTools();
});

builder.Services
    .AddRefitClient<IDiscordRepository>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://d26vco2td5wtt4.cloudfront.net"));

await builder.Build().RunAsync();