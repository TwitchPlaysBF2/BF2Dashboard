using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BF2Dashboard.WindowsApp.Infrastructure;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace BF2Dashboard.WindowsApp
{
    public partial class BlazorViewForm : Form
    {
        public BlazorViewForm()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<UI.Pages.Dashboard>("#app");
        }

        protected override void OnHandleCreated(EventArgs e) => DarkMode.Enable(Handle);
    }
}