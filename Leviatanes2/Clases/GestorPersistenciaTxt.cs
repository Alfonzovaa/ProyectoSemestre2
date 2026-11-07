using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Leviatanes2.Clases
{
    /// <summary>
    /// Modulo compartido de persistencia en archivos .TXT planos (reemplazo modular de Base de Datos SQL).
    /// Maneja: personajes.txt, jugadores.txt y bitacoras de combate .txt.
    /// </summary>
    public class GestorPersistenciaTxt
    {
        public static readonly string RutaArchivoPersonajes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "personajes.txt");
        public static readonly string RutaArchivoJugadores = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jugadores.txt");

        public GestorPersistenciaTxt()
        {
            AsegurarCatalogoInicialEnTxt();
            AsegurarJugadoresInicialesEnTxt();
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
            Catalogo.Add(new CartaTierra(8, " Behemoth Del Canion", 55, 25, 20, 80, "Raices Tectonicas", "Refuerza su coraza rocosa y sacude el suelo."));
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

            // Valida automaticamente que Vida + Defensa + Ataque == 100 en el constructor de Personaje
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
                new PerfilJugador(1, "JugadorUno", 0, 0, 0),
                new PerfilJugador(2, "JugadorDos", 0, 0, 0)
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

