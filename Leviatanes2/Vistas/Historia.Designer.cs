namespace Leviatanes2
{
	partial class Historia
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
			this.lblTitulo = new System.Windows.Forms.Label();
			this.lblTexto = new System.Windows.Forms.Label();
			this.picImagen = new System.Windows.Forms.PictureBox();
			this.lblCapitulo = new System.Windows.Forms.Label();
			this.btnSiguiente = new System.Windows.Forms.Button();
			this.btnCerrar = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picImagen)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitulo
			// 
			this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
			this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitulo.ForeColor = System.Drawing.Color.Gold;
			this.lblTitulo.Location = new System.Drawing.Point(24, 16);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(1232, 54);
			this.lblTitulo.TabIndex = 0;
			this.lblTitulo.Text = "CRÓNICAS DE LOS LEVIATANES - MODO HISTORIA";
			this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblTexto
			// 
			this.lblTexto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
			this.lblTexto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTexto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTexto.ForeColor = System.Drawing.Color.White;
			this.lblTexto.Location = new System.Drawing.Point(24, 84);
			this.lblTexto.Name = "lblTexto";
			this.lblTexto.Padding = new System.Windows.Forms.Padding(20);
			this.lblTexto.Size = new System.Drawing.Size(600, 524);
			this.lblTexto.TabIndex = 1;
			this.lblTexto.Text = "Texto de la historia...";
			// 
			// picImagen
			// 
			this.picImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(16)))), ((int)(((byte)(28)))));
			this.picImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picImagen.Location = new System.Drawing.Point(640, 84);
			this.picImagen.Name = "picImagen";
			this.picImagen.Size = new System.Drawing.Size(616, 524);
			this.picImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picImagen.TabIndex = 2;
			this.picImagen.TabStop = false;
			this.picImagen.Click += new System.EventHandler(this.PicImagenClick);
			// 
			// lblCapitulo
			// 
			this.lblCapitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCapitulo.ForeColor = System.Drawing.Color.Gold;
			this.lblCapitulo.Location = new System.Drawing.Point(440, 626);
			this.lblCapitulo.Name = "lblCapitulo";
			this.lblCapitulo.Size = new System.Drawing.Size(400, 56);
			this.lblCapitulo.TabIndex = 3;
			this.lblCapitulo.Text = "Capítulo 1 de 6";
			this.lblCapitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSiguiente
			// 
			this.btnSiguiente.BackColor = System.Drawing.Color.Gold;
			this.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSiguiente.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSiguiente.ForeColor = System.Drawing.Color.Black;
			this.btnSiguiente.Location = new System.Drawing.Point(986, 626);
			this.btnSiguiente.Name = "btnSiguiente";
			this.btnSiguiente.Size = new System.Drawing.Size(270, 56);
			this.btnSiguiente.TabIndex = 4;
			this.btnSiguiente.Text = "Continuar a Batalla 1 ▶";
			this.btnSiguiente.UseVisualStyleBackColor = false;
			this.btnSiguiente.Click += new System.EventHandler(this.BtnSiguienteClick);
			// 
			// btnCerrar
			// 
			this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
			this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCerrar.ForeColor = System.Drawing.Color.White;
			this.btnCerrar.Location = new System.Drawing.Point(24, 626);
			this.btnCerrar.Name = "btnCerrar";
			this.btnCerrar.Size = new System.Drawing.Size(180, 56);
			this.btnCerrar.TabIndex = 5;
			this.btnCerrar.Text = "◄ Cerrar";
			this.btnCerrar.UseVisualStyleBackColor = false;
			this.btnCerrar.Click += new System.EventHandler(this.BtnCerrarClick);
			// 
			// Historia
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(42)))));
			this.ClientSize = new System.Drawing.Size(1280, 720);
			this.Controls.Add(this.btnCerrar);
			this.Controls.Add(this.btnSiguiente);
			this.Controls.Add(this.lblCapitulo);
			this.Controls.Add(this.picImagen);
			this.Controls.Add(this.lblTexto);
			this.Controls.Add(this.lblTitulo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.MaximizeBox = true;
			this.Name = "Historia";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Modo Historia - Leviatanes del Caribe 2";
			((System.ComponentModel.ISupportInitialize)(this.picImagen)).EndInit();
			this.ResumeLayout(false);
		}
		
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.Label lblTexto;
		private System.Windows.Forms.PictureBox picImagen;
		private System.Windows.Forms.Label lblCapitulo;
		private System.Windows.Forms.Button btnSiguiente;
		private System.Windows.Forms.Button btnCerrar;
	}
}
