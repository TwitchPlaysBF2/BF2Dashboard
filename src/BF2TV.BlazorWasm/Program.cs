using BF2TV.BlazorWasm.Infrastructure;
using BF2TV.Frontend;
using BF2TV.Frontend.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.RegisterSharedServices();
builder.Services.RegisterBlazorWasmServices();

await builder.Build().RunAsync();