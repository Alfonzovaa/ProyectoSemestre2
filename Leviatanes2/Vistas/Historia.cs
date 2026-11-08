using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Leviatanes2
{
	/// <summary>
	/// Ventana de Modo Historia para Leviatanes del Caribe 2.
	/// Permite recorrer los 6 capítulos de la historia mediante condicionales switch case / if.
	/// </summary>
	public partial class Historia : Form
	{
		// Variable entera que controla el capítulo actual
		public int capitulo = 1;

		public Historia()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// Cargar automáticamente el capítulo 1 al iniciar la ventana
			AvanzarCapitulo();
		}

		/// <summary>
		/// Evalúa el valor de 'capitulo' usando switch case, actualiza el label y la imagen,
		/// y finaliza incrementando la variable capitulo++.
		/// </summary>
		public void AvanzarCapitulo()
		{
			// =========================================================================
			// CONDICIONAL SWITCH CASE (Capítulo 1 al 6)
			// =========================================================================
			switch (capitulo)
			{
				case 1:
					lblCapitulo.Text = "Capítulo 1 de 6";
					lblTexto.Text = "CAPÍTULO 1: EL DESPERTAR DE LAS MAREAS\n\n" +
					                "En las profundidades abisales del Mar Caribe, una antigua energía elemental sacude el lecho oceánico.\n\n" +
					                "Los míticos Leviatanes de Agua, guiados por Nereida del Abismo, despiertan tras siglos de letargo. Olas colosales azotan los arrecifes, anunciando que el equilibrio entre los cuatro elementos ha comenzado a quebrarse.\n\n" +
					                "Como joven entrenador, debes sintonizar tu empatía con las corrientes marinas antes de que la gran marea arrase con las islas costeras.";
					picImagen.Image = CargarImagen("capitulo1.png", "Capítulo 1: El Despertar de las Mareas", Color.FromArgb(28, 86, 172));
					break;

				case 2:
					lblCapitulo.Text = "Capítulo 2 de 6";
					lblTexto.Text = "CAPÍTULO 2: LA CALDERA DEL FUEGO PRIMIGENIO\n\n" +
					                "Grietas volcánicas incandescentes desgarran el suelo submarino, tiñendo el mar de rojo y magma incandescente.\n\n" +
					                "Criaturas ígneas ancestrales como Ignis y Vulcano Acorazado emergen reclamando el control de las calderas. Su calor penetra cualquier barrera protectora, amenazando con evaporar lagunas y costas enteras.\n\n" +
					                "Solo un estratega con temple de acero y tácticas de ataque perforante podrá apaciguar la furia del fuego antes de que la conflagración sea irreversible.";
					picImagen.Image = CargarImagen("capitulo2.png", "Capítulo 2: Fuego Primigenio", Color.FromArgb(204, 48, 32));
					break;

				case 3:
					lblCapitulo.Text = "Capítulo 3 de 6";
					lblTexto.Text = "CAPÍTULO 3: EL SUSURRO DE LAS TEMPESTADES\n\n" +
					                "Un cielo plomizo cargado de relámpagos se extiende de horizonte a horizonte. Ráfagas ciclónicas ponen a prueba las embarcaciones del Caribe.\n\n" +
					                "Los veloces Leviatanes de Aire danzan entre las corrientes de viento, desatando dobles ráfagas que desafían los reflejos de cualquier adversario.\n\n" +
					                "Para avanzar hacia los santuarios celestiales, deberás aprender a leer las corrientes aéreas y anticipar los movimientos veloces de estas criaturas aladas.";
					picImagen.Image = CargarImagen("capitulo3.png", "Capítulo 3: Tempestades de Aire", Color.FromArgb(32, 148, 120));
					break;

				case 4:
					lblCapitulo.Text = "Capítulo 4 de 6";
					lblTexto.Text = "CAPÍTULO 4: EL BASTIÓN DE LA TIERRA ANCESTRAL\n\n" +
					                "Un estruendo sordo resuena bajo las islas mientras enormes fallas tectónicas elevan colosos de roca milenaria y obsidiana.\n\n" +
					                "Los guardianes del elemento Tierra poseen una coraza pétrea casi invulnerable y una memoria milenaria que guarda los orígenes del mundo.\n\n" +
					                "Quebrar su muralla defensiva requerirá paciencia, gran resistencia y el máximo nivel de empatía para ganarte el respeto de los colosos terrestres.";
					picImagen.Image = CargarImagen("capitulo4.png", "Capítulo 4: Bastión de Tierra", Color.FromArgb(142, 90, 42));
					break;

				case 5:
					lblCapitulo.Text = "Capítulo 5 de 6";
					lblTexto.Text = "CAPÍTULO 5: EL CHOQUE DE LOS CUATRO ELEMENTOS\n\n" +
					                "Las cuatro fuerzas elementales convergen en una colosal encrucijada en alta mar: Agua, Fuego, Aire y Tierra colisionan con violencia cósmica.\n\n" +
					                "La rueda de afinidades se pone a prueba: el Agua extingue al Fuego, el Fuego consume el Aire, el Aire desgasta la Tierra y la Tierra contiene el Agua.\n\n" +
					                "Llegó el momento culminante: deberás armar un equipo equilibrado de cinco Leviatanes y demostrar tu maestría en la arena de combate.";
					picImagen.Image = CargarImagen("capitulo5.png", "Capítulo 5: Choque Elemental", Color.FromArgb(112, 44, 138));
					break;

				case 6:
					lblCapitulo.Text = "Capítulo 6 de 6";
					lblTexto.Text = "CAPÍTULO 6: LA LEYENDA RENACIDA DEL CARIBE\n\n" +
					                "Las tormentas se calman, el mar recobra su azul cristalino y las islas caribeñas vuelven a respirar en armonía.\n\n" +
					                "Has demostrado el valor, la disciplina y el respeto necesarios para vincularte con los Leviatanes más poderosos del planeta.\n\n" +
					                "¡Tu nombre queda grabado con letras de oro en las Crónicas del Caribe como el Maestro Legendario! Ahora la arena libre y las partidas multijugador aguardan por tus nuevas victorias.";
					picImagen.Image = CargarImagen("capitulo6.png", "Capítulo 6: La Leyenda Renacida", Color.FromArgb(218, 165, 32));
					btnSiguiente.Text = "Finalizar";
					break;

				default:
					MessageBox.Show(
						"Has completado con éxito todos los capítulos de la historia de Leviatanes del Caribe 2.\n¡Gracias por jugar!",
						"Fin del Modo Historia",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
					btnSiguiente.Enabled = false;
					return;
			}

			// =========================================================================
			// INCREMENTO DE LA VARIABLE AL FINAL: capitulo++
			// =========================================================================
			capitulo++;

			/*
			// =========================================================================
			// IMPLEMENTACIÓN ALTERNATIVA MEDIANTE CONDICIONALES IF / ELSE IF:
			// =========================================================================
			if (capitulo == 1)
			{
				lblCapitulo.Text = "Capítulo 1 de 6";
				lblTexto.Text = "CAPÍTULO 1: EL DESPERTAR DE LAS MAREAS...";
				picImagen.Image = CargarImagen("capitulo1.png", "Capítulo 1", Color.Blue);
			}
			else if (capitulo == 2)
			{
				lblCapitulo.Text = "Capítulo 2 de 6";
				lblTexto.Text = "CAPÍTULO 2: LA CALDERA DEL FUEGO PRIMIGENIO...";
				picImagen.Image = CargarImagen("capitulo2.png", "Capítulo 2", Color.Red);
			}
			else if (capitulo == 3)
			{
				lblCapitulo.Text = "Capítulo 3 de 6";
				lblTexto.Text = "CAPÍTULO 3: EL SUSURRO DE LAS TEMPESTADES...";
				picImagen.Image = CargarImagen("capitulo3.png", "Capítulo 3", Color.Teal);
			}
			else if (capitulo == 4)
			{
				lblCapitulo.Text = "Capítulo 4 de 6";
				lblTexto.Text = "CAPÍTULO 4: EL BASTIÓN DE LA TIERRA ANCESTRAL...";
				picImagen.Image = CargarImagen("capitulo4.png", "Capítulo 4", Color.Brown);
			}
			else if (capitulo == 5)
			{
				lblCapitulo.Text = "Capítulo 5 de 6";
				lblTexto.Text = "CAPÍTULO 5: EL CHOQUE DE LOS CUATRO ELEMENTOS...";
				picImagen.Image = CargarImagen("capitulo5.png", "Capítulo 5", Color.Purple);
			}
			else if (capitulo == 6)
			{
				lblCapitulo.Text = "Capítulo 6 de 6";
				lblTexto.Text = "CAPÍTULO 6: LA LEYENDA RENACIDA DEL CARIBE...";
				picImagen.Image = CargarImagen("capitulo6.png", "Capítulo 6", Color.Gold);
			}
			capitulo++;
			*/
		}

		/// <summary>
		/// Carga la imagen desde la carpeta 'imagenes'. Si no existe en disco,
		/// genera un diseño temático placeholder y lo guarda en la carpeta para uso futuro.
		/// </summary>
		private Image CargarImagen(string nombreArchivo, string tituloCapitulo, Color colorFondo)
		{
			string[] posiblesRutas = new string[]
			{
				Path.Combine(Application.StartupPath, "imagenes", nombreArchivo),
				Path.Combine(Application.StartupPath, "..", "..", "imagenes", nombreArchivo),
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes", nombreArchivo),
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "imagenes", nombreArchivo),
				Path.Combine("imagenes", nombreArchivo)
			};

			foreach (string ruta in posiblesRutas)
			{
				if (File.Exists(ruta))
				{
					try
					{
						// Usar copia en memoria para no bloquear el archivo físico en disco
						using (Image imgOriginal = Image.FromFile(ruta))
						{
							return new Bitmap(imgOriginal);
						}
					}
					catch
					{
						// Si ocurre algún problema de lectura, continuar intentando
					}
				}
			}

			// Si el archivo aún no existe en disco, generar un placeholder temático y guardarlo
			return GenerarYGuardarPlaceholder(nombreArchivo, tituloCapitulo, colorFondo);
		}

		/// <summary>
		/// Genera un gráfico visual ilustrativo para el capítulo y lo almacena en la carpeta 'imagenes'.
		/// </summary>
		private Image GenerarYGuardarPlaceholder(string nombreArchivo, string tituloCapitulo, Color colorFondo)
		{
			Bitmap bmp = new Bitmap(445, 420);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.SmoothingMode = SmoothingMode.AntiAlias;

				// 1. Fondo con degradado hacia azul oscuro
				using (LinearGradientBrush brushFondo = new LinearGradientBrush(
					new Rectangle(0, 0, 445, 420),
					colorFondo,
					Color.FromArgb(14, 18, 30),
					LinearGradientMode.Vertical))
				{
					g.FillRectangle(brushFondo, 0, 0, 445, 420);
				}

				// 2. Líneas ornamentales de escaneo sutil
				using (Pen penLinea = new Pen(Color.FromArgb(20, Color.White), 1))
				{
					for (int y = 20; y < 400; y += 12)
					{
						g.DrawLine(penLinea, 15, y, 430, y);
					}
				}

				// 3. Marco exterior dorado
				using (Pen penOro = new Pen(Color.Gold, 3))
				{
					g.DrawRectangle(penOro, 10, 10, 425, 400);
				}

				// 4. Medallón central
				using (Pen penCirculo = new Pen(Color.FromArgb(200, Color.Gold), 2))
				using (SolidBrush brushOrbe = new SolidBrush(Color.FromArgb(40, Color.White)))
				{
					g.FillEllipse(brushOrbe, 142, 105, 160, 160);
					g.DrawEllipse(penCirculo, 142, 105, 160, 160);
				}

				// 5. Textos informativos dentro de la ilustración
				using (Font fontSub = new Font("Segoe UI", 9.5F, FontStyle.Bold))
				using (Font fontTitulo = new Font("Segoe UI", 12F, FontStyle.Bold))
				using (Font fontIcono = new Font("Segoe UI", 32F, FontStyle.Bold))
				using (Font fontRuta = new Font("Consolas", 8.5F, FontStyle.Regular))
				using (SolidBrush brushOro = new SolidBrush(Color.Gold))
				using (SolidBrush brushBlanco = new SolidBrush(Color.WhiteSmoke))
				{
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;

					g.DrawString("CRÓNICAS ELEMENTALES", fontSub, brushOro, new RectangleF(0, 35, 445, 25), sf);
					g.DrawString("⚔", fontIcono, brushOro, new RectangleF(142, 105, 160, 160), sf);
					g.DrawString(tituloCapitulo, fontTitulo, brushBlanco, new RectangleF(25, 285, 395, 55), sf);
					g.DrawString("Archivo: imagenes\\" + nombreArchivo, fontRuta, Brushes.LightGray, new RectangleF(20, 360, 405, 25), sf);
				}
			}

			// Intentar persistir en la carpeta 'imagenes'
			try
			{
				string[] posiblesCarpetas = new string[]
				{
					Path.Combine(Application.StartupPath, "imagenes"),
					Path.Combine(Application.StartupPath, "..", "..", "imagenes"),
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes"),
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "imagenes"),
					"imagenes"
				};

				foreach (string carpeta in posiblesCarpetas)
				{
					if (Directory.Exists(carpeta))
					{
						string rutaDestino = Path.Combine(carpeta, nombreArchivo);
						if (!File.Exists(rutaDestino))
						{
							bmp.Save(rutaDestino, System.Drawing.Imaging.ImageFormat.Png);
						}
					}
				}
			}
			catch
			{
				// Si no hay permisos de escritura, se conserva la imagen generada en memoria
			}

			return bmp;
		}

		void BtnSiguienteClick(object sender, EventArgs e)
		{
			AvanzarCapitulo();
		}

		void BtnCerrarClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
