using BF2TV.Frontend;
using BF2TV.Frontend.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
// TODO: Might have to pass according assemblies here for Fluxor to work properly
builder.Services.RegisterFrontendServices(typeof(Program).Assembly);

await builder.Build().RunAsync();