using AutoUpdaterDotNET;
using BF2TV.Frontend.Infrastructure;
using BF2TV.WindowsApp.Infrastructure;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace BF2TV.WindowsApp
{
    public partial class BlazorViewForm : Form
    {
        public BlazorViewForm()
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddBlazorWebViewDeveloperTools();
            services.RegisterSharedServices();
            services.RegisterWinFormsServices();

            InitializeComponent();

            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();
            blazorWebView.RootComponents.Add<Frontend.App>("#app");

            UpdateService.PromptForUpdateIfThereIsOneAvailable();
        }

        protected override void OnHandleCreated(EventArgs e) => DarkAppMode.Enable(Handle);
    }
}