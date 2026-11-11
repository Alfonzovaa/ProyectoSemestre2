using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Leviatanes2.Clases;

namespace Leviatanes2
{
	public partial class CrearP : Form
	{
		private GestorPersistenciaTxt GestorTxt;
		private List<Personaje> ListaCatalogoActual;
		private bool ActualizandoControlesInternamente;
		private int IdPersonajeVistaPrevia = 1;

		public CrearP()
		{
			InitializeComponent();
			ConfigurarListBoxCatalogoModerno();
			InicializarModuloCrearPersonaje();
		}

		private void ConfigurarListBoxCatalogoModerno()
		{
			lstPersonajesTxt.DrawMode = DrawMode.OwnerDrawFixed;
			lstPersonajesTxt.ItemHeight = 40;
			lstPersonajesTxt.HorizontalScrollbar = false;
			lstPersonajesTxt.BackColor = Color.FromArgb(16, 24, 36);
			lstPersonajesTxt.DrawItem += new DrawItemEventHandler(LstPersonajesTxtDrawItem);
		}

		private void LstPersonajesTxtDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0 || ListaCatalogoActual == null || e.Index >= ListaCatalogoActual.Count)
				return;

			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			bool Seleccionado = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
			Rectangle RectFila = e.Bounds;
			Personaje Carta = ListaCatalogoActual[e.Index];
			Color ColorElem = ObtenerColorPorElemento(Carta.Faccion);

			// 1. Fondo de tarjeta
			Color ColorFondo = Seleccionado
				? Color.FromArgb(36, 54, 82)
				: (e.Index % 2 == 0 ? Color.FromArgb(18, 26, 40) : Color.FromArgb(23, 33, 50));

			using (SolidBrush BrochaFondo = new SolidBrush(ColorFondo))
			{
				G.FillRectangle(BrochaFondo, RectFila);
			}

			// 2. Barra lateral de faccion
			using (SolidBrush BrochaBarra = new SolidBrush(ColorElem))
			{
				G.FillRectangle(BrochaBarra, RectFila.X, RectFila.Y + 2, 5, RectFila.Height - 4);
			}

			// 3. Pastilla del Elemento
			string TextoElem = Carta.Faccion.ToString().ToUpper();
			Rectangle RectBadge = new Rectangle(RectFila.X + 9, RectFila.Y + 5, 48, 15);
			using (SolidBrush BrochaBadge = new SolidBrush(ColorElem))
			{
				G.FillRectangle(BrochaBadge, RectBadge);
			}

			using (Font FuenteBadge = new Font("Consolas", 7F, FontStyle.Bold))
			using (SolidBrush BrochaBlanca = new SolidBrush(Color.White))
			{
				StringFormat FormatoCentro = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				};
				G.DrawString(TextoElem, FuenteBadge, BrochaBlanca, RectBadge, FormatoCentro);
			}

			// 4. Linea 1: #ID + Nombre
			string EtiquetaOrigen = Carta.IdPersonaje <= 12 ? "" : " *";
			string Titulo = string.Format("#{0:00} {1}{2}", Carta.IdPersonaje, Carta.Nombre, EtiquetaOrigen);
			using (Font FuenteTitulo = new Font("Consolas", 8.5F, FontStyle.Bold))
			using (SolidBrush BrochaTitulo = new SolidBrush(Seleccionado ? Color.FromArgb(245, 203, 66) : Color.White))
			{
				G.DrawString(Titulo, FuenteTitulo, BrochaTitulo, RectFila.X + 62, RectFila.Y + 4);
			}

			// 5. Linea 2: Estadisticas completas sin recorte
			string Subtitulo = string.Format("VIT:{0} | BLI:{1} | POD:{2} | VINC:{3}",
				Carta.VidaMaxima, Carta.DefensaBase, Carta.AtaqueBase, Carta.Empatia);
			using (Font FuenteStats = new Font("Consolas", 7.5F, FontStyle.Bold))
			using (SolidBrush BrochaStats = new SolidBrush(Seleccionado ? Color.LightCyan : Color.FromArgb(150, 175, 205)))
			{
				G.DrawString(Subtitulo, FuenteStats, BrochaStats, RectFila.X + 9, RectFila.Y + 22);
			}

			// 6. Borde de seleccion dorado o divisor sutil
			if (Seleccionado)
			{
				using (Pen BordeSeleccion = new Pen(Color.FromArgb(245, 203, 66), 1.5f))
				{
					G.DrawRectangle(BordeSeleccion, RectFila.X + 1, RectFila.Y + 1, RectFila.Width - 3, RectFila.Height - 3);
				}
			}
			else
			{
				using (Pen LineaSep = new Pen(Color.FromArgb(30, 255, 255, 255), 1))
				{
					G.DrawLine(LineaSep, RectFila.X, RectFila.Bottom - 1, RectFila.Right, RectFila.Bottom - 1);
				}
			}
		}

		private void InicializarModuloCrearPersonaje()
		{
			ActualizandoControlesInternamente = true;

			GestorTxt = new GestorPersistenciaTxt();
			ListaCatalogoActual = new List<Personaje>();

			cmbFaccion.Items.Clear();
			cmbFaccion.Items.Add(Elemento.Agua);
			cmbFaccion.Items.Add(Elemento.Fuego);
			cmbFaccion.Items.Add(Elemento.Tierra);
			cmbFaccion.Items.Add(Elemento.Aire);
			cmbFaccion.SelectedIndex = 0;

			ActualizandoControlesInternamente = false;

			RecargarListaDesdeArchivoTxt();
			ActualizarMedidorBalanceCien();
			if (lstPersonajesTxt.Items.Count > 0)
			{
				lstPersonajesTxt.SelectedIndex = 0;
			}
			else
			{
				ActualizarTarjetaVistaPreviaDesdeFormulario();
			}
		}

		private void RecargarListaDesdeArchivoTxt()
		{
			ListaCatalogoActual = GestorTxt.CargarPersonajesDesdeTxt();
			lstPersonajesTxt.Items.Clear();

			for (int I = 0; I < ListaCatalogoActual.Count; I++)
			{
				Personaje C = ListaCatalogoActual[I];
				string Etiqueta = C.IdPersonaje <= 12 ? "[BASE]" : "[TXT]";
				lstPersonajesTxt.Items.Add(string.Format("#{0:00} {1} {2} [{3}] (HP:{4}/DEF:{5}/ATK:{6})",
					C.IdPersonaje,
					Etiqueta,
					C.Nombre,
					C.Faccion.ToString().ToUpper(),
					C.VidaMaxima,
					C.DefensaBase,
					C.AtaqueBase));
			}

			lblTotalRegistrados.Text = string.Format("Leviatanes en personajes.txt: {0}", ListaCatalogoActual.Count);
		}

		private Elemento ObtenerFaccionSeleccionada()
		{
			if (cmbFaccion.SelectedItem is Elemento)
			{
				return (Elemento)cmbFaccion.SelectedItem;
			}
			return Elemento.Agua;
		}

		private void ActualizarMedidorBalanceCien()
		{
			int Vida = (int)numVida.Value;
			int Defensa = (int)numDefensa.Value;
			int Ataque = (int)numAtaque.Value;
			int Suma = Vida + Defensa + Ataque;

			pbBalanceCien.Value = Math.Min(100, Math.Max(0, Suma));

			if (Suma == 100)
			{
				lblEstadoBalance.BackColor = Color.FromArgb(210, 245, 215);
				lblEstadoBalance.ForeColor = Color.DarkGreen;
				lblEstadoBalance.Text = string.Format("SUMA: {0} + {1} + {2} = 100 / 100 [PERFECTO]", Vida, Defensa, Ataque);
				btnGuardarPersonajeTxt.BackColor = Color.FromArgb(204, 48, 32);
			}
			else
			{
				int Dif = 100 - Suma;
				string Detalle = Dif > 0 ? ("FALTAN " + Dif + " PTS") : ("SOBRAN " + Math.Abs(Dif) + " PTS");
				lblEstadoBalance.BackColor = Color.FromArgb(255, 220, 220);
				lblEstadoBalance.ForeColor = Color.DarkRed;
				lblEstadoBalance.Text = string.Format("SUMA: {0}/100 ({1})", Suma, Detalle);
				btnGuardarPersonajeTxt.BackColor = Color.FromArgb(130, 60, 55);
			}
		}

		private void ActualizarTarjetaVistaPreviaDesdeFormulario()
		{
			if (ActualizandoControlesInternamente)
				return;

			Elemento Fac = ObtenerFaccionSeleccionada();
			string NombreCarta = string.IsNullOrWhiteSpace(txtNombre.Text) ? "Leviatan Sin Nombre" : txtNombre.Text.Trim();
			int Vida = (int)numVida.Value;
			int Def = (int)numDefensa.Value;
			int Atk = (int)numAtaque.Value;
			int Emp = (int)numEmpatia.Value;
			string Especial = string.IsNullOrWhiteSpace(txtNombreEspecial.Text) ? "Arte Elemental" : txtNombreEspecial.Text.Trim();
			string Desc = string.IsNullOrWhiteSpace(txtDescripcionEspecial.Text) ? "Sin descripcion." : txtDescripcionEspecial.Text.Trim();

			AplicarDatosATarjetaVistaPrevia(NombreCarta, Fac, Vida, Def, Atk, Emp, Especial, Desc);
		}

		private void AplicarDatosATarjetaVistaPrevia(
			string NombreCarta,
			Elemento Fac,
			int Vida,
			int Def,
			int Atk,
			int Emp,
			string Especial,
			string Desc)
		{
			Color ColorTema = ObtenerColorPorElemento(Fac);
			lblCartaFaccionBanner.BackColor = ColorTema;
			lblCartaFaccionBanner.Text = string.Format("BESTIARIO :: [{0}]", Fac.ToString().ToUpper());

			lblCartaNombreVista.Text = NombreCarta;
			lblCartaHp.Text = string.Format("VIDA   : {0}", Vida);
			lblCartaDef.Text = string.Format("ESCUDO : {0}", Def);
			lblCartaAtk.Text = string.Format("ATAQUE : {0}", Atk);

			pbCartaHp.Value = Math.Min(100, Math.Max(0, Vida));
			pbCartaDef.Value = Math.Min(100, Math.Max(0, Def));
			pbCartaAtk.Value = Math.Min(100, Math.Max(0, Atk));

			lblCartaEmp.Text = string.Format("VINCULO SISMICO: {0}/100 | EN: 2", Emp);
			lblCartaPasiva.Text = ObtenerTextoPasivaSubclase(Fac);
			lblCartaEspecialVista.Text = string.Format("ARTE ABISAL [{0}]:\r\n{1}", Especial, Desc);

			pnlSpriteArte.Invalidate();
		}

		private Color ObtenerColorPorElemento(Elemento Fac)
		{
			switch (Fac)
			{
				case Elemento.Fuego:
					return Color.FromArgb(204, 41, 18);
				case Elemento.Agua:
					return Color.FromArgb(28, 108, 196);
				case Elemento.Tierra:
					return Color.FromArgb(148, 98, 42);
				default:
					return Color.FromArgb(46, 152, 126);
			}
		}

		private static string ObtenerTituloLinaje(Elemento Faccion)
		{
			switch (Faccion)
			{
				case Elemento.Fuego:
					return "DRAKO PIRICO";
				case Elemento.Agua:
					return "BESTIA ABISAL";
				case Elemento.Tierra:
					return "COLOSO LITICO";
				default:
					return "SIERPE CELESTE";
			}
		}

		private string ObtenerTextoPasivaSubclase(Elemento Fac)
		{
			switch (Fac)
			{
				case Elemento.Fuego:
					return "LINAJE CartaFuego: Furia (+15% ATK si HP < 50%). Especial ignora escudo (1.45x).";
				case Elemento.Agua:
					return "LINAJE CartaAgua: Especial roba 1 Energia al rival, cura +25% HP y ataca.";
				case Elemento.Tierra:
					return "LINAJE CartaTierra: Bono Roca (+15% Escudo al ATK) y mitiga 50% del Terremoto.";
				default:
					return "LINAJE CartaAire: Especial ejecuta doble impacto aereo consecutivo (2 x 0.85x).";
			}
		}

		void PnlSpriteArtePaint(object sender, PaintEventArgs e)
		{
			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;

			Rectangle Rect = pnlSpriteArte.ClientRectangle;
			Color ColorElem = lblCartaFaccionBanner.BackColor;

			// 1. Fondo abisal profundo
			using (LinearGradientBrush Fondo = new LinearGradientBrush(
				Rect,
				Color.FromArgb(10, 16, 28),
				Color.FromArgb(24, 38, 60),
				LinearGradientMode.Vertical))
			{
				G.FillRectangle(Fondo, Rect);
			}

			// 2. Rejilla tactica del Bestiario
			using (Pen LapizRejilla = new Pen(Color.FromArgb(28, 180, 220, 255), 1))
			{
				for (int X = 0; X < Rect.Width; X += 16)
					G.DrawLine(LapizRejilla, X, 0, X, Rect.Height);
				for (int Y = 0; Y < Rect.Height; Y += 16)
					G.DrawLine(LapizRejilla, 0, Y, Rect.Width, Y);
			}

			int CentroX = Rect.Width / 2;
			int CentroY = (Rect.Height / 2) + 6;

			// 3. Sonar concéntrico y plataforma inferior
			using (Pen LapizSonar = new Pen(Color.FromArgb(45, 245, 203, 66), 1f))
			{
				LapizSonar.DashStyle = DashStyle.Dash;
				G.DrawEllipse(LapizSonar, CentroX - 48, CentroY - 42, 96, 84);
			}

			using (SolidBrush SombraPlat = new SolidBrush(Color.FromArgb(90, 0, 0, 0)))
			using (SolidBrush BrochaPlat = new SolidBrush(Color.FromArgb(65, ColorElem)))
			using (Pen BordePlat = new Pen(Color.FromArgb(160, 245, 203, 66), 1.5f))
			{
				G.FillEllipse(SombraPlat, CentroX - 66, Rect.Height - 28, 132, 20);
				G.FillEllipse(BrochaPlat, CentroX - 62, Rect.Height - 30, 124, 18);
				G.DrawEllipse(BordePlat, CentroX - 62, Rect.Height - 30, 124, 18);
			}

			// 4. Esquinas de enfoque doradas
			using (Pen LapizMarco = new Pen(Color.FromArgb(190, 245, 203, 66), 2f))
			{
				int M = 5;
				int L = 10;
				G.DrawLine(LapizMarco, M, M + 14, M + L, M + 14);
				G.DrawLine(LapizMarco, M, M + 14, M, M + 14 + L);
				G.DrawLine(LapizMarco, Rect.Width - M - L, M + 14, Rect.Width - M, M + 14);
				G.DrawLine(LapizMarco, Rect.Width - M, M + 14, Rect.Width - M, M + 14 + L);
				G.DrawLine(LapizMarco, M, Rect.Height - M, M + L, Rect.Height - M);
				G.DrawLine(LapizMarco, M, Rect.Height - M - L, M, Rect.Height - M);
				G.DrawLine(LapizMarco, Rect.Width - M - L, Rect.Height - M, Rect.Width - M, Rect.Height - M);
				G.DrawLine(LapizMarco, Rect.Width - M, Rect.Height - M - L, Rect.Width - M, Rect.Height - M);
			}

			// 5. Franja superior del Codice
			using (SolidBrush BrochaTop = new SolidBrush(Color.FromArgb(160, 10, 14, 22)))
			{
				G.FillRectangle(BrochaTop, 0, 0, Rect.Width, 16);
			}

			Elemento FacActual = lblCartaFaccionBanner.Text.Contains("FUEGO") ? Elemento.Fuego :
			                     lblCartaFaccionBanner.Text.Contains("TIERRA") ? Elemento.Tierra :
			                     lblCartaFaccionBanner.Text.Contains("AIRE") ? Elemento.Aire : Elemento.Agua;

			string TextoRegistro = string.Format("CODICE #{0:00} // {1}", IdPersonajeVistaPrevia, ObtenerTituloLinaje(FacActual));
			using (Font FuenteReg = new Font("Consolas", 7.5F, FontStyle.Bold))
			using (SolidBrush BrochaReg = new SolidBrush(Color.FromArgb(245, 203, 66)))
			{
				G.DrawString(TextoRegistro, FuenteReg, BrochaReg, 5, 2);
			}

			// 6. Dibujar PNG del Leviatan si existe en la carpeta de imagenes
			using (Image imgPersonaje = GestorPersistenciaTxt.CargarImagenPersonaje(IdPersonajeVistaPrevia, lblCartaNombreVista.Text))
			{
				if (imgPersonaje != null)
				{
					int maxAncho = Rect.Width - 24;
					int maxAlto = Rect.Height - 28;
					float escala = Math.Min((float)maxAncho / imgPersonaje.Width, (float)maxAlto / imgPersonaje.Height);
					int anchoDibujo = Math.Max(1, (int)(imgPersonaje.Width * escala));
					int altoDibujo = Math.Max(1, (int)(imgPersonaje.Height * escala));
					int posX = (Rect.Width - anchoDibujo) / 2;
					int posY = 18 + ((Rect.Height - 24 - altoDibujo) / 2);

					G.InterpolationMode = InterpolationMode.HighQualityBicubic;
					G.DrawImage(imgPersonaje, new Rectangle(posX, posY, anchoDibujo, altoDibujo));
					return;
				}
			}

			// 7. Respaldo mientras no tenga PNG: Sello Runico de Leviatan
			using (SolidBrush BrochaAura = new SolidBrush(Color.FromArgb(65, ColorElem)))
			{
				G.FillEllipse(BrochaAura, CentroX - 36, CentroY - 34, 72, 68);
			}

			Point[] RomboRunico = new Point[]
			{
				new Point(CentroX, CentroY - 28),
				new Point(CentroX + 28, CentroY),
				new Point(CentroX, CentroY + 28),
				new Point(CentroX - 28, CentroY)
			};

			using (SolidBrush BrochaNucleo = new SolidBrush(ColorElem))
			using (Pen BordeDorado = new Pen(Color.FromArgb(245, 203, 66), 2f))
			{
				G.FillPolygon(BrochaNucleo, RomboRunico);
				G.DrawPolygon(BordeDorado, RomboRunico);
			}

			string Sigla = FacActual.ToString().Substring(0, 2).ToUpper();
			using (Font FuenteSigla = new Font("Consolas", 11F, FontStyle.Bold))
			using (SolidBrush BrochaTexto = new SolidBrush(Color.White))
			{
				SizeF Tam = G.MeasureString(Sigla, FuenteSigla);
				G.DrawString(Sigla, FuenteSigla, BrochaTexto, CentroX - (Tam.Width / 2f), CentroY - (Tam.Height / 2f));
			}
		}

		void AtributosCienChanged(object sender, EventArgs e)
		{
			if (ActualizandoControlesInternamente)
				return;

			ActualizarMedidorBalanceCien();
			ActualizarTarjetaVistaPreviaDesdeFormulario();
		}

		void CamposVistaPreviaChanged(object sender, EventArgs e)
		{
			if (ActualizandoControlesInternamente)
				return;

			ActualizarTarjetaVistaPreviaDesdeFormulario();
		}

		void BtnAjustarCienClick(object sender, EventArgs e)
		{
			ActualizandoControlesInternamente = true;

			int Vida = (int)numVida.Value;
			int Def = (int)numDefensa.Value;
			int Restante = 100 - (Vida + Def);

			if (Restante >= 1 && Restante <= 98)
			{
				numAtaque.Value = Restante;
			}
			else
			{
				numVida.Value = 50;
				numDefensa.Value = 25;
				numAtaque.Value = 25;
			}

			ActualizandoControlesInternamente = false;
			ActualizarMedidorBalanceCien();
			ActualizarTarjetaVistaPreviaDesdeFormulario();

			lblTextoProfesorGba.Text = "PROF. LUCENA: ¡Atributos ajustados automaticamente para que VIT + BLI + POD sumen exactamente 100! ▼";
		}

		void BtnGuardarPersonajeTxtClick(object sender, EventArgs e)
		{
			try
			{
				string Nombre = txtNombre.Text.Trim();
				if (string.IsNullOrEmpty(Nombre))
				{
					MessageBox.Show("Debes escribir un nombre para el Leviatan antes de guardarlo.", "Nombre Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				Elemento Faccion = ObtenerFaccionSeleccionada();
				int Vida = (int)numVida.Value;
				int Defensa = (int)numDefensa.Value;
				int Ataque = (int)numAtaque.Value;
				int Empatia = (int)numEmpatia.Value;
				string Especial = txtNombreEspecial.Text.Trim();
				string Desc = txtDescripcionEspecial.Text.Trim();

				Personaje Nuevo = GestorTxt.RegistrarNuevoPersonajeEnTxt(
					Nombre,
					Faccion,
					Vida,
					Defensa,
					Ataque,
					Empatia,
					Especial,
					Desc);

				RecargarListaDesdeArchivoTxt();
				if (lstPersonajesTxt.Items.Count > 0)
				{
					lstPersonajesTxt.SelectedIndex = lstPersonajesTxt.Items.Count - 1;
				}

				lblTextoProfesorGba.Text = string.Format(
					"¡REGISTRO EXITOSO EN PERSONAJES.TXT! Se guardo #{0:00} {1} [{2}] (VIT:{3} + BLI:{4} + POD:{5} = 100). ▼",
					Nuevo.IdPersonaje, Nuevo.Nombre, Nuevo.Faccion, Nuevo.VidaMaxima, Nuevo.DefensaBase, Nuevo.AtaqueBase);

				MessageBox.Show(
					string.Format("¡Leviatan guardado exitosamente en 'personajes.txt'!\n\nID: #{0:00}\nNombre: {1}\nElemento: {2}\nBalance: VIT({3}) + BLI({4}) + POD({5}) = 100\nVinculo: {6}\nArte Abisal: {7}",
						Nuevo.IdPersonaje, Nuevo.Nombre, Nuevo.Faccion, Nuevo.VidaMaxima, Nuevo.DefensaBase, Nuevo.AtaqueBase, Nuevo.Empatia, Nuevo.NombreEspecial),
					"Guardado en personajes.txt",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}
			catch (Exception Ex)
			{
				lblTextoProfesorGba.Text = "ERROR DE BALANCE: " + Ex.Message;
				MessageBox.Show(Ex.Message, "Regla de Balance (VIT + BLI + POD = 100)", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void BtnLimpiarCamposClick(object sender, EventArgs e)
		{
			ActualizandoControlesInternamente = true;
			txtNombre.Text = "Nuevo Leviatan";
			cmbFaccion.SelectedIndex = 0;
			numVida.Value = 50;
			numDefensa.Value = 25;
			numAtaque.Value = 25;
			numEmpatia.Value = 70;
			txtNombreEspecial.Text = "Arte Ancestral";
			txtDescripcionEspecial.Text = "Ataque especial elemental.";
			ActualizandoControlesInternamente = false;

			int siguienteId = 1;
			if (ListaCatalogoActual != null)
			{
				for (int i = 0; i < ListaCatalogoActual.Count; i++)
				{
					if (ListaCatalogoActual[i].IdPersonaje >= siguienteId)
						siguienteId = ListaCatalogoActual[i].IdPersonaje + 1;
				}
			}
			IdPersonajeVistaPrevia = siguienteId;

			ActualizarMedidorBalanceCien();
			ActualizarTarjetaVistaPreviaDesdeFormulario();
			lblTextoProfesorGba.Text = "Campos reiniciados. Forja un nuevo Leviatan y pulsa GUARDAR EN .TXT ▼";
		}

		void LstPersonajesTxtSelectedIndexChanged(object sender, EventArgs e)
		{
			int Idx = lstPersonajesTxt.SelectedIndex;
			if (Idx < 0 || Idx >= ListaCatalogoActual.Count)
				return;

			Personaje Seleccionado = ListaCatalogoActual[Idx];
			IdPersonajeVistaPrevia = Seleccionado.IdPersonaje;
			AplicarDatosATarjetaVistaPrevia(
				Seleccionado.Nombre,
				Seleccionado.Faccion,
				Seleccionado.VidaMaxima,
				Seleccionado.DefensaBase,
				Seleccionado.AtaqueBase,
				Seleccionado.Empatia,
				Seleccionado.NombreEspecial,
				Seleccionado.DescripcionEspecial);

			lblTextoProfesorGba.Text = string.Format(
				"INSPECCIONANDO #{0:00} {1} [{2}] -> VIT:{3} | BLI:{4} | POD:{5} | VINC:{6} | Arte: {7} ▼",
				Seleccionado.IdPersonaje,
				Seleccionado.Nombre,
				Seleccionado.Faccion,
				Seleccionado.VidaMaxima,
				Seleccionado.DefensaBase,
				Seleccionado.AtaqueBase,
				Seleccionado.Empatia,
				Seleccionado.NombreEspecial);
		}

		void BtnCargarEnEditorClick(object sender, EventArgs e)
		{
			int Idx = lstPersonajesTxt.SelectedIndex;
			if (Idx < 0 || Idx >= ListaCatalogoActual.Count)
				return;

			Personaje C = ListaCatalogoActual[Idx];
			IdPersonajeVistaPrevia = C.IdPersonaje;
			ActualizandoControlesInternamente = true;
			txtNombre.Text = C.Nombre;
			cmbFaccion.SelectedItem = C.Faccion;
			numVida.Value = C.VidaMaxima;
			numDefensa.Value = C.DefensaBase;
			numAtaque.Value = C.AtaqueBase;
			numEmpatia.Value = C.Empatia;
			txtNombreEspecial.Text = C.NombreEspecial;
			txtDescripcionEspecial.Text = C.DescripcionEspecial;
			ActualizandoControlesInternamente = false;

			ActualizarMedidorBalanceCien();
			ActualizarTarjetaVistaPreviaDesdeFormulario();
			lblTextoProfesorGba.Text = string.Format("Datos de '{0}' cargados en la forja izquierda para usarlos como plantilla. ▼", C.Nombre);
		}

		void BtnEliminarDeTxtClick(object sender, EventArgs e)
		{
			int Idx = lstPersonajesTxt.SelectedIndex;
			if (Idx < 0 || Idx >= ListaCatalogoActual.Count)
				return;

			Personaje C = ListaCatalogoActual[Idx];
			DialogResult Resp = MessageBox.Show(
				string.Format("¿Deseas eliminar a '{0}' (#{1:00}) del archivo personajes.txt?", C.Nombre, C.IdPersonaje),
				"Confirmar Eliminacion en .TXT",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (Resp == DialogResult.Yes)
			{
				GestorTxt.EliminarPersonajeDeTxt(C.IdPersonaje);
				RecargarListaDesdeArchivoTxt();
				lblTextoProfesorGba.Text = string.Format("Se elimino '{0}' del archivo personajes.txt. ▼", C.Nombre);
			}
		}

		void BtnRestaurarBaseClick(object sender, EventArgs e)
		{
			DialogResult Resp = MessageBox.Show(
				"¿Restaurar 'personajes.txt' con los 12 Leviatanes elementales originales?",
				"Restaurar Catalogo Base",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (Resp == DialogResult.Yes)
			{
				GestorTxt.RestaurarCatalogoPorDefecto();
				RecargarListaDesdeArchivoTxt();
				lblTextoProfesorGba.Text = "Catalogo restaurado con los 12 Leviatanes elementales base en personajes.txt. ▼";
			}
		}

		void BtnIrSeleccionModalidadClick(object sender, EventArgs e)
		{
			SeleccionModalidad VentanaModalidad = new SeleccionModalidad();
			VentanaModalidad.FormClosed += (s, args) => Application.Exit();
			VentanaModalidad.Show();
			this.Hide();
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			Application.Exit();
		}

		void VolverMenuClick(object sender, EventArgs e)
		{
			MainForm abrirVentana = new MainForm();
			abrirVentana.Show();
			abrirVentana.FormClosed += (s, args) => Application.Exit();
			this.Hide();
		}
		
		void LblSubtituloGbaClick(object sender, EventArgs e)
		{
		}
		
		void CrearPLoad(object sender, EventArgs e)
		{
		}
	}
}