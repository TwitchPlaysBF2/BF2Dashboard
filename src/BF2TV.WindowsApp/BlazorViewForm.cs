using AutoUpdaterDotNET;
using BF2TV.WindowsApp.Infrastructure;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using BF2TV.Frontend.Infrastructure;

namespace BF2TV.WindowsApp
{
    public partial class BlazorViewForm : Form
    {
        public BlazorViewForm()
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddBlazorWebViewDeveloperTools();
            // TODO: Might have to pass according assemblies here for Fluxor to work properly
            services.RegisterSharedServices(typeof(Program).Assembly);

            InitializeComponent();

            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();
            blazorWebView.RootComponents.Add<Frontend.App>("#app");

            // Checks for version update & prompts a dialog, if available
            AutoUpdater.Start("https://raw.githubusercontent.com/TwitchPlaysBF2/BF2Dashboard/main/build/AutoUpdater.xml");
        }

        protected override void OnHandleCreated(EventArgs e) => DarkAppMode.Enable(Handle);
    }
}