using System.ComponentModel;
using BF2TV.Frontend.Infrastructure;
using BF2TV.WindowsApp.Infrastructure;
using BF2TV.WindowsApp.Infrastructure.Tray;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

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

            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.RootComponents.Add<Frontend.App>("#app");
            blazorWebView.Services = _serviceProvider;

            UpdateService.PromptForUpdateIfThereIsOneAvailable();
        }

        private void InitializeTrayIcon()
        {
            _trayService = _serviceProvider.GetService<TrayService>()
                           ?? throw new InvalidOperationException($"Failed to resolve {nameof(TrayService)}");
            _trayService.Initialize(this);
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