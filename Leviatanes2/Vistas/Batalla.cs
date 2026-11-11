using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Leviatanes2.Clases;
using Leviatanes2.LogicaBat;

namespace Leviatanes2
{
	/// <summary>
	/// Ventana de combate por turnos (Jugador vs CPU) con Arena Abierta Dinámica,
	/// HUD Táctico GDI+, rotación de biomas cada 2 turnos y animaciones a 60 FPS.
	/// </summary>
	public partial class Batalla : Form
	{
		// Panel interno con DoubleBuffered activado para renderizar la arena a 60 FPS sin parpadeo
		private class LienzoArenaBuffer : Panel
		{
			public LienzoArenaBuffer()
			{
				this.DoubleBuffered = true;
				this.SetStyle(
					ControlStyles.AllPaintingInWmPaint |
					ControlStyles.UserPaint |
					ControlStyles.OptimizedDoubleBuffer,
					true);
				this.UpdateStyles();
			}
		}

		private enum TipoAnimacionCombate
		{
			Ninguna,
			AtaqueJugador,
			EspecialJugador,
			AtaqueCpu,
			DefensaJugador,
			CargaJugador,
			DefensaOCargaCpu
		}

		public int numeroBatalla = 1;
		public static int MaximaBatallaDesbloqueada = 1;

		private MotorPartida motorCombate;
		private GestorPersistenciaTxt gestorTxt;
		private List<Escenario> catalogoEscenariosRotacion;
		private readonly Random azarEscenario = new Random();

		private bool partidaGuardadaEnTxt = false;
		private bool batallaGanada = false;
		private bool esIngles = false;
		private bool actualizandoListaBanca = false;

		// =====================================================================
		// MOTOR GRAFICO DE LA ARENA ABIERTA Y ANIMACIONES (60 FPS)
		// =====================================================================
		private LienzoArenaBuffer lienzoArena;
		private Timer tmrAnimacionArena;
		private TipoAnimacionCombate animacionActual = TipoAnimacionCombate.Ninguna;
		private int frameAnimacion = 0;
		private const int TotalFramesAtaque = 28;
		private int tickRespiracion = 0;

		// Control de rotación de escenario cada 2 turnos y banner sutil
		private int ultimoTurnoVerificadoEscenario = 1;
		private string nombreEscenarioPrevio = "";
		private int framesBannerCambioEscenario = 0;
		private string textoBannerCambioEscenario = "";

		// Texto flotante de daño / curación / escudo sobre los Leviatanes
		private string textoFlotanteValor = "";
		private Color colorTextoFlotante = Color.White;
		private bool textoFlotanteEnCpu = true;
		private int framesTextoFlotante = 0;

		public Batalla() : this(1)
		{
		}

		public Batalla(int batalla)
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			this.numeroBatalla = batalla;
			this.gestorTxt = new GestorPersistenciaTxt();
			this.catalogoEscenariosRotacion = GestorPersistenciaTxt.ObtenerCatalogoEscenarios();

			InicializarLienzoArenaAbierta();
			ConfigurarListasLateralesModernas();
			ConfigurarBatalla();
		}

		private void InicializarLienzoArenaAbierta()
		{
			// Ocultamos los subpaneles viejos para que toda la arena sea un escenario abierto continuo
			pnlHudCpu.Visible = false;
			pnlSpriteCpu.Visible = false;
			pnlVentajaElemental.Visible = false;
			pnlSpriteJugador.Visible = false;
			pnlHudJugador.Visible = false;

			lienzoArena = new LienzoArenaBuffer();
			lienzoArena.Dock = DockStyle.Fill;
			lienzoArena.BackColor = Color.FromArgb(12, 20, 34);
			lienzoArena.Paint += new PaintEventHandler(LienzoArenaPaint);
			pnlArenaCombate.Controls.Add(lienzoArena);
			lienzoArena.BringToFront();

			tmrAnimacionArena = new Timer(this.components);
			tmrAnimacionArena.Interval = 22; // ~45-50 FPS estables en WinForms
			tmrAnimacionArena.Tick += new EventHandler(TmrAnimacionArenaTick);
			tmrAnimacionArena.Start();
		}

		private void ConfigurarListasLateralesModernas()
		{
			lstColaJugador.DrawMode = DrawMode.OwnerDrawFixed;
			lstColaJugador.ItemHeight = 36;
			lstColaJugador.HorizontalScrollbar = false;
			lstColaJugador.DrawItem += new DrawItemEventHandler(LstColaJugadorDrawItem);

			lstDerrotadosCpu.DrawMode = DrawMode.OwnerDrawFixed;
			lstDerrotadosCpu.ItemHeight = 36;
			lstDerrotadosCpu.HorizontalScrollbar = false;
			lstDerrotadosCpu.DrawItem += new DrawItemEventHandler(LstDerrotadosCpuDrawItem);
		}

		private void ConfigurarBatalla()
		{
			tmrTurnoCpu.Stop();
			tmrPausaDerrota.Stop();

			animacionActual = TipoAnimacionCombate.Ninguna;
			frameAnimacion = 0;
			framesTextoFlotante = 0;
			framesBannerCambioEscenario = 0;

			this.StartPosition = FormStartPosition.CenterScreen;

			if (this.numeroBatalla > MaximaBatallaDesbloqueada)
			{
				this.numeroBatalla = MaximaBatallaDesbloqueada;
			}

			this.esIngles = (this.numeroBatalla == 2);
			this.batallaGanada = false;
			this.partidaGuardadaEnTxt = false;

			if (!SeleccionModalidad.ModoContraCpu && SeleccionModalidad.JugadorUnoConfigurado != null)
			{
				pnlArenaCombate.Visible = false;
				pnlLateralDerecho.Visible = false;
				btnAtacar.Enabled = false;
				btnEspecial.Enabled = false;
				btnDefender.Enabled = false;
				btnCambiarLeviatan.Enabled = false;
				btnCargarEnergia.Enabled = false;
				btnReiniciarBatalla.Enabled = false;
				button1.Enabled = false;
				lblTituloBatalla.Text = "Modo exclusivo Jugador vs CPU";
				lblDialogoCombate.Text = "Esta batalla solo está disponible en modo Jugador vs CPU.";
				return;
			}

			pnlArenaCombate.Visible = true;
			pnlLateralDerecho.Visible = true;

			BloquearBotonAvance();
			AplicarIdiomaControles();

			PerfilJugador jugador = PrepararEquipoJugador(this.esIngles);
			PerfilJugador cpu = null;
			Escenario escenarioCombate = null;

			switch (numeroBatalla)
			{
				case 1:
					cpu = new PerfilJugador(901, "CPU - Fosa Abisal", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaAgua(101, "Leviatán de la Fosa", 55, 20, 25, 75, "Pulso Abisal", "Roba energía, se cura y golpea."));
					cpu.MazoSeleccionado.Add(new CartaFuego(102, "Ignis De La Grieta", 45, 20, 35, 45, "Llamarada Submarina", "Vapor ardiente que perfora escudo."));
					cpu.MazoSeleccionado.Add(new CartaAire(103, "Sierpe De Remolino", 50, 20, 30, 65, "Ráfaga De Fosa", "Doble embestida veloz."));

					escenarioCombate = SeleccionModalidad.EscenarioInicialSorteado != null
						? SeleccionModalidad.EscenarioInicialSorteado
						: new Escenario(1, "Abismo Oceánico", Elemento.Agua, Elemento.Fuego, 6, 4, 65, "Agua +6, Fuego -4.");

					this.Text = "Batalla 1: Leviatán de la Fosa";
					lblTituloBatalla.Text = "Batalla 1: Leviatán de la Fosa";
					break;

				case 2:
					cpu = new PerfilJugador(902, "CPU - Black Waters", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaTierra(201, "Tar Leviathan", 55, 25, 20, 70, "Tar Tsunami", "Hardens armor and crushes the enemy."));
					cpu.MazoSeleccionado.Add(new CartaFuego(202, "Oil Well Pyromancer", 45, 20, 35, 50, "Crude Ignition", "Pierces enemy shield with fire."));
					cpu.MazoSeleccionado.Add(new CartaAgua(203, "Corrupted Hydra", 50, 20, 30, 60, "Dark Drain", "Drains energy and restores HP."));

					escenarioCombate = new Escenario(2, "Black Waters Canyon", Elemento.Tierra, Elemento.Agua, 6, 4, 75, "Earth +6, Water -4.");

					this.Text = "Battle 2: Tar Leviathan";
					lblTituloBatalla.Text = "Battle 2: Tar Leviathan";
					break;

				case 3:
					cpu = new PerfilJugador(903, "CPU - Mar Espejo", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaAire(301, "Leviatán Espectral", 45, 25, 30, 80, "Niebla Tóxica Doble", "Doble ataque fantasmal."));
					cpu.MazoSeleccionado.Add(new CartaAgua(302, "Espectro Del Arrecife", 55, 20, 25, 70, "Eco Abisal", "Drena vigor y se cura."));
					cpu.MazoSeleccionado.Add(new CartaTierra(303, "Coloso De Chatarra", 55, 25, 20, 75, "Muro Oxidado", "Refuerza escudo y golpea."));

					escenarioCombate = new Escenario(3, "Mar Espejo Brumoso", Elemento.Aire, Elemento.Tierra, 6, 4, 70, "Aire +6, Tierra -4.");

					this.Text = "Batalla 3: Leviatán Espectral";
					lblTituloBatalla.Text = "Batalla 3: Leviatán Espectral";
					break;

				case 4:
					cpu = new PerfilJugador(904, "CPU - Tormenta Ácida", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaAire(401, "Leviatán Tormenta Ácida", 45, 20, 35, 75, "Ciclón Corrosivo", "Doble impacto aéreo ácido."));
					cpu.MazoSeleccionado.Add(new CartaFuego(402, "Centella Sulfúrica", 40, 20, 40, 55, "Relámpago Ígneo", "Perfora el escudo enemigo."));
					cpu.MazoSeleccionado.Add(new CartaAgua(403, "Vórtice De Salmuera", 55, 20, 25, 80, "Tifón Drenante", "Roba energía y regenera vida."));

					escenarioCombate = new Escenario(4, "Cumbre De Tormentas", Elemento.Aire, Elemento.Tierra, 6, 4, 70, "Aire +6, Tierra -4.");

					this.Text = "Batalla 4: Tormentas Ácidas";
					lblTituloBatalla.Text = "Batalla 4: Leviatán Tormenta Ácida";
					break;

				case 5:
				default:
					cpu = new PerfilJugador(905, "CPU - Núcleo Primordial", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaFuego(501, "Leviatán Primordial", 45, 20, 35, 85, "Cataclismo De Petróleo", "Ignora todo escudo rival."));
					cpu.MazoSeleccionado.Add(new CartaTierra(502, "Titán Del Pozo Negro", 55, 25, 20, 80, "Aplastamiento Abisal", "Aumenta coraza y sacude."));
					cpu.MazoSeleccionado.Add(new CartaAgua(503, "Kraken De Aguas Negras", 55, 20, 25, 75, "Marea Corrupta", "Roba energía y se cura."));
					cpu.MazoSeleccionado.Add(new CartaAire(504, "Heraldo Del Huracán", 45, 20, 35, 70, "Doble Vendaval", "Golpea dos veces."));

					escenarioCombate = new Escenario(5, "Pozo Primordial Activo", Elemento.Fuego, Elemento.Aire, 6, 4, 70, "Fuego +6, Aire -4.");

					this.Text = "Batalla Final 5: Leviatán Primordial";
					lblTituloBatalla.Text = "Batalla Final 5: Leviatán Primordial";
					break;
			}

			motorCombate = new MotorPartida(jugador, cpu, escenarioCombate, this.esIngles);
			ultimoTurnoVerificadoEscenario = motorCombate.TurnoActual;
			nombreEscenarioPrevio = motorCombate.EscenarioActual != null ? motorCombate.EscenarioActual.Nombre : "";

			if (this.esIngles)
			{
				lblDialogoCombate.Text = string.Format(
					"Battle {0} starts! {1} vs {2}.\r\nChoose your action below.",
					numeroBatalla,
					motorCombate.ActivoJugador.Nombre,
					motorCombate.ActivoCpu.Nombre);
			}
			else
			{
				lblDialogoCombate.Text = string.Format(
					"¡Comienza la Batalla {0}! {1} vs {2}.\r\nSelecciona una acción para atacar o cambiar.",
					numeroBatalla,
					motorCombate.ActivoJugador.Nombre,
					motorCombate.ActivoCpu.Nombre);
			}

			ActualizarInterfazCompleta();
		}

		// =====================================================================
		// 1. LOGICA DE ROTACION DE ESCENARIO CADA 2 TURNOS Y DISPARO DE ANIMACION
		// =====================================================================
		private void VerificarRotacionEscenarioCadaDosTurnos()
		{
			if (motorCombate == null || motorCombate.EscenarioActual == null)
				return;

			int turno = motorCombate.TurnoActual;
			string nombreActual = motorCombate.EscenarioActual.Nombre;

			// Si el MotorPartida ya rotó el escenario internamente, mostramos el aviso sutil
			if (!string.IsNullOrEmpty(nombreEscenarioPrevio) &&
			    !string.Equals(nombreEscenarioPrevio, nombreActual, StringComparison.OrdinalIgnoreCase))
			{
				nombreEscenarioPrevio = nombreActual;
				ultimoTurnoVerificadoEscenario = turno;
				ActivarBannerSutilEscenario(motorCombate.EscenarioActual);
				return;
			}

			// Si cambió a un nuevo turno impar > 1 (es decir, pasaron 2 turnos completos: 3, 5, 7...)
			// y el motor no rotó el escenario por sí mismo, lo rotamos entre los 4 biomas
			if (turno > ultimoTurnoVerificadoEscenario && turno > 1 && ((turno - 1) % 2 == 0))
			{
				ultimoTurnoVerificadoEscenario = turno;
				if (catalogoEscenariosRotacion == null || catalogoEscenariosRotacion.Count == 0)
				{
					catalogoEscenariosRotacion = GestorPersistenciaTxt.ObtenerCatalogoEscenarios();
				}

				if (catalogoEscenariosRotacion != null && catalogoEscenariosRotacion.Count > 1)
				{
					List<Escenario> candidatos = new List<Escenario>();
					for (int i = 0; i < catalogoEscenariosRotacion.Count; i++)
					{
						if (!string.Equals(catalogoEscenariosRotacion[i].Nombre, nombreActual, StringComparison.OrdinalIgnoreCase))
						{
							candidatos.Add(catalogoEscenariosRotacion[i]);
						}
					}

					if (candidatos.Count > 0)
					{
						Escenario nuevoEsc = candidatos[azarEscenario.Next(candidatos.Count)];
						// Intentar asignar al motor si la propiedad lo permite o actualizar sus campos
						try
						{
							var prop = typeof(MotorPartida).GetProperty("EscenarioActual");
							if (prop != null && prop.CanWrite)
							{
								prop.SetValue(motorCombate, nuevoEsc, null);
							}
						}
						catch
						{
						}
						nombreEscenarioPrevio = motorCombate.EscenarioActual.Nombre;
						ActivarBannerSutilEscenario(motorCombate.EscenarioActual);
					}
				}
			}
			else if (turno != ultimoTurnoVerificadoEscenario)
			{
				ultimoTurnoVerificadoEscenario = turno;
			}
		}

		private void ActivarBannerSutilEscenario(Escenario esc)
		{
			if (esc == null)
				return;

			textoBannerCambioEscenario = this.esIngles
				? string.Format("⚡ ARENA SHIFT: {0} ({1}) ⚡", esc.Nombre.ToUpper(), esc.Descripcion)
				: string.Format("⚡ ROTACION DE ARENA: {0} ({1}) ⚡", esc.Nombre.ToUpper(), esc.Descripcion);
			framesBannerCambioEscenario = 65; // ~1.4 segundos visible
		}

		private void IniciarAnimacionCombate(
			TipoAnimacionCombate tipo,
			string textoFlotante,
			Color colorFlotante,
			bool sobreCpu)
		{
			animacionActual = tipo;
			frameAnimacion = 0;

			if (!string.IsNullOrEmpty(textoFlotante))
			{
				textoFlotanteValor = textoFlotante;
				colorTextoFlotante = colorFlotante;
				textoFlotanteEnCpu = sobreCpu;
				framesTextoFlotante = 36;
			}

			if (lienzoArena != null)
			{
				lienzoArena.Invalidate();
			}
		}

		private void TmrAnimacionArenaTick(object sender, EventArgs e)
		{
			tickRespiracion = (tickRespiracion + 1) % 360;

			bool requiereRedibujo = (tickRespiracion % 2 == 0);

			if (animacionActual != TipoAnimacionCombate.Ninguna)
			{
				frameAnimacion++;
				requiereRedibujo = true;
				if (frameAnimacion >= TotalFramesAtaque)
				{
					animacionActual = TipoAnimacionCombate.Ninguna;
					frameAnimacion = 0;
				}
			}

			if (framesTextoFlotante > 0)
			{
				framesTextoFlotante--;
				requiereRedibujo = true;
			}

			if (framesBannerCambioEscenario > 0)
			{
				framesBannerCambioEscenario--;
				requiereRedibujo = true;
			}

			if (requiereRedibujo && lienzoArena != null && pnlArenaCombate.Visible)
			{
				lienzoArena.Invalidate();
			}
		}

		// =====================================================================
		// 2. RENDERIZADO COMPLETO DE LA ARENA ABIERTA, HUD Y ANIMACIONES
		// =====================================================================
		private void LienzoArenaPaint(object sender, PaintEventArgs e)
		{
			if (motorCombate == null)
				return;

			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			Rectangle RectTotal = lienzoArena.ClientRectangle;
			int AltoBarraInf = 36;
			Rectangle RectEscenario = new Rectangle(0, 0, RectTotal.Width, RectTotal.Height - AltoBarraInf);
			Rectangle RectBarraInf = new Rectangle(0, RectTotal.Height - AltoBarraInf, RectTotal.Width, AltoBarraInf);

			Escenario esc = motorCombate.EscenarioActual;
			Elemento bioma = ObtenerElementoBioma(esc);

			// 1. Dibujar el Bioma de fondo (Cielo, horizonte, terreno y efectos ambientales)
			DibujarBiomaEscenarioAbierto(G, RectEscenario, bioma);

			// 2. Coordenadas base de las dos plataformas en perspectiva diagonal (estilo portátil clásico)
			Point CentroPlataformaCpu = new Point(RectEscenario.Width - 205, 132);
			Point CentroPlataformaPj = new Point(205, RectEscenario.Height - 66);

			// 3. Calcular desplazamientos por animación (embestida, sacudida por daño, parpadeo)
			int offsetPjX = 0, offsetPjY = 0;
			int offsetCpuX = 0, offsetCpuY = 0;
			bool ocultarPjPorParpadeo = false;
			bool ocultarCpuPorParpadeo = false;
			bool destelloImpactoEnCpu = false;
			bool destelloImpactoEnPj = false;

			// Respiración suave constante cuando están en reposo
			int bobPj = (int)Math.Round(Math.Sin(tickRespiracion * 0.12) * 3.0);
			int bobCpu = (int)Math.Round(Math.Cos(tickRespiracion * 0.12) * 3.0);
			offsetPjY += bobPj;
			offsetCpuY += bobCpu;

			if (animacionActual == TipoAnimacionCombate.AtaqueJugador || animacionActual == TipoAnimacionCombate.EspecialJugador)
			{
				if (frameAnimacion <= 8)
				{
					// Fase 1: Embestida diagonal hacia el enemigo (arriba-derecha)
					float t = frameAnimacion / 8f;
					offsetPjX += (int)(44 * t);
					offsetPjY -= (int)(22 * t);
				}
				else if (frameAnimacion <= 14)
				{
					// Regreso de la embestida
					float t = 1f - ((frameAnimacion - 8) / 6f);
					offsetPjX += (int)(44 * t);
					offsetPjY -= (int)(22 * t);
				}

				if (frameAnimacion >= 9 && frameAnimacion <= 22)
				{
					// Fase 2: El rival tiembla horizontalmente y parpadea al recibir el golpe
					offsetCpuX += ((frameAnimacion % 2 == 0) ? 9 : -9);
					ocultarCpuPorParpadeo = (frameAnimacion % 3 == 0);
					destelloImpactoEnCpu = (frameAnimacion <= 16);
				}
			}
			else if (animacionActual == TipoAnimacionCombate.AtaqueCpu)
			{
				if (frameAnimacion <= 8)
				{
					// Fase 1: CPU embiste hacia abajo-izquierda
					float t = frameAnimacion / 8f;
					offsetCpuX -= (int)(44 * t);
					offsetCpuY += (int)(22 * t);
				}
				else if (frameAnimacion <= 14)
				{
					float t = 1f - ((frameAnimacion - 8) / 6f);
					offsetCpuX -= (int)(44 * t);
					offsetCpuY += (int)(22 * t);
				}

				if (frameAnimacion >= 9 && frameAnimacion <= 22)
				{
					// Fase 2: El jugador tiembla y parpadea al recibir daño
					offsetPjX += ((frameAnimacion % 2 == 0) ? 9 : -9);
					ocultarPjPorParpadeo = (frameAnimacion % 3 == 0);
					destelloImpactoEnPj = (frameAnimacion <= 16);
				}
			}

			// 4. Dibujar plataformas tácticas sobre el terreno
			DibujarPlataformaTerreno(G, CentroPlataformaCpu.X, CentroPlataformaCpu.Y, 196, 46, bioma, false);
			DibujarPlataformaTerreno(G, CentroPlataformaPj.X, CentroPlataformaPj.Y, 224, 54, bioma, true);

			// 5. Dibujar Leviatán CPU y Leviatán Jugador (con soporte PNG + animaciones)
			if (motorCombate.ActivoCpu != null)
			{
				DibujarLeviatanEnEscenario(
					G,
					motorCombate.ActivoCpu,
					CentroPlataformaCpu.X + offsetCpuX,
					CentroPlataformaCpu.Y - 40 + offsetCpuY,
					132,
					112,
					ocultarCpuPorParpadeo,
					destelloImpactoEnCpu,
					animacionActual == TipoAnimacionCombate.DefensaOCargaCpu);
			}

			if (motorCombate.ActivoJugador != null)
			{
				DibujarLeviatanEnEscenario(
					G,
					motorCombate.ActivoJugador,
					CentroPlataformaPj.X + offsetPjX,
					CentroPlataformaPj.Y - 48 + offsetPjY,
					152,
					128,
					ocultarPjPorParpadeo,
					destelloImpactoEnPj,
					animacionActual == TipoAnimacionCombate.DefensaJugador || animacionActual == TipoAnimacionCombate.CargaJugador);
			}

			// 6. Dibujar ráfaga / proyectil elemental durante el ataque
			DibujarEfectosProyectilEImpacto(
				G,
				new Point(CentroPlataformaPj.X + 30, CentroPlataformaPj.Y - 48),
				new Point(CentroPlataformaCpu.X - 20, CentroPlataformaCpu.Y - 38));

			// 7. Dibujar texto flotante de daño / escudo / energía
			if (framesTextoFlotante > 0 && !string.IsNullOrEmpty(textoFlotanteValor))
			{
				Point ptBase = textoFlotanteEnCpu
					? new Point(CentroPlataformaCpu.X, CentroPlataformaCpu.Y - 88)
					: new Point(CentroPlataformaPj.X, CentroPlataformaPj.Y - 98);

				int elevacion = (36 - framesTextoFlotante) * 1;
				DibujarTextoFlotanteCombate(G, textoFlotanteValor, colorTextoFlotante, ptBase.X, ptBase.Y - elevacion);
			}

			// 8. Dibujar los 2 HUD Tácticos Flotantes (Opción A: Vida + Escudo + Orbes de Energía + Especial)
			Rectangle RectHudCpu = new Rectangle(16, 14, 376, 108);
			Rectangle RectHudPj = new Rectangle(RectEscenario.Width - 408, RectEscenario.Height - 134, 392, 124);

			if (motorCombate.ActivoCpu != null)
			{
				DibujarHudTacticoFlotante(G, RectHudCpu, motorCombate.ActivoCpu, motorCombate.ActivoJugador, esc, true);
			}

			if (motorCombate.ActivoJugador != null)
			{
				DibujarHudTacticoFlotante(G, RectHudPj, motorCombate.ActivoJugador, motorCombate.ActivoCpu, esc, false);
			}

			// 9. Banner sutil superior cuando rota el escenario cada 2 turnos
			if (framesBannerCambioEscenario > 0 && !string.IsNullOrEmpty(textoBannerCambioEscenario))
			{
				DibujarBannerSutilRotacion(G, RectEscenario.Width, textoBannerCambioEscenario, bioma);
			}

			// 10. Barra Inferior Integrada (Opción B: Reglas/Desventajas de la Arena + Ventaja Elemental)
			DibujarBarraInferiorIntegrada(G, RectBarraInf, esc, motorCombate.ActivoJugador, motorCombate.ActivoCpu);
		}

		// =====================================================================
		// 3. BIOMAS DE LA ARENA, PLATAFORMAS, LEVIATANES Y HUD GDI+
		// =====================================================================
		private Elemento ObtenerElementoBioma(Escenario esc)
		{
			if (esc == null)
				return Elemento.Agua;

			string txt = (esc.Nombre + " " + esc.Descripcion).ToUpperInvariant();
			if (txt.Contains("FUEGO") || txt.Contains("FIRE") || txt.Contains("VOLCAN") || txt.Contains("POZO") || txt.Contains("CALDERA"))
				return Elemento.Fuego;
			if (txt.Contains("TIERRA") || txt.Contains("EARTH") || txt.Contains("ROCA") || txt.Contains("CANYON") || txt.Contains("CANION") || txt.Contains("NEGRA"))
				return Elemento.Tierra;
			if (txt.Contains("AIRE") || txt.Contains("AIR") || txt.Contains("VIENTO") || txt.Contains("TORMENTA") || txt.Contains("ESPEJO"))
				return Elemento.Aire;

			return Elemento.Agua;
		}

		private void DibujarBiomaEscenarioAbierto(Graphics G, Rectangle R, Elemento bioma)
		{
			Color cieloSup, cieloMed, sueloInf, colorLineaHorizonte, colorParticula;

			switch (bioma)
			{
				case Elemento.Fuego:
					cieloSup = Color.FromArgb(28, 10, 14);
					cieloMed = Color.FromArgb(78, 22, 18);
					sueloInf = Color.FromArgb(42, 14, 14);
					colorLineaHorizonte = Color.FromArgb(140, 255, 110, 40);
					colorParticula = Color.FromArgb(95, 255, 160, 50);
					break;
				case Elemento.Tierra:
					cieloSup = Color.FromArgb(24, 18, 14);
					cieloMed = Color.FromArgb(64, 46, 28);
					sueloInf = Color.FromArgb(38, 28, 18);
					colorLineaHorizonte = Color.FromArgb(130, 215, 165, 85);
					colorParticula = Color.FromArgb(85, 210, 170, 105);
					break;
				case Elemento.Aire:
					cieloSup = Color.FromArgb(12, 26, 38);
					cieloMed = Color.FromArgb(28, 68, 74);
					sueloInf = Color.FromArgb(16, 40, 48);
					colorLineaHorizonte = Color.FromArgb(130, 120, 245, 210);
					colorParticula = Color.FromArgb(90, 160, 255, 235);
					break;
				default: // Agua / Fosa Abisal
					cieloSup = Color.FromArgb(8, 16, 32);
					cieloMed = Color.FromArgb(16, 48, 86);
					sueloInf = Color.FromArgb(10, 28, 52);
					colorLineaHorizonte = Color.FromArgb(130, 70, 185, 255);
					colorParticula = Color.FromArgb(85, 110, 220, 255);
					break;
			}

			int yHorizonte = (int)(R.Height * 0.44f);

			// Cielo / Atmósfera superior
			Rectangle rectCielo = new Rectangle(R.X, R.Y, R.Width, yHorizonte + 4);
			using (LinearGradientBrush brCielo = new LinearGradientBrush(rectCielo, cieloSup, cieloMed, LinearGradientMode.Vertical))
			{
				G.FillRectangle(brCielo, rectCielo);
			}

			// Siluetas de montañas / arrecifes / volcanes en el horizonte
			Point[] cordillera = new Point[]
			{
				new Point(0, yHorizonte),
				new Point(95, yHorizonte - 28),
				new Point(190, yHorizonte - 8),
				new Point(310, yHorizonte - 36),
				new Point(430, yHorizonte - 12),
				new Point(560, yHorizonte - 32),
				new Point(690, yHorizonte - 10),
				new Point(790, yHorizonte - 26),
				new Point(R.Width, yHorizonte - 6),
				new Point(R.Width, yHorizonte)
			};
			using (SolidBrush brSilueta = new SolidBrush(Color.FromArgb(110, 6, 10, 18)))
			{
				G.FillPolygon(brSilueta, cordillera);
			}

			// Suelo / Campo de batalla en perspectiva
			Rectangle rectSuelo = new Rectangle(R.X, yHorizonte, R.Width, R.Height - yHorizonte);
			using (LinearGradientBrush brSuelo = new LinearGradientBrush(rectSuelo, cieloMed, sueloInf, LinearGradientMode.Vertical))
			{
				G.FillRectangle(brSuelo, rectSuelo);
			}

			// Líneas de perspectiva táctica en el suelo
			using (Pen lapizPerspectiva = new Pen(Color.FromArgb(26, colorLineaHorizonte), 1))
			{
				int puntoFugaX = R.Width / 2;
				for (int x = -240; x <= R.Width + 240; x += 85)
				{
					G.DrawLine(lapizPerspectiva, puntoFugaX + ((x - puntoFugaX) / 3), yHorizonte, x, R.Bottom);
				}
				for (int y = yHorizonte + 18; y < R.Bottom; y += 26)
				{
					G.DrawLine(lapizPerspectiva, 0, y, R.Width, y);
				}
			}

			using (Pen lapizHorizonte = new Pen(colorLineaHorizonte, 1.5f))
			{
				G.DrawLine(lapizHorizonte, 0, yHorizonte, R.Width, yHorizonte);
			}

			// Partículas ambientales animadas según el bioma (brasas, burbujas, ráfagas o polvo sísmico)
			using (SolidBrush brPart = new SolidBrush(colorParticula))
			{
				for (int i = 0; i < 12; i++)
				{
					int px = ((i * 79) + (tickRespiracion * (bioma == Elemento.Aire ? 3 : 1))) % Math.Max(1, R.Width - 10);
					int py = ((i * 47) + (R.Height - (tickRespiracion % Math.Max(1, R.Height)))) % Math.Max(1, R.Height - 20);
					int tam = (i % 3) + 3;
					if (bioma == Elemento.Aire)
					{
						G.FillRectangle(brPart, px, py, tam * 4, 2);
					}
					else
					{
						G.FillEllipse(brPart, px, py, tam, tam);
					}
				}
			}
		}

		private void DibujarPlataformaTerreno(Graphics G, int cx, int cy, int ancho, int alto, Elemento bioma, bool esJugador)
		{
			Color colorBase = ObtenerColorElemento(bioma);

			// Sombra exterior profunda
			using (SolidBrush sombra = new SolidBrush(Color.FromArgb(135, 0, 0, 0)))
			{
				G.FillEllipse(sombra, cx - (ancho / 2) - 4, cy - (alto / 2) + 7, ancho + 8, alto + 4);
			}

			// Cuerpo de la plataforma con degradado
			Rectangle rectPlat = new Rectangle(cx - (ancho / 2), cy - (alto / 2), ancho, alto);
			using (LinearGradientBrush brPlat = new LinearGradientBrush(
				rectPlat,
				Color.FromArgb(165, colorBase),
				Color.FromArgb(95, 16, 24, 38),
				LinearGradientMode.Vertical))
			{
				G.FillEllipse(brPlat, rectPlat);
			}

			// Anillos tácticos concéntricos estilo arena GBA
			using (Pen bordeExterior = new Pen(Color.FromArgb(230, 245, 203, 66), esJugador ? 2.5f : 2f))
			using (Pen anilloInterno = new Pen(Color.FromArgb(110, 255, 255, 255), 1f))
			{
				G.DrawEllipse(bordeExterior, rectPlat);
				anilloInterno.DashStyle = DashStyle.Dash;
				G.DrawEllipse(anilloInterno, cx - (ancho / 2) + 14, cy - (alto / 2) + 6, ancho - 28, alto - 12);
			}
		}

		private void DibujarLeviatanEnEscenario(
			Graphics G,
			Personaje p,
			int cx,
			int cy,
			int maxAncho,
			int maxAlto,
			bool ocultarPorParpadeo,
			bool destelloImpacto,
			bool efectoDefensaOCarga)
		{
			if (ocultarPorParpadeo)
				return;

			Color colorElem = p.EstaVivo ? ObtenerColorElemento(p.Faccion) : Color.FromArgb(95, 95, 105);

			// Aura de Escudo Activo o Carga de Energía
			if (p.EstaVivo && (p.EscudoActual > 0 || efectoDefensaOCarga))
			{
				Color colorAura = efectoDefensaOCarga
					? Color.FromArgb(190, 255, 215, 60)
					: Color.FromArgb(145, 80, 210, 255);
				using (SolidBrush rellEscudo = new SolidBrush(Color.FromArgb(28, colorAura)))
				using (Pen lapizEscudo = new Pen(colorAura, efectoDefensaOCarga ? 3f : 2f))
				{
					G.FillEllipse(rellEscudo, cx - 54, cy - 48, 108, 96);
					G.DrawEllipse(lapizEscudo, cx - 54, cy - 48, 108, 96);
				}
			}

			// 1. Intentar dibujar PNG del Leviatán desde la carpeta imagenes
			bool pngDibujado = false;
			if (p.EstaVivo)
			{
				using (Image img = GestorPersistenciaTxt.CargarImagenPersonaje(p.IdPersonaje, p.Nombre))
				{
					if (img != null)
					{
						float escala = Math.Min((float)maxAncho / img.Width, (float)maxAlto / img.Height);
						int dw = Math.Max(1, (int)(img.Width * escala));
						int dh = Math.Max(1, (int)(img.Height * escala));
						int dx = cx - (dw / 2);
						int dy = cy - (dh / 2);

						G.InterpolationMode = InterpolationMode.HighQualityBicubic;
						G.DrawImage(img, new Rectangle(dx, dy, dw, dh));
						pngDibujado = true;
					}
				}
			}

			// 2. Respaldo si aún no tiene foto PNG: Emblema Rúnico de Leviatán
			if (!pngDibujado)
			{
				using (SolidBrush aura = new SolidBrush(Color.FromArgb(65, colorElem)))
				{
					G.FillEllipse(aura, cx - 44, cy - 42, 88, 84);
				}

				Point[] rombo = new Point[]
				{
					new Point(cx, cy - 38),
					new Point(cx + 38, cy),
					new Point(cx, cy + 38),
					new Point(cx - 38, cy)
				};

				using (SolidBrush brochaCuerpo = new SolidBrush(colorElem))
				using (Pen bordeOro = new Pen(p.EstaVivo ? Color.FromArgb(245, 203, 66) : Color.Crimson, 2.5f))
				using (Font fuenteElem = new Font("Consolas", 12F, FontStyle.Bold))
				{
					G.FillPolygon(brochaCuerpo, rombo);
					G.DrawPolygon(bordeOro, rombo);

					string elemTxt = p.EstaVivo
						? MotorPartida.FormatearElemento(p.Faccion, this.esIngles).Substring(0, 2).ToUpper()
						: "KO";

					SizeF tam = G.MeasureString(elemTxt, fuenteElem);
					G.DrawString(elemTxt, fuenteElem, Brushes.White, cx - (tam.Width / 2f), cy - (tam.Height / 2f));
				}
			}

			// 3. Efecto de impacto directo (estallido cruzado sobre el objetivo golpeado)
			if (destelloImpacto)
			{
				using (Pen lapizImpacto = new Pen(Color.Yellow, 3f))
				using (SolidBrush destello = new SolidBrush(Color.FromArgb(140, 255, 80, 40)))
				{
					G.FillEllipse(destello, cx - 32, cy - 32, 64, 64);
					G.DrawLine(lapizImpacto, cx - 36, cy - 36, cx + 36, cy + 36);
					G.DrawLine(lapizImpacto, cx + 36, cy - 36, cx - 36, cy + 36);
					G.DrawLine(lapizImpacto, cx - 44, cy, cx + 44, cy);
				}
			}
		}

		private void DibujarEfectosProyectilEImpacto(Graphics G, Point origenPj, Point origenCpu)
		{
			if (animacionActual != TipoAnimacionCombate.AtaqueJugador &&
			    animacionActual != TipoAnimacionCombate.EspecialJugador &&
			    animacionActual != TipoAnimacionCombate.AtaqueCpu)
			{
				return;
			}

			if (frameAnimacion < 3 || frameAnimacion > 14)
				return;

			bool desdeJugador = (animacionActual == TipoAnimacionCombate.AtaqueJugador || animacionActual == TipoAnimacionCombate.EspecialJugador);
			bool esEspecial = (animacionActual == TipoAnimacionCombate.EspecialJugador);

			Personaje atacante = desdeJugador ? motorCombate.ActivoJugador : motorCombate.ActivoCpu;
			Color colorRayo = atacante != null ? ObtenerColorElemento(atacante.Faccion) : Color.Gold;

			Point pInicio = desdeJugador ? origenPj : origenCpu;
			Point pFin = desdeJugador ? origenCpu : origenPj;

			float progreso = Math.Min(1f, Math.Max(0f, (frameAnimacion - 3) / 9f));
			int px = (int)(pInicio.X + (pFin.X - pInicio.X) * progreso);
			int py = (int)(pInicio.Y + (pFin.Y - pInicio.Y) * progreso);

			// Estela energética
			using (Pen lapizEstela = new Pen(Color.FromArgb(180, colorRayo), esEspecial ? 10f : 5f))
			{
				float progCola = Math.Max(0f, progreso - 0.25f);
				int colaX = (int)(pInicio.X + (pFin.X - pInicio.X) * progCola);
				int colaY = (int)(pInicio.Y + (pFin.Y - pInicio.Y) * progCola);
				G.DrawLine(lapizEstela, colaX, colaY, px, py);
			}

			// Núcleo del proyectil
			int radio = esEspecial ? 18 : 12;
			using (SolidBrush brExterior = new SolidBrush(colorRayo))
			using (SolidBrush brNucleo = new SolidBrush(Color.Yellow))
			{
				G.FillEllipse(brExterior, px - radio, py - radio, radio * 2, radio * 2);
				G.FillEllipse(brNucleo, px - (radio / 2), py - (radio / 2), radio, radio);
			}
		}

		private void DibujarTextoFlotanteCombate(Graphics G, string texto, Color color, int cx, int cy)
		{
			using (Font fuente = new Font("Consolas", 13F, FontStyle.Bold))
			{
				SizeF tam = G.MeasureString(texto, fuente);
				float x = cx - (tam.Width / 2f);
				float y = cy - (tam.Height / 2f);

				using (SolidBrush sombra = new SolidBrush(Color.FromArgb(210, 0, 0, 0)))
				using (SolidBrush brocha = new SolidBrush(color))
				{
					G.DrawString(texto, fuente, sombra, x + 2, y + 2);
					G.DrawString(texto, fuente, brocha, x, y);
				}
			}
		}

		private void DibujarHudTacticoFlotante(
			Graphics G,
			Rectangle R,
			Personaje p,
			Personaje rival,
			Escenario esc,
			bool esCpu)
		{
			Color colorElem = ObtenerColorElemento(p.Faccion);

			// 1. Sombra y fondo translúcido del HUD
			using (SolidBrush sombra = new SolidBrush(Color.FromArgb(140, 0, 0, 0)))
			{
				G.FillRectangle(sombra, R.X + 4, R.Y + 4, R.Width, R.Height);
			}

			using (LinearGradientBrush fondoHud = new LinearGradientBrush(
				R,
				Color.FromArgb(235, 16, 26, 44),
				Color.FromArgb(235, 10, 18, 30),
				LinearGradientMode.Vertical))
			{
				G.FillRectangle(fondoHud, R);
			}

			using (SolidBrush franjaElem = new SolidBrush(colorElem))
			{
				G.FillRectangle(franjaElem, R.X, R.Y, 6, R.Height);
			}

			// 2. Nombre del Leviatán + Pastilla de Elemento (sin elementos en el medio que lo tapen)
			Rectangle rectElem = new Rectangle(R.Right - 78, R.Y + 7, 68, 19);
			int anchoMaxNombre = rectElem.Left - (R.X + 18);

			StringFormat fmtNombre = new StringFormat
			{
				Trimming = StringTrimming.EllipsisCharacter,
				FormatFlags = StringFormatFlags.NoWrap
			};

			using (Font fuenteNombre = new Font("Consolas", 10.5F, FontStyle.Bold))
			using (SolidBrush brNombre = new SolidBrush(esCpu ? Color.FromArgb(255, 215, 100) : Color.Cyan))
			{
				G.DrawString(p.Nombre, fuenteNombre, brNombre, new RectangleF(R.X + 14, R.Y + 8, anchoMaxNombre, 20), fmtNombre);
			}

			using (SolidBrush brElem = new SolidBrush(colorElem))
			using (Font fuenteElem = new Font("Consolas", 8F, FontStyle.Bold))
			{
				G.FillRectangle(brElem, rectElem);
				StringFormat centro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
				G.DrawString(MotorPartida.FormatearElemento(p.Faccion, this.esIngles).ToUpper(), fuenteElem, Brushes.White, rectElem, centro);
			}

			// 3. Barra de VIDA (HP)
			float ratioHp = p.VidaMaxima > 0 ? (float)p.VidaActual / p.VidaMaxima : 0f;
			ratioHp = Math.Min(1f, Math.Max(0f, ratioHp));
			Color colorHp = ratioHp > 0.5f
				? Color.FromArgb(46, 204, 90)
				: (ratioHp > 0.25f ? Color.FromArgb(245, 190, 40) : Color.FromArgb(225, 55, 45));

			Rectangle rectBarraHp = new Rectangle(R.X + 14, R.Y + 32, R.Width - 26, 15);
			using (SolidBrush brFondoHp = new SolidBrush(Color.FromArgb(10, 14, 22)))
			using (SolidBrush brLlenoHp = new SolidBrush(colorHp))
			using (Pen bordeBarra = new Pen(Color.FromArgb(90, 255, 255, 255), 1))
			{
				G.FillRectangle(brFondoHp, rectBarraHp);
				int anchoHp = (int)(rectBarraHp.Width * ratioHp);
				if (anchoHp > 0)
				{
					G.FillRectangle(brLlenoHp, rectBarraHp.X, rectBarraHp.Y, anchoHp, rectBarraHp.Height);
				}
				G.DrawRectangle(bordeBarra, rectBarraHp);
			}

			using (Font fuenteHp = new Font("Consolas", 8F, FontStyle.Bold))
			{
				string txtHp = string.Format("HP: {0}/{1}", p.VidaActual, p.VidaMaxima);
				G.DrawString(txtHp, fuenteHp, Brushes.White, rectBarraHp.X + 6, rectBarraHp.Y + 1);
			}

			// 4. Barra de ESCUDO + Celdas de ENERGÍA (EN)
			Rectangle rectBarraEscudo = new Rectangle(R.X + 14, R.Y + 53, 175, 13);
			float ratioEsc = Math.Min(1f, Math.Max(0f, p.EscudoActual / 40f));
			using (SolidBrush brFondoEsc = new SolidBrush(Color.FromArgb(10, 14, 22)))
			using (SolidBrush brLlenoEsc = new SolidBrush(Color.FromArgb(45, 165, 245)))
			using (Pen bordeEsc = new Pen(Color.FromArgb(90, 120, 210, 255), 1))
			{
				G.FillRectangle(brFondoEsc, rectBarraEscudo);
				int anchoEsc = (int)(rectBarraEscudo.Width * ratioEsc);
				if (anchoEsc > 0)
				{
					G.FillRectangle(brLlenoEsc, rectBarraEscudo.X, rectBarraEscudo.Y, anchoEsc, rectBarraEscudo.Height);
				}
				G.DrawRectangle(bordeEsc, rectBarraEscudo);
			}

			using (Font fuenteEsc = new Font("Consolas", 7.5F, FontStyle.Bold))
			{
				string etiEsc = this.esIngles ? "SHIELD: " : "ESCUDO: ";
				G.DrawString(etiEsc + p.EscudoActual, fuenteEsc, Brushes.White, rectBarraEscudo.X + 5, rectBarraEscudo.Y);
			}

			int enX = rectBarraEscudo.Right + 14;
			int enY = R.Y + 52;
			using (Font fuenteEn = new Font("Consolas", 8F, FontStyle.Bold))
			using (SolidBrush brEnTxt = new SolidBrush(Color.FromArgb(245, 203, 66)))
			{
				G.DrawString("EN:", fuenteEn, brEnTxt, enX, enY);
			}

			int maxOrbesMostrar = Math.Max(4, p.EnergiaActual);
			for (int i = 0; i < Math.Min(6, maxOrbesMostrar); i++)
			{
				bool activo = (i < p.EnergiaActual);
				Rectangle rectOrbe = new Rectangle(enX + 28 + (i * 18), enY + 1, 13, 13);
				using (SolidBrush brOrbe = new SolidBrush(activo ? Color.Gold : Color.FromArgb(28, 38, 54)))
				using (Pen penOrbe = new Pen(activo ? Color.White : Color.FromArgb(80, 120, 150), 1))
				{
					G.FillEllipse(brOrbe, rectOrbe);
					G.DrawEllipse(penOrbe, rectOrbe);
				}
			}

			// 5. Fila inferior del HUD: Medidor Especial + Bolitas de Equipo Vivo + Bono de Arena a la derecha
			int yFilaBolitas = esCpu ? (R.Y + 76) : (R.Y + 94);

			if (!esCpu)
			{
				Rectangle rectEsp = new Rectangle(R.X + 14, R.Y + 73, R.Width - 26, 12);
				float ratioEsp = Math.Min(1f, Math.Max(0f, p.CargaEspecial / 100f));
				using (SolidBrush brFondoEsp = new SolidBrush(Color.FromArgb(10, 14, 22)))
				using (SolidBrush brLlenoEsp = new SolidBrush(p.EspecialListo ? Color.Gold : Color.FromArgb(215, 135, 30)))
				using (Pen penEsp = new Pen(Color.FromArgb(100, 245, 203, 66), 1))
				{
					G.FillRectangle(brFondoEsp, rectEsp);
					int anchoEsp = (int)(rectEsp.Width * ratioEsp);
					if (anchoEsp > 0)
					{
						G.FillRectangle(brLlenoEsp, rectEsp.X, rectEsp.Y, anchoEsp, rectEsp.Height);
					}
					G.DrawRectangle(penEsp, rectEsp);
				}

				using (Font fEsp = new Font("Consolas", 7.5F, FontStyle.Bold))
				{
					string txtEsp = string.Format("★ {0} ({1}%)", p.NombreEspecial, p.CargaEspecial);
					G.DrawString(txtEsp, fEsp, Brushes.White, rectEsp.X + 5, rectEsp.Y - 1);
				}

				int totalPj = motorCombate.JugadorHumano.MazoSeleccionado.Count;
				int bajasPj = motorCombate.DerrotadosJugador.Count;
				string bolitasPj = string.Format("{0}  ({1}: {2}/{3})",
					GenerarIconosEquipo(motorCombate.TotalVivosJugador, totalPj),
					this.esIngles ? "Fainted" : "Bajas",
					bajasPj,
					totalPj);

				using (Font fBol = new Font("Consolas", 8F, FontStyle.Bold))
				using (SolidBrush brBol = new SolidBrush(Color.LightGreen))
				{
					G.DrawString(bolitasPj, fBol, brBol, R.X + 14, yFilaBolitas);
				}
			}
			else
			{
				int totalCpu = motorCombate.JugadorCpu.MazoSeleccionado.Count;
				int bajasCpu = motorCombate.DerrotadosCpu.Count;
				string bolitasCpu = string.Format("{0}: {1}  ({2}: {3}/{4})",
					this.esIngles ? "CPU Team" : "Equipo CPU",
					GenerarIconosEquipo(motorCombate.TotalVivosCpu, totalCpu),
					this.esIngles ? "Defeated" : "Bajas",
					bajasCpu,
					totalCpu);

				using (Font fBol = new Font("Consolas", 8F, FontStyle.Bold))
				using (SolidBrush brBol = new SolidBrush(Color.Salmon))
				{
					G.DrawString(bolitasCpu, fBol, brBol, R.X + 14, yFilaBolitas);
				}
			}

			// Insignia de Bono/Penalización de la Arena abajo a la derecha (sin pisar ningún texto)
			if (esc != null)
			{
				string descMayus = (esc.Nombre + " " + esc.Descripcion).ToUpperInvariant();
				string facEsp = p.Faccion.ToString().ToUpperInvariant();
				bool potenciado = descMayus.Contains("POTENCIA A " + facEsp) || descMayus.Contains(facEsp + " +");
				bool debilitado = descMayus.Contains("EROSIONA A " + facEsp) || descMayus.Contains("SOFOCA A " + facEsp) || descMayus.Contains("DEBILITA A " + facEsp) || descMayus.Contains(facEsp + " -");

				if (potenciado || debilitado)
				{
					string txtBono = potenciado ? "▲ +6 ARENA" : "▼ -4 ARENA";
					Color colBono = potenciado ? Color.LightGreen : Color.Salmon;
					using (Font fBono = new Font("Consolas", 7.5F, FontStyle.Bold))
					using (SolidBrush bBono = new SolidBrush(colBono))
					{
						G.DrawString(txtBono, fBono, bBono, R.Right - 96, yFilaBolitas + 1);
					}
				}
			}

			// Marco dorado exterior del HUD
			using (Pen bordeOro = new Pen(Color.FromArgb(215, 245, 203, 66), 1.5f))
			{
				G.DrawRectangle(bordeOro, R.X, R.Y, R.Width - 1, R.Height - 1);
			}
		}

		private void DibujarBannerSutilRotacion(Graphics G, int anchoArena, string texto, Elemento bioma)
		{
			int w = 520;
			int h = 28;
			Rectangle rect = new Rectangle((anchoArena - w) / 2, 10, w, h);

			Color colorElem = ObtenerColorElemento(bioma);
			using (SolidBrush brFondo = new SolidBrush(Color.FromArgb(225, 12, 18, 30)))
			using (Pen brBorde = new Pen(Color.FromArgb(245, 203, 66), 1.5f))
			using (SolidBrush brBarra = new SolidBrush(colorElem))
			{
				G.FillRectangle(brFondo, rect);
				G.FillRectangle(brBarra, rect.X, rect.Y, 6, rect.Height);
				G.FillRectangle(brBarra, rect.Right - 6, rect.Y, 6, rect.Height);
				G.DrawRectangle(brBorde, rect);
			}

			StringFormat centro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
			using (Font f = new Font("Consolas", 8.5F, FontStyle.Bold))
			using (SolidBrush brTxt = new SolidBrush(Color.Yellow))
			{
				G.DrawString(texto, f, brTxt, rect, centro);
			}
		}

		private void DibujarBarraInferiorIntegrada(
			Graphics G,
			Rectangle R,
			Escenario esc,
			Personaje pj,
			Personaje cpu)
		{
			using (SolidBrush brFondo = new SolidBrush(Color.FromArgb(12, 18, 30)))
			using (Pen lineaOro = new Pen(Color.FromArgb(245, 203, 66), 1.5f))
			{
				G.FillRectangle(brFondo, R);
				G.DrawLine(lineaOro, R.X, R.Y, R.Right, R.Y);
			}

			int anchoBadgeDerecho = 248;
			Rectangle rectBadge = new Rectangle(R.Right - anchoBadgeDerecho - 8, R.Y + 5, anchoBadgeDerecho, 26);

			// Izquierda: Información del Escenario encuadrada antes del badge derecho para que nunca se tapen
			string descLimpia = esc != null ? esc.Descripcion.TrimEnd('.') : "";
			string textoEscenario = esc != null
				? string.Format("🌍 {0}: {1}  |  ⚡ Sismo: {2} pts",
					esc.Nombre.ToUpper(), descLimpia, esc.UmbralEmpatia)
				: "🌍 ARENA ELEMENTAL";

			int anchoDisponibleIzq = rectBadge.Left - (R.X + 16);
			RectangleF rectTextoIzq = new RectangleF(R.X + 10, R.Y + 2, anchoDisponibleIzq, R.Height - 4);

			StringFormat formatoIzq = new StringFormat
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center,
				Trimming = StringTrimming.EllipsisCharacter,
				FormatFlags = StringFormatFlags.NoWrap
			};

			using (Font fEsc = new Font("Consolas", 8F, FontStyle.Bold))
			using (SolidBrush brEsc = new SolidBrush(Color.Aquamarine))
			{
				G.DrawString(textoEscenario, fEsc, brEsc, rectTextoIzq, formatoIzq);
			}

			// Derecha: Indicador compacto de Ventaja / Desventaja Elemental (x1.5 / x0.75 / x1.0)
			if (pj != null && cpu != null)
			{
				double mult = pj.ObtenerMultiplicadorElemental(cpu.Faccion);
				string elemPj = MotorPartida.FormatearElemento(pj.Faccion, this.esIngles).ToUpper();
				string elemCpu = MotorPartida.FormatearElemento(cpu.Faccion, this.esIngles).ToUpper();

				string txtVentaja;
				Color colorFondoBadge;
				Color colorTextoBadge;

				if (mult > 1.0)
				{
					colorFondoBadge = Color.FromArgb(24, 95, 52);
					colorTextoBadge = Color.LightGreen;
					txtVentaja = this.esIngles
						? string.Format("★ ADVANTAGE: {0}>{1} (x1.5)", elemPj, elemCpu)
						: string.Format("★ VENTAJA: {0}>{1} (x1.5)", elemPj, elemCpu);
				}
				else if (mult < 1.0)
				{
					colorFondoBadge = Color.FromArgb(125, 34, 26);
					colorTextoBadge = Color.FromArgb(255, 210, 130);
					txtVentaja = this.esIngles
						? string.Format("⚠ DISADVANTAGE: {0}>{1}", elemCpu, elemPj)
						: string.Format("⚠ DESVENTAJA: {0}>{1} (x0.75)", elemCpu, elemPj);
				}
				else
				{
					colorFondoBadge = Color.FromArgb(32, 50, 78);
					colorTextoBadge = Color.WhiteSmoke;
					txtVentaja = this.esIngles
						? string.Format("⚖ NEUTRAL: {0} vs {1} (x1.0)", elemPj, elemCpu)
						: string.Format("⚖ NEUTRAL: {0} vs {1} (x1.0)", elemPj, elemCpu);
				}

				using (SolidBrush brBadge = new SolidBrush(colorFondoBadge))
				using (Pen penBadge = new Pen(colorTextoBadge, 1))
				using (Font fBadge = new Font("Consolas", 8F, FontStyle.Bold))
				using (SolidBrush brTxt = new SolidBrush(colorTextoBadge))
				{
					G.FillRectangle(brBadge, rectBadge);
					G.DrawRectangle(penBadge, rectBadge);
					StringFormat centro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
					G.DrawString(txtVentaja, fBadge, brTxt, rectBadge, centro);
				}
			}
		}

		// =====================================================================
		// 4. TARJETAS MODERNAS EN BANCA Y EQUIPO CPU (PANEL LATERAL DERECHO)
		// =====================================================================
		private void LstColaJugadorDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0 || e.Index >= lstColaJugador.Items.Count)
				return;

			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			bool sel = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
			Rectangle R = e.Bounds;
			string texto = lstColaJugador.Items[e.Index].ToString();
			bool esCaido = texto.Contains("💀") || texto.Contains("(0 HP)");
			bool tieneVentaja = texto.Contains("★");

			Color colFondo = sel
				? Color.FromArgb(34, 62, 92)
				: (esCaido ? Color.FromArgb(26, 18, 22) : Color.FromArgb(16, 26, 40));

			using (SolidBrush br = new SolidBrush(colFondo))
			{
				G.FillRectangle(br, R);
			}

			Color colBarra = esCaido ? Color.Crimson : (tieneVentaja ? Color.LimeGreen : Color.FromArgb(245, 203, 66));
			using (SolidBrush brBarra = new SolidBrush(colBarra))
			{
				G.FillRectangle(brBarra, R.X, R.Y + 2, 4, R.Height - 4);
			}

			using (Font f = new Font("Consolas", 8.5F, FontStyle.Bold))
			using (SolidBrush brTxt = new SolidBrush(esCaido ? Color.RosyBrown : (tieneVentaja ? Color.LightGreen : Color.White)))
			{
				G.DrawString(texto, f, brTxt, R.X + 10, R.Y + 10);
			}

			if (sel)
			{
				using (Pen pSel = new Pen(Color.FromArgb(245, 203, 66), 1.5f))
				{
					G.DrawRectangle(pSel, R.X + 1, R.Y + 1, R.Width - 3, R.Height - 3);
				}
			}
		}

		private void LstDerrotadosCpuDrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0 || e.Index >= lstDerrotadosCpu.Items.Count)
				return;

			Graphics G = e.Graphics;
			G.SmoothingMode = SmoothingMode.AntiAlias;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			Rectangle R = e.Bounds;
			string texto = lstDerrotadosCpu.Items[e.Index].ToString();
			bool esActivo = texto.StartsWith("⚔");
			bool esCaido = texto.StartsWith("✖");

			Color colFondo = esActivo
				? Color.FromArgb(46, 26, 30)
				: (esCaido ? Color.FromArgb(20, 14, 18) : Color.FromArgb(28, 20, 26));

			using (SolidBrush br = new SolidBrush(colFondo))
			{
				G.FillRectangle(br, R);
			}

			Color colBarra = esActivo ? Color.Gold : (esCaido ? Color.DimGray : Color.Firebrick);
			using (SolidBrush brBarra = new SolidBrush(colBarra))
			{
				G.FillRectangle(brBarra, R.X, R.Y + 2, 4, R.Height - 4);
			}

			using (Font f = new Font("Consolas", 8.5F, FontStyle.Bold))
			using (SolidBrush brTxt = new SolidBrush(esCaido ? Color.Gray : (esActivo ? Color.Gold : Color.FromArgb(255, 205, 190))))
			{
				G.DrawString(texto, f, brTxt, R.X + 10, R.Y + 10);
			}
		}

		// =====================================================================
		// 5. METODOS DE CONTROL E INTERFAZ DEL COMBATE
		// =====================================================================
		private void BloquearBotonAvance()
		{
			if (this.button1 == null)
				return;

			this.button1.Enabled = false;
			this.button1.BackColor = Color.FromArgb(75, 80, 92);
			this.button1.ForeColor = Color.LightGray;
			this.button1.Text = this.esIngles ? "🔒 Win to Advance" : "🔒 Gana para avanzar";
		}

		private void DesbloquearBotonAvancePorVictoria()
		{
			this.batallaGanada = true;

			if (this.numeroBatalla >= MaximaBatallaDesbloqueada)
			{
				MaximaBatallaDesbloqueada = this.numeroBatalla + 1;
			}

			if (this.button1 != null)
			{
				this.button1.Enabled = true;
				this.button1.BackColor = Color.Gold;
				this.button1.ForeColor = Color.Black;
				if (this.numeroBatalla < 5)
				{
					this.button1.Text = this.esIngles
						? "Continue to Chapter " + (this.numeroBatalla + 1) + " ▶"
						: "Continuar al Capítulo " + (this.numeroBatalla + 1) + " ▶";
				}
				else
				{
					this.button1.Text = "Ver Final (Capítulo 6) ▶";
				}
			}
		}

		private void AplicarIdiomaControles()
		{
			if (this.esIngles)
			{
				lblGuiaElemental.Text = "Elements: Water > Fire > Air > Earth > Water";
				lblHeaderCola.Text = " Your Bench (Click to switch)";
				lblHeaderDerrotadosCpu.Text = " CPU Team (Defeated: 0/3)";
				btnAtacar.Text = "⚔ Attack (1 EN)";
				btnEspecial.Text = "⚡ Special (2 EN)";
				btnDefender.Text = "🛡 Defend (1 EN)";
				btnCambiarLeviatan.Text = "🔄 Switch";
				btnCargarEnergia.Text = "🔋 Charge (+2 EN)";
				btnReiniciarBatalla.Text = "↻ Restart";
			}
			else
			{
				lblGuiaElemental.Text = "Ciclo: Agua > Fuego > Aire > Tierra > Agua";
				lblHeaderCola.Text = " Tu Banca (Clic para cambiar)";
				lblHeaderDerrotadosCpu.Text = " Equipo CPU (Derrotados: 0/3)";
				btnAtacar.Text = "⚔ Atacar (1 EN)";
				btnEspecial.Text = "⚡ Especial (2 EN)";
				btnDefender.Text = "🛡 Defender (1 EN)";
				btnCambiarLeviatan.Text = "🔄 Cambiar";
				btnCargarEnergia.Text = "🔋 Cargar (+2 EN)";
				btnReiniciarBatalla.Text = "↻ Reiniciar";
			}

			btnReiniciarBatalla.BackColor = Color.FromArgb(70, 80, 100);
			btnReiniciarBatalla.ForeColor = Color.White;
		}

		private PerfilJugador PrepararEquipoJugador(bool enIngles)
		{
			if (enIngles)
			{
				string nombreIngles = (SeleccionModalidad.JugadorUnoConfigurado != null && !string.IsNullOrEmpty(SeleccionModalidad.JugadorUnoConfigurado.NombreUsuario))
					? SeleccionModalidad.JugadorUnoConfigurado.NombreUsuario
					: "CaribbeanCaptain";

				PerfilJugador jugadorIngles = new PerfilJugador(1, nombreIngles, 0, 0, 0);
				jugadorIngles.MazoSeleccionado.Add(new CartaAire(10, "Zephyr Serpent", 45, 25, 30, 75, "Slashing Storm", "Double aerial strike."));
				jugadorIngles.MazoSeleccionado.Add(new CartaAgua(4, "Abyssal Nereid", 55, 25, 20, 85, "Healing Tide", "Steals energy and heals."));
				jugadorIngles.MazoSeleccionado.Add(new CartaTierra(7, "Basalt Golem", 60, 30, 10, 90, "Monolithic Wall", "Raises shield and crushes."));
				jugadorIngles.MazoSeleccionado.Add(new CartaFuego(1, "Ignis Leviathan", 45, 20, 35, 40, "Solar Flare", "Pierces enemy shield."));
				jugadorIngles.MazoSeleccionado.Add(new CartaAire(11, "Hurricane Valkyrie", 50, 20, 30, 80, "Rising Gust", "Swift wind slashes."));
				return jugadorIngles;
			}

			if (SeleccionModalidad.JugadorUnoConfigurado != null &&
			    SeleccionModalidad.JugadorUnoConfigurado.MazoSeleccionado != null &&
			    SeleccionModalidad.JugadorUnoConfigurado.MazoSeleccionado.Count > 0)
			{
				return SeleccionModalidad.JugadorUnoConfigurado;
			}

			PerfilJugador jugador = gestorTxt.AutenticarORegistrarJugador("CapitanCaribe");
			List<Personaje> catalogo = gestorTxt.CargarPersonajesDesdeTxt();
			jugador.MazoSeleccionado.Clear();

			AgregarPrimeroDeElemento(jugador.MazoSeleccionado, catalogo, Elemento.Tierra);
			AgregarPrimeroDeElemento(jugador.MazoSeleccionado, catalogo, Elemento.Agua);
			AgregarPrimeroDeElemento(jugador.MazoSeleccionado, catalogo, Elemento.Fuego);
			AgregarPrimeroDeElemento(jugador.MazoSeleccionado, catalogo, Elemento.Aire);

			for (int i = 0; i < catalogo.Count && jugador.MazoSeleccionado.Count < 5; i++)
			{
				if (!ContienePersonaje(jugador.MazoSeleccionado, catalogo[i].Nombre))
				{
					jugador.MazoSeleccionado.Add(catalogo[i].ClonarParaBatalla());
				}
			}

			return jugador;
		}

		private void AgregarPrimeroDeElemento(List<Personaje> destino, List<Personaje> catalogo, Elemento elem)
		{
			for (int i = 0; i < catalogo.Count; i++)
			{
				if (catalogo[i].Faccion == elem && !ContienePersonaje(destino, catalogo[i].Nombre))
				{
					destino.Add(catalogo[i].ClonarParaBatalla());
					return;
				}
			}
		}

		private bool ContienePersonaje(List<Personaje> lista, string nombre)
		{
			for (int i = 0; i < lista.Count; i++)
			{
				if (string.Equals(lista[i].Nombre, nombre, StringComparison.OrdinalIgnoreCase))
					return true;
			}
			return false;
		}

		private void ActualizarInterfazCompleta()
		{
			if (motorCombate == null)
				return;

			VerificarRotacionEscenarioCadaDosTurnos();

			Personaje pj = motorCombate.ActivoJugador;
			Personaje cpu = motorCombate.ActivoCpu;
			int totalEquipoCpu = motorCombate.JugadorCpu.MazoSeleccionado.Count;
			int derrotadosCpuCant = motorCombate.DerrotadosCpu.Count;

			if (motorCombate.PartidaTerminada)
			{
				bool ganoJugador = (motorCombate.Ganador == motorCombate.JugadorHumano);
				if (ganoJugador)
				{
					lblTurnoActual.Text = this.esIngles ? "🏆 VICTORY!" : "🏆 ¡VICTORIA!";
					lblTurnoActual.BackColor = Color.FromArgb(32, 150, 75);
					DesbloquearBotonAvancePorVictoria();
				}
				else
				{
					lblTurnoActual.Text = this.esIngles ? "💀 DEFEAT" : "💀 DERROTA";
					lblTurnoActual.BackColor = Color.FromArgb(180, 40, 40);
					BloquearBotonAvance();
					btnReiniciarBatalla.BackColor = Color.Gold;
					btnReiniciarBatalla.ForeColor = Color.Black;
				}
			}
			else if (motorCombate.EsperandoRelevoJugadorCaido)
			{
				lblTurnoActual.Text = this.esIngles ? "💀 Ally Down..." : "💀 Baja aliada...";
				lblTurnoActual.BackColor = Color.FromArgb(175, 35, 35);
			}
			else if (motorCombate.EsperandoRelevoCpuCaido)
			{
				lblTurnoActual.Text = this.esIngles ? "🎯 CPU Down..." : "🎯 ¡CPU cayó!...";
				lblTurnoActual.BackColor = Color.FromArgb(40, 120, 180);
			}
			else if (motorCombate.EsTurnoJugador)
			{
				lblTurnoActual.Text = this.esIngles
					? string.Format("Turn {0} • Your Turn", motorCombate.TurnoActual)
					: string.Format("Turno {0} • Tu turno", motorCombate.TurnoActual);
				lblTurnoActual.BackColor = Color.FromArgb(30, 130, 90);
			}
			else
			{
				lblTurnoActual.Text = this.esIngles
					? string.Format("Turn {0} • CPU Turn", motorCombate.TurnoActual)
					: string.Format("Turno {0} • Turno CPU", motorCombate.TurnoActual);
				lblTurnoActual.BackColor = Color.FromArgb(204, 85, 25);
			}

			lblHeaderCola.Text = this.esIngles
				? string.Format(" Your Bench ({0} available)", motorCombate.ColaJugador.Count)
				: string.Format(" Tu Banca ({0} disponibles)", motorCombate.ColaJugador.Count);

			actualizandoListaBanca = true;
			lstColaJugador.Items.Clear();
			Personaje[] enBanca = motorCombate.ColaJugador.ToArray();
			int indiceRecomendado = 0;
			bool encontroVentaja = false;

			for (int i = 0; i < enBanca.Length; i++)
			{
				Personaje suplente = enBanca[i];
				double multSuplente = (cpu != null) ? suplente.ObtenerMultiplicadorElemental(cpu.Faccion) : 1.0;

				if (!encontroVentaja && multSuplente > 1.0)
				{
					indiceRecomendado = i;
					encontroVentaja = true;
				}

				string marca = multSuplente > 1.0 ? (this.esIngles ? " ★ Advantage" : " ★ Ventaja") : "";
				lstColaJugador.Items.Add(string.Format("{0} [{1}] ({2}/{3} HP){4}",
					suplente.Nombre,
					MotorPartida.FormatearElemento(suplente.Faccion, this.esIngles),
					suplente.VidaActual,
					suplente.VidaMaxima,
					marca));
			}

			for (int d = 0; d < motorCombate.DerrotadosJugador.Count; d++)
			{
				Personaje caidoPj = motorCombate.DerrotadosJugador[d];
				lstColaJugador.Items.Add(string.Format("💀 {0} (0 HP)", caidoPj.Nombre));
			}

			if (enBanca.Length > 0)
			{
				lstColaJugador.SelectedIndex = indiceRecomendado;
				ActualizarTextoBotonCambio(enBanca[indiceRecomendado]);
			}
			else
			{
				btnCambiarLeviatan.Text = this.esIngles ? "🔄 Switch (Empty)" : "🔄 Sin suplentes";
			}
			actualizandoListaBanca = false;

			ActualizarListaEstadoCpu(totalEquipoCpu, derrotadosCpuCant);

			bool enPausaCaida = motorCombate.EsperandoRelevoJugadorCaido || motorCombate.EsperandoRelevoCpuCaido;
			bool habilitarComandos = !motorCombate.PartidaTerminada && !enPausaCaida && motorCombate.EsTurnoJugador && pj != null && pj.EstaVivo;
			btnAtacar.Enabled = habilitarComandos && (pj.EnergiaActual >= Personaje.CostoAtaque);
			btnDefender.Enabled = habilitarComandos && (pj.EnergiaActual >= Personaje.CostoDefensa);
			btnEspecial.Enabled = habilitarComandos && pj.EspecialListo;
			btnCambiarLeviatan.Enabled = habilitarComandos && (motorCombate.ColaJugador.Count > 0);
			btnCargarEnergia.Enabled = habilitarComandos;

			if (motorCombate.PartidaTerminada && !partidaGuardadaEnTxt)
			{
				partidaGuardadaEnTxt = true;
				try
				{
					gestorTxt.RegistrarPartidaFinalizadaEnTxt(
						motorCombate.JugadorHumano,
						motorCombate.JugadorCpu,
						motorCombate.Ganador,
						motorCombate.EscenarioActual,
						motorCombate.TurnoActual,
						motorCombate.PilaEventos);
				}
				catch
				{
				}
			}

			if (lienzoArena != null)
			{
				lienzoArena.Invalidate();
			}
		}

		private void ActualizarListaEstadoCpu(int totalEquipoCpu, int derrotadosCpuCant)
		{
			lblHeaderDerrotadosCpu.Text = this.esIngles
				? string.Format(" CPU Team (Defeated: {0}/{1})", derrotadosCpuCant, totalEquipoCpu)
				: string.Format(" Equipo CPU (Derrotados: {0}/{1})", derrotadosCpuCant, totalEquipoCpu);

			lstDerrotadosCpu.Items.Clear();

			if (motorCombate.ActivoCpu != null && motorCombate.ActivoCpu.EstaVivo)
			{
				Personaje act = motorCombate.ActivoCpu;
				string elem = MotorPartida.FormatearElemento(act.Faccion, this.esIngles);
				lstDerrotadosCpu.Items.Add(string.Format("⚔ {0} [{1}] ({2}/{3} HP)",
					act.Nombre, elem, act.VidaActual, act.VidaMaxima));
			}

			Personaje[] colaCpuArr = motorCombate.ColaCpu.ToArray();
			for (int i = 0; i < colaCpuArr.Length; i++)
			{
				Personaje esp = colaCpuArr[i];
				string elem = MotorPartida.FormatearElemento(esp.Faccion, this.esIngles);
				lstDerrotadosCpu.Items.Add(string.Format("⏳ {0} [{1}] ({2}/{3} HP)",
					esp.Nombre, elem, esp.VidaActual, esp.VidaMaxima));
			}

			for (int i = 0; i < motorCombate.DerrotadosCpu.Count; i++)
			{
				Personaje caido = motorCombate.DerrotadosCpu[i];
				lstDerrotadosCpu.Items.Add(string.Format("✖ {0} (0 HP)", caido.Nombre));
			}
		}

		private string GenerarIconosEquipo(int vivos, int total)
		{
			string texto = "";
			for (int i = 0; i < total; i++)
			{
				texto += (i < vivos) ? "● " : "○ ";
			}
			return texto.TrimEnd();
		}

		// =====================================================================
		// 6. ACCIONES DE COMBATE CON DISPARO DE ANIMACIONES Y DAÑO FLOTANTE
		// =====================================================================
		private void BtnAtacarClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			int hpAntes = motorCombate.ActivoCpu != null ? motorCombate.ActivoCpu.VidaActual : 0;
			int escAntes = motorCombate.ActivoCpu != null ? motorCombate.ActivoCpu.EscudoActual : 0;

			lblDialogoCombate.Text = motorCombate.EjecutarAtaqueJugador();

			int hpDespues = motorCombate.ActivoCpu != null ? motorCombate.ActivoCpu.VidaActual : 0;
			int escDespues = motorCombate.ActivoCpu != null ? motorCombate.ActivoCpu.EscudoActual : 0;
			int danoTotal = Math.Max(0, (hpAntes - hpDespues) + (escAntes - escDespues));

			IniciarAnimacionCombate(
				TipoAnimacionCombate.AtaqueJugador,
				danoTotal > 0 ? string.Format("-{0} HP", danoTotal) : "¡IMPACTO!",
				Color.FromArgb(255, 95, 80),
				true);

			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnEspecialClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			int hpAntes = motorCombate.ActivoCpu != null ? motorCombate.ActivoCpu.VidaActual : 0;
			int escAntes = motorCombate.ActivoCpu != null ? motorCombate.ActivoCpu.EscudoActual : 0;

			lblDialogoCombate.Text = motorCombate.EjecutarEspecialJugador();

			int hpDespues = motorCombate.ActivoCpu != null ? motorCombate.ActivoCpu.VidaActual : 0;
			int escDespues = motorCombate.ActivoCpu != null ? motorCombate.ActivoCpu.EscudoActual : 0;
			int danoTotal = Math.Max(0, (hpAntes - hpDespues) + (escAntes - escDespues));

			IniciarAnimacionCombate(
				TipoAnimacionCombate.EspecialJugador,
				danoTotal > 0 ? string.Format("★ -{0} HP!", danoTotal) : "★ ¡ARTE ABISAL!",
				Color.Gold,
				true);

			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnDefenderClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			lblDialogoCombate.Text = motorCombate.EjecutarDefensaJugador();
			IniciarAnimacionCombate(TipoAnimacionCombate.DefensaJugador, "🛡 +ESCUDO", Color.Cyan, false);
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void ActualizarTextoBotonCambio(Personaje suplenteSeleccionado)
		{
			if (suplenteSeleccionado == null)
				return;

			btnCambiarLeviatan.Text = this.esIngles
				? string.Format("🔄 Switch: {0}", suplenteSeleccionado.Nombre)
				: string.Format("🔄 Cambiar: {0}", suplenteSeleccionado.Nombre);
		}

		private void LstColaJugadorSelectedIndexChanged(object sender, EventArgs e)
		{
			if (actualizandoListaBanca || motorCombate == null)
				return;

			Personaje[] enBanca = motorCombate.ColaJugador.ToArray();
			int idx = lstColaJugador.SelectedIndex;
			if (idx >= 0 && idx < enBanca.Length)
			{
				Personaje elegido = enBanca[idx];
				ActualizarTextoBotonCambio(elegido);
			}
			lstColaJugador.Invalidate();
		}

		private void LstColaJugadorDoubleClick(object sender, EventArgs e)
		{
			BtnCambiarLeviatanClick(sender, e);
		}

		private void BtnCambiarLeviatanClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			Personaje[] enBanca = motorCombate.ColaJugador.ToArray();
			int indiceSeleccionado = lstColaJugador.SelectedIndex;
			if (indiceSeleccionado >= enBanca.Length)
			{
				indiceSeleccionado = -1;
			}

			lblDialogoCombate.Text = motorCombate.EjecutarCambioPersonajeJugador(indiceSeleccionado);
			IniciarAnimacionCombate(TipoAnimacionCombate.DefensaJugador, "🔄 ¡RELEVO!", Color.Aquamarine, false);
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnCargarEnergiaClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			lblDialogoCombate.Text = motorCombate.EjecutarCargaEnergiaJugador();
			IniciarAnimacionCombate(TipoAnimacionCombate.CargaJugador, "⚡ +2 ENERGIA", Color.Gold, false);
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnReiniciarBatallaClick(object sender, EventArgs e)
		{
			tmrTurnoCpu.Stop();
			tmrPausaDerrota.Stop();
			ConfigurarBatalla();
		}

		private void ContinuarFlujoConPausas()
		{
			if (motorCombate == null || motorCombate.PartidaTerminada)
				return;

			if (motorCombate.EsperandoRelevoCpuCaido || motorCombate.EsperandoRelevoJugadorCaido)
			{
				tmrTurnoCpu.Stop();
				tmrPausaDerrota.Start();
			}
			else if (!motorCombate.EsTurnoJugador)
			{
				tmrTurnoCpu.Start();
			}
		}

		private void TmrTurnoCpuTick(object sender, EventArgs e)
		{
			tmrTurnoCpu.Stop();
			if (motorCombate == null || motorCombate.PartidaTerminada)
				return;

			int hpPjAntes = motorCombate.ActivoJugador != null ? motorCombate.ActivoJugador.VidaActual : 0;
			int escPjAntes = motorCombate.ActivoJugador != null ? motorCombate.ActivoJugador.EscudoActual : 0;

			lblDialogoCombate.Text = motorCombate.EjecutarTurnoCpu();

			int hpPjDespues = motorCombate.ActivoJugador != null ? motorCombate.ActivoJugador.VidaActual : 0;
			int escPjDespues = motorCombate.ActivoJugador != null ? motorCombate.ActivoJugador.EscudoActual : 0;
			int danoAlJugador = Math.Max(0, (hpPjAntes - hpPjDespues) + (escPjAntes - escPjDespues));

			if (danoAlJugador > 0)
			{
				IniciarAnimacionCombate(
					TipoAnimacionCombate.AtaqueCpu,
					string.Format("-{0} HP", danoAlJugador),
					Color.FromArgb(255, 90, 75),
					false);
			}
			else
			{
				IniciarAnimacionCombate(
					TipoAnimacionCombate.DefensaOCargaCpu,
					"⚡ CPU",
					Color.Khaki,
					true);
			}

			ActualizarInterfazCompleta();

			if (motorCombate.EsperandoRelevoJugadorCaido)
			{
				tmrPausaDerrota.Start();
			}
		}

		private void TmrPausaDerrotaTick(object sender, EventArgs e)
		{
			tmrPausaDerrota.Stop();
			if (motorCombate == null || motorCombate.PartidaTerminada)
				return;

			if (motorCombate.EsperandoRelevoJugadorCaido)
			{
				lblDialogoCombate.Text = motorCombate.ProcesarEntradaSiguienteJugadorTrasDerrota();
				ActualizarInterfazCompleta();
			}
			else if (motorCombate.EsperandoRelevoCpuCaido)
			{
				lblDialogoCombate.Text = motorCombate.ProcesarEntradaSiguienteCpuTrasDerrota();
				ActualizarInterfazCompleta();
			}
		}

		// Mantenemos los manejadores del Designer vacíos por compatibilidad
		private void PnlArenaCombatePaint(object sender, PaintEventArgs e) { }
		private void PnlSpriteCpuPaint(object sender, PaintEventArgs e) { }
		private void PnlSpriteJugadorPaint(object sender, PaintEventArgs e) { }

		private static Color ObtenerColorElemento(Elemento faccion)
		{
			switch (faccion)
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

		private void MostrarModalBatallaEnCurso()
		{
			using (Form Modal = new Form())
			{
				Modal.FormBorderStyle = FormBorderStyle.None;
				Modal.StartPosition = FormStartPosition.CenterParent;
				Modal.ClientSize = new Size(460, 230);
				Modal.BackColor = Color.FromArgb(245, 203, 66);
				Modal.ShowInTaskbar = false;

				Panel PnlInterior = new Panel();
				PnlInterior.SetBounds(3, 3, 454, 224);
				PnlInterior.BackColor = Color.FromArgb(12, 20, 34);
				Modal.Controls.Add(PnlInterior);

				PnlInterior.Paint += (s, e) =>
				{
					Graphics G = e.Graphics;
					G.SmoothingMode = SmoothingMode.AntiAlias;
					G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
					Rectangle R = PnlInterior.ClientRectangle;

					using (LinearGradientBrush Fondo = new LinearGradientBrush(
						R,
						Color.FromArgb(14, 22, 38),
						Color.FromArgb(24, 38, 62),
						LinearGradientMode.Vertical))
					{
						G.FillRectangle(Fondo, R);
					}

					Rectangle RectHeader = new Rectangle(0, 0, R.Width, 34);
					using (SolidBrush BrochaHeader = new SolidBrush(Color.FromArgb(204, 41, 18)))
					using (Pen LineaOro = new Pen(Color.FromArgb(245, 203, 66), 2))
					{
						G.FillRectangle(BrochaHeader, RectHeader);
						G.DrawLine(LineaOro, 0, 34, R.Width, 34);
					}

					StringFormat Centro = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					};

					string titulo = this.esIngles ? "🔒 BATTLE IN PROGRESS 🔒" : "🔒 COMBATE EN CURSO 🔒";
					using (Font FuenteHeader = new Font("Consolas", 10.5F, FontStyle.Bold))
					{
						G.DrawString(titulo, FuenteHeader, Brushes.Yellow, RectHeader, Centro);
					}

					string msg = this.esIngles
						? "You must defeat all enemy Leviathans before advancing to the next chapter!"
						: "¡Debes derrotar a todos los Leviatanes enemigos en la arena antes de avanzar al siguiente capítulo!";

					using (Font FuenteMsg = new Font("Consolas", 9.5F, FontStyle.Bold))
					using (SolidBrush BrochaBlanca = new SolidBrush(Color.WhiteSmoke))
					{
						G.DrawString(msg, FuenteMsg, BrochaBlanca, new RectangleF(24, 54, R.Width - 48, 90), Centro);
					}
				};

				Button BtnOk = new Button();
				BtnOk.SetBounds(127, 160, 200, 42);
				BtnOk.FlatStyle = FlatStyle.Flat;
				BtnOk.FlatAppearance.BorderColor = Color.FromArgb(245, 203, 66);
				BtnOk.FlatAppearance.BorderSize = 2;
				BtnOk.BackColor = Color.FromArgb(204, 41, 18);
				BtnOk.ForeColor = Color.Yellow;
				BtnOk.Font = new Font("Consolas", 10F, FontStyle.Bold);
				BtnOk.Cursor = Cursors.Hand;
				BtnOk.Text = this.esIngles ? "RETURN TO ARENA" : "VOLVER A LA ARENA";
				BtnOk.DialogResult = DialogResult.OK;

				PnlInterior.Controls.Add(BtnOk);
				Modal.AcceptButton = BtnOk;

				Modal.ShowDialog(this);
			}
		}

		void Button1Click(object sender, EventArgs e)
		{
			if (!this.batallaGanada || motorCombate == null || !motorCombate.PartidaTerminada || motorCombate.Ganador != motorCombate.JugadorHumano)
			{
				MostrarModalBatallaEnCurso();
				return;
			}

			tmrTurnoCpu.Stop();
			tmrPausaDerrota.Stop();
			if (tmrAnimacionArena != null)
			{
				tmrAnimacionArena.Stop();
			}

			if (this.numeroBatalla >= 1 && this.numeroBatalla <= 5)
			{
				Historia ventanaHistoria = new Historia(this.numeroBatalla + 1);
				ventanaHistoria.FormClosed += (s, args) => Application.Exit();
				ventanaHistoria.Show();
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
	}
}