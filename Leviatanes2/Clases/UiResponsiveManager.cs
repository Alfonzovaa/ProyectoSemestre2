using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Leviatanes2.Clases
{
	/// <summary>
	/// Gestor centralizado de resolucion unificada (1280x720 px),
	/// modo de pantalla completa persistente y escalado responsivo proporcional.
	/// </summary>
	public static class UiResponsiveManager
	{
		public const int ANCHO_BASE = 1280;
		public const int ALTO_BASE = 720;

		private static bool _modoPantallaCompleta = false;
		public static bool ModoPantallaCompleta
		{
			get { return _modoPantallaCompleta; }
			set { _modoPantallaCompleta = value; }
		}

		private static FormWindowState _ultimoEstadoVentana = FormWindowState.Normal;
		public static FormWindowState UltimoEstadoVentana
		{
			get { return _ultimoEstadoVentana; }
			set { _ultimoEstadoVentana = value; }
		}

		private static Rectangle? _ultimosBoundsVentana = null;
		public static Rectangle? UltimosBoundsVentana
		{
			get { return _ultimosBoundsVentana; }
			set { _ultimosBoundsVentana = value; }
		}

		private static readonly Dictionary<Form, GestorEscaladoForm> GestoresPorForm =
			new Dictionary<Form, GestorEscaladoForm>();

		/// <summary>
		/// Configura la ventana para la resolucion estandar 1280x720, activa buffering,
		/// atajos de pantalla completa (F11 / Alt+Enter), y registra todos los controles
		/// para su escalado responsivo automatico.
		/// </summary>
		public static void ConfigurarFormulario(Form form)
		{
			if (form == null)
				return;

			form.AutoScaleMode = AutoScaleMode.None;
			form.KeyPreview = true;
			form.MaximizeBox = true;
			form.MinimumSize = new Size(960, 540);
			form.StartPosition = FormStartPosition.CenterScreen;

			HabilitarDoubleBufferingRecursivo(form);

			// Atajos de teclado universales: F11 y Alt+Enter
			form.KeyDown -= FormKeyDownGlobal;
			form.KeyDown += FormKeyDownGlobal;

			form.FormClosed -= FormCerradoSalirAplicacion;
			form.FormClosed += FormCerradoSalirAplicacion;

			// Registrar controles para responsividad
			GestorEscaladoForm gestor;
			if (!GestoresPorForm.TryGetValue(form, out gestor))
			{
				gestor = new GestorEscaladoForm(form);
				GestoresPorForm[form] = gestor;
			}
			gestor.CapturarDisenoOriginal();

			// Aplicar estado (pantalla completa o 1280x720 normal)
			AplicarModo(form);

			form.Resize -= FormResizeGlobal;
			form.Resize += FormResizeGlobal;

			form.FormClosed -= FormClosedGlobal;
			form.FormClosed += FormClosedGlobal;
		}

		private static void FormKeyDownGlobal(object sender, KeyEventArgs e)
		{
			Form f = sender as Form;
			if (f == null)
				return;

			if (e.KeyCode == Keys.F11 || (e.Alt && e.KeyCode == Keys.Enter))
			{
				AlternarPantallaCompleta(f);
				e.Handled = true;
			}
		}

		private static void FormResizeGlobal(object sender, EventArgs e)
		{
			Form f = sender as Form;
			if (f == null)
				return;

			GestorEscaladoForm gestor;
			if (GestoresPorForm.TryGetValue(f, out gestor))
			{
				gestor.Escalar();
			}
		}

		/// <summary>
		/// Al cerrar cualquier ventana con la X se termina toda la aplicacion,
		/// evitando que los formularios ocultos queden vivos en segundo plano.
		/// </summary>
		private static void FormCerradoSalirAplicacion(object sender, FormClosedEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Application.Exit();
			}
		}

		private static void FormClosedGlobal(object sender, FormClosedEventArgs e)
		{
			Form f = sender as Form;
			if (f != null && GestoresPorForm.ContainsKey(f))
			{
				GestoresPorForm.Remove(f);
			}
		}

		/// <summary>
		/// Alterna entre pantalla completa (sin bordes) y modo ventana (1280x720).
		/// </summary>
		public static void AlternarPantallaCompleta(Form form)
		{
			ModoPantallaCompleta = !ModoPantallaCompleta;
			AplicarModo(form);
		}

		/// <summary>
		/// Aplica el modo visual segun la configuracion global actual.
		/// </summary>
		public static void AplicarModo(Form form)
		{
			if (form == null || form.IsDisposed)
				return;

			if (ModoPantallaCompleta)
			{
				if (form.WindowState == FormWindowState.Normal)
				{
					UltimosBoundsVentana = form.Bounds;
				}

				form.FormBorderStyle = FormBorderStyle.None;
				form.WindowState = FormWindowState.Normal;
				form.WindowState = FormWindowState.Maximized;

				Screen pantalla = Screen.FromControl(form);
				form.Bounds = pantalla.Bounds;
			}
			else
			{
				form.FormBorderStyle = FormBorderStyle.Sizable;
				form.WindowState = FormWindowState.Normal;
				form.ClientSize = new Size(ANCHO_BASE, ALTO_BASE);

				if (UltimosBoundsVentana.HasValue)
				{
					form.Bounds = UltimosBoundsVentana.Value;
				}
				else
				{
					Screen pantalla = Screen.FromControl(form);
					form.Location = new Point(
						pantalla.WorkingArea.Left + Math.Max(0, (pantalla.WorkingArea.Width - form.Width) / 2),
						pantalla.WorkingArea.Top + Math.Max(0, (pantalla.WorkingArea.Height - form.Height) / 2)
					);
				}
			}

			GestorEscaladoForm gestor;
			if (GestoresPorForm.TryGetValue(form, out gestor))
			{
				gestor.Escalar();
			}
		}

		/// <summary>
		/// Realiza la transicion suave de una ventana a otra manteniendo el estado
		/// de pantalla completa, posicion y proporciones.
		/// </summary>
		public static void CambiarVentana(Form actual, Form siguiente)
		{
			if (siguiente == null)
				return;

			if (actual != null && !actual.IsDisposed)
			{
				if (actual.WindowState == FormWindowState.Normal && !ModoPantallaCompleta)
				{
					UltimosBoundsVentana = actual.Bounds;
				}
			}

			ConfigurarFormulario(siguiente);
			siguiente.FormClosed -= FormCerradoSalirAplicacion;
			siguiente.FormClosed += FormCerradoSalirAplicacion;
			siguiente.Show();

			if (actual != null && !actual.IsDisposed)
			{
				actual.Hide();
			}
		}

		/// <summary>
		/// Activa DoubleBuffered por reflexion en todos los contenedores para evitar parpadeos.
		/// </summary>
		public static void HabilitarDoubleBufferingRecursivo(Control padre)
		{
			if (padre == null)
				return;

			try
			{
				typeof(Control).InvokeMember(
					"DoubleBuffered",
					BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
					null,
					padre,
					new object[] { true });
			}
			catch
			{
			}

			foreach (Control hijo in padre.Controls)
			{
				HabilitarDoubleBufferingRecursivo(hijo);
			}
		}

		/// <summary>
		/// Gestor interno de escalado responsivo para un formulario especifico.
		/// </summary>
		private class GestorEscaladoForm
		{
			private readonly Form Formulario;
			private readonly List<SnapshotControl> Snapshots = new List<SnapshotControl>();
			private bool Capturado = false;

			public GestorEscaladoForm(Form form)
			{
				this.Formulario = form;
			}

			public void CapturarDisenoOriginal()
			{
				Snapshots.Clear();
				CapturarRecursivo(Formulario, true);
				Capturado = true;
			}

			private void CapturarRecursivo(Control contenedor, bool esRaiz)
			{
				foreach (Control c in contenedor.Controls)
				{
					SnapshotControl s = new SnapshotControl();
					s.Control = c;
					s.BoundsOriginales = c.Bounds;
					s.EsHijoDirectoForm = esRaiz;
					s.DockOriginal = c.Dock;

					if (c.Font != null)
					{
						s.TamanoFuenteOriginal = c.Font.Size;
						s.EstiloFuenteOriginal = c.Font.Style;
						s.FamiliaFuenteOriginal = c.Font.FontFamily;
					}

					// Fijar anclas para evitar que el motor por defecto de WinForms deforme los controles
					if (c.Dock != DockStyle.Fill)
					{
						c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
					}

					Snapshots.Add(s);

					if (c.HasChildren)
					{
						CapturarRecursivo(c, false);
					}
				}
			}

			public void Escalar()
			{
				if (!Capturado || Formulario == null || Formulario.IsDisposed)
					return;

				int clientW = Formulario.ClientSize.Width;
				int clientH = Formulario.ClientSize.Height;
				if (clientW <= 0 || clientH <= 0)
					return;

				// Escala proporcional manteniendo aspecto 16:9
				float escalaX = (float)clientW / ANCHO_BASE;
				float escalaY = (float)clientH / ALTO_BASE;
				float escala = Math.Min(escalaX, escalaY);
				if (escala <= 0.05f)
					return;

				int offsetX = (int)Math.Round((clientW - (ANCHO_BASE * escala)) / 2.0);
				int offsetY = (int)Math.Round((clientH - (ALTO_BASE * escala)) / 2.0);

				Formulario.SuspendLayout();

				for (int i = 0; i < Snapshots.Count; i++)
				{
					SnapshotControl s = Snapshots[i];
					Control c = s.Control;
					if (c == null || c.IsDisposed)
						continue;

					if (s.DockOriginal == DockStyle.Fill)
					{
						// Dejar que DockStyle.Fill gestione su tamano con el padre
					}
					else if (s.EsHijoDirectoForm)
					{
						int nx = offsetX + (int)Math.Round(s.BoundsOriginales.X * escala);
						int ny = offsetY + (int)Math.Round(s.BoundsOriginales.Y * escala);
						int nw = Math.Max(1, (int)Math.Round(s.BoundsOriginales.Width * escala));
						int nh = Math.Max(1, (int)Math.Round(s.BoundsOriginales.Height * escala));
						c.SetBounds(nx, ny, nw, nh);
					}
					else
					{
						int nx = (int)Math.Round(s.BoundsOriginales.X * escala);
						int ny = (int)Math.Round(s.BoundsOriginales.Y * escala);
						int nw = Math.Max(1, (int)Math.Round(s.BoundsOriginales.Width * escala));
						int nh = Math.Max(1, (int)Math.Round(s.BoundsOriginales.Height * escala));
						c.SetBounds(nx, ny, nw, nh);
					}

					// Escalado proporcional de fuentes
					if (s.TamanoFuenteOriginal > 0 && s.FamiliaFuenteOriginal != null)
					{
						float nuevoTamFuente = Math.Max(5.5f, s.TamanoFuenteOriginal * escala);
						if (Math.Abs(c.Font.Size - nuevoTamFuente) > 0.25f)
						{
							try
							{
								c.Font = new Font(s.FamiliaFuenteOriginal, nuevoTamFuente, s.EstiloFuenteOriginal);
							}
							catch
							{
							}
						}
					}
				}

				Formulario.ResumeLayout(true);
				Formulario.Invalidate();
			}
		}

		private class SnapshotControl
		{
			public Control Control { get; set; }
			public Rectangle BoundsOriginales { get; set; }
			public bool EsHijoDirectoForm { get; set; }
			public DockStyle DockOriginal { get; set; }
			public float TamanoFuenteOriginal { get; set; }
			public FontStyle EstiloFuenteOriginal { get; set; }
			public FontFamily FamiliaFuenteOriginal { get; set; }
		}
	}
}
