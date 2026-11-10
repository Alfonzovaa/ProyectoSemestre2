using System;
using System.Collections.Generic;
using System.Drawing;
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
		// Variable entera que almacena el capítulo actual
		public int capitulo = 1;

		public Historia() : this(1)
		{
		}

		public Historia(int capituloInicial)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			// Sincronizar imágenes desde la carpeta raíz del proyecto hacia bin\Debug si aplica
			SincronizarCarpetasDeImagenes();

			// Cargar y mostrar en pantalla el capítulo especificado
			MostrarCapitulo(capituloInicial);
		}

		/// <summary>
		/// Carga en pantalla el texto, la imagen y el botón de acción correspondiente al capítulo actual.
		/// </summary>
		public void MostrarCapitulo(int numCapitulo)
		{
			this.capitulo = numCapitulo;

			// =========================================================================
			// CONDICIONAL SWITCH CASE (Capítulos 1 al 6)
			// =========================================================================
			switch (capitulo)
			{
				case 1:
					lblCapitulo.Text = "Capítulo 1 de 6";
					lblTexto.Text = "1. El Despertar en la Fosa (La Falsa Victoria)\n\n" +
					                "Tras la batalla final del primer juego, una de las gemas cayó al fondo del mar. La tripulación festejaba pensando que todos los leviatanes habían muerto para siempre. Pero la gema liberó un pulso de energía en las profundidades de la fosa, alterando las aguas y haciendo que la cubierta vuelva a temblar. El pánico regresa: la victoria era una ilusión nacida de la ignorancia.\n\n" +
					                "Batalla 1: Leviatán de la Fosa, una mole acorazada que emerge furiosa del grieta donde cayó la gema.";
					ActualizarImagen(1, "El Despertar en la Fosa");
					btnSiguiente.Text = "Ir a la Batalla 1 ▶";
					break;

				case 2:
					lblCapitulo.Text = "Capítulo 2 de 6";
					lblTexto.Text = "2. Las Aguas Negras (El Rastro del Petróleo)\n\n" +
					                "Al adentrarse en mar abierto buscando respuestas, el barco cruza una enorme mancha de petróleo abandonada. Las cartas de navegación antiguas mostraban yacimientos activos, pero ahora los pozos están secos. La tripulación empieza a encajar piezas: el petróleo no se agotó por arte de magia, sino por una sobreexplotación desmedida que pudrió el lecho marino.\n\n" +
					                "Batalla 2: Leviatán del Alquitrán, una bestia mutada y ciega que habita en las aguas densas por los viejos derrames industriales.";
					ActualizarImagen(2, "Las Aguas Negras");
					btnSiguiente.Text = "Ir a la Batalla 2 ▶";
					break;

				case 3:
					lblCapitulo.Text = "Capítulo 3 de 6";
					lblTexto.Text = "3. El Mar Espejo (La Culpa Colectiva)\n\n" +
					                "Navegando entre plataformas petroleras abandonadas y residuos flotantes, la nave entra en una zona de aguas muertas. La superficie refleja la basura acumulada y la ambición humana que destruyó las costas. El capitán y los marineros enfrentan el peso de la culpa: los monstruos no aparecieron por maldición, sino como respuesta de la naturaleza a la contaminación del hombre.\n\n" +
					                "Batalla 3: Leviatán Espectral, un monstruo hecho de sombras y residuos tóxicos que toma la forma del daño provocado al océano.";
					ActualizarImagen(3, "El Mar Espejo");
					btnSiguiente.Text = "Ir a la Batalla 3 ▶";
					break;

				case 4:
					lblCapitulo.Text = "Capítulo 4 de 6";
					lblTexto.Text = "4. La Revelación del Origen (Voluntad contra la Ruina)\n\n" +
					                "Atrapados en una tormenta ácida cerca del epicentro del desastre ambiental, la tripulación descubre la verdad definitiva: la contaminación tóxica y el drenaje de petróleo mutaron a la vida marina, convirtiéndola en los leviatanes que amenazan con destruir todo. La tripulación entiende que la tormenta no los va a perdonar, pero deciden tomar firmemente el timón para corregir los errores del pasado.\n\n" +
					                "Batalla 4: Leviatán de las Tormentas Ácidas, la criatura que alimenta los vientos tóxicos que asfixian los mares.";
					ActualizarImagen(4, "La Revelacion del Origen");
					btnSiguiente.Text = "Ir a la Batalla 4 ▶";
					break;

				case 5:
					lblCapitulo.Text = "Capítulo 5 de 6";
					lblTexto.Text = "5. El Núcleo de la Podredumbre (El Abismo Final)\n\n" +
					                "En el corazón del Caribe yace el pozo original, donde la gema perdida encajó directamente en el cuerpo del primer leviatán, alimentándose de la contaminación acumulada durante décadas. Para detener el nacimiento de más monstruos, no basta con luchar: hay que destruir el núcleo del desecho tóxico antes de que consuma todo el océano.\n\n" +
					                "Batalla 5: Leviatán Primordial del Petróleo, la bestia colosal engendrada por la contaminación extrema, fortalecida por el poder de la gema.";
					ActualizarImagen(5, "El Nucleo de la Podredumbre");
					btnSiguiente.Text = "Ir a la Batalla 5 ▶";
					break;

				case 6:
					lblCapitulo.Text = "Capítulo 6 de 6 (Final)";
					lblTexto.Text = "6. El Limpie del Caribe (Final)\n\n" +
					                "Con la criatura primordial vencida y la gema recuperada, la fuente de la contaminación se neutraliza. Las aguas negras comienzan a aclararse y el ciclo de mutaciones se detiene definitivamente: no volverán a nacer más leviatanes. El mar recupera su cauce natural y la tripulación navega hacia un horizonte limpio, habiendo detenido la amenaza y devolviéndole el equilibrio al Caribe.";
					ActualizarImagen(6, "El Limpie del Caribe");
					btnSiguiente.Text = "Continuar al Menú Principal ▶";
					break;

				default:
					lblCapitulo.Text = "Fin de la Historia";
					lblTexto.Text = "¡Has completado con éxito la historia de Leviatanes 2!";
					btnSiguiente.Text = "Ir al Menú Principal ▶";
					break;
			}
		}

		/// <summary>
		/// Avanza el flujo de juego cuando el usuario presiona 'Continuar':
		/// - Del capítulo 1 al 5: pasa a la Batalla correspondiente (Batalla 1 a 5).
		/// - En el capítulo 6 (final): lleva de vuelta al formulario principal (MainForm).
		/// - Al final de la evaluación se incrementa capitulo++.
		/// </summary>
		public void AvanzarCapitulo()
		{
			// =========================================================================
			// CONDICIONAL SWITCH CASE PARA EL FLUJO HACIA BATALLA O MAINFORM
			// =========================================================================
			switch (capitulo)
			{
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					// Pasar a la ventana de Batalla para la batalla actual
					Batalla ventanaBatalla = new Batalla(capitulo);
					ventanaBatalla.FormClosed += (s, args) => Application.Exit();
					ventanaBatalla.Show();
					this.Hide();
					break;

				case 6:
					// En el capítulo 6 (final), el botón de continuar lleva a MainForm
					MainForm ventanaPrincipal = new MainForm();
					ventanaPrincipal.FormClosed += (s, args) => Application.Exit();
					ventanaPrincipal.Show();
					this.Hide();
					break;

				default:
					MainForm menuDefault = new MainForm();
					menuDefault.FormClosed += (s, args) => Application.Exit();
					menuDefault.Show();
					this.Hide();
					break;
			}

			capitulo++;
	
		}

		/// <summary>
		/// Libera la imagen anterior y carga la imagen correspondiente al capítulo desde la carpeta 'imagenes'.
		/// </summary>
		private void ActualizarImagen(int numCapitulo, string nombreCapitulo)
		{
			if (picImagen.Image != null)
			{
				picImagen.Image.Dispose();
				picImagen.Image = null;
			}

			picImagen.Image = CargarImagenDeCarpeta(numCapitulo, nombreCapitulo);
		}

		/// <summary>
		/// Busca y carga el archivo de imagen para el capítulo especificado.
		/// Prioriza la carpeta 'imagenes' del proyecto (donde el usuario coloca sus imágenes).
		/// No bloquea archivos físicos y soporta múltiples extensiones (.png, .jpg, .jpeg, .bmp, .gif).
		/// </summary>
		private Image CargarImagenDeCarpeta(int numCapitulo, string nombreCapitulo)
		{
			List<string> carpetas = ObtenerCarpetasImagenes();
			if (carpetas.Count == 0)
			{
				return null;
			}

			string[] extensiones = new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
			string[] palabrasClave = ObtenerPalabrasClaveCapitulo(numCapitulo);

			// 1. Recorrer cada carpeta disponible (la carpeta del proyecto tiene máxima prioridad)
			foreach (string carpeta in carpetas)
			{
				if (!Directory.Exists(carpeta))
					continue;

				// A. Búsqueda por nombres directos estándar
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

				// B. Búsqueda flexible entre todos los archivos de la carpeta
				try
				{
					string[] archivos = Directory.GetFiles(carpeta);
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

						// 1. Coincidencia por número
						if (nombreSolo.StartsWith("capitulo" + numCapitulo) ||
						    nombreSolo.StartsWith("capitulo " + numCapitulo) ||
						    nombreSolo.StartsWith("capitulo_" + numCapitulo) ||
						    nombreSolo.StartsWith("cap" + numCapitulo) ||
						    nombreSolo.StartsWith("cap " + numCapitulo) ||
						    nombreSolo.StartsWith("cap_" + numCapitulo) ||
						    nombreSolo.StartsWith(numCapitulo + ".") ||
						    nombreSolo.StartsWith(numCapitulo + " ") ||
						    nombreSolo.StartsWith(numCapitulo + "_") ||
						    nombreSolo.StartsWith(numCapitulo + "-") ||
						    nombreSolo == numCapitulo.ToString())
						{
							Image img = IntentarCargarImagenSinBloqueo(rutaArchivo);
							if (img != null)
							{
								SincronizarHaciaBinDebug(rutaArchivo);
								return img;
							}
						}

						// 2. Coincidencia por palabra clave del capítulo
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
					// Ignorar excepciones al explorar el directorio
				}
			}

			return null;
		}

		/// <summary>
		/// Carga una imagen en memoria mediante bytes para evitar bloqueos de archivos en disco.
		/// </summary>
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

		/// <summary>
		/// Retorna una lista ordenada de carpetas candidatas donde pueden residir las imágenes.
		/// La carpeta 'imagenes' de la raíz del proyecto tiene máxima prioridad sobre bin\Debug.
		/// </summary>
		private List<string> ObtenerCarpetasImagenes()
		{
			List<string> lista = new List<string>();

			// 1. PRIORIDAD MÁXIMA: Carpeta imagenes en la raíz del proyecto (donde trabaja el desarrollador)
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

			// 2. Carpeta imagenes en la carpeta de compilación / ejecución (bin\Debug o distribución)
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

		/// <summary>
		/// Palabras clave asociadas a cada capítulo para facilitar la detección de nombres personalizados.
		/// </summary>
		private string[] ObtenerPalabrasClaveCapitulo(int numCapitulo)
		{
			switch (numCapitulo)
			{
				case 1:
					return new string[] { "fosa", "despertar", "victoria" };
				case 2:
					return new string[] { "aguas negras", "negras", "petroleo", "alquitran" };
				case 3:
					return new string[] { "espejo", "culpa" };
				case 4:
					return new string[] { "origen", "ruina", "tormenta" };
				case 5:
					return new string[] { "podredumbre", "abismo", "nucleo" };
				case 6:
					return new string[] { "limpie", "final", "caribe" };
				default:
					return new string[0];
			}
		}

		/// <summary>
		/// Normaliza cadenas retirando tildes para comparaciones seguras.
		/// </summary>
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

		/// <summary>
		/// Sincroniza automáticamente los archivos de imagen desde la raíz del proyecto hacia bin\Debug.
		/// </summary>
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
				// No interrumpir si hay restricciones de permisos
			}
		}

		/// <summary>
		/// Si una imagen se cargó desde la raíz del proyecto, asegura una copia actualizada en bin\Debug\imagenes.
		/// </summary>
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
