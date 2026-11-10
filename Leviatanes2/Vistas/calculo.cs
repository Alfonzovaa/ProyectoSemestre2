using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Leviatanes2
{
	/// <summary>
	/// Formulario de Retos de Cálculo para Leviatanes del Caribe 2.
	/// Se presenta después de cada batalla (Batalla 1 a 5) antes de continuar con el siguiente capítulo de la historia.
	/// A la izquierda muestra el enunciado interactivo y a la derecha la imagen correspondiente (Ejercicio1, Ejercicio2, etc.).
	/// </summary>
	public partial class Calculo : Form
	{
		// Número de ejercicio solicitado (1 al 5)
		public int ejercicioSolicitado = 1;

		// Variables para validación del reto
		private string respuestaCorrectaTexto = "";
		private bool respuestaValidada = false;

		// Variables para el Reto 1 (Pendiente con puntos aleatorios)
		private static readonly Random random = new Random();
		private int x1, y1, x2, y2;
		private double pendienteEsperada;

		public Calculo() : this(1)
		{
		}

		public Calculo(int numeroEjercicio)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			this.ejercicioSolicitado = numeroEjercicio;
			SincronizarCarpetasDeImagenes();
			CargarEjercicio();
		}

		/// <summary>
		/// Configura el enunciado, los datos y la imagen correspondiente al ejercicio actual.
		/// </summary>
		public void CargarEjercicio()
		{
			respuestaValidada = false;
			btnSiguiente.Enabled = false;
			txtRespuesta.Clear();

			bool esIngles = (ejercicioSolicitado == 2);

			if (esIngles)
			{
				this.Text = "Math Challenge - Leviathans of the Caribbean 2";
				lblInstruccion.Text = "Type your answer:";
				btnComprobar.Text = "✔ Check";
				btnVolver.Text = "◄ Exit";
				btnSiguiente.Text = "Continue to Battle 2 ▶";
				lblFeedback.Text = "Type your answer and press the '✔ Check' button.";
				lblFeedback.ForeColor = Color.Gold;
			}
			else
			{
				this.Text = "Reto de Cálculo - Leviatanes del Caribe 2";
				lblInstruccion.Text = "Escribe tu respuesta:";
				btnComprobar.Text = "✔ Comprobar";
				btnVolver.Text = "◄ Salir";
				btnSiguiente.Text = string.Format("Ir a la Batalla {0} ▶", ejercicioSolicitado);
				lblFeedback.Text = "Escribe tu respuesta y presiona el botón '✔ Comprobar'.";
				lblFeedback.ForeColor = Color.Gold;
			}

			switch (ejercicioSolicitado)
			{
				case 1:
					GenerarPuntosAleatorios();
					lblTitulo.Text = "RETO 1: PENDIENTE DE LA RECTA";
					lblEnunciado.Text = "Reto 1: Ecuación de la Recta\n\n" +
					                    "Hallar la pendiente de la recta que pasa por los puntos:\n\n" +
					                    string.Format("    P1 = ({0}, {1})\n", x1, y1) +
					                    string.Format("    P2 = ({0}, {1})\n\n", x2, y2) +
					                    "Indicaciones:\n" +
					                    "• Si el resultado es negativo, recuerda incluir el signo '-'.\n" +
					                    "• Si el resultado es decimal, se toman en cuenta los primeros 2 decimales (ejemplo: -1.25 o 0.66).";
					ActualizarImagen("Ejercicio1");
					break;

				case 2:
					lblTitulo.Text = "CHALLENGE 2: DIRECT LIMITS";
					lblEnunciado.Text = "Challenge 2: Direct Limits\n\n" +
					                    "The limit of a function as x approaches 'a' consists of evaluating the function at that point if it does not produce an indeterminacy:\n\n" +
					                    "    Calculate:  lim (x -> 3) of (2x + 4)\n\n" +
					                    "Note: Type the numerical result in the box and press Check.";
					respuestaCorrectaTexto = "10";
					ActualizarImagen("Ejercicio2");
					break;

				case 3:
					lblTitulo.Text = "RETO 3: FORMA INDETERMINADA 0/0";
					lblEnunciado.Text = "Reto 3: Forma Indeterminada 0/0\n\n" +
					                    "Cuando la sustitución directa produce 0/0, factoriza la diferencia de cuadrados para simplificar:\n\n" +
					                    "    Calcula:  lim (x -> 2) de (x² - 4) / (x - 2)\n\n" +
					                    "Nota: Escribe el valor numérico obtenido en la casilla y presiona Comprobar.";
					respuestaCorrectaTexto = "4";
					ActualizarImagen("Ejercicio3");
					break;

				case 4:
					lblTitulo.Text = "RETO 4: DERIVADA (REGLA DE LA POTENCIA)";
					lblEnunciado.Text = "Reto 4: Derivada (Regla de la Potencia)\n\n" +
					                    "Para derivar f(x) = x^n se multiplica por el exponente y se disminuye en 1 su grado: f'(x) = n · x^(n - 1).\n\n" +
					                    "    Calcula la derivada de:  f(x) = x⁴\n\n" +
						 				"Ejemplo: 7x^3";
					respuestaCorrectaTexto = "4x^3";
					ActualizarImagen("Ejercicio4");
					break;

				case 5:
					lblTitulo.Text = "RETO 5: DERIVADA (CONSTANTE POR FUNCIÓN)";
					lblEnunciado.Text = "Reto 5: Derivada (Constante por Función)\n\n" +
					                    "La derivada de una constante multiplicada por una función es c · f'(x):\n\n" +
					                    "    Calcula la derivada de:  f(x) = 3x²\n\n" +
					                    "Ejemplo: 4x";
					respuestaCorrectaTexto = "6x";
					ActualizarImagen("Ejercicio5");
					break;

				default:
					lblTitulo.Text = "RETO DE CÁLCULO";
					lblEnunciado.Text = "Has completado todos los ejercicios matemáticos de la campaña.";
					btnSiguiente.Enabled = true;
					break;
			}
		}

		/// <summary>
		/// Genera dos puntos P1(x1, y1) y P2(x2, y2) aleatorios con valores enteros entre -9 y 9.
		/// Garantiza que x1 != x2 para evitar división entre cero en la pendiente.
		/// </summary>
		private void GenerarPuntosAleatorios()
		{
			x1 = random.Next(-9, 10);
			y1 = random.Next(-9, 10);

			do
			{
				x2 = random.Next(-9, 10);
			} while (x2 == x1);

			y2 = random.Next(-9, 10);

			pendienteEsperada = (double)(y2 - y1) / (double)(x2 - x1);
		}

		/// <summary>
		/// Valida la respuesta del usuario para el Reto 1:
		/// - Detecta si el resultado es negativo y verifica que el usuario haya escrito el signo '-'.
		/// - En caso de ser decimal, toma en cuenta los primeros 2 decimales (tanto redondeados como truncados).
		/// </summary>
		private bool ValidarPendiente(string textoUsuario)
		{
			if (string.IsNullOrEmpty(textoUsuario))
				return false;

			string normalizado = textoUsuario.Trim().Replace(',', '.');

			double valorUsuario;
			bool esValido = double.TryParse(normalizado, NumberStyles.Any, CultureInfo.InvariantCulture, out valorUsuario);

			if (!esValido)
				return false;

			// 1. Verificación estricta del signo negativo
			if (pendienteEsperada < -0.0001 && valorUsuario > 0)
			{
				lblFeedback.Text = "✘ Atención: La pendiente resultante es negativa. Recuerda escribir el signo '-'.";
				lblFeedback.ForeColor = Color.Salmon;
				return false;
			}

			if (pendienteEsperada > 0.0001 && valorUsuario < 0)
			{
				lblFeedback.Text = "✘ Atención: La pendiente resultante es positiva. No debe llevar signo '-'.";
				lblFeedback.ForeColor = Color.Salmon;
				return false;
			}

			// 2. Tomar en cuenta los primeros 2 decimales del resultado
			double redondeado = Math.Round(pendienteEsperada, 2);
			double truncado = Math.Truncate(pendienteEsperada * 100.0) / 100.0;
			double diferencia = Math.Abs(valorUsuario - pendienteEsperada);

			// Coincidencia con redondeo, truncamiento o tolerancia de 2 decimales
			if (Math.Abs(valorUsuario - redondeado) < 0.001 ||
			    Math.Abs(valorUsuario - truncado) < 0.001 ||
			    diferencia <= 0.012)
			{
				return true;
			}

			// Comparación por formato de texto con 2 decimales
			string strRedondeado = redondeado.ToString("0.00", CultureInfo.InvariantCulture);
			string strRedondeadoSimple = redondeado.ToString("0.##", CultureInfo.InvariantCulture);
			string strTruncado = truncado.ToString("0.00", CultureInfo.InvariantCulture);
			string strTruncadoSimple = truncado.ToString("0.##", CultureInfo.InvariantCulture);

			if (normalizado == strRedondeado || normalizado == strRedondeadoSimple ||
			    normalizado == strTruncado || normalizado == strTruncadoSimple)
			{
				return true;
			}

			return false;
		}

		private void BtnComprobarClick(object sender, EventArgs e)
		{
			bool esIngles = (ejercicioSolicitado == 2);
			string input = txtRespuesta.Text.Trim();
			if (string.IsNullOrEmpty(input))
			{
				lblFeedback.ForeColor = Color.Gold;
				lblFeedback.Text = esIngles
					? "Please type a result in the box before checking."
					: "Por favor, escribe un resultado en la casilla antes de comprobar.";
				return;
			}

			bool esCorrecto = false;

			if (ejercicioSolicitado == 1)
			{
				esCorrecto = ValidarPendiente(input);
			}
			else
			{
				// Ejercicios 2 al 5: comparación directa sin distinguir mayúsculas ni espacios
				string usuarioNorm = input.ToLower().Replace(" ", "").Replace(',', '.');
				string esperadoNorm = respuestaCorrectaTexto.ToLower().Replace(" ", "").Replace(',', '.');
				esCorrecto = (usuarioNorm == esperadoNorm);
			}

			if (esCorrecto)
			{
				respuestaValidada = true;
				lblFeedback.ForeColor = Color.LightGreen;
				lblFeedback.Text = esIngles
					? "✔ Correct answer! Click 'Continue to Battle 2' to advance."
					: string.Format("✔ ¡Respuesta correcta! Haz clic en 'Ir a la Batalla {0}' para avanzar.", ejercicioSolicitado);
				btnSiguiente.Enabled = true;
				btnSiguiente.Focus();
			}
			else
			{
				// Si no fue error de signo ya advertido en ValidarPendiente, mostrar mensaje de error general
				if (ejercicioSolicitado != 1 || !lblFeedback.Text.Contains("Atención:"))
				{
					lblFeedback.ForeColor = Color.Salmon;
					lblFeedback.Text = esIngles
						? "✘ Incorrect answer. Check your calculations and try again."
						: "✘ Respuesta incorrecta. Revisa tus cálculos e inténtalo de nuevo.";
				}
			}
		}

		private void BtnSiguienteClick(object sender, EventArgs e)
		{
			// Después del Ejercicio N (1 al 5), se abre directamente la Batalla N correspondiente
			int numBatalla = this.ejercicioSolicitado;

			if (numBatalla >= 1 && numBatalla <= 5)
			{
				Batalla ventanaBatalla = new Batalla(numBatalla);
				ventanaBatalla.FormClosed += (s, args) => Application.Exit();
				ventanaBatalla.Show();
				this.Hide();
			}
			else
			{
				MainForm ventanaPrincipal = new MainForm();
				ventanaPrincipal.FormClosed += (s, args) => Application.Exit();
				ventanaPrincipal.Show();
				this.Hide();
			}
		}

		private void BtnVolverClick(object sender, EventArgs e)
		{
			bool esIngles = (ejercicioSolicitado == 2);
			string pregunta = esIngles ? "Do you want to return to the Main Menu?" : "¿Deseas salir al Menú Principal?";
			string titulo = esIngles ? "Math Challenge" : "Reto de Cálculo";

			DialogResult resultado = MessageBox.Show(
				pregunta,
				titulo,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (resultado == DialogResult.Yes)
			{
				MainForm menu = new MainForm();
				menu.FormClosed += (s, args) => Application.Exit();
				menu.Show();
				this.Hide();
			}
		}

		private void TxtRespuestaKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				BtnComprobarClick(sender, e);
			}
		}

		/// <summary>
		/// Carga la imagen correspondiente al ejercicio (ej: Ejercicio1.png) desde la carpeta 'imagenes'.
		/// </summary>
		private void ActualizarImagen(string nombreBaseEjercicio)
		{
			if (picImagen.Image != null)
			{
				picImagen.Image.Dispose();
				picImagen.Image = null;
			}

			picImagen.Image = CargarImagenDeCarpeta(nombreBaseEjercicio);
		}

		/// <summary>
		/// Busca y carga el archivo de imagen correspondiente en la carpeta 'imagenes'.
		/// Prioriza la carpeta 'imagenes' de la raíz del proyecto para desarrollo.
		/// </summary>
		private Image CargarImagenDeCarpeta(string nombreEjercicio)
		{
			List<string> carpetas = ObtenerCarpetasImagenes();
			if (carpetas.Count == 0) return null;

			string[] extensiones = new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
			List<string> nombresBusqueda = new List<string>
			{
				nombreEjercicio,
				nombreEjercicio.ToLowerInvariant(),
				nombreEjercicio.ToUpperInvariant(),
				nombreEjercicio.Replace("Ejercicio", "ejercicio"),
				nombreEjercicio.Replace("Ejercicio", "Reto"),
				nombreEjercicio.Replace("Ejercicio", "reto"),
				ejercicioSolicitado.ToString()
			};

			foreach (string carpeta in carpetas)
			{
				if (!Directory.Exists(carpeta)) continue;

				foreach (string baseName in nombresBusqueda)
				{
					foreach (string ext in extensiones)
					{
						string ruta = Path.Combine(carpeta, baseName + ext);
						if (File.Exists(ruta))
						{
							Image img = IntentarCargarImagenSinBloqueo(ruta);
							if (img != null)
							{
								SincronizarHaciaBinDebug(ruta);
								return img;
							}
						}
					}
				}

				// Búsqueda flexible por prefijo
				try
				{
					string[] archivos = Directory.GetFiles(carpeta);
					foreach (string archivo in archivos)
					{
						string soloNombre = Path.GetFileNameWithoutExtension(archivo).ToLowerInvariant();
						if (soloNombre.StartsWith(nombreEjercicio.ToLowerInvariant()) ||
						    soloNombre.StartsWith("ejercicio" + ejercicioSolicitado) ||
						    soloNombre.StartsWith("reto" + ejercicioSolicitado))
						{
							Image img = IntentarCargarImagenSinBloqueo(archivo);
							if (img != null)
							{
								SincronizarHaciaBinDebug(archivo);
								return img;
							}
						}
					}
				}
				catch { }
			}

			return null;
		}

		private Image IntentarCargarImagenSinBloqueo(string rutaArchivo)
		{
			try
			{
				byte[] bytes = File.ReadAllBytes(rutaArchivo);
				using (MemoryStream ms = new MemoryStream(bytes))
				{
					using (Image original = Image.FromStream(ms))
					{
						return new Bitmap(original);
					}
				}
			}
			catch
			{
				return null;
			}
		}

		private List<string> ObtenerCarpetasImagenes()
		{
			List<string> lista = new List<string>();

			string[] posiblesRaices = new string[]
			{
				Path.Combine(Application.StartupPath, "..", "..", "imagenes"),
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "imagenes"),
				Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "imagenes")
			};

			foreach (string ruta in posiblesRaices)
			{
				try
				{
					if (Directory.Exists(ruta))
					{
						string completa = Path.GetFullPath(ruta);
						if (!lista.Contains(completa))
							lista.Add(completa);
					}
				}
				catch { }
			}

			string[] posiblesSalidas = new string[]
			{
				Path.Combine(Application.StartupPath, "imagenes"),
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes"),
				Path.Combine(Directory.GetCurrentDirectory(), "imagenes"),
				"imagenes"
			};

			foreach (string ruta in posiblesSalidas)
			{
				try
				{
					if (Directory.Exists(ruta))
					{
						string completa = Path.GetFullPath(ruta);
						if (!lista.Contains(completa))
							lista.Add(completa);
					}
				}
				catch { }
			}

			return lista;
		}

		private void SincronizarCarpetasDeImagenes()
		{
			try
			{
				string rutaProyecto = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "imagenes"));
				string rutaSalida = Path.GetFullPath(Path.Combine(Application.StartupPath, "imagenes"));

				if (Directory.Exists(rutaProyecto))
				{
					if (!Directory.Exists(rutaSalida))
						Directory.CreateDirectory(rutaSalida);

					foreach (string archivo in Directory.GetFiles(rutaProyecto))
					{
						string nombre = Path.GetFileName(archivo);
						string destino = Path.Combine(rutaSalida, nombre);

						if (!File.Exists(destino) || File.GetLastWriteTimeUtc(archivo) > File.GetLastWriteTimeUtc(destino))
						{
							File.Copy(archivo, destino, true);
						}
					}
				}
			}
			catch { }
		}

		private void SincronizarHaciaBinDebug(string rutaArchivoOrigen)
		{
			try
			{
				string rutaSalida = Path.GetFullPath(Path.Combine(Application.StartupPath, "imagenes"));
				if (Directory.Exists(rutaSalida))
				{
					string destino = Path.Combine(rutaSalida, Path.GetFileName(rutaArchivoOrigen));
					if (!string.Equals(Path.GetFullPath(rutaArchivoOrigen), destino, StringComparison.OrdinalIgnoreCase))
					{
						if (!File.Exists(destino) || File.GetLastWriteTimeUtc(rutaArchivoOrigen) > File.GetLastWriteTimeUtc(destino))
						{
							File.Copy(rutaArchivoOrigen, destino, true);
						}
					}
				}
			}
			catch { }
		}
	}
}
