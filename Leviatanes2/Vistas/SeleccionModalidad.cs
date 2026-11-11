using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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
            this.ClientSize = new Size(UiResponsiveManager.ANCHO_BASE, UiResponsiveManager.ALTO_BASE);
            ConfigurarListasModernas();

            GestorTxt = new GestorPersistenciaTxt();
            GeneradorAzar = new Random();
            CatalogoDisponible = new List<Personaje>();
            ListaEscenariosDisponibles = GestorPersistenciaTxt.ObtenerCatalogoEscenarios();

            InicializarDatosPantalla();
            UiResponsiveManager.ConfigurarFormulario(this);
        }

        // =====================================================================
        // 0. RENDERIZADO MODERNO DE TARJETAS EN LISTBOX (SIN SCROLL HORIZONTAL)
        // =====================================================================
        private void ConfigurarListasModernas()
        {
            lstCatalogo.DrawMode = DrawMode.OwnerDrawFixed;
            lstCatalogo.ItemHeight = 40;
            lstCatalogo.HorizontalScrollbar = false;
            lstCatalogo.BackColor = Color.FromArgb(16, 24, 36);
            lstCatalogo.DrawItem += new DrawItemEventHandler(LstCatalogoDrawItem);

            lstEquipo1.DrawMode = DrawMode.OwnerDrawFixed;
            lstEquipo1.ItemHeight = 42;
            lstEquipo1.HorizontalScrollbar = false;
            lstEquipo1.DrawItem += new DrawItemEventHandler(LstEquipoDrawItem);

            lstEquipo2.DrawMode = DrawMode.OwnerDrawFixed;
            lstEquipo2.ItemHeight = 42;
            lstEquipo2.HorizontalScrollbar = false;
            lstEquipo2.DrawItem += new DrawItemEventHandler(LstEquipoDrawItem);
        }

        private void LstCatalogoDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || CatalogoDisponible == null || e.Index >= CatalogoDisponible.Count)
                return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            bool Seleccionado = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Rectangle RectFila = e.Bounds;
            Personaje Carta = CatalogoDisponible[e.Index];
            Color ColorElem = ObtenerColorElemento(Carta.Faccion);

            Color ColorFondo = Seleccionado
                ? Color.FromArgb(36, 54, 82)
                : (e.Index % 2 == 0 ? Color.FromArgb(18, 26, 40) : Color.FromArgb(23, 33, 50));

            using (SolidBrush BrochaFondo = new SolidBrush(ColorFondo))
            {
                G.FillRectangle(BrochaFondo, RectFila);
            }

            using (SolidBrush BrochaBarra = new SolidBrush(ColorElem))
            {
                G.FillRectangle(BrochaBarra, RectFila.X, RectFila.Y + 2, 5, RectFila.Height - 4);
            }

            string TextoElem = Carta.Faccion.ToString().ToUpper();
            Rectangle RectBadge = new Rectangle(RectFila.X + 10, RectFila.Y + 5, 50, 15);
            using (SolidBrush BrochaBadge = new SolidBrush(ColorElem))
            {
                G.FillRectangle(BrochaBadge, RectBadge);
            }

            using (Font FuenteBadge = new Font("Consolas", 7F, FontStyle.Bold))
            using (SolidBrush BrochaBlanca = new SolidBrush(Color.White))
            {
                StringFormat FormatoCentro = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                G.DrawString(TextoElem, FuenteBadge, BrochaBlanca, RectBadge, FormatoCentro);
            }

            string Titulo = string.Format("#{0:00} {1}", Carta.IdPersonaje, Carta.Nombre);
            using (Font FuenteTitulo = new Font("Consolas", 9F, FontStyle.Bold))
            using (SolidBrush BrochaTitulo = new SolidBrush(Seleccionado ? Color.FromArgb(245, 203, 66) : Color.White))
            {
                G.DrawString(Titulo, FuenteTitulo, BrochaTitulo, RectFila.X + 66, RectFila.Y + 4);
            }

            string Subtitulo = string.Format("VIT:{0}  |  BLI:{1}  |  POD:{2}  |  VINC:{3}",
                Carta.VidaMaxima, Carta.DefensaBase, Carta.AtaqueBase, Carta.Empatia);
            using (Font FuenteStats = new Font("Consolas", 7.5F, FontStyle.Bold))
            using (SolidBrush BrochaStats = new SolidBrush(Seleccionado ? Color.LightCyan : Color.FromArgb(150, 175, 205)))
            {
                G.DrawString(Subtitulo, FuenteStats, BrochaStats, RectFila.X + 10, RectFila.Y + 22);
            }

            if (Seleccionado)
            {
                using (Pen BordeSeleccion = new Pen(Color.FromArgb(245, 203, 66), 1.5f))
                {
                    G.DrawRectangle(BordeSeleccion, RectFila.X + 1, RectFila.Y + 1, RectFila.Width - 3, RectFila.Height - 3);
                }
            }
            else
            {
                using (Pen LineaSep = new Pen(Color.FromArgb(30, 255, 255, 255), 1))
                {
                    G.DrawLine(LineaSep, RectFila.X, RectFila.Bottom - 1, RectFila.Right, RectFila.Bottom - 1);
                }
            }
        }

        private void LstEquipoDrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox Lista = sender as ListBox;
            if (e.Index < 0 || Lista == null)
                return;

            PerfilJugador Perfil = (Lista == lstEquipo1) ? PerfilEntrenador1 : PerfilEntrenador2;
            if (Perfil == null || e.Index >= Perfil.MazoSeleccionado.Count)
                return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Personaje P = Perfil.MazoSeleccionado[e.Index];
            Color ColorElem = ObtenerColorElemento(P.Faccion);
            Rectangle RectFila = e.Bounds;
            bool EsLider = (e.Index == 0);

            Color FondoBase = (Lista == lstEquipo1)
                ? (EsLider ? Color.FromArgb(28, 52, 38) : Color.FromArgb(18, 30, 24))
                : (EsLider ? Color.FromArgb(54, 28, 34) : Color.FromArgb(32, 18, 22));

            using (SolidBrush BrochaFondo = new SolidBrush(FondoBase))
            {
                G.FillRectangle(BrochaFondo, RectFila);
            }

            using (SolidBrush BrochaBarra = new SolidBrush(ColorElem))
            {
                G.FillRectangle(BrochaBarra, RectFila.X, RectFila.Y + 2, 4, RectFila.Height - 4);
            }

            string Rol = EsLider
                ? string.Format("★ LIDER [{0}]", P.Faccion.ToString().ToUpper())
                : string.Format("COLA #{0} [{1}]", e.Index, P.Faccion.ToString().ToUpper());
            Color ColorRol = EsLider ? Color.FromArgb(245, 203, 66) : Color.FromArgb(170, 195, 215);

            using (Font FuenteRol = new Font("Consolas", 7.5F, FontStyle.Bold))
            using (SolidBrush BrochaRol = new SolidBrush(ColorRol))
            {
                G.DrawString(Rol, FuenteRol, BrochaRol, RectFila.X + 8, RectFila.Y + 4);
            }

            using (Font FuenteNombre = new Font("Consolas", 8.5F, FontStyle.Bold))
            using (SolidBrush BrochaNombre = new SolidBrush(Color.White))
            {
                G.DrawString(P.Nombre, FuenteNombre, BrochaNombre, RectFila.X + 8, RectFila.Y + 20);
            }

            using (Pen LineaSep = new Pen(Color.FromArgb(35, 255, 255, 255), 1))
            {
                G.DrawLine(LineaSep, RectFila.X, RectFila.Bottom - 1, RectFila.Right, RectFila.Bottom - 1);
            }
        }

        private void InicializarDatosPantalla()
        {
            CargarPerfilesEntrenadoresDesdeTxt(false);
            CargarCatalogoPersonajesDesdeTxt();

            AutocompletarEquipo(PerfilEntrenador1, 0);
            AutocompletarEquipo(PerfilEntrenador2, 5);
            RefrescarListasDeEquipos();

            ActualizarControlesPorModalidad();
        }

        // =====================================================================
        // 1. MODALIDAD DE COMBATE (HISTORIA/CPU vs DUELO LOCAL)
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
            bool EsCpu = rbModoCpu.Checked;

            if (EsCpu)
            {
                txtEntrenador2.Text = "CPU_Leviatan";
                txtEntrenador2.ReadOnly = true;
                lblTextoProfesorGba.Text = "PROF. LUCENA: En Modo Historia/IA puedes armarle el mazo a la CPU o dejar que se autocomplete ▼";
            }
            else
            {
                txtEntrenador2.ReadOnly = false;
                if (txtEntrenador2.Text == "CPU_Leviatan")
                    txtEntrenador2.Text = "JugadorDos";
                lblTextoProfesorGba.Text = "PROF. LUCENA: Duelo Local activo. Ambos domadores eligen sus 5 Leviatanes (1 Lider + 4 en Cola) ▼";
            }

            CargarPerfilesEntrenadoresDesdeTxt(false);
        }

        // =====================================================================
        // 2. DOMADORES Y ESTADISTICAS EN jugadores.txt
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

            lblStatsEntrenador1.Text = string.Format("PJ:{0} V:{1} D:{2} ({3:0.0}%)",
                PerfilEntrenador1.PartidasJugadas,
                PerfilEntrenador1.Victorias,
                PerfilEntrenador1.Derrotas,
                PerfilEntrenador1.PorcentajeExito);

            lblStatsEntrenador2.Text = string.Format("PJ:{0} V:{1} D:{2} ({3:0.0}%)",
                PerfilEntrenador2.PartidasJugadas,
                PerfilEntrenador2.Victorias,
                PerfilEntrenador2.Derrotas,
                PerfilEntrenador2.PorcentajeExito);

            RefrescarListasDeEquipos();

            if (MostrarConfirmacion)
            {
                lblTextoProfesorGba.Text = string.Format("PROF. LUCENA: Domadores '{0}' y '{1}' sincronizados en jugadores.txt ▼",
                    PerfilEntrenador1.NombreUsuario, PerfilEntrenador2.NombreUsuario);
            }
        }

        // =====================================================================
        // 3. CATALOGO DE LEVIATANES Y BESTIARIO DEL CARIBE
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

            lblPreviewFaccion.Text = string.Format("BESTIARIO :: [{0}]", Carta.Faccion.ToString().ToUpper());
            lblPreviewFaccion.BackColor = ObtenerColorElemento(Carta.Faccion);
            lblPreviewNombre.Text = Carta.Nombre;

            int Suma = Carta.VidaMaxima + Carta.DefensaBase + Carta.AtaqueBase;
            lblPreviewStats.Text = string.Format(
                "VIT: {0} | BLI: {1} | POD: {2}\r\nVINCULO: {3} | PODER: {4}/100",
                Carta.VidaMaxima,
                Carta.DefensaBase,
                Carta.AtaqueBase,
                Carta.Empatia,
                Suma);

            lblPreviewEspecial.Text = string.Format("ARTE ABISAL [{0}]:\r\n{1}",
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
                Color.FromArgb(10, 16, 28),
                Color.FromArgb(24, 38, 60),
                LinearGradientMode.Vertical))
            {
                G.FillRectangle(Fondo, Rect);
            }

            using (Pen LapizRejilla = new Pen(Color.FromArgb(28, 180, 220, 255), 1))
            {
                for (int X = 0; X < Rect.Width; X += 16)
                    G.DrawLine(LapizRejilla, X, 0, X, Rect.Height);
                for (int Y = 0; Y < Rect.Height; Y += 16)
                    G.DrawLine(LapizRejilla, 0, Y, Rect.Width, Y);
            }

            int CentroX = Rect.Width / 2;
            int CentroY = (Rect.Height / 2) + 6;

            using (Pen LapizSonar = new Pen(Color.FromArgb(45, 245, 203, 66), 1f))
            {
                LapizSonar.DashStyle = DashStyle.Dash;
                G.DrawEllipse(LapizSonar, CentroX - 42, CentroY - 38, 84, 76);
            }

            using (Pen LapizMarco = new Pen(Color.FromArgb(190, 245, 203, 66), 2f))
            {
                int M = 5;
                int L = 10;
                G.DrawLine(LapizMarco, M, M + 14, M + L, M + 14);
                G.DrawLine(LapizMarco, M, M + 14, M, M + 14 + L);
                G.DrawLine(LapizMarco, Rect.Width - M - L, M + 14, Rect.Width - M, M + 14);
                G.DrawLine(LapizMarco, Rect.Width - M, M + 14, Rect.Width - M, M + 14 + L);
                G.DrawLine(LapizMarco, M, Rect.Height - M, M + L, Rect.Height - M);
                G.DrawLine(LapizMarco, M, Rect.Height - M - L, M, Rect.Height - M);
                G.DrawLine(LapizMarco, Rect.Width - M - L, Rect.Height - M, Rect.Width - M, Rect.Height - M);
                G.DrawLine(LapizMarco, Rect.Width - M, Rect.Height - M - L, Rect.Width - M, Rect.Height - M);
            }

            if (PersonajeEnVistaPrevia == null)
                return;

            Color ColorElem = ObtenerColorElemento(PersonajeEnVistaPrevia.Faccion);
            using (SolidBrush BrochaTop = new SolidBrush(Color.FromArgb(160, 10, 14, 22)))
            {
                G.FillRectangle(BrochaTop, 0, 0, Rect.Width, 16);
            }

            string Linaje = ObtenerTituloLinaje(PersonajeEnVistaPrevia.Faccion);
            string TextoRegistro = string.Format("CODICE #{0:00} // {1}", PersonajeEnVistaPrevia.IdPersonaje, Linaje);
            using (Font FuenteReg = new Font("Consolas", 7.5F, FontStyle.Bold))
            using (SolidBrush BrochaReg = new SolidBrush(Color.FromArgb(245, 203, 66)))
            {
                G.DrawString(TextoRegistro, FuenteReg, BrochaReg, 5, 2);
            }

            Image imgPersonaje = GestorPersistenciaTxt.CargarImagenPersonaje(PersonajeEnVistaPrevia.IdPersonaje, PersonajeEnVistaPrevia.Nombre, false); // cache: no Dispose
            {
                if (imgPersonaje != null)
                {
                    int maxAncho = Rect.Width - 20;
                    int maxAlto = Rect.Height - 28;
                    float escala = Math.Min((float)maxAncho / imgPersonaje.Width, (float)maxAlto / imgPersonaje.Height);
                    int anchoDibujo = Math.Max(1, (int)(imgPersonaje.Width * escala));
                    int altoDibujo = Math.Max(1, (int)(imgPersonaje.Height * escala));
                    int posX = (Rect.Width - anchoDibujo) / 2;
                    int posY = 18 + ((Rect.Height - 22 - altoDibujo) / 2);

                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.DrawImage(imgPersonaje, new Rectangle(posX, posY, anchoDibujo, altoDibujo));
                    return;
                }
            }

            using (SolidBrush BrochaAura = new SolidBrush(Color.FromArgb(65, ColorElem)))
            {
                G.FillEllipse(BrochaAura, CentroX - 34, CentroY - 32, 68, 64);
            }

            Point[] RomboRunico = new Point[]
            {
                new Point(CentroX, CentroY - 26),
                new Point(CentroX + 26, CentroY),
                new Point(CentroX, CentroY + 26),
                new Point(CentroX - 26, CentroY)
            };

            using (SolidBrush BrochaNucleo = new SolidBrush(ColorElem))
            using (Pen BordeDorado = new Pen(Color.FromArgb(245, 203, 66), 2f))
            {
                G.FillPolygon(BrochaNucleo, RomboRunico);
                G.DrawPolygon(BordeDorado, RomboRunico);
            }

            string Sigla = PersonajeEnVistaPrevia.Faccion.ToString().Substring(0, 2).ToUpper();
            using (Font FuenteSigla = new Font("Consolas", 10.5F, FontStyle.Bold))
            using (SolidBrush BrochaTexto = new SolidBrush(Color.White))
            {
                SizeF Tam = G.MeasureString(Sigla, FuenteSigla);
                G.DrawString(Sigla, FuenteSigla, BrochaTexto, CentroX - (Tam.Width / 2f), CentroY - (Tam.Height / 2f));
            }
        }

        private static string ObtenerTituloLinaje(Elemento Faccion)
        {
            switch (Faccion)
            {
                case Elemento.Fuego:
                    return "DRAKO PIRICO";
                case Elemento.Agua:
                    return "BESTIA ABISAL";
                case Elemento.Tierra:
                    return "COLOSO LITICO";
                default:
                    return "SIERPE CELESTE";
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
                MostrarModalAvisoTactico(
                    "CATALOGO DE LEVIATANES",
                    "SELECCION REQUERIDA",
                    "Selecciona primero un Leviatán del Catálogo de la izquierda antes de asignarlo a un equipo.",
                    Color.FromArgb(28, 108, 196));
                return;
            }

            if (Perfil.MazoSeleccionado.Count >= 5)
            {
                MostrarModalAvisoTactico(
                    "EQUIPO COMPLETO (5/5)",
                    EtiquetaEquipo.ToUpper() + " AL MAXIMO",
                    string.Format("El {0} ({1}) ya tiene sus 5 Leviatanes asignados (1 Líder + 4 en Cola).\r\n\r\nUsa el botón VACIAR si deseas rearmarlo.", EtiquetaEquipo, Perfil.NombreUsuario),
                    Color.FromArgb(148, 98, 42));
                return;
            }

            Personaje Original = CatalogoDisponible[Indice];
            for (int I = 0; I < Perfil.MazoSeleccionado.Count; I++)
            {
                if (string.Equals(Perfil.MazoSeleccionado[I].Nombre, Original.Nombre, StringComparison.OrdinalIgnoreCase))
                {
                    MostrarModalAvisoTactico(
                        "LEVIATAN DUPLICADO",
                        Original.Nombre.ToUpper(),
                        string.Format("'{0}' ya forma parte del {1}.\r\n\r\nElige otro Leviatán distinto del catálogo.", Original.Nombre, EtiquetaEquipo),
                        Color.FromArgb(204, 41, 18));
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
                lblTextoProfesorGba.Text = "PROF. LUCENA: Se ha vaciado el Equipo 1. Selecciona 5 Leviatanes del catalogo ▼";
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
                lblTextoProfesorGba.Text = "PROF. LUCENA: Se ha vaciado el Equipo 2. Selecciona 5 Leviatanes del catalogo ▼";
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
                int Indice = (DesplazamientoInicial + (I * 3)) % TotalCatalogo;
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
            if (!rbModoCpu.Checked)
            {
                MostrarModalAvisoTactico(
                    "MODALIDAD JUGADOR VS CPU REQUERIDA",
                    "MODO HISTORIA EXCLUSIVO",
                    "La campaña del Modo Historia está habilitada únicamente para la modalidad Jugador vs CPU.\r\n\r\nSelecciona 'MODO HISTORIA / IA (J1 vs CPU)' arriba a la izquierda para continuar.",
                    Color.FromArgb(204, 41, 18));
                return;
            }

            CargarPerfilesEntrenadoresDesdeTxt(false);

            if (PerfilEntrenador1 == null || PerfilEntrenador1.MazoSeleccionado.Count < 5)
            {
                MostrarModalAvisoTactico(
                    "EQUIPO 1 INCOMPLETO",
                    "FALTAN LEVIATANES EN J1",
                    "El Equipo del Domador 1 debe tener exactamente 5 Leviatanes (1 Líder + 4 en Cola de Relevo).",
                    Color.FromArgb(204, 41, 18));
                return;
            }

            if (rbModoCpu.Checked && (PerfilEntrenador2 == null || PerfilEntrenador2.MazoSeleccionado.Count < 5))
            {
                AutocompletarEquipoAleatorio(PerfilEntrenador2);
                RefrescarListasDeEquipos();
            }

            if (PerfilEntrenador2 == null || PerfilEntrenador2.MazoSeleccionado.Count < 5)
            {
                MostrarModalAvisoTactico(
                    "EQUIPO 2 INCOMPLETO",
                    "FALTAN LEVIATANES EN J2",
                    "El Equipo del Domador 2 debe tener exactamente 5 Leviatanes (1 Líder + 4 en Cola de Relevo).",
                    Color.FromArgb(204, 41, 18));
                return;
            }

            if (ListaEscenariosDisponibles == null || ListaEscenariosDisponibles.Count == 0)
            {
                ListaEscenariosDisponibles = GestorPersistenciaTxt.ObtenerCatalogoEscenarios();
            }

            int IndiceEscenarioAzar = GeneradorAzar.Next(ListaEscenariosDisponibles.Count);
            Escenario EscenarioSorteado = ListaEscenariosDisponibles[IndiceEscenarioAzar];

            JugadorUnoConfigurado = PerfilEntrenador1;
            JugadorDosConfigurado = PerfilEntrenador2;
            EscenarioInicialSorteado = EscenarioSorteado;
            CatalogoEscenariosArena = ListaEscenariosDisponibles;
            ModoContraCpu = rbModoCpu.Checked;

            // Mostrar el nuevo Modal Táctico Abisal para el sorteo de escenario
            MostrarModalSorteoEscenario(EscenarioSorteado, PerfilEntrenador1.NombreUsuario, PerfilEntrenador2.NombreUsuario);

            Historia VentanaHistoria = new Historia(1);
            UiResponsiveManager.CambiarVentana(this, VentanaHistoria);
        }

        // =====================================================================
        // 6. MODALES TACTICOS ESTILO CODICE ABISAL (REEMPLAZAN MESSAGEBOX)
        // =====================================================================
        private void MostrarModalSorteoEscenario(Escenario Esc, string Domador1, string Domador2)
        {
            Color ColorEntorno = Color.FromArgb(46, 152, 126);
            string SiglaEntorno = "AI";
            string NombreMayus = (Esc.Nombre + " " + Esc.Descripcion).ToUpper();

            if (NombreMayus.Contains("FUEGO") || NombreMayus.Contains("VOLCAN") || NombreMayus.Contains("CALDERA") || NombreMayus.Contains("PIRIC"))
            {
                ColorEntorno = Color.FromArgb(204, 41, 18);
                SiglaEntorno = "FU";
            }
            else if (NombreMayus.Contains("AGUA") || NombreMayus.Contains("ABISM") || NombreMayus.Contains("CORAL") || NombreMayus.Contains("MAR"))
            {
                ColorEntorno = Color.FromArgb(28, 108, 196);
                SiglaEntorno = "AG";
            }
            else if (NombreMayus.Contains("TIERRA") || NombreMayus.Contains("ROCA") || NombreMayus.Contains("CANION") || NombreMayus.Contains("BASALTO"))
            {
                ColorEntorno = Color.FromArgb(148, 98, 42);
                SiglaEntorno = "TI";
            }

            using (Form Modal = new Form())
            {
                Modal.FormBorderStyle = FormBorderStyle.None;
                Modal.StartPosition = FormStartPosition.CenterParent;
                Modal.ClientSize = new Size(510, 314);
                Modal.BackColor = Color.FromArgb(245, 203, 66);
                Modal.ShowInTaskbar = false;

                Panel PnlInterior = new Panel();
                PnlInterior.SetBounds(3, 3, 504, 308);
                PnlInterior.BackColor = Color.FromArgb(12, 20, 36);
                Modal.Controls.Add(PnlInterior);

                PnlInterior.Paint += (s, e) =>
                {
                    Graphics G = e.Graphics;
                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    Rectangle R = PnlInterior.ClientRectangle;

                    using (LinearGradientBrush Fondo = new LinearGradientBrush(
                        R,
                        Color.FromArgb(10, 18, 34),
                        Color.FromArgb(20, 38, 64),
                        LinearGradientMode.Vertical))
                    {
                        G.FillRectangle(Fondo, R);
                    }

                    using (Pen LapizGrid = new Pen(Color.FromArgb(18, 140, 210, 255), 1))
                    {
                        for (int x = 0; x < R.Width; x += 24)
                            G.DrawLine(LapizGrid, x, 36, x, R.Height);
                        for (int y = 36; y < R.Height; y += 24)
                            G.DrawLine(LapizGrid, 0, y, R.Width, y);
                    }

                    Rectangle RectHeader = new Rectangle(0, 0, R.Width, 36);
                    using (SolidBrush BrochaHeader = new SolidBrush(ColorEntorno))
                    using (Pen LineaOro = new Pen(Color.FromArgb(245, 203, 66), 2))
                    {
                        G.FillRectangle(BrochaHeader, RectHeader);
                        G.DrawLine(LineaOro, 0, 36, R.Width, 36);
                    }

                    StringFormat Centro = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    using (Font FuenteHeader = new Font("Consolas", 10.5F, FontStyle.Bold))
                    {
                        G.DrawString("★ SORTEO DE ESCENARIO COMPLETADO ★", FuenteHeader, Brushes.White, RectHeader, Centro);
                    }

                    Rectangle RectCaja = new Rectangle(18, 52, R.Width - 36, 130);
                    using (SolidBrush FondoCaja = new SolidBrush(Color.FromArgb(165, 8, 14, 24)))
                    using (Pen BordeCaja = new Pen(Color.FromArgb(180, 245, 203, 66), 1.5f))
                    {
                        G.FillRectangle(FondoCaja, RectCaja);
                        G.DrawRectangle(BordeCaja, RectCaja);
                    }

                    int CX = RectCaja.X + 54;
                    int CY = RectCaja.Y + (RectCaja.Height / 2);

                    using (SolidBrush Aura = new SolidBrush(Color.FromArgb(60, ColorEntorno)))
                    {
                        G.FillEllipse(Aura, CX - 36, CY - 36, 72, 72);
                    }

                    Point[] Rombo = new Point[]
                    {
                        new Point(CX, CY - 26),
                        new Point(CX + 26, CY),
                        new Point(CX, CY + 26),
                        new Point(CX - 26, CY)
                    };

                    using (SolidBrush Nucleo = new SolidBrush(ColorEntorno))
                    using (Pen BordeRombo = new Pen(Color.FromArgb(245, 203, 66), 2f))
                    {
                        G.FillPolygon(Nucleo, Rombo);
                        G.DrawPolygon(BordeRombo, Rombo);
                    }

                    using (Font FuenteSigla = new Font("Consolas", 10.5F, FontStyle.Bold))
                    {
                        G.DrawString(SiglaEntorno, FuenteSigla, Brushes.White, new RectangleF(CX - 25, CY - 12, 50, 24), Centro);
                    }

                    int TX = RectCaja.X + 106;
                    int TW = RectCaja.Width - 116;

                    using (Font FuenteEtiq = new Font("Consolas", 8F, FontStyle.Bold))
                    using (SolidBrush BrochaCian = new SolidBrush(Color.Aquamarine))
                    {
                        G.DrawString("ENTORNO INICIAL SORTEADO:", FuenteEtiq, BrochaCian, TX, RectCaja.Y + 12);
                    }

                    using (Font FuenteEsc = new Font("Consolas", 13F, FontStyle.Bold))
                    using (SolidBrush BrochaOro = new SolidBrush(Color.FromArgb(245, 203, 66)))
                    {
                        G.DrawString(Esc.Nombre.ToUpper(), FuenteEsc, BrochaOro, TX, RectCaja.Y + 28);
                    }

                    using (Font FuenteRegla = new Font("Consolas", 8.5F, FontStyle.Bold))
                    using (SolidBrush BrochaBlanca = new SolidBrush(Color.WhiteSmoke))
                    {
                        G.DrawString("Regla Elemental: " + Esc.Descripcion, FuenteRegla, BrochaBlanca, new RectangleF(TX, RectCaja.Y + 56, TW, 38));
                    }

                    using (Font FuenteUmbral = new Font("Consolas", 8.5F, FontStyle.Bold))
                    using (SolidBrush BrochaCoral = new SolidBrush(Color.FromArgb(255, 180, 120)))
                    {
                        G.DrawString(string.Format("⚡ Umbral de Empatía (Terremoto): {0} pts", Esc.UmbralEmpatia), FuenteUmbral, BrochaCoral, TX, RectCaja.Y + 98);
                    }

                    Rectangle RectVs = new Rectangle(18, 194, R.Width - 36, 36);
                    using (SolidBrush FondoVs = new SolidBrush(Color.FromArgb(28, 44, 70)))
                    using (Pen BordeVs = new Pen(Color.FromArgb(100, 245, 203, 66), 1))
                    {
                        G.FillRectangle(FondoVs, RectVs);
                        G.DrawRectangle(BordeVs, RectVs);
                    }

                    using (Font FuenteVs = new Font("Consolas", 11F, FontStyle.Bold))
                    using (SolidBrush BrochaOro = new SolidBrush(Color.FromArgb(245, 203, 66)))
                    {
                        string TextoDuelo = string.Format("⚔  {0}   VS   {1}  ⚔", Domador1, Domador2);
                        G.DrawString(TextoDuelo, FuenteVs, BrochaOro, RectVs, Centro);
                    }
                };

                Button BtnAceptar = new Button();
                BtnAceptar.SetBounds(142, 246, 220, 44);
                BtnAceptar.FlatStyle = FlatStyle.Flat;
                BtnAceptar.FlatAppearance.BorderColor = Color.FromArgb(245, 203, 66);
                BtnAceptar.FlatAppearance.BorderSize = 2;
                BtnAceptar.BackColor = Color.FromArgb(204, 41, 18);
                BtnAceptar.ForeColor = Color.FromArgb(245, 203, 66);
                BtnAceptar.Font = new Font("Consolas", 10.5F, FontStyle.Bold);
                BtnAceptar.Cursor = Cursors.Hand;
                BtnAceptar.Text = "¡ZARPAR A LA ARENA! >>";
                BtnAceptar.DialogResult = DialogResult.OK;

                PnlInterior.Controls.Add(BtnAceptar);
                Modal.AcceptButton = BtnAceptar;

                Modal.ShowDialog(this);
            }
        }

        private void MostrarModalAvisoTactico(string tituloBarra, string subtituloOro, string mensaje, Color colorTema)
        {
            using (Form Modal = new Form())
            {
                Modal.FormBorderStyle = FormBorderStyle.None;
                Modal.StartPosition = FormStartPosition.CenterParent;
                Modal.ClientSize = new Size(460, 240);
                Modal.BackColor = Color.FromArgb(245, 203, 66);
                Modal.ShowInTaskbar = false;

                Panel PnlInterior = new Panel();
                PnlInterior.SetBounds(3, 3, 454, 234);
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
                    using (SolidBrush BrochaHeader = new SolidBrush(colorTema))
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

                    using (Font FuenteHeader = new Font("Consolas", 10F, FontStyle.Bold))
                    {
                        G.DrawString("◆ " + tituloBarra + " ◆", FuenteHeader, Brushes.White, RectHeader, Centro);
                    }

                    using (Font FuenteSub = new Font("Consolas", 10F, FontStyle.Bold))
                    using (SolidBrush BrochaOro = new SolidBrush(Color.FromArgb(245, 203, 66)))
                    {
                        G.DrawString(subtituloOro, FuenteSub, BrochaOro, new RectangleF(20, 46, R.Width - 40, 22), Centro);
                    }

                    using (Font FuenteMsg = new Font("Consolas", 9F, FontStyle.Bold))
                    using (SolidBrush BrochaBlanca = new SolidBrush(Color.WhiteSmoke))
                    {
                        G.DrawString(mensaje, FuenteMsg, BrochaBlanca, new RectangleF(24, 74, R.Width - 48, 86), Centro);
                    }
                };

                Button BtnOk = new Button();
                BtnOk.SetBounds(137, 174, 180, 42);
                BtnOk.FlatStyle = FlatStyle.Flat;
                BtnOk.FlatAppearance.BorderColor = Color.FromArgb(245, 203, 66);
                BtnOk.FlatAppearance.BorderSize = 2;
                BtnOk.BackColor = colorTema;
                BtnOk.ForeColor = Color.Yellow;
                BtnOk.Font = new Font("Consolas", 10F, FontStyle.Bold);
                BtnOk.Cursor = Cursors.Hand;
                BtnOk.Text = "ENTENDIDO";
                BtnOk.DialogResult = DialogResult.OK;

                PnlInterior.Controls.Add(BtnOk);
                Modal.AcceptButton = BtnOk;

                Modal.ShowDialog(this);
            }
        }

        private void BtnVolverMenuClick(object sender, EventArgs e)
        {
            MainForm MenuPrincipal = new MainForm();
            UiResponsiveManager.CambiarVentana(this, MenuPrincipal);
        }

        private void SeleccionModalidadFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}