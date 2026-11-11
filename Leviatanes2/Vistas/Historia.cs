using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Leviatanes2
{
	/// <summary>
	/// Formulario de Modo Historia para Leviatanes del Caribe 2.
	/// Flujo de navegación:
	/// Capítulo 1 -> Batalla 1 -> Capítulo 2 -> Batalla 2 -> Capítulo 3 -> Batalla 3
	/// -> Capítulo 4 -> Batalla 4 -> Capítulo 5 -> Batalla 5 -> Capítulo 6 -> MainForm.
	/// </summary>
	public partial class Historia : Form
	{
		public int capitulo = 1;

		public Historia() : this(1)
		{
		}

		public Historia(int capituloInicial)
		{
			InitializeComponent();
			SincronizarCarpetasDeImagenes();
			MostrarCapitulo(capituloInicial);
		}

		public void MostrarCapitulo(int numCapitulo)
		{
			this.capitulo = numCapitulo;

			if (this.capitulo == 2)
			{
				MostrarModalAlertaAbisal(
					"⚠ FALLO DE CODIFICACION EN BITACORA ⚠",
					"INTERFERENCIA EN EL TRADUCTOR NAVAL",
					"El pulso electromagnético de las Aguas Negras alteró el módulo de idioma.\r\n\r\nEste capítulo y su combate se mostrarán en INGLÉS hasta restaurar el sistema.",
					"ENTENDIDO / CONTINUE >>",
					Color.FromArgb(28, 108, 196),
					"EN");
			}

			switch (capitulo)
			{
				case 1:
					this.Text = "Modo Historia - Leviatanes del Caribe 2";
					lblCapitulo.Text = "Capítulo 1 de 6";
					lblTexto.Text = "1. El Despertar en la Fosa (La Falsa Victoria)\n\n" +
					                "Tras la batalla final del primer juego, una de las gemas cayó al fondo del mar. La tripulación festejaba pensando que todos los leviatanes habían muerto para siempre. Pero la gema liberó un pulso de energía en las profundidades de la fosa, alterando las aguas y haciendo que la cubierta vuelva a temblar. El pánico regresa: la victoria era una ilusión nacida de la ignorancia.\n\n" +
					                "Batalla 1: Leviatán de la Fosa, una mole acorazada que emerge furiosa de la grieta donde cayó la gema.";
					ActualizarImagen(1, "El Despertar en la Fosa");
					btnSiguiente.Text = "Continuar al Ejercicio 1 ▶";
					break;

				case 2:
					this.Text = "Story Mode - Leviathans of the Caribbean 2";
					lblCapitulo.Text = "Chapter 2 of 6";
					lblTexto.Text = "2. The Black Waters (The Oil Trail)\n\n" +
					                "Venturing into open sea in search of answers, the ship crosses a massive abandoned oil slick. Ancient navigation charts showed active oil fields, but now the wells are dry. The crew begins to piece things together: the oil did not vanish by magic, but through reckless overexploitation that rotted the seabed.\n\n" +
					                "Battle 2: Leviathan of Tar, a mutated and blind beast lurking in dense waters polluted by old industrial spills.";
					ActualizarImagen(2, "Las Aguas Negras");
					btnSiguiente.Text = "Continue to Exercise 2 ▶";
					break;

				case 3:
					this.Text = "Modo Historia - Leviatanes del Caribe 2";
					lblCapitulo.Text = "Capítulo 3 de 6";
					lblTexto.Text = "3. El Mar Espejo (La Culpa Colectiva)\n\n" +
					                "Navegando entre plataformas petroleras abandonadas y residuos flotantes, la nave entra en una zona de aguas muertas. La superficie refleja la basura acumulada y la ambición humana que destruyó las costas. El capitán y los marineros enfrentan el peso de la culpa: los monstruos no aparecieron por maldición, sino como respuesta de la naturaleza a la contaminación del hombre.\n\n" +
					                "Batalla 3: Leviatán Espectral, un monstruo hecho de sombras y residuos tóxicos que toma la forma del daño provocado al océano.";
					ActualizarImagen(3, "El Mar Espejo");
					btnSiguiente.Text = "Continuar al Ejercicio 3 ▶";
					break;

				case 4:
					this.Text = "Modo Historia - Leviatanes del Caribe 2";
					lblCapitulo.Text = "Capítulo 4 de 6";
					lblTexto.Text = "4. La Revelación del Origen (Voluntad contra la Ruina)\n\n" +
					                "Atrapados en una tormenta ácida cerca del epicentro del desastre ambiental, la tripulación descubre la verdad definitiva: la contaminación tóxica y el drenaje de petróleo mutaron a la vida marina, convirtiéndola en los leviatanes que amenazan con destruir todo. La tripulación entiende que la tormenta no los va a perdonar, pero deciden tomar firmemente el timón para corregir los errores del pasado.\n\n" +
					                "Batalla 4: Leviatán de las Tormentas Ácidas, la criatura que alimenta los vientos tóxicos que asfixian los mares.";
					ActualizarImagen(4, "La Revelacion del Origen");
					btnSiguiente.Text = "Continuar al Ejercicio 4 ▶";
					break;

				case 5:
					this.Text = "Modo Historia - Leviatanes del Caribe 2";
					lblCapitulo.Text = "Capítulo 5 de 6";
					lblTexto.Text = "5. El Núcleo de la Podredumbre (El Abismo Final)\n\n" +
					                "En el corazón del Caribe yace el pozo original, donde la gema perdida encajó directamente en el cuerpo del primer leviatán, alimentándose de la contaminación acumulada durante décadas. Para detener el nacimiento de más monstruos, no basta con luchar: hay que destruir el núcleo del desecho tóxico antes de que consuma todo el océano.\n\n" +
					                "Batalla 5: Leviatán Primordial del Petróleo, la bestia colosal engendrada por la contaminación extrema, fortalecida por el poder de la gema.";
					ActualizarImagen(5, "El Nucleo de la Podredumbre");
					btnSiguiente.Text = "Continuar al Ejercicio 5 ▶";
					break;

				case 6:
					this.Text = "Modo Historia - Leviatanes del Caribe 2";
					lblCapitulo.Text = "Capítulo 6 de 6 (Final)";
					lblTexto.Text = "6. El Limpie del Caribe (Final)\n\n" +
					                "Con la criatura primordial vencida y la gema recuperada, la fuente de la contaminación se neutraliza. Las aguas negras comienzan a aclararse y el ciclo de mutaciones se detiene definitivamente: no volverán a nacer más leviatanes. El mar recupera su cauce natural y la tripulación navega hacia un horizonte limpio, habiendo detenido la amenaza y devolviéndole el equilibrio al Caribe.";
					ActualizarImagen(6, "El Limpie del Caribe");
					btnSiguiente.Text = "Continuar al Menú Principal ▶";
					break;

				default:
					lblCapitulo.Text = "Fin de la Historia";
					lblTexto.Text = "¡Has completado con éxito la historia de Leviatanes del Caribe 2!";
					btnSiguiente.Text = "Ir al Menú Principal ▶";
					break;
			}
		}

		public void AvanzarCapitulo()
		{
			switch (capitulo)
			{
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					bool esCapIngles = (capitulo == 2);
					MostrarModalAlertaAbisal(
						esCapIngles ? "⚠ CRITICAL SYSTEM ERROR IN PC ⚠" : "⚠ ALERTA CRITICA EN LA CONSOLA DE MANDO ⚠",
						esCapIngles ? "NAVIGATION CONSOLE LOCKED!" : "¡ANOMALIA DETECTADA EN LA PC!",
						esCapIngles
							? "An abyssal pulse has locked the ship's tactical computer.\r\n\r\nYou must solve this calculus challenge to calibrate the system and enter the battle."
							: "El pulso abisal ha bloqueado la consola táctica de la PC.\r\n\r\nDebes resolver este problema de cálculo para calibrar el sistema y avanzar a la batalla.",
						esCapIngles ? "CALIBRATE SYSTEM >>" : "CALIBRAR SISTEMA >>",
						Color.FromArgb(204, 41, 18),
						"!");

					Calculo ventanaCalculo = new Calculo(capitulo);
					ventanaCalculo.FormClosed += (s, args) => Application.Exit();
					ventanaCalculo.Show();
					this.Hide();
					break;

				case 6:
				default:
					MainForm ventanaPrincipal = new MainForm();
					ventanaPrincipal.FormClosed += (s, args) => Application.Exit();
					ventanaPrincipal.Show();
					this.Hide();
					break;
			}

			capitulo++;
		}

		// =====================================================================
		// MODAL TACTICO ESTILO CODICE ABISAL (REEMPLAZA MESSAGEBOX BLANCO)
		// =====================================================================
		private void MostrarModalAlertaAbisal(
			string tituloBarra,
			string subtituloOro,
			string cuerpoMensaje,
			string textoBoton,
			Color colorTema,
			string siglaSello)
		{
			using (Form Modal = new Form())
			{
				Modal.FormBorderStyle = FormBorderStyle.None;
				Modal.StartPosition = FormStartPosition.CenterParent;
				Modal.ClientSize = new Size(490, 265);
				Modal.BackColor = Color.FromArgb(245, 203, 66);
				Modal.ShowInTaskbar = false;

				Panel PnlInterior = new Panel();
				PnlInterior.SetBounds(3, 3, 484, 259);
				PnlInterior.BackColor = Color.FromArgb(12, 18, 30);
				Modal.Controls.Add(PnlInterior);

				PnlInterior.Paint += (s, e) =>
				{
					Graphics G = e.Graphics;
					G.SmoothingMode = SmoothingMode.AntiAlias;
					G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
					Rectangle R = PnlInterior.ClientRectangle;

					using (LinearGradientBrush Fondo = new LinearGradientBrush(
						R,
						Color.FromArgb(18, 16, 28),
						Color.FromArgb(10, 20, 36),
						LinearGradientMode.Vertical))
					{
						G.FillRectangle(Fondo, R);
					}

					using (Pen LapizScan = new Pen(Color.FromArgb(20, colorTema), 1))
					{
						for (int y = 38; y < R.Height; y += 6)
							G.DrawLine(LapizScan, 0, y, R.Width, y);
					}

					Rectangle RectHeader = new Rectangle(0, 0, R.Width, 36);
					using (SolidBrush BrochaHeader = new SolidBrush(colorTema))
					using (Pen LineaOro = new Pen(Color.FromArgb(245, 203, 66), 2))
					{
						G.FillRectangle(BrochaHeader, RectHeader);
						G.DrawLine(LineaOro, 0, 36, R.Width, 36);
					}

					StringFormat Centro = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};

					using (Font FuenteHeader = new Font("Consolas", 10F, FontStyle.Bold))
					{
						G.DrawString(tituloBarra, FuenteHeader, Brushes.Yellow, RectHeader, Centro);
					}

					Rectangle RectCaja = new Rectangle(16, 50, R.Width - 32, 132);
					using (SolidBrush FondoCaja = new SolidBrush(Color.FromArgb(175, 8, 14, 24)))
					using (Pen BordeCaja = new Pen(Color.FromArgb(190, colorTema), 1.5f))
					{
						G.FillRectangle(FondoCaja, RectCaja);
						G.DrawRectangle(BordeCaja, RectCaja);
					}

					int CX = RectCaja.X + 46;
					int CY = RectCaja.Y + (RectCaja.Height / 2);

					using (SolidBrush Aura = new SolidBrush(Color.FromArgb(60, colorTema)))
					{
						G.FillEllipse(Aura, CX - 32, CY - 32, 64, 64);
					}

					Point[] Rombo = new Point[]
					{
						new Point(CX, CY - 24),
						new Point(CX + 24, CY),
						new Point(CX, CY + 24),
						new Point(CX - 24, CY)
					};

					using (SolidBrush Nucleo = new SolidBrush(colorTema))
					using (Pen BordeOro = new Pen(Color.FromArgb(245, 203, 66), 2f))
					{
						G.FillPolygon(Nucleo, Rombo);
						G.DrawPolygon(BordeOro, Rombo);
					}

					using (Font FuenteIcono = new Font("Consolas", 12F, FontStyle.Bold))
					{
						G.DrawString(siglaSello, FuenteIcono, Brushes.Yellow, new RectangleF(CX - 22, CY - 12, 44, 24), Centro);
					}

					int TX = RectCaja.X + 92;
					int TW = RectCaja.Width - 102;

					using (Font FuenteSub = new Font("Consolas", 9.5F, FontStyle.Bold))
					using (SolidBrush BrochaOro = new SolidBrush(Color.FromArgb(245, 203, 66)))
					{
						G.DrawString(subtituloOro, FuenteSub, BrochaOro, TX, RectCaja.Y + 12);
					}

					using (Font FuenteMsg = new Font("Consolas", 8.5F, FontStyle.Bold))
					using (SolidBrush BrochaBlanca = new SolidBrush(Color.WhiteSmoke))
					{
						G.DrawString(cuerpoMensaje, FuenteMsg, BrochaBlanca, new RectangleF(TX, RectCaja.Y + 34, TW, 88));
					}
				};

				Button BtnAccion = new Button();
				BtnAccion.SetBounds(122, 198, 240, 44);
				BtnAccion.FlatStyle = FlatStyle.Flat;
				BtnAccion.FlatAppearance.BorderColor = Color.FromArgb(245, 203, 66);
				BtnAccion.FlatAppearance.BorderSize = 2;
				BtnAccion.BackColor = colorTema;
				BtnAccion.ForeColor = Color.Yellow;
				BtnAccion.Font = new Font("Consolas", 10F, FontStyle.Bold);
				BtnAccion.Cursor = Cursors.Hand;
				BtnAccion.Text = textoBoton;
				BtnAccion.DialogResult = DialogResult.OK;

				PnlInterior.Controls.Add(BtnAccion);
				Modal.AcceptButton = BtnAccion;

				Modal.ShowDialog(this);
			}
		}

		private void ActualizarImagen(int numCapitulo, string nombreCapitulo)
		{
			if (picImagen.Image != null)
			{
				picImagen.Image.Dispose();
				picImagen.Image = null;
			}

			picImagen.Image = CargarImagenDeCarpeta(numCapitulo, nombreCapitulo);
		}

		private Image CargarImagenDeCarpeta(int numCapitulo, string nombreCapitulo)
		{
			List<string> carpetas = ObtenerCarpetasImagenes();
			if (carpetas.Count == 0)
			{
				return null;
			}

			string[] extensiones = new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
			string[] palabrasClave = ObtenerPalabrasClaveCapitulo(numCapitulo);

			foreach (string carpeta in carpetas)
			{
				if (!Directory.Exists(carpeta))
					continue;

				List<string> nombresBase = new List<string>
				{
					"capitulo" + numCapitulo,
					"capitulo " + numCapitulo,
					"capitulo_" + numCapitulo,
					"Capitulo" + numCapitulo,
					"Capitulo " + numCapitulo,
					"Capitulo_" + numCapitulo,
					"cap" + numCapitulo,
					"cap " + numCapitulo,
					"cap_" + numCapitulo,
					numCapitulo.ToString(),
					nombreCapitulo,
					nombreCapitulo.Replace(" ", ""),
					LimpiarAcentos(nombreCapitulo),
					LimpiarAcentos(nombreCapitulo).Replace(" ", "")
				};

				foreach (string nombre in nombresBase)
				{
					foreach (string ext in extensiones)
					{
						string rutaDirecta = Path.Combine(carpeta, nombre + ext);
						if (File.Exists(rutaDirecta))
						{
							Image img = IntentarCargarImagenSinBloqueo(rutaDirecta);
							if (img != null)
							{
								SincronizarHaciaBinDebug(rutaDirecta);
								return img;
							}
						}
					}
				}

				try
				{
					string[] archivos = Directory.GetFiles(carpeta);
					// Paso 1: Coincidencia estricta por número de capítulo primero
					foreach (string rutaArchivo in archivos)
					{
						string ext = Path.GetExtension(rutaArchivo).ToLowerInvariant();
						bool esExtensionValida = false;
						foreach (string e in extensiones)
						{
							if (ext == e)
							{
								esExtensionValida = true;
								break;
							}
						}
						if (!esExtensionValida) continue;

						string nombreSolo = Path.GetFileNameWithoutExtension(rutaArchivo).ToLowerInvariant();

						if (nombreSolo == "capitulo" + numCapitulo ||
						    nombreSolo == "capitulo " + numCapitulo ||
						    nombreSolo == "capitulo_" + numCapitulo ||
						    nombreSolo == "cap" + numCapitulo ||
						    nombreSolo == "cap " + numCapitulo ||
						    nombreSolo == "cap_" + numCapitulo ||
						    nombreSolo == numCapitulo.ToString())
						{
							Image img = IntentarCargarImagenSinBloqueo(rutaArchivo);
							if (img != null)
							{
								SincronizarHaciaBinDebug(rutaArchivo);
								return img;
							}
						}
					}

					// Paso 2: Coincidencia por palabra clave solo si el archivo no pertenece a otro número de capítulo
					foreach (string rutaArchivo in archivos)
					{
						string ext = Path.GetExtension(rutaArchivo).ToLowerInvariant();
						bool esExtensionValida = false;
						foreach (string e in extensiones)
						{
							if (ext == e)
							{
								esExtensionValida = true;
								break;
							}
						}
						if (!esExtensionValida) continue;

						string nombreSolo = Path.GetFileNameWithoutExtension(rutaArchivo).ToLowerInvariant();
						if (nombreSolo.StartsWith("capitulo") || nombreSolo.StartsWith("cap"))
							continue;

						foreach (string palabra in palabrasClave)
						{
							if (!string.IsNullOrEmpty(palabra) && nombreSolo.Contains(palabra.ToLowerInvariant()))
							{
								Image img = IntentarCargarImagenSinBloqueo(rutaArchivo);
								if (img != null)
								{
									SincronizarHaciaBinDebug(rutaArchivo);
									return img;
								}
							}
						}
					}
				}
				catch
				{
				}
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

			string[] posiblesRaicesProyecto = new string[]
			{
				Path.Combine(Application.StartupPath, "..", "..", "imagenes"),
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "imagenes"),
				Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "imagenes")
			};

			foreach (string ruta in posiblesRaicesProyecto)
			{
				try
				{
					if (Directory.Exists(ruta))
					{
						string completa = Path.GetFullPath(ruta);
						if (!lista.Contains(completa))
						{
							lista.Add(completa);
						}
					}
				}
				catch { }
			}

			string[] posiblesCarpetasEjecutable = new string[]
			{
				Path.Combine(Application.StartupPath, "imagenes"),
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes"),
				Path.Combine(Directory.GetCurrentDirectory(), "imagenes"),
				"imagenes"
			};

			foreach (string ruta in posiblesCarpetasEjecutable)
			{
				try
				{
					if (Directory.Exists(ruta))
					{
						string completa = Path.GetFullPath(ruta);
						if (!lista.Contains(completa))
						{
							lista.Add(completa);
						}
					}
				}
				catch { }
			}

			return lista;
		}

		private string[] ObtenerPalabrasClaveCapitulo(int numCapitulo)
		{
			switch (numCapitulo)
			{
				case 1:
					return new string[] { "fosa", "despertar" };
				case 2:
					return new string[] { "aguas negras", "petroleo", "alquitran" };
				case 3:
					return new string[] { "espejo", "culpa" };
				case 4:
					return new string[] { "origen", "ruina", "tormenta" };
				case 5:
					return new string[] { "podredumbre", "nucleo" };
				case 6:
					return new string[] { "limpie", "final" };
				default:
					return new string[0];
			}
		}

		private string LimpiarAcentos(string texto)
		{
			if (string.IsNullOrEmpty(texto)) return "";
			return texto.Replace("á", "a")
			            .Replace("é", "e")
			            .Replace("í", "i")
			            .Replace("ó", "o")
			            .Replace("ú", "u")
			            .Replace("Á", "A")
			            .Replace("É", "E")
			            .Replace("Í", "I")
			            .Replace("Ó", "O")
			            .Replace("Ú", "U");
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
					{
						Directory.CreateDirectory(rutaSalida);
					}

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
			catch
			{
			}
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

		void BtnSiguienteClick(object sender, EventArgs e)
		{
			AvanzarCapitulo();
		}

		void PicImagenClick(object sender, EventArgs e)
		{
			AvanzarCapitulo();
		}

		void BtnCerrarClick(object sender, EventArgs e)
		{
			MainForm menu = new MainForm();
			menu.FormClosed += (s, args) => Application.Exit();
			menu.Show();
			this.Hide();
		}
	}
}