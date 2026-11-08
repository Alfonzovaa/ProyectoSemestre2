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

		public CrearP()
		{
			InitializeComponent();

			InicializarModuloCrearPersonaje();
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
			ActualizarTarjetaVistaPreviaDesdeFormulario();
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

			lblTotalRegistrados.Text = string.Format("Cartas registradas en 'personajes.txt': {0}", ListaCatalogoActual.Count);
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
			string Especial = string.IsNullOrWhiteSpace(txtNombreEspecial.Text) ? "Ataque Elemental" : txtNombreEspecial.Text.Trim();
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
			lblCartaFaccionBanner.Text = string.Format("VISTA PREVIA :: [{0}]", Fac.ToString().ToUpper());

			lblCartaNombreVista.Text = NombreCarta;
			lblCartaHp.Text = string.Format("VIDA   : {0}", Vida);
			lblCartaDef.Text = string.Format("ESCUDO : {0}", Def);
			lblCartaAtk.Text = string.Format("ATAQUE : {0}", Atk);

			pbCartaHp.Value = Math.Min(100, Math.Max(0, Vida));
			pbCartaDef.Value = Math.Min(100, Math.Max(0, Def));
			pbCartaAtk.Value = Math.Min(100, Math.Max(0, Atk));

			lblCartaEmp.Text = string.Format("EMPATIA SISMICA: {0} / 100  |  ENERGIA: 2", Emp);
			lblCartaPasiva.Text = ObtenerTextoPasivaSubclase(Fac);
			lblCartaEspecialVista.Text = string.Format("[{0}]: {1}", Especial, Desc);

			pnlSpriteArte.Invalidate();
		}

		private Color ObtenerColorPorElemento(Elemento Fac)
		{
			switch (Fac)
			{
				case Elemento.Fuego:
					return Color.FromArgb(215, 55, 35);
				case Elemento.Agua:
					return Color.FromArgb(35, 115, 210);
				case Elemento.Tierra:
					return Color.FromArgb(150, 95, 45);
				default:
					return Color.FromArgb(45, 165, 130);
			}
		}

		private string ObtenerTextoPasivaSubclase(Elemento Fac)
		{
			switch (Fac)
			{
				case Elemento.Fuego:
					return "SUBCLASE CartaFuego: Furia (+15% ATK si HP < 50%). Especial ignora escudo (1.45x).";
				case Elemento.Agua:
					return "SUBCLASE CartaAgua: Especial roba 1 Energia al rival, cura +25% HP y ataca.";
				case Elemento.Tierra:
					return "SUBCLASE CartaTierra: Bono Roca (+15% Escudo al ATK) y mitiga 50% del Terremoto.";
				default:
					return "SUBCLASE CartaAire: Especial ejecuta doble impacto aereo consecutivo (2 x 0.85x).";
			}
		}

		void PnlSpriteArtePaint(object sender, PaintEventArgs e)
		{
			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;

			int W = pnlSpriteArte.Width;
			int H = pnlSpriteArte.Height;
			Elemento Fac = ObtenerFaccionSeleccionada();

			// Si el usuario selecciono una carta del catalogo y su faccion difiere, usamos el color del banner actual
			Color ColorElem = lblCartaFaccionBanner.BackColor;

			using (LinearGradientBrush Fondo = new LinearGradientBrush(
				new Rectangle(0, 0, W, H),
				Color.FromArgb(16, 24, 38),
				Color.FromArgb(45, ColorElem.R / 2, ColorElem.G / 2, ColorElem.B / 2),
				LinearGradientMode.Vertical))
			{
				G.FillRectangle(Fondo, 0, 0, W, H);
			}

			// Lineas de escaneo estilo pantalla GBA
			using (Pen LapizScan = new Pen(Color.FromArgb(25, 255, 255, 255), 1))
			{
				for (int Y = 0; Y < H; Y += 4)
				{
					G.DrawLine(LapizScan, 0, Y, W, Y);
				}
			}

			// Plataforma de combate estilo Pokemon GBA y emblema elemental
			int CX = W / 2;
			int CY = H / 2;

			using (SolidBrush SombraPlataforma = new SolidBrush(Color.FromArgb(110, 0, 0, 0)))
			using (SolidBrush Plataforma = new SolidBrush(Color.FromArgb(90, ColorElem)))
			using (Pen BordePlataforma = new Pen(Color.Gold, 2))
			{
				G.FillEllipse(SombraPlataforma, CX - 82, CY + 26, 164, 34);
				G.FillEllipse(Plataforma, CX - 78, CY + 22, 156, 32);
				G.DrawEllipse(BordePlataforma, CX - 78, CY + 22, 156, 32);
			}

			// Orbe / Insignia Elemental del Leviatan
			using (SolidBrush BrochaExterior = new SolidBrush(ColorElem))
			using (SolidBrush BrochaNucleo = new SolidBrush(Color.FromArgb(230, 255, 245, 210)))
			using (Pen BordeOro = new Pen(Color.Gold, 3))
			using (Font FuenteSimbolo = new Font("Consolas", 11F, FontStyle.Bold))
			{
				G.FillEllipse(BrochaExterior, CX - 42, CY - 46, 84, 84);
				G.DrawEllipse(BordeOro, CX - 42, CY - 46, 84, 84);
				G.FillEllipse(BrochaNucleo, CX - 24, CY - 28, 48, 48);

				string Inicial = lblCartaFaccionBanner.Text.Contains("FUEGO") ? "FUEGO" :
				                 lblCartaFaccionBanner.Text.Contains("TIERRA") ? "TIERRA" :
				                 lblCartaFaccionBanner.Text.Contains("AIRE") ? "AIRE" : "AGUA";

				SizeF TamTexto = G.MeasureString(Inicial, FuenteSimbolo);
				G.DrawString(Inicial, FuenteSimbolo, Brushes.Black, CX - (TamTexto.Width / 2), CY - 12);
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

			lblTextoProfesorGba.Text = "PROF. LUCENA: ¡Atributos ajustados automáticamente para que HP + DEF + ATK sumen exactamente 100! ▼";
		}

		void BtnGuardarPersonajeTxtClick(object sender, EventArgs e)
		{
			try
			{
				string Nombre = txtNombre.Text.Trim();
				if (string.IsNullOrEmpty(Nombre))
				{
					MessageBox.Show("Debes escribir un nombre para el Leviatán antes de guardarlo.", "Nombre Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
					"¡REGISTRO EXITOSO EN PERSONAJES.TXT! Se guardó #{0:00} {1} [{2}] (HP:{3} + DEF:{4} + ATK:{5} = 100). ▼",
					Nuevo.IdPersonaje, Nuevo.Nombre, Nuevo.Faccion, Nuevo.VidaMaxima, Nuevo.DefensaBase, Nuevo.AtaqueBase);

				MessageBox.Show(
					string.Format("¡Leviatán guardado exitosamente en 'personajes.txt'!\n\nID: #{0:00}\nNombre: {1}\nFacción: {2}\nBalance: HP({3}) + DEF({4}) + ATK({5}) = 100\nEmpatía: {6}\nEspecial: {7}",
						Nuevo.IdPersonaje, Nuevo.Nombre, Nuevo.Faccion, Nuevo.VidaMaxima, Nuevo.DefensaBase, Nuevo.AtaqueBase, Nuevo.Empatia, Nuevo.NombreEspecial),
					"Guardado en personajes.txt",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}
			catch (Exception Ex)
			{
				lblTextoProfesorGba.Text = "ERROR DE BALANCE: " + Ex.Message;
				MessageBox.Show(Ex.Message, "Regla de Balance (HP + DEF + ATK = 100)", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			txtNombreEspecial.Text = "Tecnica Ancestral";
			txtDescripcionEspecial.Text = "Ataque especial elemental.";
			ActualizandoControlesInternamente = false;

			ActualizarMedidorBalanceCien();
			ActualizarTarjetaVistaPreviaDesdeFormulario();
			lblTextoProfesorGba.Text = "Campos reiniciados. Diseña un nuevo Leviatán y pulsa GUARDAR EN PERSONAJES.TXT ▼";
		}

		void LstPersonajesTxtSelectedIndexChanged(object sender, EventArgs e)
		{
			int Idx = lstPersonajesTxt.SelectedIndex;
			if (Idx < 0 || Idx >= ListaCatalogoActual.Count)
				return;

			Personaje Seleccionado = ListaCatalogoActual[Idx];
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
				"INSPECCIONANDO #{0:00} {1} [{2}] -> HP:{3} | DEF:{4} | ATK:{5} | EMP:{6} | Especial: {7} ▼",
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
			lblTextoProfesorGba.Text = string.Format("Datos de '{0}' cargados en el editor izquierdo para usarlos como plantilla. ▼", C.Nombre);
		}

		void BtnEliminarDeTxtClick(object sender, EventArgs e)
		{
			int Idx = lstPersonajesTxt.SelectedIndex;
			if (Idx < 0 || Idx >= ListaCatalogoActual.Count)
				return;

			Personaje C = ListaCatalogoActual[Idx];
			DialogResult Resp = MessageBox.Show(
				string.Format("¿Deseas eliminar a '{0}' (#{1:00}) del archivo personajes.txt?", C.Nombre, C.IdPersonaje),
				"Confirmar Eliminación en .TXT",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (Resp == DialogResult.Yes)
			{
				GestorTxt.EliminarPersonajeDeTxt(C.IdPersonaje);
				RecargarListaDesdeArchivoTxt();
				lblTextoProfesorGba.Text = string.Format("Se eliminó '{0}' del archivo personajes.txt. ▼", C.Nombre);
			}
		}

		void BtnRestaurarBaseClick(object sender, EventArgs e)
		{
			DialogResult Resp = MessageBox.Show(
				"¿Restaurar 'personajes.txt' con las 12 cartas elementales originales del enunciado?",
				"Restaurar Catálogo Base",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (Resp == DialogResult.Yes)
			{
				GestorTxt.RestaurarCatalogoPorDefecto();
				RecargarListaDesdeArchivoTxt();
				lblTextoProfesorGba.Text = "Catálogo restaurado con las 12 cartas elementales base en personajes.txt. ▼";
			}
		}

		void BtnIrSeleccionModalidadClick(object sender, EventArgs e)
		{
			SeleccionModalidad VentanaModalidad = new SeleccionModalidad();
			VentanaModalidad.Show();
			this.Hide();
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
