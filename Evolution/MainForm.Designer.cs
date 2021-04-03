
namespace Evolution
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mainPictureBox = new System.Windows.Forms.PictureBox();
			this.gameClock = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// mainPictureBox
			// 
			this.mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
			this.mainPictureBox.Margin = new System.Windows.Forms.Padding(0);
			this.mainPictureBox.Name = "mainPictureBox";
			this.mainPictureBox.Size = new System.Drawing.Size(800, 800);
			this.mainPictureBox.TabIndex = 0;
			this.mainPictureBox.TabStop = false;
			// 
			// gameClock
			// 
			this.gameClock.Enabled = true;
			this.gameClock.Interval = 1;
			this.gameClock.Tick += new System.EventHandler(this.GameClock_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 800);
			this.Controls.Add(this.mainPictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Evolution";
			((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox mainPictureBox;
		private System.Windows.Forms.Timer gameClock;
	}
}