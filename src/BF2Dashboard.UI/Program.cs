using BF2Dashboard.Domain.Services;
using BF2Dashboard.UI;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ServerCachingService>();
builder.Services.AddScoped<PinningService>();
builder.Services.AddScoped<ServerHandlingService>();

await builder.Build().RunAsync();