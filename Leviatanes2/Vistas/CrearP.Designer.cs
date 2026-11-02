/*
 * Created by SharpDevelop.
 * User: Alfonzovaa
 * Date: 30/10/2026
 * Time: 11:24 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Leviatanes2
{
	partial class CrearP
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.VolverMenu = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// VolverMenu
			// 
			this.VolverMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.VolverMenu.Location = new System.Drawing.Point(1210, 574);
			this.VolverMenu.Name = "VolverMenu";
			this.VolverMenu.Size = new System.Drawing.Size(208, 59);
			this.VolverMenu.TabIndex = 1;
			this.VolverMenu.Text = "Volver al menú";
			this.VolverMenu.UseVisualStyleBackColor = true;
			this.VolverMenu.Click += new System.EventHandler(this.VolverMenuClick);
			// 
			// CrearP
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1444, 661);
			this.Controls.Add(this.VolverMenu);
			this.Name = "CrearP";
			this.Text = "CrearP";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button VolverMenu;
	}
}
