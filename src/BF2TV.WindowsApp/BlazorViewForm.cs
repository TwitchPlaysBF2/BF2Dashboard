using AutoUpdaterDotNET;
using BF2TV.WindowsApp.Infrastructure;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;

namespace BF2TV.WindowsApp
{
    public partial class BlazorViewForm : Form
    {
        public BlazorViewForm(IServiceProvider serviceProvider)
        {
            // TODO add blazor side runtime detection
            // https://stackoverflow.com/a/72194120

            // TODO Introduce notifyIcon
            // https://github.com/hardcodet/wpf-notifyicon

            // Checks for version update & prompts a dialog, if available
            AutoUpdater.Start("https://raw.githubusercontent.com/TwitchPlaysBF2/BF2Dashboard/main/build/AutoUpdater.xml");

            InitializeComponent();
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = serviceProvider;
            blazorWebView.RootComponents.Add<BlazorWasm.Pages.Dashboard>("#app");
        }

        protected override void OnHandleCreated(EventArgs e) => DarkMode.Enable(Handle);
    }
}