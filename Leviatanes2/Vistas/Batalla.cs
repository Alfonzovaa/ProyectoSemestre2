using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Leviatanes2.Clases;
using Leviatanes2.LogicaBat;

namespace Leviatanes2
{
	/// <summary>
	/// Ventana de combate por turnos estilo Pokémon (exclusivo para Jugador vs CPU)
	/// para el Modo Historia de Leviatanes del Caribe 2.
	/// - Requiere ganar la batalla actual para desbloquear el avance a la siguiente (1 -> 2 -> 3 -> 4 -> 5).
	/// - La Batalla 2 (case 2) está íntegramente en inglés.
	/// - Muestra el conteo y lista de personajes que van perdiendo de la CPU y aplica pausas cuando cae un personaje.
	/// </summary>
	public partial class Batalla : Form
	{
		// Número de batalla actual en el flujo de la historia (1 a 5)
		public int numeroBatalla = 1;

		// Control estático de progreso: obliga a ganar la batalla N para desbloquear la N+1
		public static int MaximaBatallaDesbloqueada = 1;

		private MotorPartida motorCombate;
		private GestorPersistenciaTxt gestorTxt;
		private bool partidaGuardadaEnTxt = false;
		private bool batallaGanada = false;
		private bool esIngles = false;
		private bool actualizandoListaBanca = false;

		public Batalla() : this(1)
		{
		}

		public Batalla(int batalla)
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			this.numeroBatalla = batalla;
			this.gestorTxt = new GestorPersistenciaTxt();

			ConfigurarBatalla();
		}

		/// <summary>
		/// Configura la batalla actual mediante un switch-case según el número de batalla (case 1 al 5)
		/// únicamente para la modalidad Jugador vs CPU.
		/// </summary>
		private void ConfigurarBatalla()
		{
			tmrTurnoCpu.Stop();
			tmrPausaDerrota.Stop();

			this.StartPosition = FormStartPosition.CenterScreen;

			// Asegurar que no se pueda saltar a una batalla superior sin haber ganado la anterior
			if (this.numeroBatalla > MaximaBatallaDesbloqueada)
			{
				this.numeroBatalla = MaximaBatallaDesbloqueada;
			}

			// La Batalla 2 (case 2) se muestra completamente en inglés
			this.esIngles = (this.numeroBatalla == 2);
			this.batallaGanada = false;
			this.partidaGuardadaEnTxt = false;

			// Verificar que estemos en modalidad Jugador vs CPU
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
				lblTituloBatalla.Text = "MODO EXCLUSIVO PARA JUGADOR VS CPU";
				lblDialogoCombate.Text = "Esta batalla solo está disponible en la modalidad Jugador vs CPU (IA).";
				return;
			}

			pnlArenaCombate.Visible = true;
			pnlLateralDerecho.Visible = true;

			// Bloquear el botón de continuar hasta que el jugador gane la batalla actual
			BloquearBotonAvance();

			// Configurar textos de interfaz según el idioma de la batalla (Inglés en Batalla 2, Español en el resto)
			AplicarIdiomaControles();

			// Preparar perfil y mazo del Jugador
			PerfilJugador jugador = PrepararEquipoJugador(this.esIngles);
			PerfilJugador cpu = null;
			Escenario escenarioCombate = null;

			// =========================================================================
			// SWITCH CASE DE BATALLA (CASO 1 AL 5: EXCLUSIVO JUGADOR VS CPU)
			// =========================================================================
			switch (numeroBatalla)
			{
				case 1:
					// CASO 1: Batalla 1 - Leviatán de la Fosa (Jugador vs CPU)
					cpu = new PerfilJugador(901, "CPU - Fosa Abisal", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaAgua(101, "Leviatán de la Fosa", 55, 20, 25, 75, "Pulso Abisal", "Emerge de la grieta marina, roba energía, se cura y golpea."));
					cpu.MazoSeleccionado.Add(new CartaFuego(102, "Ignis De La Grieta", 45, 20, 35, 45, "Llamarada Submarina", "Vapor ardiente que perfora el escudo rival."));
					cpu.MazoSeleccionado.Add(new CartaAire(103, "Sierpe De Remolino", 50, 20, 30, 65, "Ráfaga De Fosa", "Doble embestida veloz desde las corrientes."));

					escenarioCombate = SeleccionModalidad.EscenarioInicialSorteado != null
						? SeleccionModalidad.EscenarioInicialSorteado
						: new Escenario(1, "Abismo Oceánico", Elemento.Agua, Elemento.Fuego, 6, 4, 65, "Potencia a Agua (+6) y apaga a Fuego (-4).");

					this.Text = "Batalla 1: Leviatán de la Fosa (Jugador vs CPU) - Leviatanes 2";
					lblTituloBatalla.Text = "BATALLA 1 (CASO 1): JUGADOR VS CPU - LEVIATÁN DE LA FOSA [" + escenarioCombate.Nombre.ToUpper() + "]";
					break;

				case 2:
					// CASE 2 (ENGLISH): Battle 2 - Tar Leviathan (Player vs CPU)
					cpu = new PerfilJugador(902, "CPU - Black Waters", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaTierra(201, "Tar Leviathan", 55, 25, 20, 70, "Tar Tsunami", "Hardens its asphalt armor and crushes the enemy."));
					cpu.MazoSeleccionado.Add(new CartaFuego(202, "Oil Well Pyromancer", 45, 20, 35, 50, "Crude Ignition", "Unleashes burning oil that pierces shields."));
					cpu.MazoSeleccionado.Add(new CartaAgua(203, "Corrupted Hydra", 50, 20, 30, 60, "Dark Drain", "Drains enemy energy and restores 25% HP."));

					escenarioCombate = new Escenario(2, "Black Waters Canyon", Elemento.Tierra, Elemento.Agua, 6, 4, 75, "Boosts Earth (+6) and weakens Water (-4).");

					this.Text = "Battle 2: Tar Leviathan (Player vs CPU) - Leviathans 2";
					lblTituloBatalla.Text = "BATTLE 2 (CASE 2): PLAYER VS CPU - TAR LEVIATHAN [" + escenarioCombate.Nombre.ToUpper() + "]";
					break;

				case 3:
					// CASO 3: Batalla 3 - Leviatán Espectral (Jugador vs CPU)
					cpu = new PerfilJugador(903, "CPU - Mar Espejo", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaAire(301, "Leviatán Espectral", 45, 25, 30, 80, "Niebla Tóxica Doble", "Ejecuta un doble ataque fantasmal consecutivo."));
					cpu.MazoSeleccionado.Add(new CartaAgua(302, "Espectro Del Arrecife", 55, 20, 25, 70, "Eco Abisal", "Drena vigor y restaura su salud."));
					cpu.MazoSeleccionado.Add(new CartaTierra(303, "Coloso De Chatarra", 55, 25, 20, 75, "Muro Oxidado", "Refuerza su escudo y golpea con peso brutal."));

					escenarioCombate = new Escenario(3, "Mar Espejo Brumoso", Elemento.Aire, Elemento.Tierra, 6, 4, 70, "Potencia a Aire (+6) y erosiona a Tierra (-4).");

					this.Text = "Batalla 3: Leviatán Espectral (Jugador vs CPU) - Leviatanes 2";
					lblTituloBatalla.Text = "BATALLA 3: JUGADOR VS CPU - LEVIATÁN ESPECTRAL [" + escenarioCombate.Nombre.ToUpper() + "]";
					break;

				case 4:
					// CASO 4: Batalla 4 - Leviatán de las Tormentas Ácidas (Jugador vs CPU)
					cpu = new PerfilJugador(904, "CPU - Tormenta Ácida", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaAire(401, "Leviatán Tormenta Ácida", 45, 20, 35, 75, "Ciclón Corrosivo", "Doble impacto aéreo de lluvia ácida."));
					cpu.MazoSeleccionado.Add(new CartaFuego(402, "Centella Sulfúrica", 40, 20, 40, 55, "Relámpago Ígneo", "Perfora el escudo enemigo con fuego ácido."));
					cpu.MazoSeleccionado.Add(new CartaAgua(403, "Vórtice De Salmuera", 55, 20, 25, 80, "Tifón Drenante", "Roba energía y regenera su vitalidad."));

					escenarioCombate = new Escenario(4, "Cumbre De Tormentas", Elemento.Aire, Elemento.Tierra, 6, 4, 70, "Potencia a Aire (+6) y debilita a Tierra (-4).");

					this.Text = "Batalla 4: Leviatán de las Tormentas Ácidas (Jugador vs CPU) - Leviatanes 2";
					lblTituloBatalla.Text = "BATALLA 4: JUGADOR VS CPU - LEVIATÁN DE LAS TORMENTAS ÁCIDAS";
					break;

				case 5:
				default:
					// CASO 5: Batalla 5 Final - Leviatán Primordial del Petróleo (Jugador vs CPU)
					cpu = new PerfilJugador(905, "CPU - Núcleo Primordial", 0, 0, 0);
					cpu.MazoSeleccionado.Add(new CartaFuego(501, "Leviatán Primordial", 45, 20, 35, 85, "Cataclismo De Petróleo", "Desata el poder de la gema ignorando todo escudo."));
					cpu.MazoSeleccionado.Add(new CartaTierra(502, "Titán Del Pozo Negro", 55, 25, 20, 80, "Aplastamiento Abisal", "Aumenta su coraza y sacude el lecho marino."));
					cpu.MazoSeleccionado.Add(new CartaAgua(503, "Kraken De Aguas Negras", 55, 20, 25, 75, "Marea Corrupta", "Roba energía, se cura y embiste."));
					cpu.MazoSeleccionado.Add(new CartaAire(504, "Heraldo Del Huracán", 45, 20, 35, 70, "Doble Vendaval", "Golpea dos veces desde las alturas."));

					escenarioCombate = new Escenario(5, "Pozo Primordial Activo", Elemento.Fuego, Elemento.Aire, 6, 4, 70, "Potencia a Fuego (+6) y sofoca a Aire (-4).");

					this.Text = "Batalla Final 5: Leviatán Primordial del Petróleo (Jugador vs CPU) - Leviatanes 2";
					lblTituloBatalla.Text = "BATALLA FINAL 5: JUGADOR VS CPU - LEVIATÁN PRIMORDIAL DEL PETRÓLEO";
					break;
			}

			// Inicializar el motor de combate por turnos indicando si está en inglés (Batalla 2)
			motorCombate = new MotorPartida(jugador, cpu, escenarioCombate, this.esIngles);

			if (this.esIngles)
			{
				lblDialogoCombate.Text = string.Format(
					"Battle {0} Begins (Player vs CPU)!\r\n" +
					"{1} sends out {2} [{3}].\r\n" +
					"Your {4} [{5}] enters the arena. Defeat all {6} CPU Leviathans to advance!",
					numeroBatalla,
					cpu.NombreUsuario,
					motorCombate.ActivoCpu.Nombre,
					MotorPartida.FormatearElemento(motorCombate.ActivoCpu.Faccion, true),
					motorCombate.ActivoJugador.Nombre,
					MotorPartida.FormatearElemento(motorCombate.ActivoJugador.Faccion, true),
					cpu.MazoSeleccionado.Count);
			}
			else
			{
				lblDialogoCombate.Text = string.Format(
					"¡Comienza la Batalla {0} (Jugador vs CPU)!\r\n" +
					"{1} envía a {2} [{3}].\r\n" +
					"Tu {4} [{5}] entra en acción. ¡Derrota a los {6} Leviatanes de la CPU para avanzar!",
					numeroBatalla,
					cpu.NombreUsuario,
					motorCombate.ActivoCpu.Nombre,
					motorCombate.ActivoCpu.Faccion,
					motorCombate.ActivoJugador.Nombre,
					motorCombate.ActivoJugador.Faccion,
					cpu.MazoSeleccionado.Count);
			}

			ActualizarInterfazCompleta();
		}

		private void BloquearBotonAvance()
		{
			if (this.button1 == null)
				return;

			this.button1.Enabled = false;
			this.button1.BackColor = Color.FromArgb(75, 80, 92);
			this.button1.ForeColor = Color.LightGray;

			if (this.esIngles)
			{
				this.button1.Text = "🔒 Win Battle 2 to Advance";
			}
			else
			{
				this.button1.Text = "🔒 Gana Batalla " + numeroBatalla + " para avanzar";
			}
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
				int siguienteCapitulo = this.numeroBatalla + 1;
				this.button1.Enabled = true;
				this.button1.BackColor = Color.Gold;
				this.button1.ForeColor = Color.Black;

				if (this.esIngles)
				{
					this.button1.Text = "Continue to Chapter " + siguienteCapitulo + " ▶";
				}
				else
				{
					this.button1.Text = "Continuar al Cap. " + siguienteCapitulo + " ▶";
				}
			}
		}

		private void AplicarIdiomaControles()
		{
			if (this.esIngles)
			{
				lblGuiaElemental.Text = "ELEMENTAL TYPE CHART:  [WATER] beats [FIRE]  >  [FIRE] beats [AIR]  >  [AIR] beats [EARTH]  >  [EARTH] beats [WATER]";
				lblHeaderCola.Text = " YOUR POKÉMON BENCH (CLICK TO SELECT SWITCH)";
				lblHeaderDerrotadosCpu.Text = " CPU ROSTER & DEFEATED COUNTER (0/3)";
				lblHeaderBitacora.Text = " TURN-BASED BATTLE LOG";
				btnAtacar.Text = "⚔ ATTACK\r\n(1 ENERGY)";
				btnEspecial.Text = "⚡ SPECIAL SKILL\r\n(2 ENERGY | 100%)";
				btnDefender.Text = "🛡 DEFEND\r\n(+SHIELD | 1 EN)";
				btnCambiarLeviatan.Text = "🔄 SWITCH LEVIATHAN\r\n(FIND ADVANTAGE)";
				btnCargarEnergia.Text = "🔋 CHARGE (+2 ENERGY)";
				btnReiniciarBatalla.Text = "↻ RESTART BATTLE";
			}
			else
			{
				lblGuiaElemental.Text = "TABLA DE TIPOS ELEMENTALES:  [AGUA] vence a [FUEGO]  >  [FUEGO] vence a [AIRE]  >  [AIRE] vence a [TIERRA]  >  [TIERRA] vence a [AGUA]";
				lblHeaderCola.Text = " TU BANCA POKÉMON / CAMBIO (CLIC PARA ELEGIR)";
				lblHeaderDerrotadosCpu.Text = " CONTEO EQUIPO CPU (DERROTADOS: 0/3)";
				lblHeaderBitacora.Text = " HISTORIAL DE COMBATE POR TURNOS";
				btnAtacar.Text = "⚔ ATACAR\r\n(1 ENERGÍA)";
				btnEspecial.Text = "⚡ HABILIDAD ESPECIAL\r\n(2 ENERGÍA | 100%)";
				btnDefender.Text = "🛡 DEFENDER\r\n(+ESCUDO | 1 EN)";
				btnCambiarLeviatan.Text = "🔄 CAMBIAR LEVIATÁN\r\n(ELEGIR DE BANCA)";
				btnCargarEnergia.Text = "🔋 CARGAR (+2 ENERGÍA)";
				btnReiniciarBatalla.Text = "↻ REINICIAR BATALLA";
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
				jugadorIngles.MazoSeleccionado.Add(new CartaAire(10, "Zephyr Winged Serpent", 45, 25, 30, 75, "Slashing Storm", "Executes a consecutive double aerial strike."));
				jugadorIngles.MazoSeleccionado.Add(new CartaAgua(4, "Abyssal Nereid", 55, 25, 20, 85, "Healing Tide", "Steals 1 energy, heals 25% HP and strikes."));
				jugadorIngles.MazoSeleccionado.Add(new CartaTierra(7, "Basalt Golem", 60, 30, 10, 90, "Monolithic Wall", "Raises shield and crushes adding ATK + DEF."));
				jugadorIngles.MazoSeleccionado.Add(new CartaFuego(1, "Ignis Pyric Leviathan", 45, 20, 35, 40, "Solar Flare", "Ignores enemy shield and deals piercing damage."));
				jugadorIngles.MazoSeleccionado.Add(new CartaAire(11, "Hurricane Valkyrie", 50, 20, 30, 80, "Rising Gust", "Two swift slashes with wind blades."));
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

		/// <summary>
		/// Actualiza barras de vida, elementos, conteo de personajes derrotados de la CPU, banca y botones.
		/// </summary>
		private void ActualizarInterfazCompleta()
		{
			if (motorCombate == null)
				return;

			Personaje pj = motorCombate.ActivoJugador;
			Personaje cpu = motorCombate.ActivoCpu;
			int totalEquipoCpu = motorCombate.JugadorCpu.MazoSeleccionado.Count;
			int derrotadosCpuCant = motorCombate.DerrotadosCpu.Count;
			int totalEquipoPj = motorCombate.JugadorHumano.MazoSeleccionado.Count;
			int derrotadosPjCant = motorCombate.DerrotadosJugador.Count;

			// 1. Indicador de Turno / Pausas por Caída / Victoria o Derrota
			if (motorCombate.PartidaTerminada)
			{
				bool ganoJugador = (motorCombate.Ganador == motorCombate.JugadorHumano);
				if (ganoJugador)
				{
					lblTurnoActual.Text = this.esIngles ? "VICTORY! YOU WIN!" : "¡VICTORIA DEL JUGADOR!";
					lblTurnoActual.BackColor = Color.FromArgb(32, 150, 75);
					DesbloquearBotonAvancePorVictoria();
				}
				else
				{
					lblTurnoActual.Text = this.esIngles ? "DEFEAT - RESTART!" : "DERROTA - REINTENTA";
					lblTurnoActual.BackColor = Color.FromArgb(180, 40, 40);
					BloquearBotonAvance();
					btnReiniciarBatalla.BackColor = Color.Gold;
					btnReiniciarBatalla.ForeColor = Color.Black;
				}
			}
			else if (motorCombate.EsperandoRelevoJugadorCaido)
			{
				lblTurnoActual.Text = this.esIngles ? "💀 ALLY DEFEATED (PAUSE)" : "💀 BAJA EN TU EQUIPO...";
				lblTurnoActual.BackColor = Color.FromArgb(175, 35, 35);
			}
			else if (motorCombate.EsperandoRelevoCpuCaido)
			{
				lblTurnoActual.Text = this.esIngles ? "🎯 CPU DEFEATED (PAUSE)" : "🎯 ¡CPU DERROTADO!...";
				lblTurnoActual.BackColor = Color.FromArgb(40, 120, 180);
			}
			else if (motorCombate.EsTurnoJugador)
			{
				lblTurnoActual.Text = this.esIngles
					? string.Format("TURN #{0:00} - YOUR TURN", motorCombate.TurnoActual)
					: string.Format("TURNO #{0:00} - TU TURNO", motorCombate.TurnoActual);
				lblTurnoActual.BackColor = Color.FromArgb(30, 130, 90);
			}
			else
			{
				lblTurnoActual.Text = this.esIngles
					? string.Format("TURN #{0:00} - CPU TURN...", motorCombate.TurnoActual)
					: string.Format("TURNO #{0:00} - TURNO CPU...", motorCombate.TurnoActual);
				lblTurnoActual.BackColor = Color.FromArgb(204, 85, 25);
			}

			// 2. HUD CPU con conteo de vivos y derrotados
			if (cpu != null)
			{
				string estadoCaidoCpu = !cpu.EstaVivo ? (this.esIngles ? " [DEFEATED ✖]" : " [DERROTADO ✖]") : "";
				lblNombreCpu.Text = "CPU: " + cpu.Nombre + estadoCaidoCpu;
				lblElementoCpu.Text = "[" + MotorPartida.FormatearElemento(cpu.Faccion, this.esIngles) + "]";
				lblElementoCpu.BackColor = ObtenerColorElemento(cpu.Faccion);

				int porcVidaCpu = (int)Math.Round(((double)cpu.VidaActual / (double)cpu.VidaMaxima) * 100.0);
				pbVidaCpu.Value = Math.Min(100, Math.Max(0, porcVidaCpu));

				if (this.esIngles)
				{
					lblStatsCpu.Text = string.Format(
						"HP: {0}/{1} | SHIELD: {2} | ATK: {3} | ENERGY: {4}/{5}\r\nSPECIAL CHARGE: {6}% [{7}]",
						cpu.VidaActual, cpu.VidaMaxima, cpu.EscudoActual, cpu.AtaqueBase,
						cpu.EnergiaActual, Personaje.EnergiaMaxima, cpu.CargaEspecial, cpu.NombreEspecial);
					lblBolitasCpu.Text = string.Format("CPU ALIVE: {0} | DEFEATED: {1}/{2}",
						GenerarIconosEquipo(motorCombate.TotalVivosCpu, totalEquipoCpu), derrotadosCpuCant, totalEquipoCpu);
				}
				else
				{
					lblStatsCpu.Text = string.Format(
						"HP: {0}/{1} | ESCUDO: {2} | ATK: {3} | ENERGÍA: {4}/{5}\r\nCARGA ESPECIAL: {6}% [{7}]",
						cpu.VidaActual, cpu.VidaMaxima, cpu.EscudoActual, cpu.AtaqueBase,
						cpu.EnergiaActual, Personaje.EnergiaMaxima, cpu.CargaEspecial, cpu.NombreEspecial);
					lblBolitasCpu.Text = string.Format("CPU VIVOS: {0} | DERROTADOS: {1}/{2}",
						GenerarIconosEquipo(motorCombate.TotalVivosCpu, totalEquipoCpu), derrotadosCpuCant, totalEquipoCpu);
				}
			}

			// 3. HUD Jugador con conteo de vivos y bajas
			if (pj != null)
			{
				string estadoCaidoPj = !pj.EstaVivo ? (this.esIngles ? " [FAINTED 💀]" : " [DERROTADO 💀]") : "";
				lblNombreJugador.Text = (this.esIngles ? "YOU: " : "TÚ: ") + pj.Nombre + estadoCaidoPj;
				lblElementoJugador.Text = "[" + MotorPartida.FormatearElemento(pj.Faccion, this.esIngles) + "]";
				lblElementoJugador.BackColor = ObtenerColorElemento(pj.Faccion);

				int porcVidaPj = (int)Math.Round(((double)pj.VidaActual / (double)pj.VidaMaxima) * 100.0);
				pbVidaJugador.Value = Math.Min(100, Math.Max(0, porcVidaPj));

				if (this.esIngles)
				{
					lblStatsJugador.Text = string.Format(
						"HP: {0}/{1} | SHIELD: {2} | ATK: {3} | ENERGY: {4}/{5}",
						pj.VidaActual, pj.VidaMaxima, pj.EscudoActual, pj.AtaqueBase,
						pj.EnergiaActual, Personaje.EnergiaMaxima);
					lblEspecialInfoJugador.Text = string.Format("SPECIAL ({0}%): [{1}] - {2}",
						pj.CargaEspecial, pj.NombreEspecial, pj.DescripcionEspecial);
					lblBolitasJugador.Text = string.Format("YOUR TEAM ALIVE: {0} | DEFEATED: {1}/{2}",
						GenerarIconosEquipo(motorCombate.TotalVivosJugador, totalEquipoPj), derrotadosPjCant, totalEquipoPj);
				}
				else
				{
					lblStatsJugador.Text = string.Format(
						"HP: {0}/{1} | ESCUDO: {2} | ATK: {3} | ENERGÍA: {4}/{5}",
						pj.VidaActual, pj.VidaMaxima, pj.EscudoActual, pj.AtaqueBase,
						pj.EnergiaActual, Personaje.EnergiaMaxima);
					lblEspecialInfoJugador.Text = string.Format("ESPECIAL ({0}%): [{1}] - {2}",
						pj.CargaEspecial, pj.NombreEspecial, pj.DescripcionEspecial);
					lblBolitasJugador.Text = string.Format("TU EQUIPO VIVO: {0} | DERROTADOS: {1}/{2}",
						GenerarIconosEquipo(motorCombate.TotalVivosJugador, totalEquipoPj), derrotadosPjCant, totalEquipoPj);
				}

				pbEspecialJugador.Value = Math.Min(100, Math.Max(0, pj.CargaEspecial));
			}

			// 4. Panel Central de Ventaja Elemental
			ActualizarBannerVentajaElemental(pj, cpu);

			// 5. Banca / Cola de Relevo del Jugador + Conteo de Derrotados de Tu Equipo
			if (this.esIngles)
			{
				lblHeaderCola.Text = string.Format(" YOUR TEAM (DEFEATED: {0}/{1} | ALIVE: {2})",
					derrotadosPjCant, totalEquipoPj, motorCombate.TotalVivosJugador);
			}
			else
			{
				lblHeaderCola.Text = string.Format(" TU EQUIPO (DERROTADOS: {0}/{1} | VIVOS: {2})",
					derrotadosPjCant, totalEquipoPj, motorCombate.TotalVivosJugador);
			}

			actualizandoListaBanca = true;
			lstColaJugador.Items.Clear();
			Personaje[] enBanca = motorCombate.ColaJugador.ToArray();
			int indiceRecomendado = 0;
			bool encontroVentaja = false;

			// 5A. Mostrar suplentes vivos en la banca disponibles para cambio
			for (int i = 0; i < enBanca.Length; i++)
			{
				Personaje suplente = enBanca[i];
				double multSuplente = (cpu != null) ? suplente.ObtenerMultiplicadorElemental(cpu.Faccion) : 1.0;

				if (!encontroVentaja && multSuplente > 1.0)
				{
					indiceRecomendado = i;
					encontroVentaja = true;
				}

				string tagVentaja;
				if (this.esIngles)
				{
					tagVentaja = multSuplente > 1.0 ? "★ [ADVANTAGE x1.5]" :
					             multSuplente < 1.0 ? "[DISADVANTAGE x0.75]" : "[NEUTRAL x1.0]";
				}
				else
				{
					tagVentaja = multSuplente > 1.0 ? "★ [VENTAJA x1.5]" :
					             multSuplente < 1.0 ? "[DESVENTAJA x0.75]" : "[NEUTRAL x1.0]";
				}

				lstColaJugador.Items.Add(string.Format("#{0} {1} [{2}] HP:{3}/{4} -> {5}",
					i + 1,
					suplente.Nombre,
					MotorPartida.FormatearElemento(suplente.Faccion, this.esIngles),
					suplente.VidaActual,
					suplente.VidaMaxima,
					tagVentaja));
			}

			// 5B. Mostrar en la misma lista los personajes de TU EQUIPO que ya fueron derrotados
			for (int d = 0; d < motorCombate.DerrotadosJugador.Count; d++)
			{
				Personaje caidoPj = motorCombate.DerrotadosJugador[d];
				string elemCaido = MotorPartida.FormatearElemento(caidoPj.Faccion, this.esIngles);
				if (this.esIngles)
				{
					lstColaJugador.Items.Add(string.Format("💀 [YOUR DEFEATED #{0}] {1} [{2}] (0/{3} HP)",
						d + 1, caidoPj.Nombre, elemCaido, caidoPj.VidaMaxima));
				}
				else
				{
					lstColaJugador.Items.Add(string.Format("💀 [TU DERROTADO #{0}] {1} [{2}] (0/{3} HP - Perdió)",
						d + 1, caidoPj.Nombre, elemCaido, caidoPj.VidaMaxima));
				}
			}

			if (enBanca.Length > 0)
			{
				lstColaJugador.SelectedIndex = indiceRecomendado;
				ActualizarTextoBotonCambio(enBanca[indiceRecomendado]);
			}
			else
			{
				if (motorCombate.DerrotadosJugador.Count == 0)
				{
					lstColaJugador.Items.Add(this.esIngles ? "(No more Leviathans on bench)" : "(Sin más Leviatanes en la banca)");
				}
				btnCambiarLeviatan.Text = this.esIngles
					? "🔄 SWITCH LEVIATHAN\r\n(BENCH EMPTY)"
					: "🔄 CAMBIAR LEVIATÁN\r\n(SIN SUPLENTES)";
			}
			actualizandoListaBanca = false;

			// 6. Conteo y Lista de Personajes Derrotados / Vivos (CPU y Resumen de tu Equipo)
			ActualizarListaEstadoCpu(totalEquipoCpu, derrotadosCpuCant, totalEquipoPj, derrotadosPjCant);

			// 7. Bitácora de eventos (más reciente abajo)
			lstBitacoraCombate.Items.Clear();
			string[] eventos = motorCombate.PilaEventos.ToArray();
			Array.Reverse(eventos);
			for (int i = 0; i < eventos.Length; i++)
			{
				lstBitacoraCombate.Items.Add(eventos[i]);
			}
			if (lstBitacoraCombate.Items.Count > 0)
			{
				lstBitacoraCombate.TopIndex = lstBitacoraCombate.Items.Count - 1;
			}

			// 8. Habilitación de botones de turno (se deshabilitan durante pausas por caída o turno CPU)
			bool enPausaCaida = motorCombate.EsperandoRelevoJugadorCaido || motorCombate.EsperandoRelevoCpuCaido;
			bool habilitarComandos = !motorCombate.PartidaTerminada && !enPausaCaida && motorCombate.EsTurnoJugador && pj != null && pj.EstaVivo;
			btnAtacar.Enabled = habilitarComandos && (pj.EnergiaActual >= Personaje.CostoAtaque);
			btnDefender.Enabled = habilitarComandos && (pj.EnergiaActual >= Personaje.CostoDefensa);
			btnEspecial.Enabled = habilitarComandos && pj.EspecialListo;
			btnCambiarLeviatan.Enabled = habilitarComandos && (motorCombate.ColaJugador.Count > 0);
			btnCargarEnergia.Enabled = habilitarComandos;

			// Si la partida terminó, guardar estadísticas en TXT una sola vez
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

			pnlSpriteCpu.Invalidate();
			pnlSpriteJugador.Invalidate();
			pnlArenaCombate.Invalidate();
		}

		/// <summary>
		/// Actualiza el cuadro de conteo mostrando los personajes derrotados de ambos equipos
		/// (CPU y Jugador) y los restantes de la CPU.
		/// </summary>
		private void ActualizarListaEstadoCpu(int totalEquipoCpu, int derrotadosCpuCant, int totalEquipoPj, int derrotadosPjCant)
		{
			if (this.esIngles)
			{
				lblHeaderDerrotadosCpu.Text = string.Format(" DEFEATED COUNT -> CPU: {0}/{1} | YOUR TEAM: {2}/{3}",
					derrotadosCpuCant, totalEquipoCpu, derrotadosPjCant, totalEquipoPj);
			}
			else
			{
				lblHeaderDerrotadosCpu.Text = string.Format(" CONTEO DERROTADOS -> CPU: {0}/{1} | TÚ: {2}/{3}",
					derrotadosCpuCant, totalEquipoCpu, derrotadosPjCant, totalEquipoPj);
			}

			lstDerrotadosCpu.Items.Clear();

			// A. Mostrar los personajes de la CPU que ya fueron derrotados
			for (int i = 0; i < motorCombate.DerrotadosCpu.Count; i++)
			{
				Personaje caido = motorCombate.DerrotadosCpu[i];
				string elem = MotorPartida.FormatearElemento(caido.Faccion, this.esIngles);
				if (this.esIngles)
				{
					lstDerrotadosCpu.Items.Add(string.Format("✖ [CPU DEFEATED #{0}] {1} [{2}] (0/{3} HP)",
						i + 1, caido.Nombre, elem, caido.VidaMaxima));
				}
				else
				{
					lstDerrotadosCpu.Items.Add(string.Format("✖ [CPU PERDIÓ #{0}] {1} [{2}] (0/{3} HP)",
						i + 1, caido.Nombre, elem, caido.VidaMaxima));
				}
			}

			// B. Mostrar los personajes de TU EQUIPO que ya fueron derrotados
			for (int j = 0; j < motorCombate.DerrotadosJugador.Count; j++)
			{
				Personaje caidoPj = motorCombate.DerrotadosJugador[j];
				string elemPj = MotorPartida.FormatearElemento(caidoPj.Faccion, this.esIngles);
				if (this.esIngles)
				{
					lstDerrotadosCpu.Items.Add(string.Format("💀 [YOUR DEFEATED #{0}] {1} [{2}] (0/{3} HP)",
						j + 1, caidoPj.Nombre, elemPj, caidoPj.VidaMaxima));
				}
				else
				{
					lstDerrotadosCpu.Items.Add(string.Format("💀 [TU BAJA #{0}] {1} [{2}] (0/{3} HP)",
						j + 1, caidoPj.Nombre, elemPj, caidoPj.VidaMaxima));
				}
			}

			// C. Mostrar el personaje activo de la CPU (si sigue vivo)
			if (motorCombate.ActivoCpu != null && motorCombate.ActivoCpu.EstaVivo)
			{
				Personaje act = motorCombate.ActivoCpu;
				string elem = MotorPartida.FormatearElemento(act.Faccion, this.esIngles);
				if (this.esIngles)
				{
					lstDerrotadosCpu.Items.Add(string.Format("⚔ [CPU FIGHTING] {0} [{1}] (HP: {2}/{3})",
						act.Nombre, elem, act.VidaActual, act.VidaMaxima));
				}
				else
				{
					lstDerrotadosCpu.Items.Add(string.Format("⚔ [CPU EN CAMPO] {0} [{1}] (HP: {2}/{3})",
						act.Nombre, elem, act.VidaActual, act.VidaMaxima));
				}
			}

			// D. Mostrar los que quedan en la banca de la CPU
			Personaje[] colaCpuArr = motorCombate.ColaCpu.ToArray();
			for (int i = 0; i < colaCpuArr.Length; i++)
			{
				Personaje esp = colaCpuArr[i];
				string elem = MotorPartida.FormatearElemento(esp.Faccion, this.esIngles);
				if (this.esIngles)
				{
					lstDerrotadosCpu.Items.Add(string.Format("⏳ [CPU WAITING #{0}] {1} [{2}] (HP: {3}/{4})",
						i + 1, esp.Nombre, elem, esp.VidaActual, esp.VidaMaxima));
				}
				else
				{
					lstDerrotadosCpu.Items.Add(string.Format("⏳ [CPU EN ESPERA #{0}] {1} [{2}] (HP: {3}/{4})",
						i + 1, esp.Nombre, elem, esp.VidaActual, esp.VidaMaxima));
				}
			}
		}

		private void ActualizarBannerVentajaElemental(Personaje pj, Personaje cpu)
		{
			if (pj == null || cpu == null)
				return;

			double multJugador = pj.ObtenerMultiplicadorElemental(cpu.Faccion);
			int bonoPj = motorCombate.EscenarioActual != null ? motorCombate.EscenarioActual.ObtenerModificadorEscenario(pj) : 0;
			int bonoCpu = motorCombate.EscenarioActual != null ? motorCombate.EscenarioActual.ObtenerModificadorEscenario(cpu) : 0;
			string elemPj = MotorPartida.FormatearElemento(pj.Faccion, this.esIngles);
			string elemCpu = MotorPartida.FormatearElemento(cpu.Faccion, this.esIngles);

			if (multJugador > 1.0)
			{
				pnlVentajaElemental.BackColor = Color.FromArgb(20, 65, 38);
				lblVentajaElemental.ForeColor = Color.LightGreen;

				if (this.esIngles)
				{
					lblVentajaElemental.Text = string.Format(
						"★ YOUR LEVIATHAN HAS ELEMENTAL ADVANTAGE! [{0}] beats [{1}]\r\n" +
						"Your attacks deal x1.50 (Super Effective!) and CPU deals x0.75 | Stage Bonus: You {2:+#;-#;0} / CPU {3:+#;-#;0}",
						elemPj, elemCpu, bonoPj, bonoCpu);
				}
				else
				{
					lblVentajaElemental.Text = string.Format(
						"★ ¡TU LEVIATÁN TIENE LA VENTAJA ELEMENTAL! [{0}] vence a [{1}]\r\n" +
						"Tus ataques causan x1.50 (¡Súper eficaz!) y la CPU solo te hace x0.75 | Bono Escenario: Tú {2:+#;-#;0} / CPU {3:+#;-#;0}",
						elemPj, elemCpu, bonoPj, bonoCpu);
				}
			}
			else if (multJugador < 1.0)
			{
				pnlVentajaElemental.BackColor = Color.FromArgb(75, 28, 24);
				lblVentajaElemental.ForeColor = Color.FromArgb(255, 190, 120);

				if (this.esIngles)
				{
					lblVentajaElemental.Text = string.Format(
						"⚠ CPU HAS ELEMENTAL ADVANTAGE! [{0}] beats your [{1}]\r\n" +
						"CPU deals x1.50 damage and you deal x0.75. Use 'SWITCH LEVIATHAN' to bring out a counter or 'DEFEND'!",
						elemCpu, elemPj);
				}
				else
				{
					lblVentajaElemental.Text = string.Format(
						"⚠ ¡LA CPU TIENE LA VENTAJA ELEMENTAL! [{0}] vence a tu [{1}]\r\n" +
						"La CPU te causa daño x1.50 y tú x0.75. ¡Usa 'CAMBIAR LEVIATÁN' para sacar uno con ventaja o 'DEFENDER'!",
						elemCpu, elemPj);
				}
			}
			else
			{
				pnlVentajaElemental.BackColor = Color.FromArgb(28, 45, 68);
				lblVentajaElemental.ForeColor = Color.Aquamarine;

				if (this.esIngles)
				{
					lblVentajaElemental.Text = string.Format(
						"⚖ BALANCED ELEMENTAL MATCHUP: Your [{0}] vs CPU [{1}] (x1.00 Neutral Damage)\r\n" +
						"Stage Bonus ({2}): You {3:+#;-#;0} ATK | CPU {4:+#;-#;0} ATK",
						elemPj, elemCpu,
						motorCombate.EscenarioActual != null ? motorCombate.EscenarioActual.Nombre : "Arena",
						bonoPj, bonoCpu);
				}
				else
				{
					lblVentajaElemental.Text = string.Format(
						"⚖ DUELO ELEMENTAL EQUILIBRADO: Tu [{0}] vs CPU [{1}] (Daño x1.00 Neutral)\r\n" +
						"Bono de Escenario ({2}): Tú {3:+#;-#;0} ATK | CPU {4:+#;-#;0} ATK",
						elemPj, elemCpu,
						motorCombate.EscenarioActual != null ? motorCombate.EscenarioActual.Nombre : "Arena",
						bonoPj, bonoCpu);
				}
			}
		}

		private string GenerarIconosEquipo(int vivos, int total)
		{
			string texto = "";
			for (int i = 0; i < total; i++)
			{
				texto += (i < vivos) ? "● " : "○ ";
			}
			return string.Format("{0}({1}/{2})", texto, vivos, total);
		}

		// =========================================================================
		// EVENTOS DE LOS BOTONES DE COMBATE Y TEMPORIZADORES CON PAUSAS
		// =========================================================================
		private void BtnAtacarClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			string resultado = motorCombate.EjecutarAtaqueJugador();
			lblDialogoCombate.Text = resultado;
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnEspecialClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			string resultado = motorCombate.EjecutarEspecialJugador();
			lblDialogoCombate.Text = resultado;
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnDefenderClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			string resultado = motorCombate.EjecutarDefensaJugador();
			lblDialogoCombate.Text = resultado;
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void ActualizarTextoBotonCambio(Personaje suplenteSeleccionado)
		{
			if (suplenteSeleccionado == null)
				return;

			string elem = MotorPartida.FormatearElemento(suplenteSeleccionado.Faccion, this.esIngles);
			if (this.esIngles)
			{
				btnCambiarLeviatan.Text = string.Format("🔄 SWITCH TO:\r\n{0} [{1}]", suplenteSeleccionado.Nombre, elem);
			}
			else
			{
				btnCambiarLeviatan.Text = string.Format("🔄 CAMBIAR A:\r\n{0} [{1}]", suplenteSeleccionado.Nombre, elem);
			}
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

				if (!motorCombate.PartidaTerminada && motorCombate.EsTurnoJugador && !motorCombate.EsperandoRelevoJugadorCaido && !motorCombate.EsperandoRelevoCpuCaido)
				{
					string ventaja = MotorPartida.ObtenerTextoVentajaElemental(elegido, motorCombate.ActivoCpu, this.esIngles);
					lblDialogoCombate.Text = this.esIngles
						? string.Format("Selected on bench: {0} [{1}] (HP:{2}/{3}).\r\n{4}\r\nClick 'SWITCH TO' or double-click to send into battle!",
							elegido.Nombre, MotorPartida.FormatearElemento(elegido.Faccion, true), elegido.VidaActual, elegido.VidaMaxima, ventaja)
						: string.Format("Seleccionado en banca: {0} [{1}] (HP:{2}/{3}).\r\n{4}\r\n¡Pulsa 'CAMBIAR A' o haz doble clic para enviarlo al combate!",
							elegido.Nombre, elegido.Faccion, elegido.VidaActual, elegido.VidaMaxima, ventaja);
				}
			}
			else if (idx >= enBanca.Length)
			{
				int idxDerrotado = idx - enBanca.Length;
				if (idxDerrotado >= 0 && idxDerrotado < motorCombate.DerrotadosJugador.Count)
				{
					Personaje caido = motorCombate.DerrotadosJugador[idxDerrotado];
					lblDialogoCombate.Text = this.esIngles
						? string.Format("💀 {0} [{1}] has already been defeated (0 HP) and cannot return to battle. Select an alive Leviathan!",
							caido.Nombre, MotorPartida.FormatearElemento(caido.Faccion, true))
						: string.Format("💀 {0} [{1}] ya fue derrotado (0 HP) y no puede volver al combate. ¡Selecciona un Leviatán vivo de tu banca!",
							caido.Nombre, caido.Faccion);
				}
			}
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

			string resultado = motorCombate.EjecutarCambioPersonajeJugador(indiceSeleccionado);
			lblDialogoCombate.Text = resultado;
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnCargarEnergiaClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			string resultado = motorCombate.EjecutarCargaEnergiaJugador();
			lblDialogoCombate.Text = resultado;
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnReiniciarBatallaClick(object sender, EventArgs e)
		{
			tmrTurnoCpu.Stop();
			tmrPausaDerrota.Stop();
			ConfigurarBatalla();
		}

		/// <summary>
		/// Administra si debe iniciarse una pausa por caída de personaje (2.4 seg)
		/// o el turno normal de la CPU (1.7 seg).
		/// </summary>
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

			string respuestaCpu = motorCombate.EjecutarTurnoCpu();
			lblDialogoCombate.Text = respuestaCpu;
			ActualizarInterfazCompleta();

			// Si la CPU derrotó al personaje activo del jugador, iniciar la pausa de 2.4s antes de sacar al siguiente
			if (motorCombate.EsperandoRelevoJugadorCaido)
			{
				tmrPausaDerrota.Start();
			}
		}

		/// <summary>
		/// Temporizador de pausa (2.4 segundos) cuando cae derrotado un personaje de tu equipo o de la CPU,
		/// para que el jugador pueda ver claramente quién perdió antes de que entre el relevo.
		/// </summary>
		private void TmrPausaDerrotaTick(object sender, EventArgs e)
		{
			tmrPausaDerrota.Stop();
			if (motorCombate == null || motorCombate.PartidaTerminada)
				return;

			if (motorCombate.EsperandoRelevoJugadorCaido)
			{
				string msgEntradaJugador = motorCombate.ProcesarEntradaSiguienteJugadorTrasDerrota();
				lblDialogoCombate.Text = msgEntradaJugador;
				ActualizarInterfazCompleta();
			}
			else if (motorCombate.EsperandoRelevoCpuCaido)
			{
				string msgEntradaCpu = motorCombate.ProcesarEntradaSiguienteCpuTrasDerrota();
				lblDialogoCombate.Text = msgEntradaCpu;
				ActualizarInterfazCompleta();
			}
		}

		// =========================================================================
		// RENDERIZADO GRÁFICO ESTILO POKÉMON GBA
		// =========================================================================
		private void PnlArenaCombatePaint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen lapizScan = new Pen(Color.FromArgb(18, 255, 255, 255), 1))
			{
				for (int y = 0; y < pnlArenaCombate.Height; y += 6)
				{
					g.DrawLine(lapizScan, 0, y, pnlArenaCombate.Width, y);
				}
			}
		}

		private void PnlSpriteCpuPaint(object sender, PaintEventArgs e)
		{
			if (motorCombate == null || motorCombate.ActivoCpu == null)
				return;

			DibujarCriaturaEnPlataformaGba(e.Graphics, pnlSpriteCpu.ClientRectangle, motorCombate.ActivoCpu, true);
		}

		private void PnlSpriteJugadorPaint(object sender, PaintEventArgs e)
		{
			if (motorCombate == null || motorCombate.ActivoJugador == null)
				return;

			DibujarCriaturaEnPlataformaGba(e.Graphics, pnlSpriteJugador.ClientRectangle, motorCombate.ActivoJugador, false);
		}

		private void DibujarCriaturaEnPlataformaGba(Graphics g, Rectangle rect, Personaje p, bool esCpu)
		{
			g.SmoothingMode = SmoothingMode.AntiAlias;
			Color colorElem = p.EstaVivo ? ObtenerColorElemento(p.Faccion) : Color.FromArgb(95, 95, 105);

			using (LinearGradientBrush fondo = new LinearGradientBrush(
				rect,
				Color.FromArgb(14, 22, 38),
				Color.FromArgb(55, colorElem.R / 2, colorElem.G / 2, colorElem.B / 2),
				LinearGradientMode.Vertical))
			{
				g.FillRectangle(fondo, rect);
			}

			int cx = rect.Width / 2;
			int cy = rect.Height / 2;

			using (SolidBrush sombra = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
			using (SolidBrush plataforma = new SolidBrush(Color.FromArgb(100, colorElem)))
			using (Pen bordeOro = new Pen(p.EstaVivo ? Color.Gold : Color.Crimson, 2f))
			{
				g.FillEllipse(sombra, cx - 95, cy + 22, 190, 34);
				g.FillEllipse(plataforma, cx - 90, cy + 18, 180, 30);
				g.DrawEllipse(bordeOro, cx - 90, cy + 18, 180, 30);
			}

			if (p.EscudoActual > 0 && p.EstaVivo)
			{
				using (Pen lapizEscudo = new Pen(Color.FromArgb(160, 100, 220, 255), 3f))
				{
					g.DrawEllipse(lapizEscudo, cx - 48, cy - 48, 96, 92);
				}
			}

			using (SolidBrush brochaCuerpo = new SolidBrush(colorElem))
			using (SolidBrush brochaNucleo = new SolidBrush(p.EstaVivo ? Color.FromArgb(235, 255, 245, 215) : Color.FromArgb(180, 70, 70)))
			using (Pen bordeBlanco = new Pen(p.EstaVivo ? Color.White : Color.Red, 2.5f))
			using (Font fuenteElem = new Font("Consolas", 10F, FontStyle.Bold))
			using (Font fuenteSub = new Font("Consolas", 8.5F, FontStyle.Bold))
			{
				g.FillEllipse(brochaCuerpo, cx - 38, cy - 40, 76, 76);
				g.DrawEllipse(bordeBlanco, cx - 38, cy - 40, 76, 76);
				g.FillEllipse(brochaNucleo, cx - 22, cy - 24, 44, 44);

				string elemTxt = p.EstaVivo
					? MotorPartida.FormatearElemento(p.Faccion, this.esIngles)
					: (this.esIngles ? "KO ✖" : "CAÍDO ✖");

				SizeF tam = g.MeasureString(elemTxt, fuenteElem);
				g.DrawString(elemTxt, fuenteElem, p.EstaVivo ? Brushes.Black : Brushes.White, cx - (tam.Width / 2f), cy - 10);

				string etiquetaRol;
				if (!p.EstaVivo)
					etiquetaRol = this.esIngles ? "✖ DEFEATED (0 HP)" : "✖ DERROTADO (0 HP)";
				else if (this.esIngles)
					etiquetaRol = esCpu ? "CPU RIVAL" : "YOUR LEADER";
				else
					etiquetaRol = esCpu ? "RIVAL CPU" : "TU LÍDER";

				g.DrawString(etiquetaRol, fuenteSub, p.EstaVivo ? Brushes.Gold : Brushes.Salmon, 8, 6);
			}
		}

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

		private string ObtenerNombreLeviatan(int batalla)
		{
			switch (batalla)
			{
				case 1:
					return "Leviatán de la Fosa";
				case 2:
					return "Tar Leviathan";
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
			if (!this.batallaGanada || motorCombate == null || !motorCombate.PartidaTerminada || motorCombate.Ganador != motorCombate.JugadorHumano)
			{
				string msg = this.esIngles
					? "You must defeat the CPU in Battle 2 before advancing to the next stage!"
					: "Debes ganar la Batalla " + this.numeroBatalla + " contra la CPU para poder avanzar a la siguiente.";
				string titulo = this.esIngles ? "Stage Locked" : "Batalla no superada";
				MessageBox.Show(msg, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			tmrTurnoCpu.Stop();
			tmrPausaDerrota.Stop();

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
				MainForm ventanaPrincipal = new MainForm();
				ventanaPrincipal.FormClosed += (s, args) => Application.Exit();
				ventanaPrincipal.Show();
				this.Hide();
			}
		}
	}
}
