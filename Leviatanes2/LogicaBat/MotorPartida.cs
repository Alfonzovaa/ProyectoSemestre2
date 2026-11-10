using System;
using System.Collections.Generic;
using Leviatanes2.Clases;

namespace Leviatanes2.LogicaBat
{
	/// <summary>
	/// Motor de combate por turnos estilo Pokémon (Jugador vs CPU) para Leviatanes del Caribe 2.
	/// Mantiene todas las mecánicas (ventaja elemental, escudo, energía, especial, relevo y pausas por caída)
	/// con mensajes breves y directos.
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

		public List<Personaje> DerrotadosCpu { get; private set; }
		public List<Personaje> DerrotadosJugador { get; private set; }

		public Stack<string> PilaEventos { get; private set; }

		public int TurnoActual { get; private set; }
		public bool EsTurnoJugador { get; private set; }
		public bool PartidaTerminada { get; private set; }
		public PerfilJugador Ganador { get; private set; }
		public bool IdiomaIngles { get; set; }

		public bool EsperandoRelevoJugadorCaido { get; private set; }
		public bool EsperandoRelevoCpuCaido { get; private set; }

		private readonly Random GeneradorAzar;
		private int proximoTurnoTerremoto;

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

		public void InicializarCombate()
		{
			ColaJugador.Clear();
			ColaCpu.Clear();
			DerrotadosCpu.Clear();
			DerrotadosJugador.Clear();
			PilaEventos.Clear();

			EsperandoRelevoJugadorCaido = false;
			EsperandoRelevoCpuCaido = false;

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

			// Momento aleatorio para el primer evento Terremoto (entre el turno 2 y el 4)
			proximoTurnoTerremoto = GeneradorAzar.Next(2, 5);

			if (IdiomaIngles)
			{
				RegistrarEvento(string.Format("Battle starts: {0} vs {1}.",
					ActivoJugador != null ? ActivoJugador.Nombre : "Player",
					ActivoCpu != null ? ActivoCpu.Nombre : "CPU"));
			}
			else
			{
				RegistrarEvento(string.Format("Inicia el combate: {0} vs {1}.",
					ActivoJugador != null ? ActivoJugador.Nombre : "Jugador",
					ActivoCpu != null ? ActivoCpu.Nombre : "CPU"));
			}
		}

		public static string ObtenerTextoVentajaElemental(Personaje atacante, Personaje defensor, bool enIngles)
		{
			if (atacante == null || defensor == null)
				return "";

			double mult = atacante.ObtenerMultiplicadorElemental(defensor.Faccion);

			if (mult > 1.0)
				return enIngles ? "Advantage (x1.5)" : "Ventaja (x1.5)";
			if (mult < 1.0)
				return enIngles ? "Disadvantage (x0.75)" : "Desventaja (x0.75)";

			return enIngles ? "Neutral (x1.0)" : "Neutral (x1.0)";
		}

		public string EjecutarAtaqueJugador()
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null || ActivoCpu == null)
				return IdiomaIngles ? "Wait for your turn." : "Espera tu turno.";

			if (ActivoJugador.EnergiaActual < Personaje.CostoAtaque)
				return IdiomaIngles ? "Need 1 Energy to attack." : "Necesitas 1 de Energía para atacar.";

			double mult = ActivoJugador.ObtenerMultiplicadorElemental(ActivoCpu.Faccion);
			int bonoEsc = EscenarioActual != null ? EscenarioActual.ObtenerModificadorEscenario(ActivoJugador) : 0;
			int escudoAntes = ActivoCpu.EscudoActual;

			int danioVida = ActivoJugador.Atacar(ActivoCpu, bonoEsc);
			int escudoAbs = Math.Max(0, escudoAntes - ActivoCpu.EscudoActual);

			string msg;
			if (IdiomaIngles)
			{
				string extra = mult > 1.0 ? " Super effective!" : (mult < 1.0 ? " Not very effective." : "");
				string infoEsc = escudoAbs > 0 ? string.Format(" (-{0} Shield)", escudoAbs) : "";
				msg = string.Format("⚔ {0} attacks {1}: -{2} HP{3}.{4}",
					ActivoJugador.Nombre, ActivoCpu.Nombre, danioVida, infoEsc, extra);
			}
			else
			{
				string extra = mult > 1.0 ? " ¡Súper eficaz!" : (mult < 1.0 ? " Poco eficaz." : "");
				string infoEsc = escudoAbs > 0 ? string.Format(" (-{0} Escudo)", escudoAbs) : "";
				msg = string.Format("⚔ {0} ataca a {1}: -{2} HP{3}.{4}",
					ActivoJugador.Nombre, ActivoCpu.Nombre, danioVida, infoEsc, extra);
			}

			RegistrarEvento(msg);
			VerificarBajaCpuTrasAccionJugador(ref msg);
			return msg;
		}

		public string EjecutarEspecialJugador()
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null || ActivoCpu == null)
				return IdiomaIngles ? "Wait for your turn." : "Espera tu turno.";

			if (!ActivoJugador.EspecialListo)
				return IdiomaIngles ? "Requires 100% Charge and 2 Energy." : "Requiere 100% de carga y 2 de Energía.";

			int vidaAntesCpu = ActivoCpu.VidaActual;
			int vidaAntesPj = ActivoJugador.VidaActual;
			ActivoJugador.UsarHabilidadEspecial(ActivoCpu);

			int danio = Math.Max(0, vidaAntesCpu - ActivoCpu.VidaActual);
			int cura = Math.Max(0, ActivoJugador.VidaActual - vidaAntesPj);

			string msg;
			if (IdiomaIngles)
			{
				string txtCura = cura > 0 ? string.Format(" (+{0} HP healed)", cura) : "";
				msg = string.Format("⚡ {0} uses {1}: -{2} HP to {3}{4}!",
					ActivoJugador.Nombre, ActivoJugador.NombreEspecial, danio, ActivoCpu.Nombre, txtCura);
			}
			else
			{
				string txtCura = cura > 0 ? string.Format(" (+{0} HP curados)", cura) : "";
				msg = string.Format("⚡ {0} usa {1}: -{2} HP a {3}{4}.",
					ActivoJugador.Nombre, ActivoJugador.NombreEspecial, danio, ActivoCpu.Nombre, txtCura);
			}

			RegistrarEvento(msg);
			VerificarBajaCpuTrasAccionJugador(ref msg);
			return msg;
		}

		public string EjecutarDefensaJugador()
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null)
				return IdiomaIngles ? "Wait for your turn." : "Espera tu turno.";

			if (ActivoJugador.EnergiaActual < Personaje.CostoDefensa)
				return IdiomaIngles ? "Need 1 Energy to defend." : "Necesitas 1 de Energía para defender.";

			int escudoGanado = ActivoJugador.Defender();
			string msg = IdiomaIngles
				? string.Format("🛡 {0} defends (+{1} Shield).", ActivoJugador.Nombre, escudoGanado)
				: string.Format("🛡 {0} se defiende (+{1} Escudo).", ActivoJugador.Nombre, escudoGanado);

			RegistrarEvento(msg);
			EsTurnoJugador = false;
			return msg;
		}

		public string EjecutarCambioPersonajeJugador(int indiceSeleccionado)
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null)
				return IdiomaIngles ? "Wait for your turn." : "Espera tu turno.";

			if (ColaJugador.Count == 0)
				return IdiomaIngles ? "No more allies on bench." : "No quedan suplentes en la banca.";

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

			string msg = IdiomaIngles
				? string.Format("🔄 {0} switches out. Go, {1}!", saliente.Nombre, ActivoJugador.Nombre)
				: string.Format("🔄 Sale {0} y entra {1}.", saliente.Nombre, ActivoJugador.Nombre);

			RegistrarEvento(msg);
			EsTurnoJugador = false;
			return msg;
		}

		public string EjecutarRelevoConVentajaJugador()
		{
			return EjecutarCambioPersonajeJugador(-1);
		}

		public string EjecutarCargaEnergiaJugador()
		{
			if (PartidaTerminada || !EsTurnoJugador || EsperandoRelevoJugadorCaido || EsperandoRelevoCpuCaido || ActivoJugador == null)
				return IdiomaIngles ? "Wait for your turn." : "Espera tu turno.";

			ActivoJugador.EnergiaActual += 2;
			ActivoJugador.CargaEspecial += 25;

			string msg = IdiomaIngles
				? string.Format("🔋 {0} charges +2 Energy and +25% Special.", ActivoJugador.Nombre)
				: string.Format("🔋 {0} recarga +2 de Energía y +25% Especial.", ActivoJugador.Nombre);

			RegistrarEvento(msg);
			EsTurnoJugador = false;
			return msg;
		}

		public string EjecutarTurnoCpu()
		{
			if (PartidaTerminada || ActiveCombatantsInvalid())
				return IdiomaIngles ? "Battle ended." : "Combate finalizado.";

			string msgAccion = "";
			double multCpu = ActivoCpu.ObtenerMultiplicadorElemental(ActivoJugador.Faccion);

			if (ActivoCpu.EspecialListo)
			{
				int vidaAntesPj = ActivoJugador.VidaActual;
				ActivoCpu.UsarHabilidadEspecial(ActivoJugador);
				int danioTotal = Math.Max(0, vidaAntesPj - ActivoJugador.VidaActual);

				msgAccion = IdiomaIngles
					? string.Format("⚡ CPU {0} uses {1}: -{2} HP!", ActivoCpu.Nombre, ActivoCpu.NombreEspecial, danioTotal)
					: string.Format("⚡ CPU {0} usa {1}: -{2} HP.", ActivoCpu.Nombre, ActivoCpu.NombreEspecial, danioTotal);
			}
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
					? string.Format("🔄 CPU switches to {0}!", ActivoCpu.Nombre)
					: string.Format("🔄 La CPU cambia a {0}.", ActivoCpu.Nombre);
			}
			else if (ActivoCpu.VidaActual < (ActivoCpu.VidaMaxima * 0.45) && ActivoCpu.EscudoActual == 0 && ActivoCpu.EnergiaActual >= Personaje.CostoDefensa && GeneradorAzar.Next(100) < 30)
			{
				int esc = ActivoCpu.Defender();
				msgAccion = IdiomaIngles
					? string.Format("🛡 CPU {0} defends (+{1} Shield).", ActivoCpu.Nombre, esc)
					: string.Format("🛡 CPU {0} se defiende (+{1} Escudo).", ActivoCpu.Nombre, esc);
			}
			else if (ActivoCpu.EnergiaActual >= Personaje.CostoAtaque)
			{
				int bonoEsc = EscenarioActual != null ? EscenarioActual.ObtenerModificadorEscenario(ActivoCpu) : 0;
				int escudoAntes = ActivoJugador.EscudoActual;
				int danioVida = ActivoCpu.Atacar(ActivoJugador, bonoEsc);
				int escudoAbs = Math.Max(0, escudoAntes - ActivoJugador.EscudoActual);

				if (IdiomaIngles)
				{
					string infoEsc = escudoAbs > 0 ? string.Format(" (-{0} Shield)", escudoAbs) : "";
					msgAccion = string.Format("⚔ CPU {0} attacks: -{1} HP{2}.", ActivoCpu.Nombre, danioVida, infoEsc);
				}
				else
				{
					string infoEsc = escudoAbs > 0 ? string.Format(" (-{0} Escudo)", escudoAbs) : "";
					msgAccion = string.Format("⚔ CPU {0} ataca: -{1} HP{2}.", ActivoCpu.Nombre, danioVida, infoEsc);
				}
			}
			else
			{
				ActivoCpu.EnergiaActual += 2;
				ActivoCpu.CargaEspecial += 25;
				msgAccion = IdiomaIngles
					? string.Format("🔋 CPU {0} charges +2 Energy.", ActivoCpu.Nombre)
					: string.Format("🔋 CPU {0} recarga +2 de Energía.", ActivoCpu.Nombre);
			}

			RegistrarEvento(msgAccion);

			if (!ActivoJugador.EstaVivo)
			{
				if (!DerrotadosJugador.Contains(ActivoJugador))
				{
					DerrotadosJugador.Add(ActivoJugador);
				}

				if (ColaJugador.Count > 0)
				{
					EsperandoRelevoJugadorCaido = true;
					Personaje siguiente = ColaJugador.Peek();
					msgAccion += IdiomaIngles
						? string.Format("\r\n💀 {0} fainted! Sending out {1}...", ActivoJugador.Nombre, siguiente.Nombre)
						: string.Format("\r\n💀 ¡{0} cayó! Entrará {1}...", ActivoJugador.Nombre, siguiente.Nombre);
					return msgAccion;
				}
				else
				{
					PartidaTerminada = true;
					Ganador = JugadorCpu;
					msgAccion += IdiomaIngles
						? "\r\n💀 Your team was defeated!"
						: "\r\n💀 ¡Tu equipo fue derrotado!";
					return msgAccion;
				}
			}

			AvanzarNuevoTurnoGlobal(ref msgAccion);
			return msgAccion;
		}

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

			string msg = IdiomaIngles
				? string.Format("⚡ {0} enters the battle! Your turn.", ActivoJugador.Nombre)
				: string.Format("⚡ ¡{0} entra al combate! Tu turno.", ActivoJugador.Nombre);

			RegistrarEvento(msg);
			string dummy = "";
			AvanzarNuevoTurnoGlobal(ref dummy);
			return msg;
		}

		private void VerificarBajaCpuTrasAccionJugador(ref string msgAccion)
		{
			if (!ActivoCpu.EstaVivo)
			{
				if (!DerrotadosCpu.Contains(ActivoCpu))
				{
					DerrotadosCpu.Add(ActivoCpu);
				}

				int totalEquipoCpu = JugadorCpu != null && JugadorCpu.MazoSeleccionado != null ? JugadorCpu.MazoSeleccionado.Count : (DerrotadosCpu.Count + ColaCpu.Count);

				if (ColaCpu.Count > 0)
				{
					EsperandoRelevoCpuCaido = true;
					EsTurnoJugador = false;
					Personaje siguienteCpu = ColaCpu.Peek();

					msgAccion += IdiomaIngles
						? string.Format("\r\n🎯 {0} defeated ({1}/{2})! Next: {3}...", ActivoCpu.Nombre, DerrotadosCpu.Count, totalEquipoCpu, siguienteCpu.Nombre)
						: string.Format("\r\n🎯 ¡{0} derrotado ({1}/{2})! Siguiente: {3}...", ActivoCpu.Nombre, DerrotadosCpu.Count, totalEquipoCpu, siguienteCpu.Nombre);
				}
				else
				{
					PartidaTerminada = true;
					Ganador = JugadorHumano;
					msgAccion += IdiomaIngles
						? "\r\n🏆 VICTORY! You defeated the CPU team!"
						: "\r\n🏆 ¡VICTORIA! Derrotaste a todo el equipo CPU.";
				}
			}
			else
			{
				EsTurnoJugador = false;
			}
		}

		public string ProcesarEntradaSiguienteCpuTrasDerrota()
		{
			if (!EsperandoRelevoCpuCaido || ColaCpu.Count == 0)
			{
				EsperandoRelevoCpuCaido = false;
				return "";
			}

			EsperandoRelevoCpuCaido = false;
			ActivoCpu = ColaCpu.Dequeue();

			string msg = IdiomaIngles
				? string.Format("⚠ CPU sends out {0}! Your turn.", ActivoCpu.Nombre)
				: string.Format("⚠ ¡La CPU envía a {0}! Tu turno.", ActivoCpu.Nombre);

			RegistrarEvento(msg);
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

			// Evento Terremoto en un momento totalmente aleatorio de la partida
			if (TurnoActual == proximoTurnoTerremoto)
			{
				proximoTurnoTerremoto = TurnoActual + GeneradorAzar.Next(2, 5);
				string msgSismo = EjecutarTerremotoAleatorioEnCampo();
				if (!string.IsNullOrEmpty(msgSismo))
				{
					RegistrarEvento(msgSismo);
					if (string.IsNullOrEmpty(msgAccion))
						msgAccion = msgSismo;
					else
						msgAccion += "\r\n" + msgSismo;
				}
			}
		}

		/// <summary>
		/// Evento Terremoto aleatorio: quita una cantidad de vida totalmente aleatoria
		/// únicamente a los Leviatanes que están peleando en campo, excluyendo a los de tipo Aire.
		/// </summary>
		private string EjecutarTerremotoAleatorioEnCampo()
		{
			string detallePj = "";
			string detalleCpu = "";

			if (ActivoJugador != null && ActivoJugador.EstaVivo)
			{
				if (ActivoJugador.Faccion == Elemento.Aire)
				{
					detallePj = IdiomaIngles
						? string.Format("{0} [Air: immune]", ActivoJugador.Nombre)
						: string.Format("{0} [Aire: inmune]", ActivoJugador.Nombre);
				}
				else
				{
					int danioAzarPj = GeneradorAzar.Next(5, 19);
					ActivoJugador.VidaActual = Math.Max(1, ActivoJugador.VidaActual - danioAzarPj);
					detallePj = string.Format("{0}: -{1} HP", ActivoJugador.Nombre, danioAzarPj);
				}
			}

			if (ActivoCpu != null && ActivoCpu.EstaVivo)
			{
				if (ActivoCpu.Faccion == Elemento.Aire)
				{
					detalleCpu = IdiomaIngles
						? string.Format("{0} [Air: immune]", ActivoCpu.Nombre)
						: string.Format("{0} [Aire: inmune]", ActivoCpu.Nombre);
				}
				else
				{
					int danioAzarCpu = GeneradorAzar.Next(5, 19);
					ActivoCpu.VidaActual = Math.Max(1, ActivoCpu.VidaActual - danioAzarCpu);
					detalleCpu = string.Format("{0}: -{1} HP", ActivoCpu.Nombre, danioAzarCpu);
				}
			}

			return IdiomaIngles
				? string.Format("🌋 RANDOM EARTHQUAKE! {0} | {1}", detallePj, detalleCpu)
				: string.Format("🌋 ¡TERREMOTO ALEATORIO! {0} | {1}", detallePj, detalleCpu);
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
