using System;
using System.Drawing;
using System.Windows.Forms;

namespace Leviatanes2
{
	/// <summary>
	/// Ventana de combate para el Modo Historia de Leviatanes del Caribe 2.
	/// Al hacer clic en 'Continuar', avanza al siguiente capítulo de la historia.
	/// </summary>
	public partial class Batalla : Form
	{
		// Número de batalla actual en el flujo de la historia (1 a 5)
		public int numeroBatalla = 1;

		public Batalla() : this(1)
		{
		}

		public Batalla(int batalla)
		{
			InitializeComponent();
			this.numeroBatalla = batalla;
			ConfigurarBatalla();
		}

		private void ConfigurarBatalla()
		{
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Batalla " + numeroBatalla + ": " + ObtenerNombreLeviatan(numeroBatalla) + " - Leviatanes 2";

			if (this.button1 != null)
			{
				int siguienteCapitulo = numeroBatalla + 1;
				this.button1.Text = "Continuar al Capítulo " + siguienteCapitulo + " ▶";
			}
		}

		private string ObtenerNombreLeviatan(int batalla)
		{
			switch (batalla)
			{
				case 1:
					return "Leviatán de la Fosa";
				case 2:
					return "Leviatán del Alquitrán";
				case 3:
					return "Leviatán Espectral";
				case 4:
					return "Leviatán de las Tormentas Ácidas";
				case 5:
					return "Leviatán Primordial del Petróleo";
				default:
					return "Leviatán";
			}
		}

		void Button1Click(object sender, EventArgs e)
		{
			// Después de la batalla N, se pasa al capítulo N + 1 de la historia
			int siguienteCapitulo = this.numeroBatalla + 1;

			if (siguienteCapitulo <= 6)
			{
				Historia ventanaHistoria = new Historia(siguienteCapitulo);
				ventanaHistoria.FormClosed += (s, args) => Application.Exit();
				ventanaHistoria.Show();
				this.Hide();
			}
			else
			{
				// Si ya pasó el capítulo 6, regresa al formulario principal
				MainForm ventanaPrincipal = new MainForm();
				ventanaPrincipal.FormClosed += (s, args) => Application.Exit();
				ventanaPrincipal.Show();
				this.Hide();
			}
		}
	}
}
