using System;
using System.Collections.Generic;
using Leviatanes2.Clases;

namespace Leviatanes2.LogicaBat
{
	/// <summary>
	/// Motor de combate por turnos estilo Pokémon (Jugador vs CPU) para Leviatanes del Caribe 2.
	/// Gestiona los equipos (Líder + Cola de Relevo), el conteo de personajes derrotados de la CPU y del Jugador,
	/// las pausas entre caídas de personajes, el cálculo de ventaja elemental y el soporte bilingüe.
	/// </summary>
	public class MotorPartida
	{
		public PerfilJugador JugadorHumano { get; private set; }
		public PerfilJugador JugadorCpu { get; private set; }
		public Escenario EscenarioActual { get; private set; }

		public Personaje ActivoJugador { get; private set; }
		public Personaje ActivoCpu { get; private set; }

		public Queue<Personaje> ColaJugador { get; private set; }
		public Queue<Personaje> ColaCpu { get; private set; }

		// Listas para llevar el conteo e historial de los personajes que van perdiendo
		public List<Personaje> DerrotadosCpu { get; private set; }
		public List<Personaje> DerrotadosJugador { get; private set; }

		public Stack<string> PilaEventos { get; private set; }

		public int TurnoActual { get; private set; }
		public bool EsTurnoJugador { get; private set; }
		public bool PartidaTerminada { get; private set; }
		public PerfilJugador Ganador { get; private set; }
		public bool IdiomaIngles { get; set; }

		// Banderas de pausa cuando un personaje cae derrotado (0 HP) antes de que entre el siguiente
		public bool EsperandoRelevoJugadorCaido { get; private set; }
		public bool EsperandoRelevoCpuCaido { get; private set; }

		private readonly Random GeneradorAzar;

		public MotorPartida(PerfilJugador jugador, PerfilJugador cpu, Escenario escenario, bool enIngles)
		{
			GeneradorAzar = new Random();
			JugadorHumano = jugador;
			JugadorCpu = cpu;
			EscenarioActual = escenario;
			IdiomaIngles = enIngles;

			ColaJugador = new Queue<Personaje>();
			ColaCpu = new Queue<Personaje>();
			DerrotadosCpu = new List<Personaje>();
			DerrotadosJugador = new List<Personaje>();
			PilaEventos = new Stack<string>();

			InicializarCombate();
		}

		public MotorPartida(PerfilJugador jugador, PerfilJugador cpu, Escenario escenario)
			: this(jugador, cpu, escenario, false)
		{
		}

		/// <summary>
		/// Traduce el nombre del elemento cuando la batalla está en modo Inglés (Batalla 2).
		/// </summary>
		public static string FormatearElemento(Elemento elem, bool enIngles)
		{
			if (!enIngles)
				return elem.ToString().ToUpper();

			switch (elem)
			{
				case Elemento.Agua:
					return "WATER";
				case Elemento.Fuego:
					return "FIRE";
				case Elemento.Tierra:
					return "EARTH";
				default:
					return "AIR";
			}
		}

		/// <summary>
		/// Prepara los equipos clonando las cartas para la batalla, asigna el Líder activo
		/// de cada bando y registra el inicio del duelo.
		/// </summary>
		public void InicializarCombate()
		{
			ColaJugador.Clear();
			ColaCpu.Clear();
			DerrotadosCpu.Clear();
			DerrotadosJugador.Clear();
			PilaEventos.Clear();

			EsperandoRelevoJugadorCaido = false;
			EsperandoRelevoCpuCaido = false;

			// Cargar equipo del Jugador
			if (JugadorHumano != null && JugadorHumano.MazoSeleccionado != null)
			{
				for (int i = 0; i < JugadorHumano.MazoSeleccionado.Count; i++)
				{
					Personaje clon = JugadorHumano.MazoSeleccionado[i].ClonarParaBatalla();
					if (i == 0)
						ActivoJugador = clon;
					else
						ColaJugador.Enqueue(clon);
				}
			}

			// Cargar equipo de la CPU
			if (JugadorCpu != null && JugadorCpu.MazoSeleccionado != null)
			{
				for (int i = 0; i < JugadorCpu.MazoSeleccionado.Count; i++)
				{
					Personaje clon = JugadorCpu.MazoSeleccionado[i].ClonarParaBatalla();
					if (i == 0)
						ActivoCpu = clon;
					else
						ColaCpu.Enqueue(clon);
				}
			}

			TurnoActual = 1;
			EsTurnoJugador = true;
			PartidaTerminada = false;
			Ganador = null;

			string nombreEscenario = EscenarioActual != null ? EscenarioActual.Nombre : (IdiomaIngles ? "Elemental Arena" : "Arena Elemental");
			if (IdiomaIngles)
			{
				RegistrarEvento(string.Format("[T{0:00}] [START] Battle begins at [{1}]! {2} sends out {3} [{4}] vs {5} with {6} [{7}].",
					TurnoActual,
					nombreEscenario,
					JugadorHumano != null ? JugadorHumano.NombreUsuario : "Player",
					ActivoJugador != null ? ActivoJugador.Nombre : "Leviathan",
					ActivoJugador != null ? FormatearElemento(ActivoJugador.Faccion, true) : "",
					JugadorCpu != null ? JugadorCpu.NombreUsuario : "CPU",
					ActivoCpu != null ? ActivoCpu.Nombre : "CPU Leviathan",
					ActivoCpu != null ? FormatearElemento(ActivoCpu.Faccion, true) : ""));
			}
			else
			{
				RegistrarEvento(string.Format("[T{0:00}] [INICIO] ¡Comienza la batalla en [{1}]! {2} envía a {3} [{4}] vs {5} con {6} [{7}].",
					TurnoActual,
					nombreEscenario,
					JugadorHumano != null ? JugadorHumano.NombreUsuario : "Jugador",
					ActivoJugador != null ? ActivoJugador.Nombre : "Leviatán",
					ActivoJugador != null ? ActivoJugador.Faccion.ToString() : "",
					JugadorCpu != null ? JugadorCpu.NombreUsuario : "CPU",
					ActivoCpu != null ? ActivoCpu.Nombre : "Leviatán CPU",
					ActivoCpu != null ? ActivoCpu.Faccion.ToString() : ""));
			}
		}

		/// <summary>
		/// Determina la relación de ventaja elemental entre el atacante y el defensor.
		/// Ciclo Elemental: Agua > Fuego > Aire > Tierra > Agua.
		/// </summary>
		public static string ObtenerTextoVentajaElemental(Personaje atacante, Personaje defensor, bool enIngles)
		{
			if (atacante == null || defensor == null)
				return enIngles ? "No elemental data" : "Sin datos elementales";

			double mult = atacante.ObtenerMultiplicadorElemental(defensor.Faccion);
			string elemAtq = FormatearElemento(atacante.Faccion, enIngles);
			string elemDef = FormatearElemento(defensor.Faccion, enIngles);

			if (mult > 1.0)
			{
				return enIngles
					? string.Format("ELEMENTAL ADVANTAGE! {0} beats {1} (Damage x{2:0.00} - It's super effective!)", elemAtq, elemDef, mult)
					: string.Format("¡VENTAJA ELEMENTAL! {0} vence a {1} (Daño x{2:0.00} - ¡Es súper eficaz!)", elemAtq, elemDef, mult);
			}
			else if (mult < 1.0)
			{
				return enIngles
					? string.Format("ELEMENTAL DISADVANTAGE: {0} is weak against {1} (Damage x{2:0.00} - Not very effective...)", elemAtq, elemDef, mult)
					: string.Format("DESVENTAJA ELEMENTAL: {0} es débil ante {1} (Daño x{2:0.00} - No es muy eficaz...)", elemAtq, elemDef, mult);
			}
			else
			{
				return enIngles
					? string.Format("NEUTRAL ELEMENT: {0} vs {1} (Normal Damage x1.00)", elemAtq, elemDef)
					: string.Format("ELEMENTO NEUTRAL: {0} vs {1} (Daño x1.00 normal)", elemAtq, elemDef);
			}
		}

		/// <summary>
		/// Ejecuta la acción de ATACAR del Jugador considerando el elemento y el escenario.
		/// </summary>
		public string EjecutarAtaqueJugador()
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null || ActivoCpu == null)
				return IdiomaIngles ? "You cannot attack right now." : "No puedes atacar en este momento.";

			if (ActivoJugador.EnergiaActual < Personaje.CostoAtaque)
				return IdiomaIngles
					? "Not enough Energy to Attack (Requires 1 Energy). Use 'Charge Energy'."
					: "No tienes suficiente Energía para Atacar (Requiere 1 Energía). Usa 'Cargar Energía'.";

			double mult = ActivoJugador.ObtenerMultiplicadorElemental(ActivoCpu.Faccion);
			int bonoEsc = EscenarioActual != null ? EscenarioActual.ObtenerModificadorEscenario(ActivoJugador) : 0;
			int escudoAntes = ActivoCpu.EscudoActual;

			int danioVida = ActivoJugador.Atacar(ActivoCpu, bonoEsc);
			int escudoAbsorbido = Math.Max(0, escudoAntes - ActivoCpu.EscudoActual);

			string msg;
			if (IdiomaIngles)
			{
				string eficacia = mult > 1.0 ? "It's super effective! (Elemental Advantage x1.5)" :
				                  mult < 1.0 ? "It's not very effective... (Elemental Disadvantage x0.75)" :
				                  "Neutral elemental hit (x1.0).";

				msg = string.Format("[T{0:00}] {1} [{2}] attacks {3} [{4}]! {5} Shield absorbs {6} and deals -{7} HP.",
					TurnoActual, ActivoJugador.Nombre, FormatearElemento(ActivoJugador.Faccion, true),
					ActivoCpu.Nombre, FormatearElemento(ActivoCpu.Faccion, true), eficacia, escudoAbsorbido, danioVida);
			}
			else
			{
				string eficacia = mult > 1.0 ? "¡Es súper eficaz! (Ventaja Elemental x1.5)" :
				                  mult < 1.0 ? "No es muy eficaz... (Desventaja Elemental x0.75)" :
				                  "Impacto elemental neutro (x1.0).";

				msg = string.Format("[T{0:00}] ¡{1} [{2}] ataca a {3} [{4}]! {5} Escudo absorbe {6} y causa -{7} HP.",
					TurnoActual, ActivoJugador.Nombre, ActivoJugador.Faccion,
					ActivoCpu.Nombre, ActivoCpu.Faccion, eficacia, escudoAbsorbido, danioVida);
			}

			RegistrarEvento(msg);
			VerificarBajaCpuTrasAccionJugador(ref msg);
			return msg;
		}

		/// <summary>
		/// Ejecuta la HABILIDAD ESPECIAL del Leviatán activo del Jugador.
		/// </summary>
		public string EjecutarEspecialJugador()
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null || ActivoCpu == null)
				return IdiomaIngles ? "You cannot use the special skill right now." : "No puedes usar la habilidad especial ahora.";

			if (!ActivoJugador.EspecialListo)
				return IdiomaIngles
					? "Special Skill requires 100% Special Charge and 2 Energy."
					: "La Habilidad Especial requiere 100% de Carga Especial y 2 de Energía.";

			double mult = ActivoJugador.ObtenerMultiplicadorElemental(ActivoCpu.Faccion);
			int vidaAntesCpu = ActivoCpu.VidaActual;
			int vidaAntesPj = ActivoJugador.VidaActual;
			int escudoAntesPj = ActivoJugador.EscudoActual;

			string detalleEspecial = ActivoJugador.UsarHabilidadEspecial(ActivoCpu);
			string msg;

			if (IdiomaIngles)
			{
				int danioTotal = Math.Max(0, vidaAntesCpu - ActivoCpu.VidaActual);
				int curacion = Math.Max(0, ActivoJugador.VidaActual - vidaAntesPj);
				int escudoGanado = Math.Max(0, ActivoJugador.EscudoActual - escudoAntesPj);
				string eficacia = mult > 1.0 ? " With ELEMENTAL ADVANTAGE (x1.5)!" :
				                  mult < 1.0 ? " (With elemental disadvantage x0.75)" : "";

				msg = string.Format("[T{0:00}] [SPECIAL] {1} unleashes [{2}] on {3}! Deals -{4} HP{5}{6}.{7}",
					TurnoActual,
					ActivoJugador.Nombre,
					ActivoJugador.NombreEspecial,
					ActivoCpu.Nombre,
					danioTotal,
					curacion > 0 ? string.Format(", heals +{0} HP", curacion) : "",
					escudoGanado > 0 ? string.Format(", gains +{0} Shield", escudoGanado) : "",
					eficacia);
			}
			else
			{
				string eficacia = mult > 1.0 ? " ¡Con VENTAJA ELEMENTAL (x1.5)!" :
				                  mult < 1.0 ? " (Con desventaja elemental x0.75)" : "";
				msg = string.Format("[T{0:00}] [ESPECIAL] {1}{2}", TurnoActual, detalleEspecial, eficacia);
			}

			RegistrarEvento(msg);
			VerificarBajaCpuTrasAccionJugador(ref msg);
			return msg;
		}

		/// <summary>
		/// Ejecuta la acción de DEFENDER del Jugador (+Escudo).
		/// </summary>
		public string EjecutarDefensaJugador()
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null)
				return IdiomaIngles ? "You cannot defend right now." : "No puedes defender en este momento.";

			if (ActivoJugador.EnergiaActual < Personaje.CostoDefensa)
				return IdiomaIngles
					? "Not enough Energy to Defend (Requires 1 Energy). Use 'Charge Energy'."
					: "No tienes suficiente Energía para Defender (Requiere 1 Energía). Usa 'Cargar Energía'.";

			int escudoGanado = ActivoJugador.Defender();
			string msg = IdiomaIngles
				? string.Format("[T{0:00}] {1} [{2}] takes a defensive stance: gains +{3} Shield (Current Shield: {4}).",
					TurnoActual, ActivoJugador.Nombre, FormatearElemento(ActivoJugador.Faccion, true), escudoGanado, ActivoJugador.EscudoActual)
				: string.Format("[T{0:00}] {1} [{2}] adopta postura defensiva: gana +{3} de Escudo (Escudo actual: {4}).",
					TurnoActual, ActivoJugador.Nombre, ActivoJugador.Faccion, escudoGanado, ActivoJugador.EscudoActual);

			RegistrarEvento(msg);
			EsTurnoJugador = false;
			return msg;
		}

		/// <summary>
		/// Cambia al Leviatán activo del Jugador por el personaje ubicado en el índice indicado de la banca.
		/// </summary>
		public string EjecutarCambioPersonajeJugador(int indiceSeleccionado)
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null)
				return IdiomaIngles ? "You cannot switch Leviathan right now." : "No puedes cambiar de Leviatán ahora.";

			if (ColaJugador.Count == 0)
				return IdiomaIngles ? "No more Leviathans available on your bench." : "No tienes más Leviatanes en la banca.";

			Personaje[] arregloCola = ColaJugador.ToArray();
			int indiceElegido = indiceSeleccionado;

			if (indiceElegido < 0 || indiceElegido >= arregloCola.Length)
			{
				indiceElegido = 0;
				double multActual = (ActivoCpu != null) ? ActivoJugador.ObtenerMultiplicadorElemental(ActivoCpu.Faccion) : 1.0;
				if (multActual <= 1.0 && ActivoCpu != null)
				{
					for (int i = 0; i < arregloCola.Length; i++)
					{
						if (arregloCola[i].ObtenerMultiplicadorElemental(ActivoCpu.Faccion) > 1.0)
						{
							indiceElegido = i;
							break;
						}
					}
				}
			}

			if (ActivoJugador.EnergiaActual >= Personaje.CostoRelevo)
			{
				ActivoJugador.EnergiaActual -= Personaje.CostoRelevo;
			}

			Personaje saliente = ActivoJugador;
			Personaje entrante = arregloCola[indiceElegido];

			ColaJugador.Clear();
			for (int i = 0; i < arregloCola.Length; i++)
			{
				if (i != indiceElegido)
				{
					ColaJugador.Enqueue(arregloCola[i]);
				}
			}
			ColaJugador.Enqueue(saliente);

			ActivoJugador = entrante;
			if (ActivoJugador.EnergiaActual < 1)
			{
				ActivoJugador.EnergiaActual = 1;
			}

			string ventajaNuevo = ObtenerTextoVentajaElemental(ActivoJugador, ActivoCpu, IdiomaIngles);
			string msg = IdiomaIngles
				? string.Format("[T{0:00}] [SWITCH] {1} returns to bench and {2} [{3}] enters battle! {4}",
					TurnoActual, saliente.Nombre, ActivoJugador.Nombre, FormatearElemento(ActivoJugador.Faccion, true), ventajaNuevo)
				: string.Format("[T{0:00}] [CAMBIO DE PERSONAJE] ¡Regresa {1} a la banca y entra {2} [{3}] al combate! {4}",
					TurnoActual, saliente.Nombre, ActivoJugador.Nombre, ActivoJugador.Faccion, ventajaNuevo);

			RegistrarEvento(msg);
			EsTurnoJugador = false;
			return msg;
		}

		public string EjecutarRelevoConVentajaJugador()
		{
			return EjecutarCambioPersonajeJugador(-1);
		}

		/// <summary>
		/// Recarga +2 de Energía y +25% de Carga Especial cuando el jugador decide concentrar poder.
		/// </summary>
		public string EjecutarCargaEnergiaJugador()
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null)
				return IdiomaIngles ? "It is not your turn." : "No es tu turno.";

			ActivoJugador.EnergiaActual += 2;
			ActivoJugador.CargaEspecial += 25;

			string msg = IdiomaIngles
				? string.Format("[T{0:00}] {1} [{2}] focuses elemental power: +2 Energy (Total: {3}) and +25% Special Charge ({4}%).",
					TurnoActual, ActivoJugador.Nombre, FormatearElemento(ActivoJugador.Faccion, true), ActivoJugador.EnergiaActual, ActivoJugador.CargaEspecial)
				: string.Format("[T{0:00}] {1} [{2}] concentra energía elemental: +2 Energía (Total: {3}) y +25% Carga Especial ({4}%).",
					TurnoActual, ActivoJugador.Nombre, ActivoJugador.Faccion, ActivoJugador.EnergiaActual, ActivoJugador.CargaEspecial);

			RegistrarEvento(msg);
			EsTurnoJugador = false;
			return msg;
		}

		/// <summary>
		/// Ejecuta el turno inteligente de la CPU. Si el personaje del jugador pierde toda su vida,
		/// activa el estado de pausa (EsperandoRelevoJugadorCaido) para que no cambie instantáneamente.
		/// </summary>
		public string EjecutarTurnoCpu()
		{
			if (PartidaTerminada || ActiveCombatantsInvalid())
				return IdiomaIngles ? "The battle has ended." : "El combate ha finalizado.";

			string msgAccion = "";
			double multCpu = ActivoCpu.ObtenerMultiplicadorElemental(ActivoJugador.Faccion);

			// 1. Si tiene habilidad especial lista, la CPU la desata
			if (ActivoCpu.EspecialListo)
			{
				int vidaAntesPj = ActivoJugador.VidaActual;
				string esp = ActivoCpu.UsarHabilidadEspecial(ActivoJugador);
				int danioTotal = Math.Max(0, vidaAntesPj - ActivoJugador.VidaActual);

				if (IdiomaIngles)
				{
					string efic = multCpu > 1.0 ? " Super effective (x1.5)!" : (multCpu < 1.0 ? " (Not very effective x0.75)" : "");
					msgAccion = string.Format("[T{0:00}] [CPU SPECIAL] {1} uses [{2}]! Deals -{3} HP to {4}.{5}",
						TurnoActual, ActivoCpu.Nombre, ActivoCpu.NombreEspecial, danioTotal, ActivoJugador.Nombre, efic);
				}
				else
				{
					string efic = multCpu > 1.0 ? " ¡Súper eficaz (x1.5)!" : (multCpu < 1.0 ? " (No muy eficaz x0.75)" : "");
					msgAccion = string.Format("[T{0:00}] [CPU ESPECIAL] {1}{2}", TurnoActual, esp, efic);
				}
			}
			// 2. Si la CPU está en desventaja elemental (x0.75), tiene compañeros con ventaja en la cola y energía, cambia (45% prob)
			else if (multCpu < 1.0 && ColaCpu.Count > 0 && ActivoCpu.EnergiaActual >= Personaje.CostoRelevo && TieneVentajaEnCola(ColaCpu, ActivoJugador.Faccion) && GeneradorAzar.Next(100) < 45)
			{
				ActivoCpu.EnergiaActual -= Personaje.CostoRelevo;
				Personaje saliente = ActivoCpu;

				int total = ColaCpu.Count;
				for (int i = 0; i < total; i++)
				{
					Personaje candidato = ColaCpu.Dequeue();
					if (ActivoCpu == saliente && candidato.ObtenerMultiplicadorElemental(ActivoJugador.Faccion) > 1.0)
					{
						ActivoCpu = candidato;
					}
					else
					{
						ColaCpu.Enqueue(candidato);
					}
				}
				ColaCpu.Enqueue(saliente);

				msgAccion = IdiomaIngles
					? string.Format("[T{0:00}] [CPU SWITCH] CPU withdraws {1} due to elemental disadvantage and sends out {2} [{3}]!",
						TurnoActual, saliente.Nombre, ActivoCpu.Nombre, FormatearElemento(ActivoCpu.Faccion, true))
					: string.Format("[T{0:00}] [CPU RELEVO] ¡La CPU retira a {1} por desventaja elemental y envía a {2} [{3}]!",
						TurnoActual, saliente.Nombre, ActivoCpu.Nombre, ActivoCpu.Faccion);
			}
			// 3. Si tiene poca vida y sin escudo, puede defenderse (30% prob)
			else if (ActivoCpu.VidaActual < (ActivoCpu.VidaMaxima * 0.45) && ActivoCpu.EscudoActual == 0 && ActivoCpu.EnergiaActual >= Personaje.CostoDefensa && GeneradorAzar.Next(100) < 30)
			{
				int esc = ActivoCpu.Defender();
				msgAccion = IdiomaIngles
					? string.Format("[T{0:00}] [CPU DEFEND] {1} [{2}] reinforces its armor and gains +{3} Shield.",
						TurnoActual, ActivoCpu.Nombre, FormatearElemento(ActivoCpu.Faccion, true), esc)
					: string.Format("[T{0:00}] [CPU DEFENSA] {1} [{2}] refuerza su coraza y gana +{3} de Escudo.",
						TurnoActual, ActivoCpu.Nombre, ActivoCpu.Faccion, esc);
			}
			// 4. Ataque estándar si tiene energía
			else if (ActivoCpu.EnergiaActual >= Personaje.CostoAtaque)
			{
				int bonoEsc = EscenarioActual != null ? EscenarioActual.ObtenerModificadorEscenario(ActivoCpu) : 0;
				int escudoAntes = ActivoJugador.EscudoActual;
				int danioVida = ActivoCpu.Atacar(ActivoJugador, bonoEsc);
				int escudoAbsorbido = Math.Max(0, escudoAntes - ActivoJugador.EscudoActual);

				if (IdiomaIngles)
				{
					string efic = multCpu > 1.0 ? "It's super effective! (CPU Elemental Advantage x1.5)" :
					              multCpu < 1.0 ? "Not very effective... (CPU Elemental Disadvantage x0.75)" :
					              "Neutral hit (x1.0).";

					msgAccion = string.Format("[T{0:00}] [CPU ATTACK] {1} [{2}] attacks your {3} [{4}]! {5} Shield absorbs {6} and you take -{7} HP.",
						TurnoActual, ActivoCpu.Nombre, FormatearElemento(ActivoCpu.Faccion, true),
						ActivoJugador.Nombre, FormatearElemento(ActivoJugador.Faccion, true), efic, escudoAbsorbido, danioVida);
				}
				else
				{
					string efic = multCpu > 1.0 ? "¡Es súper eficaz! (Ventaja Elemental CPU x1.5)" :
					              multCpu < 1.0 ? "No es muy eficaz... (Desventaja Elemental CPU x0.75)" :
					              "Impacto neutro (x1.0).";

					msgAccion = string.Format("[T{0:00}] [CPU ATACA] ¡{1} [{2}] ataca a tu {3} [{4}]! {5} Escudo absorbe {6} y recibes -{7} HP.",
						TurnoActual, ActivoCpu.Nombre, ActivoCpu.Faccion,
						ActivoJugador.Nombre, ActivoJugador.Faccion, efic, escudoAbsorbido, danioVida);
				}
			}
			// 5. Sin energía suficiente -> Recarga energía
			else
			{
				ActivoCpu.EnergiaActual += 2;
				ActivoCpu.CargaEspecial += 25;
				msgAccion = IdiomaIngles
					? string.Format("[T{0:00}] [CPU CHARGE] {1} [{2}] recharges +2 Energy.",
						TurnoActual, ActivoCpu.Nombre, FormatearElemento(ActivoCpu.Faccion, true))
					: string.Format("[T{0:00}] [CPU CARGA] {1} [{2}] recarga +2 de Energía.",
						TurnoActual, ActivoCpu.Nombre, ActivoCpu.Faccion);
			}

			RegistrarEvento(msgAccion);

			// Verificar si el Leviatán del jugador cayó debilitado
			if (!ActivoJugador.EstaVivo)
			{
				if (!DerrotadosJugador.Contains(ActivoJugador))
				{
					DerrotadosJugador.Add(ActivoJugador);
				}

				RegistrarEvento(IdiomaIngles
					? string.Format("[T{0:00}] [PLAYER KO] Your {1} [{2}] was defeated!", TurnoActual, ActivoJugador.Nombre, FormatearElemento(ActivoJugador.Faccion, true))
					: string.Format("[T{0:00}] [BAJA EN TU EQUIPO] ¡Tu {1} [{2}] cayó derrotado!", TurnoActual, ActivoJugador.Nombre, ActivoJugador.Faccion));

				if (ColaJugador.Count > 0)
				{
					// Activar pausa antes de meter al siguiente personaje del jugador
					EsperandoRelevoJugadorCaido = true;
					Personaje siguiente = ColaJugador.Peek();
					string avisoPausa = IdiomaIngles
						? string.Format("\r\n💀 YOUR {0} FAINTED! Preparing {1} [{2}] to enter battle...",
							ActivoJugador.Nombre.ToUpper(), siguiente.Nombre, FormatearElemento(siguiente.Faccion, true))
						: string.Format("\r\n💀 ¡TU {0} CAYÓ DERROTADO! Preparando la entrada de {1} [{2}]...",
							ActivoJugador.Nombre.ToUpper(), siguiente.Nombre, siguiente.Faccion);
					msgAccion += avisoPausa;
					return msgAccion;
				}
				else
				{
					PartidaTerminada = true;
					Ganador = JugadorCpu;
					string fin = IdiomaIngles
						? string.Format("\r\n💀 All your Leviathans were defeated! Winner: {0}.", JugadorCpu.NombreUsuario)
						: string.Format("\r\n💀 ¡Todos tus Leviatanes fueron derrotados! Ganador: {0}.", JugadorCpu.NombreUsuario);
					msgAccion += fin;

					RegistrarEvento(IdiomaIngles
						? string.Format("[T{0:00}] [MATCH OVER] Victory for {1}.", TurnoActual, JugadorCpu.NombreUsuario)
						: string.Format("[T{0:00}] [FIN DE PARTIDA] Victoria para {1}.", TurnoActual, JugadorCpu.NombreUsuario));
					return msgAccion;
				}
			}

			AvanzarNuevoTurnoGlobal(ref msgAccion);
			return msgAccion;
		}

		/// <summary>
		/// Se ejecuta después de la pausa cuando un personaje del Jugador pierde,
		/// dando entrada al siguiente personaje del equipo.
		/// </summary>
		public string ProcesarEntradaSiguienteJugadorTrasDerrota()
		{
			if (!EsperandoRelevoJugadorCaido || ColaJugador.Count == 0)
			{
				EsperandoRelevoJugadorCaido = false;
				return "";
			}

			EsperandoRelevoJugadorCaido = false;
			ActivoJugador = ColaJugador.Dequeue();
			if (ActivoJugador.EnergiaActual < 2)
			{
				ActivoJugador.EnergiaActual = 2;
			}

			string ventaja = ObtenerTextoVentajaElemental(ActivoJugador, ActivoCpu, IdiomaIngles);
			string msg = IdiomaIngles
				? string.Format("⚡ Go, {0} [{1}]! ({2})\r\nIt is your turn to act!",
					ActivoJugador.Nombre, FormatearElemento(ActivoJugador.Faccion, true), ventaja)
				: string.Format("⚡ ¡Adelante, {0} [{1}]! ({2})\r\n¡Es tu turno de elegir acción!",
					ActivoJugador.Nombre, ActivoJugador.Faccion, ventaja);

			RegistrarEvento(IdiomaIngles
				? string.Format("[T{0:00}] [ENTRY] {1} [{2}] enters the battlefield.", TurnoActual, ActivoJugador.Nombre, FormatearElemento(ActivoJugador.Faccion, true))
				: string.Format("[T{0:00}] [ENTRADA] Entra a combatir {1} [{2}].", TurnoActual, ActivoJugador.Nombre, ActivoJugador.Faccion));

			string dummy = "";
			AvanzarNuevoTurnoGlobal(ref dummy);
			return msg;
		}

		/// <summary>
		/// Verifica si el personaje de la CPU cayó tras el ataque del jugador.
		/// Si cayó, lo agrega a DerrotadosCpu y activa una pausa antes de enviar al siguiente.
		/// </summary>
		private void VerificarBajaCpuTrasAccionJugador(ref string msgAccion)
		{
			if (!ActivoCpu.EstaVivo)
			{
				if (!DerrotadosCpu.Contains(ActivoCpu))
				{
					DerrotadosCpu.Add(ActivoCpu);
				}

				int totalEquipoCpu = JugadorCpu != null && JugadorCpu.MazoSeleccionado != null ? JugadorCpu.MazoSeleccionado.Count : (DerrotadosCpu.Count + ColaCpu.Count);

				RegistrarEvento(IdiomaIngles
					? string.Format("[T{0:00}] [CPU DEFEATED #{1}/{2}] Enemy {3} [{4}] was defeated!",
						TurnoActual, DerrotadosCpu.Count, totalEquipoCpu, ActivoCpu.Nombre, FormatearElemento(ActivoCpu.Faccion, true))
					: string.Format("[T{0:00}] [BAJA CPU #{1}/{2}] ¡{3} [{4}] de la CPU fue derrotado!",
						TurnoActual, DerrotadosCpu.Count, totalEquipoCpu, ActivoCpu.Nombre, ActivoCpu.Faccion));

				if (ColaCpu.Count > 0)
				{
					EsperandoRelevoCpuCaido = true;
					EsTurnoJugador = false;
					Personaje siguienteCpu = ColaCpu.Peek();

					string derrotadoMsg = IdiomaIngles
						? string.Format("\r\n🎯 Enemy {0} was defeated! (CPU Defeated: {1}/{2}). CPU is preparing {3} [{4}]...",
							ActivoCpu.Nombre, DerrotadosCpu.Count, totalEquipoCpu, siguienteCpu.Nombre, FormatearElemento(siguienteCpu.Faccion, true))
						: string.Format("\r\n🎯 ¡Derrotaste a {0} de la CPU! (Derrotados CPU: {1}/{2}). La CPU prepara a {3} [{4}]...",
							ActivoCpu.Nombre, DerrotadosCpu.Count, totalEquipoCpu, siguienteCpu.Nombre, siguienteCpu.Faccion);
					msgAccion += derrotadoMsg;
				}
				else
				{
					PartidaTerminada = true;
					Ganador = JugadorHumano;
					string victoria = IdiomaIngles
						? string.Format("\r\n🏆 VICTORY! You defeated all {0}/{1} Leviathans of {2}! Next stage unlocked!",
							DerrotadosCpu.Count, totalEquipoCpu, JugadorCpu.NombreUsuario)
						: string.Format("\r\n🏆 ¡VICTORIA! Derrotaste a los {0}/{1} Leviatanes de {2}. ¡Siguiente batalla desbloqueada!",
							DerrotadosCpu.Count, totalEquipoCpu, JugadorCpu.NombreUsuario);
					msgAccion += victoria;

					RegistrarEvento(IdiomaIngles
						? string.Format("[T{0:00}] [MATCH OVER] {1}'s team was eliminated! Winner: {2}.", TurnoActual, JugadorCpu.NombreUsuario, JugadorHumano.NombreUsuario)
						: string.Format("[T{0:00}] [FIN DE PARTIDA] ¡El equipo de {1} fue eliminado! Vencedor: {2}.", TurnoActual, JugadorCpu.NombreUsuario, JugadorHumano.NombreUsuario));
				}
			}
			else
			{
				EsTurnoJugador = false;
			}
		}

		/// <summary>
		/// Se ejecuta después de la pausa cuando un personaje de la CPU fue derrotado,
		/// dando entrada al siguiente personaje de la CPU e iniciando el nuevo turno.
		/// </summary>
		public string ProcesarEntradaSiguienteCpuTrasDerrota()
		{
			if (!EsperandoRelevoCpuCaido || ColaCpu.Count == 0)
			{
				EsperandoRelevoCpuCaido = false;
				return "";
			}

			EsperandoRelevoCpuCaido = false;
			ActivoCpu = ColaCpu.Dequeue();

			string ventaja = ObtenerTextoVentajaElemental(ActivoJugador, ActivoCpu, IdiomaIngles);
			string msg = IdiomaIngles
				? string.Format("⚠ CPU sends out {0} [{1}]! (CPU Defeated: {2}/{3})\r\n{4}",
					ActivoCpu.Nombre, FormatearElemento(ActivoCpu.Faccion, true),
					DerrotadosCpu.Count, JugadorCpu.MazoSeleccionado.Count, ventaja)
				: string.Format("⚠ ¡La CPU envía a {0} [{1}] al combate! (Derrotados CPU: {2}/{3})\r\n{4}",
					ActivoCpu.Nombre, ActivoCpu.Faccion,
					DerrotadosCpu.Count, JugadorCpu.MazoSeleccionado.Count, ventaja);

			RegistrarEvento(IdiomaIngles
				? string.Format("[T{0:00}] [CPU ENTRY] CPU sends out {1} [{2}].", TurnoActual, ActivoCpu.Nombre, FormatearElemento(ActivoCpu.Faccion, true))
				: string.Format("[T{0:00}] [ENTRADA CPU] La CPU envía a {1} [{2}].", TurnoActual, ActivoCpu.Nombre, ActivoCpu.Faccion));

			string dummy = "";
			AvanzarNuevoTurnoGlobal(ref dummy);
			return msg;
		}

		private void AvanzarNuevoTurnoGlobal(ref string msgAccion)
		{
			if (PartidaTerminada)
				return;

			TurnoActual++;
			EsTurnoJugador = true;

			if (ActivoJugador != null && ActivoJugador.EstaVivo)
				ActivoJugador.RecargarInicioTurno(TurnoActual);

			if (ActivoCpu != null && ActivoCpu.EstaVivo)
				ActivoCpu.RecargarInicioTurno(TurnoActual);

			if (EscenarioActual != null && TurnoActual % 3 == 0)
			{
				string sismoJ1 = EscenarioActual.AplicarTerremoto(ActivoJugador, TurnoActual);
				string sismoCpu = EscenarioActual.AplicarTerremoto(ActivoCpu, TurnoActual);

				if (IdiomaIngles)
				{
					sismoJ1 = "[EARTHQUAKE EVENT] Seismic wave hits the arena!";
					sismoCpu = "[EARTHQUAKE EVENT] CPU is shaken by the tremor!";
				}

				RegistrarEvento(string.Format("[T{0:00}] {1}", TurnoActual, sismoJ1));
				RegistrarEvento(string.Format("[T{0:00}] {1}", TurnoActual, sismoCpu));
				msgAccion += string.Format(" | {0}", sismoJ1);

				if (!ActivoJugador.EstaVivo)
				{
					ActivoJugador.VidaActual = 1;
				}
				if (!ActivoCpu.EstaVivo)
				{
					ActivoCpu.VidaActual = 1;
				}
			}
		}

		private static bool TieneVentajaEnCola(Queue<Personaje> cola, Elemento elementoRival)
		{
			foreach (Personaje p in cola)
			{
				if (p.ObtenerMultiplicadorElemental(elementoRival) > 1.0)
					return true;
			}
			return false;
		}

		private bool ActiveCombatantsInvalid()
		{
			return ActivoJugador == null || ActivoCpu == null;
		}

		public int TotalVivosJugador
		{
			get
			{
				int vivos = (ActivoJugador != null && ActivoJugador.EstaVivo) ? 1 : 0;
				return vivos + ColaJugador.Count;
			}
		}

		public int TotalVivosCpu
		{
			get
			{
				int vivos = (ActivoCpu != null && ActivoCpu.EstaVivo) ? 1 : 0;
				return vivos + ColaCpu.Count;
			}
		}

		private void RegistrarEvento(string texto)
		{
			PilaEventos.Push(texto);
		}
	}
}
