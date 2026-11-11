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
	/// Ventana de combate por turnos (Jugador vs CPU).
	/// </summary>
	public partial class Batalla : Form
	{
		public int numeroBatalla = 1;
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

		private void ConfigurarBatalla()
		{
			tmrTurnoCpu.Stop();
			tmrPausaDerrota.Stop();

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

			Personaje pj = motorCombate.ActivoJugador;
			Personaje cpu = motorCombate.ActivoCpu;
			int totalEquipoCpu = motorCombate.JugadorCpu.MazoSeleccionado.Count;
			int derrotadosCpuCant = motorCombate.DerrotadosCpu.Count;
			int totalEquipoPj = motorCombate.JugadorHumano.MazoSeleccionado.Count;
			int derrotadosPjCant = motorCombate.DerrotadosJugador.Count;

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

			if (cpu != null)
			{
				lblNombreCpu.Text = cpu.Nombre + (!cpu.EstaVivo ? " ✖" : "");
				lblElementoCpu.Text = MotorPartida.FormatearElemento(cpu.Faccion, this.esIngles);
				lblElementoCpu.BackColor = ObtenerColorElemento(cpu.Faccion);

				int porcVidaCpu = (int)Math.Round(((double)cpu.VidaActual / (double)cpu.VidaMaxima) * 100.0);
				pbVidaCpu.Value = Math.Min(100, Math.Max(0, porcVidaCpu));

				if (this.esIngles)
				{
					lblStatsCpu.Text = string.Format("HP: {0}/{1}   |   Shield: {2}   |   Energy: {3}",
						cpu.VidaActual, cpu.VidaMaxima, cpu.EscudoActual, cpu.EnergiaActual);
					lblBolitasCpu.Text = string.Format("CPU Team: {0}  (Defeated: {1}/{2})",
						GenerarIconosEquipo(motorCombate.TotalVivosCpu, totalEquipoCpu), derrotadosCpuCant, totalEquipoCpu);
				}
				else
				{
					lblStatsCpu.Text = string.Format("HP: {0}/{1}   |   Escudo: {2}   |   Energía: {3}",
						cpu.VidaActual, cpu.VidaMaxima, cpu.EscudoActual, cpu.EnergiaActual);
					lblBolitasCpu.Text = string.Format("Equipo CPU: {0}  (Derrotados: {1}/{2})",
						GenerarIconosEquipo(motorCombate.TotalVivosCpu, totalEquipoCpu), derrotadosCpuCant, totalEquipoCpu);
				}
			}

			if (pj != null)
			{
				lblNombreJugador.Text = pj.Nombre + (!pj.EstaVivo ? " 💀" : "");
				lblElementoJugador.Text = MotorPartida.FormatearElemento(pj.Faccion, this.esIngles);
				lblElementoJugador.BackColor = ObtenerColorElemento(pj.Faccion);

				int porcVidaPj = (int)Math.Round(((double)pj.VidaActual / (double)pj.VidaMaxima) * 100.0);
				pbVidaJugador.Value = Math.Min(100, Math.Max(0, porcVidaPj));

				if (this.esIngles)
				{
					lblStatsJugador.Text = string.Format("HP: {0}/{1}   |   Shield: {2}   |   Energy: {3}",
						pj.VidaActual, pj.VidaMaxima, pj.EscudoActual, pj.EnergiaActual);
					lblEspecialInfoJugador.Text = string.Format("Special: {0} ({1}%)", pj.NombreEspecial, pj.CargaEspecial);
					lblBolitasJugador.Text = string.Format("Your Team: {0}  (Fainted: {1}/{2})",
						GenerarIconosEquipo(motorCombate.TotalVivosJugador, totalEquipoPj), derrotadosPjCant, totalEquipoPj);
				}
				else
				{
					lblStatsJugador.Text = string.Format("HP: {0}/{1}   |   Escudo: {2}   |   Energía: {3}",
						pj.VidaActual, pj.VidaMaxima, pj.EscudoActual, pj.EnergiaActual);
					lblEspecialInfoJugador.Text = string.Format("Especial: {0} ({1}%)", pj.NombreEspecial, pj.CargaEspecial);
					lblBolitasJugador.Text = string.Format("Tu Equipo: {0}  (Bajas: {1}/{2})",
						GenerarIconosEquipo(motorCombate.TotalVivosJugador, totalEquipoPj), derrotadosPjCant, totalEquipoPj);
				}

				pbEspecialJugador.Value = Math.Min(100, Math.Max(0, pj.CargaEspecial));
			}

			ActualizarBannerVentajaElemental(pj, cpu);

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
				lstColaJugador.Items.Add(string.Format("{0} [{1}]  ({2}/{3} HP){4}",
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

			pnlSpriteCpu.Invalidate();
			pnlSpriteJugador.Invalidate();
			pnlArenaCombate.Invalidate();
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

		private void ActualizarBannerVentajaElemental(Personaje pj, Personaje cpu)
		{
			if (pj == null || cpu == null)
				return;

			double multJugador = pj.ObtenerMultiplicadorElemental(cpu.Faccion);
			string elemPj = MotorPartida.FormatearElemento(pj.Faccion, this.esIngles);
			string elemCpu = MotorPartida.FormatearElemento(cpu.Faccion, this.esIngles);

			if (multJugador > 1.0)
			{
				pnlVentajaElemental.BackColor = Color.FromArgb(20, 65, 38);
				lblVentajaElemental.ForeColor = Color.LightGreen;
				lblVentajaElemental.Text = this.esIngles
					? string.Format("★ Elemental Advantage: {0} beats {1} (Damage x1.5)", elemPj, elemCpu)
					: string.Format("★ Ventaja Elemental: {0} vence a {1} (Daño x1.5)", elemPj, elemCpu);
			}
			else if (multJugador < 1.0)
			{
				pnlVentajaElemental.BackColor = Color.FromArgb(75, 28, 24);
				lblVentajaElemental.ForeColor = Color.FromArgb(255, 190, 120);
				lblVentajaElemental.Text = this.esIngles
					? string.Format("⚠ Elemental Disadvantage: {0} beats {1} (Consider switching)", elemCpu, elemPj)
					: string.Format("⚠ Desventaja Elemental: {0} vence a {1} (Puedes cambiar)", elemCpu, elemPj);
			}
			else
			{
				pnlVentajaElemental.BackColor = Color.FromArgb(28, 45, 68);
				lblVentajaElemental.ForeColor = Color.Aquamarine;
				lblVentajaElemental.Text = this.esIngles
					? string.Format("⚖ Neutral Matchup: {0} vs {1} (Damage x1.0)", elemPj, elemCpu)
					: string.Format("⚖ Duelo Neutral: {0} vs {1} (Daño x1.0)", elemPj, elemCpu);
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

		private void BtnAtacarClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			lblDialogoCombate.Text = motorCombate.EjecutarAtaqueJugador();
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnEspecialClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			lblDialogoCombate.Text = motorCombate.EjecutarEspecialJugador();
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnDefenderClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			lblDialogoCombate.Text = motorCombate.EjecutarDefensaJugador();
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
			ActualizarInterfazCompleta();
			ContinuarFlujoConPausas();
		}

		private void BtnCargarEnergiaClick(object sender, EventArgs e)
		{
			if (motorCombate == null || !motorCombate.EsTurnoJugador || motorCombate.PartidaTerminada)
				return;

			lblDialogoCombate.Text = motorCombate.EjecutarCargaEnergiaJugador();
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

			lblDialogoCombate.Text = motorCombate.EjecutarTurnoCpu();
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

		private void PnlArenaCombatePaint(object sender, PaintEventArgs e)
		{
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
				Color.FromArgb(10, 16, 28),
				Color.FromArgb(24, 38, 60),
				LinearGradientMode.Vertical))
			{
				g.FillRectangle(fondo, rect);
			}

			using (Pen lapizGrid = new Pen(Color.FromArgb(22, 180, 220, 255), 1))
			{
				for (int x = 0; x < rect.Width; x += 20)
					g.DrawLine(lapizGrid, x, 0, x, rect.Height);
				for (int y = 0; y < rect.Height; y += 20)
					g.DrawLine(lapizGrid, 0, y, rect.Width, y);
			}

			int cx = rect.Width / 2;
			int cy = rect.Height / 2;

			using (SolidBrush sombra = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
			using (SolidBrush plataforma = new SolidBrush(Color.FromArgb(90, colorElem)))
			using (Pen bordeOro = new Pen(p.EstaVivo ? Color.FromArgb(245, 203, 66) : Color.Crimson, 2f))
			{
				g.FillEllipse(sombra, cx - 85, cy + 24, 170, 28);
				g.FillEllipse(plataforma, cx - 80, cy + 20, 160, 24);
				g.DrawEllipse(bordeOro, cx - 80, cy + 20, 160, 24);
			}

			if (p.EscudoActual > 0 && p.EstaVivo)
			{
				using (Pen lapizEscudo = new Pen(Color.FromArgb(170, 100, 220, 255), 2.5f))
				{
					g.DrawEllipse(lapizEscudo, cx - 48, cy - 44, 96, 88);
				}
			}

			// Cargar PNG del Leviatan si existe en la carpeta imagenes
			if (p.EstaVivo)
			{
				using (Image img = GestorPersistenciaTxt.CargarImagenPersonaje(p.IdPersonaje, p.Nombre))
				{
					if (img != null)
					{
						int maxW = rect.Width - 28;
						int maxH = rect.Height - 20;
						float escala = Math.Min((float)maxW / img.Width, (float)maxH / img.Height);
						int dw = Math.Max(1, (int)(img.Width * escala));
						int dh = Math.Max(1, (int)(img.Height * escala));
						int dx = (rect.Width - dw) / 2;
						int dy = (rect.Height - dh) / 2;

						g.InterpolationMode = InterpolationMode.HighQualityBicubic;
						g.DrawImage(img, new Rectangle(dx, dy, dw, dh));
						return;
					}
				}
			}

			// Respaldo: Sello Runico de Leviatan
			Point[] rombo = new Point[]
			{
				new Point(cx, cy - 34),
				new Point(cx + 34, cy),
				new Point(cx, cy + 34),
				new Point(cx - 34, cy)
			};

			using (SolidBrush brochaCuerpo = new SolidBrush(colorElem))
			using (Pen bordeOro = new Pen(p.EstaVivo ? Color.FromArgb(245, 203, 66) : Color.Red, 2.5f))
			using (Font fuenteElem = new Font("Consolas", 11F, FontStyle.Bold))
			{
				g.FillPolygon(brochaCuerpo, rombo);
				g.DrawPolygon(bordeOro, rombo);

				string elemTxt = p.EstaVivo
					? MotorPartida.FormatearElemento(p.Faccion, this.esIngles).Substring(0, 2).ToUpper()
					: "KO";

				SizeF tam = g.MeasureString(elemTxt, fuenteElem);
				g.DrawString(elemTxt, fuenteElem, Brushes.White, cx - (tam.Width / 2f), cy - (tam.Height / 2f));
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