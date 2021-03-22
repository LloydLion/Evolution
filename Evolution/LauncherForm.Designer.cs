
namespace Evolution
{
	partial class LauncherForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.launchButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// launchButton
			// 
			this.launchButton.Location = new System.Drawing.Point(13, 371);
			this.launchButton.Name = "launchButton";
			this.launchButton.Size = new System.Drawing.Size(462, 44);
			this.launchButton.TabIndex = 0;
			this.launchButton.Text = "Launch";
			this.launchButton.UseVisualStyleBackColor = true;
			this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
			// 
			// LauncherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 427);
			this.Controls.Add(this.launchButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LauncherForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Evolution Launcher";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button launchButton;
	}
}

