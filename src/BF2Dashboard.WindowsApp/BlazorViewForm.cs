﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using BF2Dashboard.WindowsApp.Infrastructure;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace BF2Dashboard.WindowsApp
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
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = serviceProvider;
            blazorWebView1.RootComponents.Add<UI.Pages.Dashboard>("#app");
        }

        protected override void OnHandleCreated(EventArgs e) => DarkMode.Enable(Handle);
    }
}