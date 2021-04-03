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
		private readonly EvolutionWorker evolutionWorker = new(80, 80);


		public MainForm(LaunchSettings settings)
		{
			InitializeComponent();
			this.settings = settings;

			mainPictureBox.Image = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);

			evolutionWorker.Setup();
		}


		private void UpdateField()
		{
			evolutionWorker.Update();

			var buffer = new Bitmap(mainPictureBox.Width, mainPictureBox.Height);
			var graphics = Graphics.FromImage(buffer);

			graphics.Clear(settings.BackgroundColor);

			for(int x = 0; x < evolutionWorker.Field.Weight; x++)
			{
				for(int y = 0; y < evolutionWorker.Field.Height; y++)
				{
					var entity = evolutionWorker.Field.GetCellAt(x, y).Entity;
					Brush drawBrush;

					if(entity != null)
					{
						var ds = entity.DrawingSettings;
						if(ds.Image == null)
							drawBrush = new SolidBrush(ds.Color);
						else
							drawBrush = new TextureBrush(ds.Image);

						graphics.FillRectangle(drawBrush, new Rectangle(x * 10, (80 - y) * 10 - 10, 10, 10));
					}
				}
			}

			graphics.Save();
			graphics.Flush();

			mainPictureBox.Image = buffer;
		}

		private void GameClock_Tick(object sender, EventArgs e)
		{
			UpdateField();
		}
	}
}
