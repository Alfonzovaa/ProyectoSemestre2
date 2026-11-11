using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Leviatanes2.Clases;

namespace Leviatanes2
{
	public partial class MainForm : Form
	{
		private List<Personaje> LeviatanesInsignia;
		private Button BotonEnHover;

		public MainForm()
		{
			InitializeComponent();
			this.ClientSize = new Size(UiResponsiveManager.ANCHO_BASE, UiResponsiveManager.ALTO_BASE);
			UiResponsiveManager.ConfigurarFormulario(this);

			CargarLeviatanesInsigniaDelBestiario();
			AjustarDisenoSimetrico();
		}

		private void CargarLeviatanesInsigniaDelBestiario()
		{
			LeviatanesInsignia = new List<Personaje>();
			try
			{
				GestorPersistenciaTxt Gestor = new GestorPersistenciaTxt();
				List<Personaje> Todos = Gestor.CargarPersonajesDesdeTxt();

				Elemento[] OrdenElementos = new Elemento[] {
					Elemento.Fuego,
					Elemento.Agua,
					Elemento.Tierra,
					Elemento.Aire
				};

				for (int e = 0; e < OrdenElementos.Length; e++)
				{
					Personaje Encontrado = null;
					for (int i = 0; i < Todos.Count; i++)
					{
						if (Todos[i].Faccion == OrdenElementos[e])
						{
							Encontrado = Todos[i];
							break;
						}
					}
					if (Encontrado != null)
					{
						LeviatanesInsignia.Add(Encontrado);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void MainFormResize(object sender, EventArgs e)
		{
			AjustarDisenoSimetrico();
			this.Invalidate();
		}

		private void AjustarDisenoSimetrico()
		{
			int W = this.ClientSize.Width;
			int H = this.ClientSize.Height;
			if (W <= 0 || H <= 0)
				return;

			float escala = Math.Min((float)W / UiResponsiveManager.ANCHO_BASE, (float)H / UiResponsiveManager.ALTO_BASE);
			if (escala < 0.2f) escala = 1f;

			// 1. Barras superior e inferior
			int barSupH = (int)(46 * escala);
			int barInfH = (int)(42 * escala);
			pnlBarraSuperiorNegra.SetBounds(0, 0, W, barSupH);
			lblHeaderRomGba.SetBounds(10, Math.Max(0, (barSupH - (int)(26 * escala)) / 2), W - 20, (int)(26 * escala));
			pnlFranjaOroSup.SetBounds(0, barSupH, W, Math.Max(2, (int)(3 * escala)));

			pnlBarraInferiorNegra.SetBounds(0, H - barInfH, W, barInfH);
			lblFooterCopyrightGba.SetBounds(10, Math.Max(0, (barInfH - (int)(24 * escala)) / 2), W - 20, (int)(24 * escala));
			lblFooterCopyrightGba.Text = "LEVIATANES DEL CARIBE 2 | MOTOR TACTICO GBA | F11 / ALT+ENTER: PANTALLA COMPLETA";
			pnlFranjaOroInf.SetBounds(0, H - barInfH - Math.Max(2, (int)(3 * escala)), W, Math.Max(2, (int)(3 * escala)));

			// 2. Logo superior centrado (720x140 base)
			int LogoW = (int)(720 * escala);
			int LogoH = (int)(140 * escala);
			int LogoY = barSupH + (int)(14 * escala);
			pnlMarcoLogoOro.SetBounds((W - LogoW) / 2, LogoY, LogoW, LogoH);
			pnlInteriorLogo.SetBounds(3, 3, LogoW - 6, LogoH - 6);

			// 3. Menu central estilizado (420x310 base)
			int MenuW = (int)(420 * escala);
			int MenuH = (int)(310 * escala);
			int MenuTop = LogoY + LogoH + (int)(14 * escala);
			pnlMarcoMenuGba.SetBounds((W - MenuW) / 2, MenuTop, MenuW, MenuH);
			pnlInteriorMenuGba.SetBounds(3, 3, MenuW - 6, MenuH - 6);

			int InteriorW = pnlInteriorMenuGba.Width;
			int headH = (int)(36 * escala);
			lblEncabezadoMenu.SetBounds(0, 0, InteriorW, headH);
			lblEncabezadoMenu.Font = new Font("Consolas", Math.Max(7.5f, 9.5F * escala), FontStyle.Bold);

			int BtnW = InteriorW - (int)(36 * escala);
			int BtnH = (int)(72 * escala);
			int gap = (int)(10 * escala);
			int btnStart = headH + (int)(10 * escala);
			button1.SetBounds((int)(18 * escala), btnStart, BtnW, BtnH);
			button2.SetBounds((int)(18 * escala), btnStart + BtnH + gap, BtnW, BtnH);
			button3.SetBounds((int)(18 * escala), btnStart + (BtnH + gap) * 2, BtnW, BtnH);

			// 4. Bitacora inferior centrada
			int BitacoraW = Math.Min(W - 40, (int)(780 * escala));
			int BitacoraH = (int)(46 * escala);
			int BitacoraY = Math.Min(H - barInfH - BitacoraH - 4, MenuTop + MenuH + (int)(12 * escala));
			lblPressStartGba.SetBounds((W - BitacoraW) / 2, BitacoraY, BitacoraW, BitacoraH);
			lblPressStartGba.Font = new Font("Consolas", Math.Max(7f, 9F * escala), FontStyle.Bold);
		}

		// =====================================================================
		// 1. PORTADA PRINCIPAL: FONDO ABISAL, SONAR NAVAL Y MINI-BESTIARIO EN VIVO
		// =====================================================================
		void MainFormPaint(object sender, PaintEventArgs e)
		{
			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			int W = this.ClientSize.Width;
			int H = this.ClientSize.Height;

			// 1. Degradado Abisal Marino
			using (LinearGradientBrush FondoAbisal = new LinearGradientBrush(
				new Rectangle(0, 0, W, H),
				Color.FromArgb(6, 12, 24),
				Color.FromArgb(14, 42, 68),
				LinearGradientMode.Vertical))
			{
				ColorBlend Mezcla = new ColorBlend(3);
				Mezcla.Colors = new Color[] {
					Color.FromArgb(6, 12, 24),
					Color.FromArgb(12, 28, 52),
					Color.FromArgb(18, 52, 78)
				};
				Mezcla.Positions = new float[] { 0f, 0.55f, 1f };
				FondoAbisal.InterpolationColors = Mezcla;
				G.FillRectangle(FondoAbisal, 0, 0, W, H);
			}

			// 2. Cuadricula de Carta Nautica y Anillos de Sonar Abisal
			using (Pen LapizRed = new Pen(Color.FromArgb(14, 120, 210, 255), 1))
			{
				for (int X = 0; X < W; X += 36)
					G.DrawLine(LapizRed, X, 45, X, H - 43);
				for (int Y = 45; Y < H - 43; Y += 36)
					G.DrawLine(LapizRed, 0, Y, W, Y);
			}

			int CentroSonarX = W / 2;
			int CentroSonarY = pnlMarcoMenuGba.Top + (pnlMarcoMenuGba.Height / 2);
			using (Pen LapizSonar = new Pen(Color.FromArgb(24, 245, 203, 66), 1.5f))
			{
				LapizSonar.DashStyle = DashStyle.Dash;
				G.DrawEllipse(LapizSonar, CentroSonarX - 280, CentroSonarY - 190, 560, 380);
				G.DrawEllipse(LapizSonar, CentroSonarX - 410, CentroSonarY - 265, 820, 530);
			}

			// 3. Particulas bioluminiscentes sutiles
			int[,] PuntosLuz = new int[,] {
				{ 70, 100, 3 }, { 115, 175, 2 }, { 60, 540, 4 }, { 210, 560, 2 },
				{ W - 85, 110, 3 }, { W - 125, 180, 2 }, { W - 75, 535, 4 }, { W - 205, 555, 2 }
			};
			using (SolidBrush BrochaBio = new SolidBrush(Color.FromArgb(55, 100, 240, 220)))
			{
				for (int i = 0; i < PuntosLuz.GetLength(0); i++)
				{
					G.FillEllipse(BrochaBio, PuntosLuz[i, 0], PuntosLuz[i, 1], PuntosLuz[i, 2] * 2, PuntosLuz[i, 2] * 2);
				}
			}

			// 4. Posiciones con margen limpio para las 4 tarjetas (268x138 cada una)
			int EspacioLateral = pnlMarcoMenuGba.Left;
			int CentroIzq = EspacioLateral / 2;
			int CentroDer = pnlMarcoMenuGba.Right + (EspacioLateral / 2);

			int FilaSuperiorY = pnlMarcoMenuGba.Top + 69;
			int FilaInferiorY = pnlMarcoMenuGba.Bottom - 69;

			DibujarTarjetaBestiarioInsignia(G, CentroIzq, FilaSuperiorY, 0, Elemento.Fuego, "Ignis Leviatan", "DRAKO PIRICO");
			DibujarTarjetaBestiarioInsignia(G, CentroIzq, FilaInferiorY, 1, Elemento.Agua, "Nereida Del Abismo", "BESTIA ABISAL");
			DibujarTarjetaBestiarioInsignia(G, CentroDer, FilaSuperiorY, 2, Elemento.Tierra, "Golem De Basalto", "COLOSO LITICO");
			DibujarTarjetaBestiarioInsignia(G, CentroDer, FilaInferiorY, 3, Elemento.Aire, "Cefiro Sierpe", "SIERPE CELESTE");
		}

		// =====================================================================
		// 2. TARJETAS LATERALES COMPACTAS SIN DESBORDAMIENTO (268 x 138 px)
		// =====================================================================
		private void DibujarTarjetaBestiarioInsignia(
			Graphics G,
			int CentroX,
			int CentroY,
			int IndiceInsignia,
			Elemento FacPorDefecto,
			string NombrePorDefecto,
			string LinajeTexto)
		{
			Personaje P = (LeviatanesInsignia != null && IndiceInsignia < LeviatanesInsignia.Count)
				? LeviatanesInsignia[IndiceInsignia]
				: null;

			Elemento Fac = P != null ? P.Faccion : FacPorDefecto;
			string Nombre = P != null ? P.Nombre : NombrePorDefecto;
			int Id = P != null ? P.IdPersonaje : (IndiceInsignia * 3 + 1);
			int Vit = P != null ? P.VidaMaxima : 50;
			int Bli = P != null ? P.DefensaBase : 25;
			int Pod = P != null ? P.AtaqueBase : 25;
			string Especial = P != null ? P.NombreEspecial : "Arte Elemental";

			Color ColorElem = ObtenerColorElemento(Fac);
			int Ancho = 268;
			int Alto = 138;
			Rectangle Rect = new Rectangle(CentroX - (Ancho / 2), CentroY - (Alto / 2), Ancho, Alto);

			// Sombra proyectada
			using (SolidBrush Sombra = new SolidBrush(Color.FromArgb(115, 0, 0, 0)))
			{
				G.FillRectangle(Sombra, Rect.X + 4, Rect.Y + 4, Rect.Width, Rect.Height);
			}

			// Fondo degradado de la tarjeta
			using (LinearGradientBrush FondoCard = new LinearGradientBrush(
				Rect,
				Color.FromArgb(16, 26, 44),
				Color.FromArgb(10, 18, 30),
				LinearGradientMode.Vertical))
			{
				G.FillRectangle(FondoCard, Rect);
			}

			// Encabezado del especimen
			Rectangle RectHeader = new Rectangle(Rect.X, Rect.Y, Rect.Width, 22);
			using (SolidBrush BrochaHeader = new SolidBrush(ColorElem))
			{
				G.FillRectangle(BrochaHeader, RectHeader);
			}

			using (Font FuenteHeader = new Font("Consolas", 7.5F, FontStyle.Bold))
			using (SolidBrush BrochaBlanca = new SolidBrush(Color.White))
			{
				string TextoHeader = string.Format(" CODICE #{0:00} // {1} [{2}]", Id, LinajeTexto, Fac.ToString().ToUpper());
				G.DrawString(TextoHeader, FuenteHeader, BrochaBlanca, Rect.X + 4, Rect.Y + 4);
			}

			// Visor izquierdo para el Sprite PNG o Sello Runico (82 x 98 px)
			Rectangle RectVisor = new Rectangle(Rect.X + 8, Rect.Y + 30, 82, 98);
			using (SolidBrush FondoVisor = new SolidBrush(Color.FromArgb(10, 16, 26)))
			using (Pen BordeVisor = new Pen(Color.FromArgb(120, ColorElem), 1))
			{
				G.FillRectangle(FondoVisor, RectVisor);
				G.DrawRectangle(BordeVisor, RectVisor);
			}

			using (Pen LapizGrid = new Pen(Color.FromArgb(22, 180, 220, 255), 1))
			{
				for (int x = RectVisor.X + 16; x < RectVisor.Right; x += 16)
					G.DrawLine(LapizGrid, x, RectVisor.Y, x, RectVisor.Bottom);
				for (int y = RectVisor.Y + 16; y < RectVisor.Bottom; y += 16)
					G.DrawLine(LapizGrid, RectVisor.X, y, RectVisor.Right, y);
			}

			// Intentar cargar PNG real del Leviatan si existe
			bool PngDibujado = false;
			Image Img = GestorPersistenciaTxt.CargarImagenPersonaje(Id, Nombre, false); // cache compartido: no Dispose
			{
				if (Img != null)
				{
					int MaxW = RectVisor.Width - 8;
					int MaxH = RectVisor.Height - 8;
					float Escala = Math.Min((float)MaxW / Img.Width, (float)MaxH / Img.Height);
					int DW = Math.Max(1, (int)(Img.Width * Escala));
					int DH = Math.Max(1, (int)(Img.Height * Escala));
					int DX = RectVisor.X + (RectVisor.Width - DW) / 2;
					int DY = RectVisor.Y + (RectVisor.Height - DH) / 2;

					G.InterpolationMode = InterpolationMode.HighQualityBicubic;
					G.DrawImage(Img, new Rectangle(DX, DY, DW, DH));
					PngDibujado = true;
				}
			}

			if (!PngDibujado)
			{
				int VX = RectVisor.X + (RectVisor.Width / 2);
				int VY = RectVisor.Y + (RectVisor.Height / 2);

				using (SolidBrush Aura = new SolidBrush(Color.FromArgb(55, ColorElem)))
				{
					G.FillEllipse(Aura, VX - 28, VY - 28, 56, 56);
				}

				Point[] Rombo = new Point[] {
					new Point(VX, VY - 21),
					new Point(VX + 21, VY),
					new Point(VX, VY + 21),
					new Point(VX - 21, VY)
				};

				using (SolidBrush Nucleo = new SolidBrush(ColorElem))
				using (Pen BordeOro = new Pen(Color.FromArgb(245, 203, 66), 2f))
				{
					G.FillPolygon(Nucleo, Rombo);
					G.DrawPolygon(BordeOro, Rombo);
				}

				string Sigla = Fac.ToString().Substring(0, 2).ToUpper();
				using (Font FuenteSigla = new Font("Consolas", 9.5F, FontStyle.Bold))
				{
					SizeF Tam = G.MeasureString(Sigla, FuenteSigla);
					G.DrawString(Sigla, FuenteSigla, Brushes.White, VX - (Tam.Width / 2f), VY - (Tam.Height / 2f));
				}
			}

			// Datos del Leviatan a la derecha del visor (con recorte seguro para que no desborde)
			int InfoX = RectVisor.Right + 8;
			int AnchoInfo = Rect.Right - InfoX - 6;

			StringFormat SinDesborde = new StringFormat {
				Trimming = StringTrimming.EllipsisCharacter,
				FormatFlags = StringFormatFlags.NoWrap
			};

			using (Font FuenteNombre = new Font("Consolas", 8.5F, FontStyle.Bold))
			using (SolidBrush BrochaOro = new SolidBrush(Color.FromArgb(245, 203, 66)))
			{
				G.DrawString(Nombre, FuenteNombre, BrochaOro, new RectangleF(InfoX, Rect.Y + 29, AnchoInfo, 16), SinDesborde);
			}

			DibujarMiniBarraStat(G, InfoX, Rect.Y + 50, "VIT", Vit, Color.SeaGreen);
			DibujarMiniBarraStat(G, InfoX, Rect.Y + 69, "BLI", Bli, Color.DodgerBlue);
			DibujarMiniBarraStat(G, InfoX, Rect.Y + 88, "POD", Pod, Color.Tomato);

			using (Font FuenteArte = new Font("Consolas", 7.5F, FontStyle.Bold))
			using (SolidBrush BrochaArte = new SolidBrush(Color.LightCyan))
			{
				string TextoArte = "★ " + Especial;
				G.DrawString(TextoArte, FuenteArte, BrochaArte, new RectangleF(InfoX, Rect.Y + 110, AnchoInfo, 16), SinDesborde);
			}

			using (Pen BordeTarjeta = new Pen(Color.FromArgb(245, 203, 66), 2f))
			{
				G.DrawRectangle(BordeTarjeta, Rect);
			}
		}

		private void DibujarMiniBarraStat(Graphics G, int X, int Y, string Etiqueta, int Valor, Color ColorBarra)
		{
			using (Font Fuente = new Font("Consolas", 7.5F, FontStyle.Bold))
			using (SolidBrush BrochaTexto = new SolidBrush(Color.WhiteSmoke))
			{
				G.DrawString(string.Format("{0}:{1,2}", Etiqueta, Valor), Fuente, BrochaTexto, X, Y);
			}

			int AnchoMaxBarra = 112;
			Rectangle RectFondo = new Rectangle(X + 44, Y + 2, AnchoMaxBarra, 9);
			int AnchoLleno = Math.Max(4, Math.Min(AnchoMaxBarra, (int)(AnchoMaxBarra * (Valor / 70f))));
			Rectangle RectLleno = new Rectangle(X + 44, Y + 2, AnchoLleno, 9);

			using (SolidBrush BrochaFondo = new SolidBrush(Color.FromArgb(10, 14, 22)))
			using (SolidBrush BrochaValor = new SolidBrush(ColorBarra))
			using (Pen Borde = new Pen(Color.FromArgb(60, 255, 255, 255), 1))
			{
				G.FillRectangle(BrochaFondo, RectFondo);
				G.FillRectangle(BrochaValor, RectLleno);
				G.DrawRectangle(Borde, RectFondo);
			}
		}

		// =====================================================================
		// 3. LOGO PRINCIPAL INDIE Y BOTONES SIN DESBORDE
		// =====================================================================
		private void PnlInteriorLogoPaint(object sender, PaintEventArgs e)
		{
			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			Rectangle Rect = pnlInteriorLogo.ClientRectangle;

			using (LinearGradientBrush Fondo = new LinearGradientBrush(
				Rect,
				Color.FromArgb(10, 18, 34),
				Color.FromArgb(22, 42, 72),
				LinearGradientMode.Vertical))
			{
				G.FillRectangle(Fondo, Rect);
			}

			using (Pen LapizScan = new Pen(Color.FromArgb(18, 255, 255, 255), 1))
			{
				for (int y = 0; y < Rect.Height; y += 4)
					G.DrawLine(LapizScan, 0, y, Rect.Width, y);
			}

			using (Pen LapizOro = new Pen(Color.FromArgb(245, 203, 66), 2f))
			{
				int M = 8;
				int L = 18;
				G.DrawLine(LapizOro, M, M, M + L, M);
				G.DrawLine(LapizOro, M, M, M, M + L);
				G.DrawLine(LapizOro, Rect.Width - M - L, M, Rect.Width - M, M);
				G.DrawLine(LapizOro, Rect.Width - M, M, Rect.Width - M, M + L);
				G.DrawLine(LapizOro, M, Rect.Height - M, M + L, Rect.Height - M);
				G.DrawLine(LapizOro, M, Rect.Height - M - L, M, Rect.Height - M);
				G.DrawLine(LapizOro, Rect.Width - M - L, Rect.Height - M, Rect.Width - M, Rect.Height - M);
				G.DrawLine(LapizOro, Rect.Width - M, Rect.Height - M - L, Rect.Width - M, Rect.Height - M);
			}

			StringFormat Centro = new StringFormat {
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			using (Font FuenteSup = new Font("Consolas", 8.5F, FontStyle.Bold))
			using (SolidBrush BrochaCian = new SolidBrush(Color.Aquamarine))
			{
				G.DrawString("─── CODICE ABISAL DE NAVEGACION Y COMBATE ───", FuenteSup, BrochaCian, new RectangleF(0, 8, Rect.Width, 18), Centro);
			}

			RectangleF RectTituloSombra = new RectangleF(3, 29, Rect.Width, 54);
			RectangleF RectTitulo = new RectangleF(0, 26, Rect.Width, 54);
			using (Font FuenteTitulo = new Font("Consolas", 25F, FontStyle.Bold))
			using (SolidBrush BrochaSombra = new SolidBrush(Color.FromArgb(190, 0, 0, 0)))
			using (LinearGradientBrush BrochaOro = new LinearGradientBrush(
				new Rectangle(0, 26, Rect.Width, 54),
				Color.FromArgb(255, 238, 130),
				Color.FromArgb(218, 155, 28),
				LinearGradientMode.Vertical))
			{
				G.DrawString("LEVIATANES DEL CARIBE 2", FuenteTitulo, BrochaSombra, RectTituloSombra, Centro);
				G.DrawString("LEVIATANES DEL CARIBE 2", FuenteTitulo, BrochaOro, RectTitulo, Centro);
			}

			int CintaW = 480;
			int CintaH = 30;
			Rectangle RectCinta = new Rectangle((Rect.Width - CintaW) / 2, 88, CintaW, CintaH);

			using (LinearGradientBrush FondoCinta = new LinearGradientBrush(
				RectCinta,
				Color.FromArgb(215, 48, 24),
				Color.FromArgb(145, 24, 14),
				LinearGradientMode.Vertical))
			using (Pen BordeCinta = new Pen(Color.FromArgb(245, 203, 66), 1.5f))
			{
				G.FillRectangle(FondoCinta, RectCinta);
				G.DrawRectangle(BordeCinta, RectCinta);
			}

			using (Font FuenteSub = new Font("Consolas", 9.5F, FontStyle.Bold))
			using (SolidBrush BrochaBlanca = new SolidBrush(Color.White))
			{
				G.DrawString("★ CRONICAS DEL ABISMO - ARENA ELEMENTAL ★", FuenteSub, BrochaBlanca, RectCinta, Centro);
			}
		}

		private void BotonMenuIndiePaint(object sender, PaintEventArgs e)
		{
			Button Btn = sender as Button;
			if (Btn == null)
				return;

			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			Rectangle Rect = Btn.ClientRectangle;
			bool EsHover = (BotonEnHover == Btn);

			Color ColorSuperior;
			Color ColorInferior;
			string TituloBoton;
			string SubtituloBoton;
			string IconoLateral;

			if (Btn == button1)
			{
				ColorSuperior = EsHover ? Color.FromArgb(235, 64, 38) : Color.FromArgb(195, 42, 24);
				ColorInferior = EsHover ? Color.FromArgb(165, 28, 16) : Color.FromArgb(130, 22, 12);
				TituloBoton = "EMPEZAR JUEGO";
				SubtituloBoton = "Forja, Bestiario y Arena Elemental";
				IconoLateral = "⚔";
			}
			else if (Btn == button2)
			{
				ColorSuperior = EsHover ? Color.FromArgb(46, 130, 215) : Color.FromArgb(30, 96, 168);
				ColorInferior = EsHover ? Color.FromArgb(24, 78, 140) : Color.FromArgb(18, 58, 108);
				TituloBoton = "VER PARTIDAS";
				SubtituloBoton = "Bitacora de combates y domadores";
				IconoLateral = "📜";
			}
			else
			{
				ColorSuperior = EsHover ? Color.FromArgb(72, 86, 112) : Color.FromArgb(46, 56, 76);
				ColorInferior = EsHover ? Color.FromArgb(42, 52, 70) : Color.FromArgb(26, 32, 46);
				TituloBoton = "SALIR DEL JUEGO";
				SubtituloBoton = "Guardar registros y cerrar juego";
				IconoLateral = "✖";
			}

			using (LinearGradientBrush FondoBtn = new LinearGradientBrush(Rect, ColorSuperior, ColorInferior, LinearGradientMode.Vertical))
			{
				G.FillRectangle(FondoBtn, Rect);
			}

			using (SolidBrush BarraIzq = new SolidBrush(EsHover ? Color.Gold : Color.FromArgb(180, 245, 203, 66)))
			{
				G.FillRectangle(BarraIzq, 0, 0, 6, Rect.Height);
			}

			using (SolidBrush BrilloSup = new SolidBrush(Color.FromArgb(EsHover ? 45 : 22, 255, 255, 255)))
			{
				G.FillRectangle(BrilloSup, 6, 2, Rect.Width - 8, (Rect.Height / 2) - 4);
			}

			// Emblema circular compacto a la izquierda (38x38)
			Rectangle RectIcono = new Rectangle(16, (Rect.Height - 38) / 2, 38, 38);
			using (SolidBrush FondoIcono = new SolidBrush(Color.FromArgb(90, 0, 0, 0)))
			using (Pen BordeIcono = new Pen(Color.FromArgb(245, 203, 66), 1.5f))
			{
				G.FillEllipse(FondoIcono, RectIcono);
				G.DrawEllipse(BordeIcono, RectIcono);
			}

			StringFormat Centro = new StringFormat {
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			using (Font FuenteIcono = new Font("Segoe UI Symbol", 12F, FontStyle.Bold))
			using (SolidBrush BrochaOro = new SolidBrush(Color.FromArgb(245, 203, 66)))
			{
				G.DrawString(IconoLateral, FuenteIcono, BrochaOro, RectIcono, Centro);
			}

			// Textos encuadrados dentro del ancho real del boton
			int TextoX = 64;
			int AnchoDisponible = Rect.Width - TextoX - 8;

			using (Font FuenteTit = new Font("Consolas", 13F, FontStyle.Bold))
			using (SolidBrush BrochaTit = new SolidBrush(EsHover ? Color.Yellow : Color.White))
			{
				string Prefijo = EsHover ? "► " : "";
				G.DrawString(Prefijo + TituloBoton, FuenteTit, BrochaTit, new RectangleF(TextoX, 11, AnchoDisponible, 24));
			}

			using (Font FuenteSub = new Font("Consolas", 8F, FontStyle.Bold))
			using (SolidBrush BrochaSub = new SolidBrush(Color.FromArgb(220, 235, 250)))
			{
				G.DrawString(SubtituloBoton, FuenteSub, BrochaSub, new RectangleF(TextoX, 38, AnchoDisponible, 20));
			}

			using (Pen Borde = new Pen(EsHover ? Color.Yellow : Color.FromArgb(210, 245, 203, 66), EsHover ? 2.5f : 1.5f))
			{
				G.DrawRectangle(Borde, 1, 1, Rect.Width - 3, Rect.Height - 3);
			}
		}

		private static Color ObtenerColorElemento(Elemento Faccion)
		{
			switch (Faccion)
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

		// =====================================================================
		// 4. EVENTOS DE NAVEGACION Y HOVER
		// =====================================================================
		void Button1MouseEnter(object sender, EventArgs e)
		{
			BotonEnHover = button1;
			button1.Invalidate();
			lblPressStartGba.Text = "BITACORA: Abre la Forja y Bestiario (CrearP) para gestionar tus Leviatanes y pasar a la Arena ▼";
		}

		void Button2MouseEnter(object sender, EventArgs e)
		{
			BotonEnHover = button2;
			button2.Invalidate();
			lblPressStartGba.Text = "BITACORA: Consulta el registro historico de partidas y estadisticas de domadores ▼";
		}

		void Button3MouseEnter(object sender, EventArgs e)
		{
			BotonEnHover = button3;
			button3.Invalidate();
			lblPressStartGba.Text = "BITACORA: Guarda los datos y cierra el simulador Leviatanes del Caribe 2 ▼";
		}

		void BotonMenuMouseLeave(object sender, EventArgs e)
		{
			Button Btn = sender as Button;
			if (BotonEnHover == Btn)
			{
				BotonEnHover = null;
			}
			if (Btn != null)
			{
				Btn.Invalidate();
			}
		}

		void Button1Click(object sender, EventArgs e)
		{
			CrearP abrirVentana = new CrearP();
			UiResponsiveManager.CambiarVentana(this, abrirVentana);
		}

		void Button2Click(object sender, EventArgs e)
		{
			VerPartidas abrirVentana = new VerPartidas();
			UiResponsiveManager.CambiarVentana(this, abrirVentana);
		}

		void Button3Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}