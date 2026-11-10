using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

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

        // =========================================================================
        // 4. CARGA AUTOMÁTICA DE IMÁGENES DE PERSONAJES ("personaje 1", "personaje 2", etc.)
        // =========================================================================
        public static Image CargarImagenPersonaje(int idPersonaje, string nombrePersonaje = null)
        {
            SincronizarCarpetaImagenes();

            List<string> carpetas = ObtenerCarpetasImagenes();
            if (carpetas.Count == 0)
                return null;

            // 1. Buscar imagen exacta para el ID del personaje (ej: "personaje 1", "personaje 2"...)
            Image imgExacta = BuscarImagenPorNumeroPersonaje(carpetas, idPersonaje, nombrePersonaje);
            if (imgExacta != null)
                return imgExacta;

            // 2. Si es un personaje nuevo creado después de los 12 base (ID 13, 14...) y no existe "personaje 13",
            // probar con el índice relativo (13 -> "personaje 1", 14 -> "personaje 2", etc.)
            if (idPersonaje > 12 && idPersonaje < 100)
            {
                int idRelativo = ((idPersonaje - 1) % 12) + 1;
                Image imgRelativa = BuscarImagenPorNumeroPersonaje(carpetas, idRelativo, null);
                if (imgRelativa != null)
                    return imgRelativa;
            }

            // 3. Si es un personaje de la CPU en Batalla (IDs 101, 102, 201...) y no tiene "personaje 101",
            // asignarle una de las imágenes "personaje N" disponibles en la carpeta imagenes
            if (idPersonaje >= 100)
            {
                int slotCpu = (idPersonaje % 100);
                int batallaNum = (idPersonaje / 100);
                int idCandidato = ((batallaNum - 1) * 3) + slotCpu;
                Image imgCpu = BuscarImagenPorNumeroPersonaje(carpetas, idCandidato, null);
                if (imgCpu != null)
                    return imgCpu;

                // Si solo hay pocas imágenes (ej. personaje 1, personaje 2, personaje 3), usar slotCpu
                imgCpu = BuscarImagenPorNumeroPersonaje(carpetas, slotCpu, null);
                if (imgCpu != null)
                    return imgCpu;
            }

            return null;
        }

        private static Image BuscarImagenPorNumeroPersonaje(List<string> carpetas, int numeroBuscado, string nombrePersonaje)
        {
            if (numeroBuscado <= 0)
                return null;

            string[] extensiones = new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
            string[] variantesNombre = new string[]
            {
                "personaje " + numeroBuscado,
                "personaje" + numeroBuscado,
                "personaje_" + numeroBuscado,
                "personaje-" + numeroBuscado,
                "Personaje " + numeroBuscado,
                "Personaje" + numeroBuscado,
                "Personaje_" + numeroBuscado,
                "Personaje-" + numeroBuscado
            };

            foreach (string carpeta in carpetas)
            {
                if (!Directory.Exists(carpeta))
                    continue;

                // A. Búsqueda directa por nombre exacto ("personaje 1.png", "personaje 1.jpg", etc.)
                foreach (string baseName in variantesNombre)
                {
                    foreach (string ext in extensiones)
                    {
                        string ruta = Path.Combine(carpeta, baseName + ext);
                        if (File.Exists(ruta))
                        {
                            Image img = LeerImagenSinBloquear(ruta);
                            if (img != null)
                                return img;
                        }
                    }
                }

                // B. Búsqueda flexible recorriendo los archivos de la carpeta
                try
                {
                    string[] archivos = Directory.GetFiles(carpeta);
                    foreach (string archivo in archivos)
                    {
                        string ext = Path.GetExtension(archivo).ToLowerInvariant();
                        bool extValida = false;
                        for (int i = 0; i < extensiones.Length; i++)
                        {
                            if (ext == extensiones[i])
                            {
                                extValida = true;
                                break;
                            }
                        }
                        if (!extValida)
                            continue;

                        string soloNombre = Path.GetFileNameWithoutExtension(archivo).Trim();
                        int numExtraido = ExtraerNumeroDePersonaje(soloNombre);
                        if (numExtraido == numeroBuscado)
                        {
                            Image img = LeerImagenSinBloquear(archivo);
                            if (img != null)
                                return img;
                        }

                        if (!string.IsNullOrEmpty(nombrePersonaje) &&
                            string.Equals(soloNombre, nombrePersonaje.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            Image img = LeerImagenSinBloquear(archivo);
                            if (img != null)
                                return img;
                        }
                    }
                }
                catch
                {
                }
            }

            return null;
        }

        private static int ExtraerNumeroDePersonaje(string nombreArchivoSinExtension)
        {
            if (string.IsNullOrEmpty(nombreArchivoSinExtension))
                return -1;

            string limpio = nombreArchivoSinExtension.Trim().ToLowerInvariant();
            if (!limpio.StartsWith("personaje"))
                return -1;

            int idx = "personaje".Length;
            while (idx < limpio.Length && (limpio[idx] == ' ' || limpio[idx] == '_' || limpio[idx] == '-'))
            {
                idx++;
            }

            int inicioDigitos = idx;
            while (idx < limpio.Length && char.IsDigit(limpio[idx]))
            {
                idx++;
            }

            if (idx > inicioDigitos)
            {
                string numStr = limpio.Substring(inicioDigitos, idx - inicioDigitos);
                int resultado;
                if (int.TryParse(numStr, out resultado))
                    return resultado;
            }

            return -1;
        }

        private static Image LeerImagenSinBloquear(string rutaArchivo)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(rutaArchivo);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (Image original = Image.FromStream(ms))
                    {
                        return new Bitmap(original);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private static List<string> ObtenerCarpetasImagenes()
        {
            List<string> lista = new List<string>();
            string[] candidatas = new string[]
            {
                Path.Combine(Application.StartupPath, "..", "..", "imagenes"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "imagenes"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "imagenes"),
                Path.Combine(Application.StartupPath, "imagenes"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes"),
                Path.Combine(Directory.GetCurrentDirectory(), "imagenes"),
                "imagenes"
            };

            for (int i = 0; i < candidatas.Length; i++)
            {
                try
                {
                    if (Directory.Exists(candidatas[i]))
                    {
                        string full = Path.GetFullPath(candidatas[i]);
                        if (!lista.Contains(full))
                            lista.Add(full);
                    }
                }
                catch
                {
                }
            }

            return lista;
        }

        public static void SincronizarCarpetaImagenes()
        {
            try
            {
                string rutaProyecto = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "imagenes"));
                string rutaSalida = Path.GetFullPath(Path.Combine(Application.StartupPath, "imagenes"));

                if (Directory.Exists(rutaProyecto))
                {
                    if (!Directory.Exists(rutaSalida))
                        Directory.CreateDirectory(rutaSalida);

                    foreach (string archivo in Directory.GetFiles(rutaProyecto))
                    {
                        string destino = Path.Combine(rutaSalida, Path.GetFileName(archivo));
                        if (!File.Exists(destino) || File.GetLastWriteTimeUtc(archivo) > File.GetLastWriteTimeUtc(destino))
                        {
                            File.Copy(archivo, destino, true);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
