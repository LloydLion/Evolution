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
	partial class MainForm : Form
	{
		private readonly LaunchSettings settings;
		private readonly EvolutionWorker evolutionWorker = new(160, 160);


		public MainForm(LaunchSettings settings)
		{
			InitializeComponent();
			this.settings = settings;

			mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);

			evolutionWorker.Setup();

			Task.Run(() => { while(true) { evolutionWorker.Update(); counter++; } } );
		}


		int counter = 0;
		bool flag = false;
		private void UpdateField()
		{
			var buffer = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
			var graphics = Graphics.FromImage(buffer);

			graphics.Clear(settings.BackgroundColor);

			for (int x = 0; x < evolutionWorker.Field.Weight; x++)
			{
				for (int y = 0; y < evolutionWorker.Field.Height; y++)
				{
					var entity = evolutionWorker.Field.GetCellAt(x, y).Entity;
					Brush drawBrush;

					if (entity != null)
					{
						var ds = entity.DrawingSettings;
						if (ds.Image == null)
							drawBrush = new SolidBrush(ds.Color);
						else
							drawBrush = new TextureBrush(ds.Image);

						var width = mainPictureBox.Width / evolutionWorker.Field.Weight;
						var height = mainPictureBox.Height / evolutionWorker.Field.Height;

						graphics.FillRectangle(drawBrush, new Rectangle(x * width, (evolutionWorker.Field.Height - y - 1) * height, width, height));
					}
				}
			}

			graphics.Save();
			graphics.Flush();

			mainPictureBox.Image = buffer;

			if(!flag && counter >= 1000)
			{
				MessageBox.Show("1000!");
				flag = true;
			}
		}

		private void GameClock_Tick(object sender, EventArgs e)
		{
			UpdateField();
		}
	}
}
