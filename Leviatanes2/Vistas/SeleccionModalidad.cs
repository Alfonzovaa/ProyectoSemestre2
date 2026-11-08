using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Leviatanes2.Clases;

namespace Leviatanes2
{
    public partial class SeleccionModalidad : Form
    {
        // =====================================================================
        // ESTADO COMPARTIDO PARA PASAR DATOS A BATALLA 1
        // =====================================================================
        public static PerfilJugador JugadorUnoConfigurado { get; set; }
        public static PerfilJugador JugadorDosConfigurado { get; set; }
        public static Escenario EscenarioInicialSorteado { get; set; }
        public static List<Escenario> CatalogoEscenariosArena { get; set; }
        public static bool ModoContraCpu { get; set; }
        public static bool ModoRedLan { get; set; }
        public static TcpListener ListenerServidorLan { get; set; }
        public static TcpClient ClienteTcpLan { get; set; }

        // =====================================================================
        // CAMPOS INTERNOS DE LA VENTANA
        // =====================================================================
        private readonly GestorPersistenciaTxt GestorTxt;
        private readonly Random GeneradorAzar;
        private List<Personaje> CatalogoDisponible;
        private List<Escenario> ListaEscenariosDisponibles;
        private PerfilJugador PerfilEntrenador1;
        private PerfilJugador PerfilEntrenador2;
        private Personaje PersonajeEnVistaPrevia;

        public SeleccionModalidad()
        {
            InitializeComponent();

            GestorTxt = new GestorPersistenciaTxt();
            GeneradorAzar = new Random();
            CatalogoDisponible = new List<Personaje>();
            ListaEscenariosDisponibles = GestorPersistenciaTxt.ObtenerCatalogoEscenarios();

            InicializarDatosPantalla();
        }

        private void InicializarDatosPantalla()
        {
            // 1. Cargar o registrar perfiles desde jugadores.txt
            CargarPerfilesEntrenadoresDesdeTxt(false);

            // 2. Cargar catalogo de criaturas desde personajes.txt
            CargarCatalogoPersonajesDesdeTxt();

            // 3. Autocompletar equipos iniciales de 5 cartas para facilitar pruebas rapidas
            AutocompletarEquipo(PerfilEntrenador1, 0);
            AutocompletarEquipo(PerfilEntrenador2, 5);
            RefrescarListasDeEquipos();

            // 4. Ajustar controles de modalidad inicial
            ActualizarControlesPorModalidad();
        }

        // =====================================================================
        // 1. MODALIDAD Y RED LAN (CABLE LINK TCP/IP)
        // =====================================================================
        private void ModalidadCheckedChanged(object sender, EventArgs e)
        {
            RadioButton Radio = sender as RadioButton;
            if (Radio != null && !Radio.Checked)
                return;

            ActualizarControlesPorModalidad();
        }

        private void ActualizarControlesPorModalidad()
        {
            bool EsLan = rbModoRedLan.Checked;
            bool EsCpu = rbModoCpu.Checked;

            txtIp.Enabled = EsLan;
            txtPuerto.Enabled = EsLan;
            btnHospedar.Enabled = EsLan;
            btnUnirse.Enabled = EsLan;

            if (EsCpu)
            {
                txtEntrenador2.Text = "CPU_Leviatan";
                txtEntrenador2.ReadOnly = true;
                lblEstadoRed.ForeColor = Color.Khaki;
                lblEstadoRed.Text = "Modo Entrenamiento IA: El Entrenador 2 es controlado por la CPU.";
                lblTextoProfesorGba.Text = "PROF. LUCENA: En Modo CPU IA puedes armarle su mazo de 5 cartas o dejar que se autocomplete al iniciar ▼";
            }
            else if (EsLan)
            {
                txtEntrenador2.ReadOnly = false;
                if (txtEntrenador2.Text == "CPU_Leviatan")
                    txtEntrenador2.Text = "JugadorDos";
                lblEstadoRed.ForeColor = Color.Cyan;
                lblEstadoRed.Text = "Cable Link TCP activo: Usa HOSPEDAR SALA o UNIRSE SALA en la red LAN.";
                lblTextoProfesorGba.Text = "PROF. LUCENA: Configura la IP y Puerto (ej. 5000) para enlazar dos consolas por Sockets TCP/IP ▼";
            }
            else
            {
                txtEntrenador2.ReadOnly = false;
                if (txtEntrenador2.Text == "CPU_Leviatan")
                    txtEntrenador2.Text = "JugadorDos";
                lblEstadoRed.ForeColor = Color.Aquamarine;
                lblEstadoRed.Text = "Estado Cable Link: Modo Duelo Local activo (1v1 en la misma PC).";
                lblTextoProfesorGba.Text = "PROF. LUCENA: Modo Duelo Local seleccionado. Ambos entrenadores eligen sus 5 cartas (1 Lider + 4 en Cola) ▼";
            }

            CargarPerfilesEntrenadoresDesdeTxt(false);
        }

        private void BtnHospedarClick(object sender, EventArgs e)
        {
            int Puerto;
            if (!int.TryParse(txtPuerto.Text.Trim(), out Puerto) || Puerto < 1024 || Puerto > 65535)
            {
                MessageBox.Show("Ingresa un puerto TCP valido entre 1024 y 65535 (ej. 5000).",
                    "Puerto Invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CerrarConexionesLanPrevias();
                ListenerServidorLan = new TcpListener(IPAddress.Any, Puerto);
                ListenerServidorLan.Start();

                lblEstadoRed.ForeColor = Color.LightGreen;
                lblEstadoRed.Text = string.Format("Sala hospedada en puerto {0}. Esperando jugador remoto...", Puerto);
                lblTextoProfesorGba.Text = string.Format("PROF. LUCENA: Servidor TCP escuchando en el puerto {0}. ¡Listo para iniciar combate! ▼", Puerto);
            }
            catch (Exception Ex)
            {
                lblEstadoRed.ForeColor = Color.Salmon;
                lblEstadoRed.Text = "Error al hospedar sala: " + Ex.Message;
            }
        }

        private void BtnUnirseClick(object sender, EventArgs e)
        {
            string IpDestino = txtIp.Text.Trim();
            int Puerto;
            if (string.IsNullOrEmpty(IpDestino) || !int.TryParse(txtPuerto.Text.Trim(), out Puerto) || Puerto < 1024 || Puerto > 65535)
            {
                MessageBox.Show("Verifica la direccion IP y el puerto TCP (1024 - 65535).",
                    "Datos de Red Invalidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CerrarConexionesLanPrevias();
                ClienteTcpLan = new TcpClient();
                IAsyncResult Resultado = ClienteTcpLan.BeginConnect(IpDestino, Puerto, null, null);
                bool Conectado = Resultado.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(1200));

                if (Conectado && ClienteTcpLan.Connected)
                {
                    ClienteTcpLan.EndConnect(Resultado);
                    lblEstadoRed.ForeColor = Color.LightGreen;
                    lblEstadoRed.Text = string.Format("Conectado exitosamente al anfitrion {0}:{1}.", IpDestino, Puerto);
                    lblTextoProfesorGba.Text = "PROF. LUCENA: ¡Cable Link conectado con exito! Presiona IR A LA BATALLA 1 para combatir ▼";
                }
                else
                {
                    ClienteTcpLan.Close();
                    ClienteTcpLan = null;
                    lblEstadoRed.ForeColor = Color.Khaki;
                    lblEstadoRed.Text = string.Format("Sin respuesta inmediata en {0}:{1}. Puedes iniciar en modo simulacion LAN.", IpDestino, Puerto);
                }
            }
            catch (Exception Ex)
            {
                lblEstadoRed.ForeColor = Color.Khaki;
                lblEstadoRed.Text = "Aviso Cable Link: " + Ex.Message;
            }
        }

        private void CerrarConexionesLanPrevias()
        {
            try
            {
                if (ClienteTcpLan != null)
                {
                    ClienteTcpLan.Close();
                    ClienteTcpLan = null;
                }
            }
            catch (Exception)
            {
            }

            try
            {
                if (ListenerServidorLan != null)
                {
                    ListenerServidorLan.Stop();
                    ListenerServidorLan = null;
                }
            }
            catch (Exception)
            {
            }
        }

        // =====================================================================
        // 2. ENTRENADORES Y ESTADISTICAS EN jugadores.txt
        // =====================================================================
        private void BtnGuardarNombresClick(object sender, EventArgs e)
        {
            CargarPerfilesEntrenadoresDesdeTxt(true);
        }

        private void CargarPerfilesEntrenadoresDesdeTxt(bool MostrarConfirmacion)
        {
            string Nombre1 = txtEntrenador1.Text.Trim();
            string Nombre2 = txtEntrenador2.Text.Trim();

            if (string.IsNullOrEmpty(Nombre1))
            {
                Nombre1 = "JugadorUno";
                txtEntrenador1.Text = Nombre1;
            }

            if (string.IsNullOrEmpty(Nombre2))
            {
                Nombre2 = rbModoCpu.Checked ? "CPU_Leviatan" : "JugadorDos";
                txtEntrenador2.Text = Nombre2;
            }

            if (string.Equals(Nombre1, Nombre2, StringComparison.OrdinalIgnoreCase))
            {
                Nombre2 = Nombre2 + "_2";
                txtEntrenador2.Text = Nombre2;
            }

            List<Personaje> MazoPrevio1 = PerfilEntrenador1 != null ? new List<Personaje>(PerfilEntrenador1.MazoSeleccionado) : new List<Personaje>();
            List<Personaje> MazoPrevio2 = PerfilEntrenador2 != null ? new List<Personaje>(PerfilEntrenador2.MazoSeleccionado) : new List<Personaje>();

            PerfilEntrenador1 = GestorTxt.AutenticarORegistrarJugador(Nombre1);
            PerfilEntrenador2 = GestorTxt.AutenticarORegistrarJugador(Nombre2);

            PerfilEntrenador1.MazoSeleccionado = MazoPrevio1;
            PerfilEntrenador2.MazoSeleccionado = MazoPrevio2;

            lblStatsEntrenador1.Text = string.Format("PJ: {0} | V: {1} | D: {2} | Exito: {3:0.00}%",
                PerfilEntrenador1.PartidasJugadas,
                PerfilEntrenador1.Victorias,
                PerfilEntrenador1.Derrotas,
                PerfilEntrenador1.PorcentajeExito);

            lblStatsEntrenador2.Text = string.Format("PJ: {0} | V: {1} | D: {2} | Exito: {3:0.00}%",
                PerfilEntrenador2.PartidasJugadas,
                PerfilEntrenador2.Victorias,
                PerfilEntrenador2.Derrotas,
                PerfilEntrenador2.PorcentajeExito);

            RefrescarListasDeEquipos();

            if (MostrarConfirmacion)
            {
                lblTextoProfesorGba.Text = string.Format("PROF. LUCENA: Perfiles '{0}' y '{1}' sincronizados en jugadores.txt ▼",
                    PerfilEntrenador1.NombreUsuario, PerfilEntrenador2.NombreUsuario);
            }
        }

        // =====================================================================
        // 3. CATALOGO ELEMENTAL (personajes.txt) Y VISTA PREVIA POKEDEX GBA
        // =====================================================================
        private void CargarCatalogoPersonajesDesdeTxt()
        {
            CatalogoDisponible = GestorTxt.CargarPersonajesDesdeTxt();
            lstCatalogo.Items.Clear();

            for (int I = 0; I < CatalogoDisponible.Count; I++)
            {
                Personaje C = CatalogoDisponible[I];
                string TextoItem = string.Format("#{0:00} [{1}] {2} (HP:{3} DEF:{4} ATK:{5})",
                    C.IdPersonaje,
                    C.Faccion.ToString().ToUpper(),
                    C.Nombre,
                    C.VidaMaxima,
                    C.DefensaBase,
                    C.AtaqueBase);
                lstCatalogo.Items.Add(TextoItem);
            }

            if (lstCatalogo.Items.Count > 0)
            {
                lstCatalogo.SelectedIndex = 0;
            }
        }

        private void LstCatalogoSelectedIndexChanged(object sender, EventArgs e)
        {
            int Indice = lstCatalogo.SelectedIndex;
            if (Indice < 0 || Indice >= CatalogoDisponible.Count)
                return;

            PersonajeEnVistaPrevia = CatalogoDisponible[Indice];
            ActualizarPanelPreview(PersonajeEnVistaPrevia);
        }

        private void ActualizarPanelPreview(Personaje Carta)
        {
            if (Carta == null)
                return;

            lblPreviewFaccion.Text = string.Format("POKEDEX :: [{0}]", Carta.Faccion.ToString().ToUpper());
            lblPreviewFaccion.BackColor = ObtenerColorElemento(Carta.Faccion);
            lblPreviewNombre.Text = Carta.Nombre;

            int Suma = Carta.VidaMaxima + Carta.DefensaBase + Carta.AtaqueBase;
            lblPreviewStats.Text = string.Format("HP: {0} | DEF: {1} | ATK: {2}\r\nEMPATIA: {3} | SUMA: {4}/100",
                Carta.VidaMaxima,
                Carta.DefensaBase,
                Carta.AtaqueBase,
                Carta.Empatia,
                Suma);

            lblPreviewEspecial.Text = string.Format("[{0}]: {1}",
                Carta.NombreEspecial,
                Carta.DescripcionEspecial);

            pnlPreviewSprite.Invalidate();
        }

        private void PnlPreviewSpritePaint(object sender, PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle Rect = pnlPreviewSprite.ClientRectangle;
            using (LinearGradientBrush Fondo = new LinearGradientBrush(
                Rect,
                Color.FromArgb(14, 22, 36),
                Color.FromArgb(30, 46, 70),
                LinearGradientMode.Vertical))
            {
                G.FillRectangle(Fondo, Rect);
            }

            // Rejilla tactica GBA
            using (Pen LapizRejilla = new Pen(Color.FromArgb(35, 255, 255, 255), 1))
            {
                for (int X = 0; X < Rect.Width; X += 16)
                    G.DrawLine(LapizRejilla, X, 0, X, Rect.Height);
                for (int Y = 0; Y < Rect.Height; Y += 16)
                    G.DrawLine(LapizRejilla, 0, Y, Rect.Width, Y);
            }

            if (PersonajeEnVistaPrevia == null)
                return;

            Color ColorElem = ObtenerColorElemento(PersonajeEnVistaPrevia.Faccion);
            int CentroX = Rect.Width / 2;
            int CentroY = Rect.Height / 2;

            using (SolidBrush BrochaAura = new SolidBrush(Color.FromArgb(70, ColorElem)))
            {
                G.FillEllipse(BrochaAura, CentroX - 30, CentroY - 28, 60, 56);
            }

            using (SolidBrush BrochaNucleo = new SolidBrush(ColorElem))
            using (Pen BordeDorado = new Pen(Color.FromArgb(245, 203, 66), 2f))
            {
                G.FillEllipse(BrochaNucleo, CentroX - 20, CentroY - 20, 40, 40);
                G.DrawEllipse(BordeDorado, CentroX - 20, CentroY - 20, 40, 40);
            }

            string Sigla = PersonajeEnVistaPrevia.Faccion.ToString().Substring(0, 2).ToUpper();
            using (Font FuenteSigla = new Font("Consolas", 11F, FontStyle.Bold))
            using (SolidBrush BrochaTexto = new SolidBrush(Color.White))
            {
                SizeF Tam = G.MeasureString(Sigla, FuenteSigla);
                G.DrawString(Sigla, FuenteSigla, BrochaTexto, CentroX - (Tam.Width / 2f), CentroY - (Tam.Height / 2f));
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
        // 4. ARMADO DE EQUIPOS DE 5 CARTAS ([LIDER] + [COLA 1..4])
        // =====================================================================
        private void BtnAgregarEquipo1Click(object sender, EventArgs e)
        {
            AgregarCartaSeleccionadaAEquipo(PerfilEntrenador1, "Equipo 1");
        }

        private void BtnAgregarEquipo2Click(object sender, EventArgs e)
        {
            AgregarCartaSeleccionadaAEquipo(PerfilEntrenador2, "Equipo 2");
        }

        private void AgregarCartaSeleccionadaAEquipo(PerfilJugador Perfil, string EtiquetaEquipo)
        {
            if (Perfil == null)
                return;

            int Indice = lstCatalogo.SelectedIndex;
            if (Indice < 0 || Indice >= CatalogoDisponible.Count)
            {
                MessageBox.Show("Selecciona primero una criatura del Catalogo Elemental.",
                    "Catalogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Perfil.MazoSeleccionado.Count >= 5)
            {
                MessageBox.Show(string.Format("El {0} ({1}) ya tiene el maximo de 5 cartas (1 Lider + 4 en Cola).\nUsa VACIAR si deseas rearmarlo.",
                    EtiquetaEquipo, Perfil.NombreUsuario),
                    "Equipo Completo (5/5)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Personaje Original = CatalogoDisponible[Indice];
            for (int I = 0; I < Perfil.MazoSeleccionado.Count; I++)
            {
                if (string.Equals(Perfil.MazoSeleccionado[I].Nombre, Original.Nombre, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(string.Format("'{0}' ya forma parte del {1}. Elige otra carta distinta.",
                        Original.Nombre, EtiquetaEquipo),
                        "Carta Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Perfil.MazoSeleccionado.Add(Original.ClonarParaBatalla());
            RefrescarListasDeEquipos();

            string RolAsignado = Perfil.MazoSeleccionado.Count == 1 ? "[LIDER]" : string.Format("[COLA {0}]", Perfil.MazoSeleccionado.Count - 1);
            lblTextoProfesorGba.Text = string.Format("PROF. LUCENA: {0} se unio al {1} como {2} ({3}/5 cartas) ▼",
                Original.Nombre, EtiquetaEquipo, RolAsignado, Perfil.MazoSeleccionado.Count);
        }

        private void BtnAutoEquipo1Click(object sender, EventArgs e)
        {
            AutocompletarEquipoAleatorio(PerfilEntrenador1);
            RefrescarListasDeEquipos();
            lblTextoProfesorGba.Text = string.Format("PROF. LUCENA: Equipo de {0} autocompletado con 5 Leviatanes al azar ▼",
                PerfilEntrenador1.NombreUsuario);
        }

        private void BtnVaciarEquipo1Click(object sender, EventArgs e)
        {
            if (PerfilEntrenador1 != null)
            {
                PerfilEntrenador1.MazoSeleccionado.Clear();
                RefrescarListasDeEquipos();
                lblTextoProfesorGba.Text = "PROF. LUCENA: Se ha vaciado el Equipo 1. Selecciona 5 cartas del catalogo ▼";
            }
        }

        private void BtnAutoEquipo2Click(object sender, EventArgs e)
        {
            AutocompletarEquipoAleatorio(PerfilEntrenador2);
            RefrescarListasDeEquipos();
            lblTextoProfesorGba.Text = string.Format("PROF. LUCENA: Equipo de {0} autocompletado con 5 Leviatanes al azar ▼",
                PerfilEntrenador2.NombreUsuario);
        }

        private void BtnVaciarEquipo2Click(object sender, EventArgs e)
        {
            if (PerfilEntrenador2 != null)
            {
                PerfilEntrenador2.MazoSeleccionado.Clear();
                RefrescarListasDeEquipos();
                lblTextoProfesorGba.Text = "PROF. LUCENA: Se ha vaciado el Equipo 2. Selecciona 5 cartas del catalogo ▼";
            }
        }

        private void AutocompletarEquipo(PerfilJugador Perfil, int DesplazamientoInicial)
        {
            if (Perfil == null || CatalogoDisponible == null || CatalogoDisponible.Count == 0)
                return;

            Perfil.MazoSeleccionado.Clear();
            int TotalCatalogo = CatalogoDisponible.Count;
            for (int I = 0; I < 5 && I < TotalCatalogo; I++)
            {
                int Indice = (DesplazamientoInicial + I) % TotalCatalogo;
                Perfil.MazoSeleccionado.Add(CatalogoDisponible[Indice].ClonarParaBatalla());
            }
        }

        private void AutocompletarEquipoAleatorio(PerfilJugador Perfil)
        {
            if (Perfil == null || CatalogoDisponible == null || CatalogoDisponible.Count == 0)
                return;

            Perfil.MazoSeleccionado.Clear();
            List<Personaje> Copia = new List<Personaje>(CatalogoDisponible);
            while (Perfil.MazoSeleccionado.Count < 5 && Copia.Count > 0)
            {
                int IndiceAzar = GeneradorAzar.Next(Copia.Count);
                Perfil.MazoSeleccionado.Add(Copia[IndiceAzar].ClonarParaBatalla());
                Copia.RemoveAt(IndiceAzar);
            }
        }

        private void RefrescarListasDeEquipos()
        {
            if (PerfilEntrenador1 != null)
            {
                lblHeaderEquipo1.Text = string.Format("EQUIPO J1 ({0}/5 CARTAS)", PerfilEntrenador1.MazoSeleccionado.Count);
                lstEquipo1.Items.Clear();
                for (int I = 0; I < PerfilEntrenador1.MazoSeleccionado.Count; I++)
                {
                    Personaje P = PerfilEntrenador1.MazoSeleccionado[I];
                    string EtiquetaPosicion = (I == 0) ? "[LIDER]" : string.Format("[COLA {0}]", I);
                    lstEquipo1.Items.Add(string.Format("{0} {1} ({2})", EtiquetaPosicion, P.Nombre, P.Faccion));
                }
            }

            if (PerfilEntrenador2 != null)
            {
                lblHeaderEquipo2.Text = string.Format("EQUIPO J2 ({0}/5 CARTAS)", PerfilEntrenador2.MazoSeleccionado.Count);
                lstEquipo2.Items.Clear();
                for (int I = 0; I < PerfilEntrenador2.MazoSeleccionado.Count; I++)
                {
                    Personaje P = PerfilEntrenador2.MazoSeleccionado[I];
                    string EtiquetaPosicion = (I == 0) ? "[LIDER]" : string.Format("[COLA {0}]", I);
                    lstEquipo2.Items.Add(string.Format("{0} {1} ({2})", EtiquetaPosicion, P.Nombre, P.Faccion));
                }
            }
        }

        // =====================================================================
        // 5. SORTEO ALEATORIO DEL ESCENARIO Y NAVEGACION A BATALLA 1 / MENU
        // =====================================================================
        private void BtnIrBatallaClick(object sender, EventArgs e)
        {
            // Sincronizar nombres escritos en las cajas de texto antes de validar
            CargarPerfilesEntrenadoresDesdeTxt(false);

            if (PerfilEntrenador1 == null || PerfilEntrenador1.MazoSeleccionado.Count < 5)
            {
                MessageBox.Show("El Equipo del Entrenador 1 debe tener exactamente 5 cartas (1 Lider + 4 en Cola de Relevo).",
                    "Equipo 1 Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si es contra la CPU y el Equipo 2 tiene menos de 5 cartas, autocompletar al azar
            if (rbModoCpu.Checked && (PerfilEntrenador2 == null || PerfilEntrenador2.MazoSeleccionado.Count < 5))
            {
                AutocompletarEquipoAleatorio(PerfilEntrenador2);
                RefrescarListasDeEquipos();
            }

            if (PerfilEntrenador2 == null || PerfilEntrenador2.MazoSeleccionado.Count < 5)
            {
                MessageBox.Show("El Equipo del Entrenador 2 debe tener exactamente 5 cartas (1 Lider + 4 en Cola de Relevo).",
                    "Equipo 2 Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Sorteo 100% aleatorio del Escenario inicial con Random entre los 4 entornos
            if (ListaEscenariosDisponibles == null || ListaEscenariosDisponibles.Count == 0)
            {
                ListaEscenariosDisponibles = GestorPersistenciaTxt.ObtenerCatalogoEscenarios();
            }

            int IndiceEscenarioAzar = GeneradorAzar.Next(ListaEscenariosDisponibles.Count);
            Escenario EscenarioSorteado = ListaEscenariosDisponibles[IndiceEscenarioAzar];

            // Guardar configuracion compartida para que Batalla1 la reciba lista
            JugadorUnoConfigurado = PerfilEntrenador1;
            JugadorDosConfigurado = PerfilEntrenador2;
            EscenarioInicialSorteado = EscenarioSorteado;
            CatalogoEscenariosArena = ListaEscenariosDisponibles;
            ModoContraCpu = rbModoCpu.Checked;
            ModoRedLan = rbModoRedLan.Checked;

            MessageBox.Show(
                string.Format("¡SORTEO DE ESCENARIO COMPLETADO!\n\n" +
                              "Entorno Inicial: {0}\n" +
                              "Regla Elemental: {1}\n" +
                              "Umbral de Empatia (Terremoto): {2} pts\n\n" +
                              "{3} vs {4}",
                    EscenarioSorteado.Nombre,
                    EscenarioSorteado.Descripcion,
                    EscenarioSorteado.UmbralEmpatia,
                    PerfilEntrenador1.NombreUsuario,
                    PerfilEntrenador2.NombreUsuario),
                "Arena Elemental GBA - Inicio de Batalla 1",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            Batalla1 VentanaBatalla = new Batalla1();
            VentanaBatalla.FormClosed += (s, args) => Application.Exit();
            VentanaBatalla.Show();
            this.Hide();
        }

        private void BtnVolverMenuClick(object sender, EventArgs e)
        {
            CerrarConexionesLanPrevias();
            MainForm MenuPrincipal = new MainForm();
            MenuPrincipal.FormClosed += (s, args) => Application.Exit();
            MenuPrincipal.Show();
            this.Hide();
        }

        private void SeleccionModalidadFormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarConexionesLanPrevias();
            Application.Exit();
        }
    }
}
