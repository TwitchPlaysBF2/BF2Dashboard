using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BF2Dashboard.WindowsApp.Infrastructure;

namespace BF2Dashboard.WindowsApp
{
    public partial class LaunchForm : Form
    {
        public LaunchForm()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e) => DarkMode.Enable(Handle);
    }
}
