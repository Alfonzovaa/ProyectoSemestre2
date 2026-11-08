using System.Windows.Forms;
/*
 * Created by SharpDevelop.
 * User: alexa
 * Date: 8/11/2026
 * Time: 1:15 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Leviatanes2
{
	public partial class calculo : Form
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
			this.btnSiguiente = new System.Windows.Forms.Button();
			this.btnComprobar = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtRespuesta = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTitulo
			// 
			this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
			this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitulo.ForeColor = System.Drawing.Color.Gold;
			this.lblTitulo.Location = new System.Drawing.Point(30, 15);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(920, 48);
			this.lblTitulo.TabIndex = 0;
			this.lblTitulo.Text = "calculo";
			this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblEnunciado
			// 
			this.lblEnunciado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
			this.lblEnunciado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblEnunciado.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEnunciado.ForeColor = System.Drawing.Color.White;
			this.lblEnunciado.Location = new System.Drawing.Point(20, 11);
			this.lblEnunciado.Name = "lblEnunciado";
			this.lblEnunciado.Padding = new System.Windows.Forms.Padding(18);
			this.lblEnunciado.Size = new System.Drawing.Size(417, 294);
			this.lblEnunciado.TabIndex = 1;
			this.lblEnunciado.Text = "Anunciado";
			// 
			// btnSiguiente
			// 
			this.btnSiguiente.BackColor = System.Drawing.Color.Gold;
			this.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSiguiente.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSiguiente.ForeColor = System.Drawing.Color.Black;
			this.btnSiguiente.Location = new System.Drawing.Point(423, 511);
			this.btnSiguiente.Name = "btnSiguiente";
			this.btnSiguiente.Size = new System.Drawing.Size(175, 42);
			this.btnSiguiente.TabIndex = 4;
			this.btnSiguiente.Text = "Siguiente ▶";
			this.btnSiguiente.UseVisualStyleBackColor = false;
			this.btnSiguiente.Click += new System.EventHandler(this.BtnsiguienteClick);
			// 
			// btnComprobar
			// 
			this.btnComprobar.BackColor = System.Drawing.Color.Chartreuse;
			this.btnComprobar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnComprobar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnComprobar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnComprobar.ForeColor = System.Drawing.Color.Black;
			this.btnComprobar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnComprobar.Location = new System.Drawing.Point(147, 363);
			this.btnComprobar.Name = "btnComprobar";
			this.btnComprobar.Size = new System.Drawing.Size(175, 42);
			this.btnComprobar.TabIndex = 6;
			this.btnComprobar.Text = "        Comprobar                     ";
			this.btnComprobar.UseVisualStyleBackColor = false;
			this.btnComprobar.Click += new System.EventHandler(this.BtnComprobarClick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtRespuesta);
			this.panel1.Controls.Add(this.btnComprobar);
			this.panel1.Controls.Add(this.lblEnunciado);
			this.panel1.Location = new System.Drawing.Point(276, 66);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(450, 420);
			this.panel1.TabIndex = 7;
			// 
			// txtRespuesta
			// 
			this.txtRespuesta.Location = new System.Drawing.Point(147, 318);
			this.txtRespuesta.Multiline = true;
			this.txtRespuesta.Name = "txtRespuesta";
			this.txtRespuesta.Size = new System.Drawing.Size(175, 23);
			this.txtRespuesta.TabIndex = 7;
			// 
			// calculo
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(20)))), ((int)(((byte)(42)))));
			this.ClientSize = new System.Drawing.Size(980, 580);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnSiguiente);
			this.Controls.Add(this.lblTitulo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "calculo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Modo Historia - Leviatanes del Caribe 2";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.TextBox txtRespuesta;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnComprobar;
		
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.Label lblEnunciado;
		private System.Windows.Forms.Button btnSiguiente;
	}
}