using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Leviatanes2.Clases
{
    public class RegistroPartidaTxt
    {
        public int IdPartida { get; set; }
        public string FechaHora { get; set; }
        public string JugadorUno { get; set; }
        public string JugadorDos { get; set; }
        public string Ganador { get; set; }
        public string Escenario { get; set; }
        public int TotalTurnos { get; set; }
        public string ArchivoBitacora { get; set; }
    }

    /// <summary>
    /// Modulo compartido de persistencia en archivos .TXT planos (reemplazo modular de Base de Datos SQL).
    /// Maneja: personajes.txt, jugadores.txt, partidas.txt y bitacoras de combate .txt.
    /// </summary>
    public class GestorPersistenciaTxt
    {
        public static readonly string RutaArchivoPersonajes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "personajes.txt");
        public static readonly string RutaArchivoJugadores = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jugadores.txt");
        public static readonly string RutaArchivoPartidas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "partidas.txt");

        public GestorPersistenciaTxt()
        {
            AsegurarCatalogoInicialEnTxt();
            AsegurarJugadoresInicialesEnTxt();
            AsegurarPartidasInicialesEnTxt();
        }

        // =========================================================================
        // 1. PERSISTENCIA DE PERSONAJES / CARTAS (personajes.txt)
        // Formato: Id|Nombre|Faccion|Vida|Defensa|Ataque|Empatia|NombreEspecial|DescripcionEspecial
        // =========================================================================
        private void AsegurarCatalogoInicialEnTxt()
        {
            if (File.Exists(RutaArchivoPersonajes))
                return;

            List<Personaje> Base = ObtenerDoceCartasPorDefecto();
            GuardarListaCompletaPersonajes(Base);
        }

        public static List<Personaje> ObtenerDoceCartasPorDefecto()
        {
            List<Personaje> Catalogo = new List<Personaje>();

            // 3 de Fuego (HP + DEF + ATK = 100)
            Catalogo.Add(new CartaFuego(1, "Ignis Leviatan Pirico", 45, 20, 35, 40, "Llamarada Solar", "Ignora el escudo rival e inflige danio perforante."));
            Catalogo.Add(new CartaFuego(2, "Vulcano Acorazado", 55, 25, 20, 65, "Erupcion De Magma", "Desata magma ardiente que perfora defensas."));
            Catalogo.Add(new CartaFuego(3, "Kael Fauces De Ceniza", 40, 20, 40, 30, "Impacto Volcanico", "Ataque igneo letal de maxima potencia."));

            // 3 de Agua (HP + DEF + ATK = 100)
            Catalogo.Add(new CartaAgua(4, "Nereida Del Abismo", 55, 25, 20, 85, "Marea Sanadora", "Roba 1 energia, cura 25% HP y golpea al rival."));
            Catalogo.Add(new CartaAgua(5, "Kraken Del Caribe", 60, 25, 15, 75, "Vortice Abisal", "Drena energia enemiga y regenera su vitalidad."));
            Catalogo.Add(new CartaAgua(6, "Hidra De Coral", 50, 20, 30, 60, "Lanza De Salmuera", "Absorbe vigor rival y ejecuta embestida marina."));

            // 3 de Tierra (HP + DEF + ATK = 100)
            Catalogo.Add(new CartaTierra(7, "Golem De Basalto", 60, 30, 10, 90, "Muralla Monolitica", "Sube su escudo y aplasta sumando ATK + DEF."));
            Catalogo.Add(new CartaTierra(8, "Behemoth Del Canion", 55, 25, 20, 80, "Raices Tectonicas", "Refuerza su coraza rocosa y sacude el suelo."));
            Catalogo.Add(new CartaTierra(9, "Rocco Coloso De Jade", 50, 25, 25, 70, "Martillo Sismico", "Impacto pesado potenciado por su armadura."));

            // 3 de Aire (HP + DEF + ATK = 100)
            Catalogo.Add(new CartaAire(10, "Cefiro Sierpe Alada", 45, 25, 30, 75, "Tormenta Cortante", "Ejecuta un doble impacto aereo consecutivo."));
            Catalogo.Add(new CartaAire(11, "Valkiria Del Huracan", 50, 20, 30, 80, "Rafaga Ascendente", "Dos golpes veloces con cuchillas de viento."));
            Catalogo.Add(new CartaAire(12, "Boreas Soberano Celeste", 40, 25, 35, 50, "Ciclon Del Norte", "Doble embestida critica desde las alturas."));

            return Catalogo;
        }

        public List<Personaje> CargarPersonajesDesdeTxt()
        {
            AsegurarCatalogoInicialEnTxt();
            List<Personaje> Lista = new List<Personaje>();

            try
            {
                string[] Lineas = File.ReadAllLines(RutaArchivoPersonajes, Encoding.UTF8);
                for (int I = 0; I < Lineas.Length; I++)
                {
                    string Linea = Lineas[I];
                    if (string.IsNullOrWhiteSpace(Linea) || Linea.TrimStart().StartsWith("#"))
                        continue;

                    string[] Partes = Linea.Split('|');
                    if (Partes.Length >= 9)
                    {
                        int Id = int.Parse(Partes[0].Trim());
                        string Nombre = Partes[1].Trim();
                        Elemento Faccion = (Elemento)Enum.Parse(typeof(Elemento), Partes[2].Trim(), true);
                        int Vida = int.Parse(Partes[3].Trim());
                        int Defensa = int.Parse(Partes[4].Trim());
                        int Ataque = int.Parse(Partes[5].Trim());
                        int Empatia = int.Parse(Partes[6].Trim());
                        string Especial = Partes[7].Trim();
                        string Descripcion = Partes[8].Trim();

                        Personaje Carta = InstanciarPorFaccion(Id, Nombre, Faccion, Vida, Defensa, Ataque, Empatia, Especial, Descripcion);
                        Lista.Add(Carta);
                    }
                }
            }
            catch (Exception)
            {
                if (Lista.Count == 0)
                {
                    Lista = ObtenerDoceCartasPorDefecto();
                }
            }

            return Lista;
        }

        public static Personaje InstanciarPorFaccion(
            int Id,
            string Nombre,
            Elemento Faccion,
            int Vida,
            int Defensa,
            int Ataque,
            int Empatia,
            string Especial,
            string Descripcion)
        {
            switch (Faccion)
            {
                case Elemento.Fuego:
                    return new CartaFuego(Id, Nombre, Vida, Defensa, Ataque, Empatia, Especial, Descripcion);
                case Elemento.Agua:
                    return new CartaAgua(Id, Nombre, Vida, Defensa, Ataque, Empatia, Especial, Descripcion);
                case Elemento.Tierra:
                    return new CartaTierra(Id, Nombre, Vida, Defensa, Ataque, Empatia, Especial, Descripcion);
                default:
                    return new CartaAire(Id, Nombre, Vida, Defensa, Ataque, Empatia, Especial, Descripcion);
            }
        }

        public Personaje RegistrarNuevoPersonajeEnTxt(
            string Nombre,
            Elemento Faccion,
            int Vida,
            int Defensa,
            int Ataque,
            int Empatia,
            string NombreEspecial,
            string DescripcionEspecial)
        {
            List<Personaje> Actuales = CargarPersonajesDesdeTxt();
            int NuevoId = 1;
            for (int I = 0; I < Actuales.Count; I++)
            {
                if (Actuales[I].IdPersonaje >= NuevoId)
                    NuevoId = Actuales[I].IdPersonaje + 1;
            }

            string NombreLimpio = LimpiarTexto(Nombre);
            string EspecialLimpio = LimpiarTexto(string.IsNullOrWhiteSpace(NombreEspecial) ? "Furia Ancestral" : NombreEspecial);
            string DescLimpia = LimpiarTexto(string.IsNullOrWhiteSpace(DescripcionEspecial) ? "Tecnica especial del Leviatan." : DescripcionEspecial);

            Personaje Nuevo = InstanciarPorFaccion(NuevoId, NombreLimpio, Faccion, Vida, Defensa, Ataque, Empatia, EspecialLimpio, DescLimpia);

            string Linea = FormatearLineaPersonaje(Nuevo);
            using (StreamWriter Escritor = new StreamWriter(RutaArchivoPersonajes, true, Encoding.UTF8))
            {
                Escritor.WriteLine(Linea);
            }

            return Nuevo;
        }

        public bool EliminarPersonajeDeTxt(int IdPersonaje)
        {
            List<Personaje> Actuales = CargarPersonajesDesdeTxt();
            int Eliminados = Actuales.RemoveAll(p => p.IdPersonaje == IdPersonaje);
            if (Eliminados > 0)
            {
                GuardarListaCompletaPersonajes(Actuales);
                return true;
            }
            return false;
        }

        public void RestaurarCatalogoPorDefecto()
        {
            List<Personaje> Base = ObtenerDoceCartasPorDefecto();
            GuardarListaCompletaPersonajes(Base);
        }

        public void GuardarListaCompletaPersonajes(List<Personaje> Lista)
        {
            using (StreamWriter Escritor = new StreamWriter(RutaArchivoPersonajes, false, Encoding.UTF8))
            {
                Escritor.WriteLine("# ID|Nombre|Faccion|Vida|Defensa|Ataque|Empatia|NombreEspecial|DescripcionEspecial");
                for (int I = 0; I < Lista.Count; I++)
                {
                    Escritor.WriteLine(FormatearLineaPersonaje(Lista[I]));
                }
            }
        }

        private static string FormatearLineaPersonaje(Personaje P)
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                P.IdPersonaje,
                LimpiarTexto(P.Nombre),
                P.Faccion,
                P.VidaMaxima,
                P.DefensaBase,
                P.AtaqueBase,
                P.Empatia,
                LimpiarTexto(P.NombreEspecial),
                LimpiarTexto(P.DescripcionEspecial));
        }

        // =========================================================================
        // 2. PERSISTENCIA DE PERFILES DE JUGADORES (jugadores.txt)
        // Formato: IdJugador|NombreUsuario|PartidasJugadas|Victorias|Derrotas|PorcentajeExito
        // =========================================================================
        private void AsegurarJugadoresInicialesEnTxt()
        {
            if (File.Exists(RutaArchivoJugadores))
                return;

            List<PerfilJugador> Iniciales = new List<PerfilJugador>
            {
                new PerfilJugador(1, "JugadorUno", 3, 2, 1),
                new PerfilJugador(2, "JugadorDos", 3, 1, 2),
                new PerfilJugador(3, "CapitanCaribe", 1, 1, 0)
            };
            GuardarJugadoresEnTxt(Iniciales);
        }

        public List<PerfilJugador> CargarJugadoresDesdeTxt()
        {
            AsegurarJugadoresInicialesEnTxt();
            List<PerfilJugador> Jugadores = new List<PerfilJugador>();

            try
            {
                string[] Lineas = File.ReadAllLines(RutaArchivoJugadores, Encoding.UTF8);
                for (int I = 0; I < Lineas.Length; I++)
                {
                    string Linea = Lineas[I];
                    if (string.IsNullOrWhiteSpace(Linea) || Linea.TrimStart().StartsWith("#"))
                        continue;

                    string[] Partes = Linea.Split('|');
                    if (Partes.Length >= 5)
                    {
                        int Id = int.Parse(Partes[0].Trim());
                        string Usuario = Partes[1].Trim();
                        int PJ = int.Parse(Partes[2].Trim());
                        int V = int.Parse(Partes[3].Trim());
                        int D = int.Parse(Partes[4].Trim());
                        Jugadores.Add(new PerfilJugador(Id, Usuario, PJ, V, D));
                    }
                }
            }
            catch (Exception)
            {
            }

            return Jugadores;
        }

        public void GuardarJugadoresEnTxt(List<PerfilJugador> Jugadores)
        {
            using (StreamWriter Escritor = new StreamWriter(RutaArchivoJugadores, false, Encoding.UTF8))
            {
                Escritor.WriteLine("# IdJugador|NombreUsuario|PartidasJugadas|Victorias|Derrotas|PorcentajeExito");
                for (int I = 0; I < Jugadores.Count; I++)
                {
                    PerfilJugador J = Jugadores[I];
                    Escritor.WriteLine(string.Format("{0}|{1}|{2}|{3}|{4}|{5:0.00}",
                        J.IdJugador,
                        LimpiarTexto(J.NombreUsuario),
                        J.PartidasJugadas,
                        J.Victorias,
                        J.Derrotas,
                        J.PorcentajeExito));
                }
            }
        }

        public PerfilJugador AutenticarORegistrarJugador(string NombreUsuario)
        {
            string NombreLimpio = LimpiarTexto(NombreUsuario);
            if (string.IsNullOrEmpty(NombreLimpio))
                NombreLimpio = "JugadorAnonimo";

            List<PerfilJugador> Jugadores = CargarJugadoresDesdeTxt();
            for (int I = 0; I < Jugadores.Count; I++)
            {
                if (string.Equals(Jugadores[I].NombreUsuario, NombreLimpio, StringComparison.OrdinalIgnoreCase))
                    return Jugadores[I];
            }

            int NuevoId = 1;
            for (int I = 0; I < Jugadores.Count; I++)
            {
                if (Jugadores[I].IdJugador >= NuevoId)
                    NuevoId = Jugadores[I].IdJugador + 1;
            }

            PerfilJugador Nuevo = new PerfilJugador(NuevoId, NombreLimpio, 0, 0, 0);
            Jugadores.Add(Nuevo);
            GuardarJugadoresEnTxt(Jugadores);
            return Nuevo;
        }

        // =========================================================================
        // 3. PERSISTENCIA DE HISTORIAL DE PARTIDAS Y BITACORAS (partidas.txt)
        // Formato: IdPartida|FechaHora|JugadorUno|JugadorDos|Ganador|Escenario|TotalTurnos|ArchivoBitacora
        // =========================================================================
        private void AsegurarPartidasInicialesEnTxt()
        {
            if (File.Exists(RutaArchivoPartidas))
                return;

            string NombreBitacoraDemo = "Bitacora_ArenaElemental_Demo01.txt";
            string RutaBitacoraDemo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NombreBitacoraDemo);

            if (!File.Exists(RutaBitacoraDemo))
            {
                using (StreamWriter EscritorLog = new StreamWriter(RutaBitacoraDemo, false, Encoding.UTF8))
                {
                    EscritorLog.WriteLine("======================================================================");
                    EscritorLog.WriteLine("       BITACORA OFICIAL DE COMBATE - LEVIATANES DEL CARIBE 2          ");
                    EscritorLog.WriteLine("======================================================================");
                    EscritorLog.WriteLine("Fecha y Hora      : 06/11/2026 18:30:00");
                    EscritorLog.WriteLine("Jugador 1         : JugadorUno");
                    EscritorLog.WriteLine("Jugador 2         : JugadorDos");
                    EscritorLog.WriteLine("Vencedor          : JugadorUno");
                    EscritorLog.WriteLine("Escenario Final   : Abismo Oceanico (+6 Agua / -4 Fuego)");
                    EscritorLog.WriteLine("Total de Turnos   : 6");
                    EscritorLog.WriteLine("----------------------------------------------------------------------");
                    EscritorLog.WriteLine("HISTORIAL CRONOLOGICO DE ACCIONES (PILA DE EVENTOS):");
                    EscritorLog.WriteLine("----------------------------------------------------------------------");
                    EscritorLog.WriteLine("[T01] [INICIO] Combate en [Abismo Oceanico]. Inicia por sorteo: JugadorUno.");
                    EscritorLog.WriteLine("[T01] Kraken Del Caribe ataca a Ignis Leviatan Pirico (Mult x1.50, Escenario +6): Vida recibe -9 HP.");
                    EscritorLog.WriteLine("[T02] Ignis Leviatan Pirico refuerza su postura defensiva (+9 Escudo -> Total: 29).");
                    EscritorLog.WriteLine("[T03] Kraken Del Caribe invoca [Vortice Abisal]! Roba 1 de energia, se cura +15 HP y causa 15 de danio.");
                    EscritorLog.WriteLine("[T03] [TERREMOTO INVERTIDO - Empatia 75>=65] Kraken Del Caribe armoniza con Abismo Oceanico: +9 HP y +6 Escudo!");
                    EscritorLog.WriteLine("[T04] [TERREMOTO CATASTROFICO] Ignis Leviatan Pirico pierde su escudo y sufre -7 HP por el sismo!");
                    EscritorLog.WriteLine("[T06] [FIN DE PARTIDA] El equipo de JugadorDos fue eliminado! Vencedor: JugadorUno!");
                    EscritorLog.WriteLine("======================================================================");
                }
            }

            using (StreamWriter Escritor = new StreamWriter(RutaArchivoPartidas, false, Encoding.UTF8))
            {
                Escritor.WriteLine("# IdPartida|FechaHora|JugadorUno|JugadorDos|Ganador|Escenario|TotalTurnos|ArchivoBitacora");
                Escritor.WriteLine(string.Format("1|06/11/2026 18:30:00|JugadorUno|JugadorDos|JugadorUno|Abismo Oceanico|6|{0}", NombreBitacoraDemo));
            }
        }

        public List<RegistroPartidaTxt> CargarHistorialPartidasDesdeTxt()
        {
            AsegurarPartidasInicialesEnTxt();
            List<RegistroPartidaTxt> Lista = new List<RegistroPartidaTxt>();

            try
            {
                string[] Lineas = File.ReadAllLines(RutaArchivoPartidas, Encoding.UTF8);
                for (int I = 0; I < Lineas.Length; I++)
                {
                    string Linea = Lineas[I];
                    if (string.IsNullOrWhiteSpace(Linea) || Linea.TrimStart().StartsWith("#"))
                        continue;

                    string[] Partes = Linea.Split('|');
                    if (Partes.Length >= 8)
                    {
                        RegistroPartidaTxt Reg = new RegistroPartidaTxt();
                        Reg.IdPartida = int.Parse(Partes[0].Trim());
                        Reg.FechaHora = Partes[1].Trim();
                        Reg.JugadorUno = Partes[2].Trim();
                        Reg.JugadorDos = Partes[3].Trim();
                        Reg.Ganador = Partes[4].Trim();
                        Reg.Escenario = Partes[5].Trim();
                        Reg.TotalTurnos = int.Parse(Partes[6].Trim());
                        Reg.ArchivoBitacora = Partes[7].Trim();
                        Lista.Add(Reg);
                    }
                }
            }
            catch (Exception)
            {
            }

            return Lista;
        }

        public string RegistrarPartidaFinalizadaEnTxt(
            PerfilJugador JugadorUno,
            PerfilJugador JugadorDos,
            PerfilJugador Ganador,
            Escenario EscenarioFinal,
            int TotalTurnos,
            Stack<string> PilaEventos)
        {
            // 1. Actualizar estadisticas en jugadores.txt
            List<PerfilJugador> Jugadores = CargarJugadoresDesdeTxt();
            bool EncontroJ1 = false;
            bool EncontroJ2 = false;
            bool GanoJ1 = (Ganador != null && string.Equals(Ganador.NombreUsuario, JugadorUno.NombreUsuario, StringComparison.OrdinalIgnoreCase));

            for (int I = 0; I < Jugadores.Count; I++)
            {
                if (string.Equals(Jugadores[I].NombreUsuario, JugadorUno.NombreUsuario, StringComparison.OrdinalIgnoreCase))
                {
                    Jugadores[I].RegistrarResultado(GanoJ1);
                    EncontroJ1 = true;
                }
                else if (string.Equals(Jugadores[I].NombreUsuario, JugadorDos.NombreUsuario, StringComparison.OrdinalIgnoreCase))
                {
                    Jugadores[I].RegistrarResultado(!GanoJ1);
                    EncontroJ2 = true;
                }
            }

            if (!EncontroJ1)
            {
                PerfilJugador NuevoJ1 = new PerfilJugador(Jugadores.Count + 1, JugadorUno.NombreUsuario, 0, 0, 0);
                NuevoJ1.RegistrarResultado(GanoJ1);
                Jugadores.Add(NuevoJ1);
            }
            if (!EncontroJ2)
            {
                PerfilJugador NuevoJ2 = new PerfilJugador(Jugadores.Count + 1, JugadorDos.NombreUsuario, 0, 0, 0);
                NuevoJ2.RegistrarResultado(!GanoJ1);
                Jugadores.Add(NuevoJ2);
            }
            GuardarJugadoresEnTxt(Jugadores);

            // 2. Generar archivo de bitacora .txt
            string RutaBitacora = ExportarBitacoraCombateTxt(JugadorUno, JugadorDos, Ganador, EscenarioFinal, TotalTurnos, PilaEventos);
            string NombreArchivoBitacora = Path.GetFileName(RutaBitacora);

            // 3. Agregar registro en partidas.txt
            List<RegistroPartidaTxt> Historial = CargarHistorialPartidasDesdeTxt();
            int NuevoId = Historial.Count + 1;
            string FechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string NombreEscenario = EscenarioFinal != null ? EscenarioFinal.Nombre : "Arena Elemental";
            string NombreGanador = Ganador != null ? Ganador.NombreUsuario : JugadorUno.NombreUsuario;

            string Linea = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",
                NuevoId,
                FechaActual,
                LimpiarTexto(JugadorUno.NombreUsuario),
                LimpiarTexto(JugadorDos.NombreUsuario),
                LimpiarTexto(NombreGanador),
                LimpiarTexto(NombreEscenario),
                TotalTurnos,
                LimpiarTexto(NombreArchivoBitacora));

            using (StreamWriter Escritor = new StreamWriter(RutaArchivoPartidas, true, Encoding.UTF8))
            {
                Escritor.WriteLine(Linea);
            }

            return RutaBitacora;
        }

        public static string ExportarBitacoraCombateTxt(
            PerfilJugador JugadorUno,
            PerfilJugador JugadorDos,
            PerfilJugador Ganador,
            Escenario EscenarioFinal,
            int TotalTurnos,
            Stack<string> PilaEventos)
        {
            string NombreArchivo = string.Format("Bitacora_Leviatanes2_{0}.txt", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            string RutaCompleta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NombreArchivo);

            try
            {
                using (StreamWriter Escritor = new StreamWriter(RutaCompleta, false, Encoding.UTF8))
                {
                    Escritor.WriteLine("======================================================================");
                    Escritor.WriteLine("       BITACORA OFICIAL DE COMBATE - LEVIATANES DEL CARIBE 2          ");
                    Escritor.WriteLine("======================================================================");
                    Escritor.WriteLine("Fecha y Hora      : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    Escritor.WriteLine("Jugador 1         : " + JugadorUno.NombreUsuario);
                    Escritor.WriteLine("Jugador 2         : " + JugadorDos.NombreUsuario);
                    Escritor.WriteLine("Vencedor          : " + (Ganador != null ? Ganador.NombreUsuario : "N/A"));
                    Escritor.WriteLine("Escenario Final   : " + (EscenarioFinal != null ? EscenarioFinal.Nombre : "Arena"));
                    Escritor.WriteLine("Total de Turnos   : " + TotalTurnos);
                    Escritor.WriteLine("----------------------------------------------------------------------");
                    Escritor.WriteLine("HISTORIAL CRONOLOGICO DE ACCIONES (DESDE EL INICIO HASTA EL FINAL):");
                    Escritor.WriteLine("----------------------------------------------------------------------");

                    if (PilaEventos != null && PilaEventos.Count > 0)
                    {
                        string[] ArregloEventos = PilaEventos.ToArray();
                        Array.Reverse(ArregloEventos);
                        for (int Indice = 0; Indice < ArregloEventos.Length; Indice++)
                        {
                            Escritor.WriteLine(ArregloEventos[Indice]);
                        }
                    }

                    Escritor.WriteLine("======================================================================");
                    Escritor.WriteLine("ESTADISTICAS ACTUALIZADAS:");
                    Escritor.WriteLine(string.Format(" - {0}: PJ={1} | V={2} | D={3} | Exito={4:0.00}%",
                        JugadorUno.NombreUsuario, JugadorUno.PartidasJugadas, JugadorUno.Victorias, JugadorUno.Derrotas, JugadorUno.PorcentajeExito));
                    Escritor.WriteLine(string.Format(" - {0}: PJ={1} | V={2} | D={3} | Exito={4:0.00}%",
                        JugadorDos.NombreUsuario, JugadorDos.PartidasJugadas, JugadorDos.Victorias, JugadorDos.Derrotas, JugadorDos.PorcentajeExito));
                    Escritor.WriteLine("======================================================================");
                }
            }
            catch (Exception Ex)
            {
                return "Error al guardar el archivo TXT: " + Ex.Message;
            }

            return RutaCompleta;
        }

        public string LeerContenidoBitacora(string NombreArchivoBitacora)
        {
            if (string.IsNullOrWhiteSpace(NombreArchivoBitacora))
                return "No se especifico un archivo de bitacora para esta partida.";

            string Ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NombreArchivoBitacora);
            if (!File.Exists(Ruta))
                return "El archivo de bitacora '" + NombreArchivoBitacora + "' no se encuentra en el directorio.";

            try
            {
                return File.ReadAllText(Ruta, Encoding.UTF8);
            }
            catch (Exception Ex)
            {
                return "Error al leer bitacora: " + Ex.Message;
            }
        }

        public static List<Escenario> ObtenerCatalogoEscenarios()
        {
            List<Escenario> Escenarios = new List<Escenario>();
            Escenarios.Add(new Escenario(1, "Volcan Activo", Elemento.Fuego, Elemento.Aire, 6, 4, 70, "Potencia a Fuego (+6) y sofoca a Aire (-4)."));
            Escenarios.Add(new Escenario(2, "Abismo Oceanico", Elemento.Agua, Elemento.Fuego, 6, 4, 65, "Potencia a Agua (+6) y apaga a Fuego (-4)."));
            Escenarios.Add(new Escenario(3, "Canion Rocoso", Elemento.Tierra, Elemento.Agua, 6, 4, 75, "Potencia a Tierra (+6) y contiene a Agua (-4)."));
            Escenarios.Add(new Escenario(4, "Cumbre De Viento", Elemento.Aire, Elemento.Tierra, 6, 4, 70, "Potencia a Aire (+6) y erosiona a Tierra (-4)."));
            return Escenarios;
        }

        private static string LimpiarTexto(string Valor)
        {
            if (string.IsNullOrEmpty(Valor))
                return string.Empty;
            return Valor.Replace("|", "/").Replace("\r", " ").Replace("\n", " ").Trim();
        }
    }
}
