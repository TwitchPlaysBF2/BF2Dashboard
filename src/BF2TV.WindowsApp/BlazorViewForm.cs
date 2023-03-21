using System.ComponentModel;
using BF2TV.Frontend.Infrastructure;
using BF2TV.WindowsApp.Infrastructure;
using BF2TV.WindowsApp.Infrastructure.Tray;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Core;

namespace BF2TV.WindowsApp
{
    public partial class BlazorViewForm : Form
    {
        public FormWindowState? CachedWindowStateBeforeMinimizing;
        private readonly ServiceProvider _serviceProvider;
        private TrayService? _trayService;

        public BlazorViewForm()
        {
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            services.AddBlazorWebViewDeveloperTools();
            services.RegisterSharedServices();
            services.RegisterWinFormsServices();
            _serviceProvider = services.BuildServiceProvider();

            InitializeTrayIcon();
            InitializeComponent();
            InitializeWebView();

            UpdateService.PromptForUpdateIfThereIsOneAvailable();
        }

        private void InitializeTrayIcon()
        {
            _trayService = _serviceProvider.GetService<TrayService>()
                           ?? throw new InvalidOperationException($"Failed to resolve {nameof(TrayService)}");
            _trayService.Initialize(this);
        }

        private void InitializeWebView()
        {
            OverwriteStorageLocation();
            blazorWebView.WebView.DefaultBackgroundColor = Color.FromArgb(39, 43, 48);
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.RootComponents.Add<Frontend.App>("#app");
            blazorWebView.Services = _serviceProvider;

            void OverwriteStorageLocation()
            {
                // Reason for doing this is quite long and very silly. Let me explain.
                // Default storage path is: %localappdata%\<ASSEMBLYNAME>.WebView2\EBWebView
                // This means, if AssemblyName ever changes, the user loses all existing localstorage data.
                // This was the case, since we renamed AssemblyName from BF2TV.WindowsApp to BF2.TV.
                // Reason for rename was NotifyIcon's default baloon notification behavior:
                // The notification title would always show the AssemblyName.
                // For all these reasons, we renamed the AssemblyName AND manually had to set the old one here.
                // Not doing this, would cause users to lose their existing favorites & friends, which sucks.

                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var cacheFolderPath = Path.Combine(localAppData, "BF2TV.WindowsApp.WebView2");
                Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", cacheFolderPath);
            }
        }

        private void BlazorViewForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
            else
                CachedWindowStateBeforeMinimizing = WindowState;
        }

        protected override void OnHandleCreated(EventArgs e) => DarkAppMode.Enable(Handle);

        protected override void OnClosing(CancelEventArgs e)
        {
            _trayService?.Dispose();
            base.OnClosing(e);
        }
    }
}