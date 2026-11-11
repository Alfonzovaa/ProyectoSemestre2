namespace Leviatanes2
{
	partial class Calculo
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
			this.lblEnunciado = new System.Windows.Forms.Label();
			this.lblInstruccion = new System.Windows.Forms.Label();
			this.txtRespuesta = new System.Windows.Forms.TextBox();
			this.btnComprobar = new System.Windows.Forms.Button();
			this.lblFeedback = new System.Windows.Forms.Label();
			this.picImagen = new System.Windows.Forms.PictureBox();
			this.btnVolver = new System.Windows.Forms.Button();
			this.btnSiguiente = new System.Windows.Forms.Button();
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
			this.lblTitulo.Text = "LABORATORIO ELEMENTAL - RETO DE CÁLCULO";
			this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblEnunciado
			// 
			this.lblEnunciado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
			this.lblEnunciado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblEnunciado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEnunciado.ForeColor = System.Drawing.Color.White;
			this.lblEnunciado.Location = new System.Drawing.Point(24, 84);
			this.lblEnunciado.Name = "lblEnunciado";
			this.lblEnunciado.Padding = new System.Windows.Forms.Padding(18);
			this.lblEnunciado.Size = new System.Drawing.Size(600, 310);
			this.lblEnunciado.TabIndex = 1;
			this.lblEnunciado.Text = "Enunciado del ejercicio...";
			// 
			// lblInstruccion
			// 
			this.lblInstruccion.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInstruccion.ForeColor = System.Drawing.Color.Gold;
			this.lblInstruccion.Location = new System.Drawing.Point(24, 404);
			this.lblInstruccion.Name = "lblInstruccion";
			this.lblInstruccion.Size = new System.Drawing.Size(240, 26);
			this.lblInstruccion.TabIndex = 2;
			this.lblInstruccion.Text = "Escribe tu respuesta:";
			// 
			// txtRespuesta
			// 
			this.txtRespuesta.BackColor = System.Drawing.Color.White;
			this.txtRespuesta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRespuesta.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRespuesta.ForeColor = System.Drawing.Color.Black;
			this.txtRespuesta.Location = new System.Drawing.Point(24, 434);
			this.txtRespuesta.Name = "txtRespuesta";
			this.txtRespuesta.Size = new System.Drawing.Size(320, 34);
			this.txtRespuesta.TabIndex = 3;
			this.txtRespuesta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtRespuesta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRespuestaKeyDown);
			// 
			// btnComprobar
			// 
			this.btnComprobar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
			this.btnComprobar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnComprobar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnComprobar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnComprobar.ForeColor = System.Drawing.Color.White;
			this.btnComprobar.Location = new System.Drawing.Point(360, 432);
			this.btnComprobar.Name = "btnComprobar";
			this.btnComprobar.Size = new System.Drawing.Size(264, 38);
			this.btnComprobar.TabIndex = 4;
			this.btnComprobar.Text = "✔ Comprobar";
			this.btnComprobar.UseVisualStyleBackColor = false;
			this.btnComprobar.Click += new System.EventHandler(this.BtnComprobarClick);
			// 
			// lblFeedback
			// 
			this.lblFeedback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(28)))), ((int)(((byte)(46)))));
			this.lblFeedback.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFeedback.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFeedback.ForeColor = System.Drawing.Color.Gold;
			this.lblFeedback.Location = new System.Drawing.Point(24, 484);
			this.lblFeedback.Name = "lblFeedback";
			this.lblFeedback.Padding = new System.Windows.Forms.Padding(10);
			this.lblFeedback.Size = new System.Drawing.Size(600, 124);
			this.lblFeedback.TabIndex = 5;
			this.lblFeedback.Text = "Ingresa tu respuesta y presiona Comprobar.";
			this.lblFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// picImagen
			// 
			this.picImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(16)))), ((int)(((byte)(28)))));
			this.picImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picImagen.Location = new System.Drawing.Point(640, 84);
			this.picImagen.Name = "picImagen";
			this.picImagen.Size = new System.Drawing.Size(616, 524);
			this.picImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picImagen.TabIndex = 6;
			this.picImagen.TabStop = false;
			// 
			// btnVolver
			// 
			this.btnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
			this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnVolver.ForeColor = System.Drawing.Color.White;
			this.btnVolver.Location = new System.Drawing.Point(24, 626);
			this.btnVolver.Name = "btnVolver";
			this.btnVolver.Size = new System.Drawing.Size(180, 56);
			this.btnVolver.TabIndex = 7;
			this.btnVolver.Text = "◄ Salir";
			this.btnVolver.UseVisualStyleBackColor = false;
			this.btnVolver.Click += new System.EventHandler(this.BtnVolverClick);
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
			this.btnSiguiente.TabIndex = 8;
			this.btnSiguiente.Text = "Continuar a la Historia ▶";
			this.btnSiguiente.UseVisualStyleBackColor = false;
			this.btnSiguiente.Click += new System.EventHandler(this.BtnSiguienteClick);
			// 
			// Calculo
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(42)))));
			this.ClientSize = new System.Drawing.Size(1280, 720);
			this.Controls.Add(this.btnSiguiente);
			this.Controls.Add(this.btnVolver);
			this.Controls.Add(this.picImagen);
			this.Controls.Add(this.lblFeedback);
			this.Controls.Add(this.btnComprobar);
			this.Controls.Add(this.txtRespuesta);
			this.Controls.Add(this.lblInstruccion);
			this.Controls.Add(this.lblEnunciado);
			this.Controls.Add(this.lblTitulo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.MaximizeBox = true;
			this.Name = "Calculo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reto de Cálculo - Leviatanes del Caribe 2";
			((System.ComponentModel.ISupportInitialize)(this.picImagen)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.Label lblEnunciado;
		private System.Windows.Forms.Label lblInstruccion;
		private System.Windows.Forms.TextBox txtRespuesta;
		private System.Windows.Forms.Button btnComprobar;
		private System.Windows.Forms.Label lblFeedback;
		private System.Windows.Forms.PictureBox picImagen;
		private System.Windows.Forms.Button btnVolver;
		private System.Windows.Forms.Button btnSiguiente;
	}
}
