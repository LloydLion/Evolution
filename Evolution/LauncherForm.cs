using Evolution.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evolution
{
	partial class LauncherForm : Form
	{
		private readonly LaunchSettings settings = new ();


		public LauncherForm()
		{
			InitializeComponent();
		}


		private void launchButton_Click(object sender, EventArgs e)
		{
			new MainForm(settings).Show();
		}
	}
}
