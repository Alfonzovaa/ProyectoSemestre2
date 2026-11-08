using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Leviatanes2
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
		}

		void MainFormPaint(object sender, PaintEventArgs e)
		{
			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;

			int W = this.ClientSize.Width;
			int H = this.ClientSize.Height;

			// Fondo degradado ardiente estilo intro Pokemon Rojo Fuego (GBA)
			using (LinearGradientBrush Fondo = new LinearGradientBrush(
				new Rectangle(0, 0, W, H),
				Color.FromArgb(16, 20, 42),
				Color.FromArgb(150, 32, 22),
				LinearGradientMode.Vertical))
			{
				G.FillRectangle(Fondo, 0, 0, W, H);
			}

			// Bandas horizontales de escaneo estilo pantalla Game Boy Advance
			using (Pen LapizScan = new Pen(Color.FromArgb(24, 255, 205, 85), 2))
			{
				for (int Y = 48; Y < H - 50; Y += 6)
				{
					G.DrawLine(LapizScan, 0, Y, W, Y);
				}
			}

			// 4 Insignias Elementales flanqueando simetricamente el menu central (960x640)
			DibujarInsigniaElementalGba(G, 135, 305, Color.FromArgb(215, 55, 35), "FUEGO", "PERFORANTE");
			DibujarInsigniaElementalGba(G, 135, 445, Color.FromArgb(35, 115, 210), "AGUA", "CURA Y DRENA");
			DibujarInsigniaElementalGba(G, 825, 305, Color.FromArgb(150, 95, 45), "TIERRA", "CORAZA ROCA");
			DibujarInsigniaElementalGba(G, 825, 445, Color.FromArgb(45, 165, 130), "AIRE", "DOBLE RAFAGA");
		}

		private void DibujarInsigniaElementalGba(Graphics G, int CentroX, int CentroY, Color ColorElemento, string NombreElemento, string Subtexto)
		{
			using (SolidBrush Sombra = new SolidBrush(Color.FromArgb(110, 0, 0, 0)))
			using (SolidBrush FondoPlaca = new SolidBrush(Color.FromArgb(24, 34, 54)))
			using (SolidBrush BrochaOrbe = new SolidBrush(ColorElemento))
			using (Pen BordeOro = new Pen(Color.Gold, 3))
			using (Pen BordeBlanco = new Pen(Color.White, 2))
			using (Font FuenteTitulo = new Font("Consolas", 10F, FontStyle.Bold))
			using (Font FuenteSub = new Font("Consolas", 8F, FontStyle.Bold))
			{
				Rectangle RectPlaca = new Rectangle(CentroX - 105, CentroY - 46, 210, 92);
				G.FillRectangle(Sombra, RectPlaca.X + 5, RectPlaca.Y + 5, RectPlaca.Width, RectPlaca.Height);
				G.FillRectangle(FondoPlaca, RectPlaca);
				G.DrawRectangle(BordeOro, RectPlaca);

				G.FillEllipse(BrochaOrbe, CentroX - 90, CentroY - 28, 56, 56);
				G.DrawEllipse(BordeBlanco, CentroX - 90, CentroY - 28, 56, 56);

				G.DrawString(NombreElemento, FuenteTitulo, Brushes.Gold, CentroX - 24, CentroY - 18);
				G.DrawString(Subtexto, FuenteSub, Brushes.WhiteSmoke, CentroX - 24, CentroY + 4);
			}
		}

		void Button1MouseEnter(object sender, EventArgs e)
		{
			lblPressStartGba.Text = "Abre el Laboratorio Elemental (CrearP) para crear y gestionar Leviatanes en .TXT ▼";
		}

		void Button2MouseEnter(object sender, EventArgs e)
		{
			lblPressStartGba.Text = "Consulta el registro histórico de partidas y estadísticas de jugadores ▼";
		}

		void Button3MouseEnter(object sender, EventArgs e)
		{
			lblPressStartGba.Text = "Guarda los datos y cierra el simulador Leviatanes del Caribe 2 ▼";
		}

		void Button1Click(object sender, EventArgs e)
		{
			CrearP abrirVentana = new CrearP();
			abrirVentana.Show();

			abrirVentana.FormClosed += (s, args) => Application.Exit();

			this.Hide();
		}

		void Button2Click(object sender, EventArgs e)
		{
			VerPartidas abrirVentana = new VerPartidas();
			abrirVentana.Show();

			abrirVentana.FormClosed += (s, args) => Application.Exit();

			this.Hide();
		}

		void Button3Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
