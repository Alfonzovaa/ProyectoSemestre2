/*
 * Creado por SharpDevelop.
 * Usuario: Alfonzo
 * Fecha: 11/8/2026
 * Hora: 10:33 AM
 */
namespace Leviatanes2
{
	partial class Batalla
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pnlHeaderGba = new System.Windows.Forms.Panel();
			this.lblTurnoActual = new System.Windows.Forms.Label();
			this.lblGuiaElemental = new System.Windows.Forms.Label();
			this.lblTituloBatalla = new System.Windows.Forms.Label();
			this.pnlArenaCombate = new System.Windows.Forms.Panel();
			this.pnlHudCpu = new System.Windows.Forms.Panel();
			this.lblBolitasCpu = new System.Windows.Forms.Label();
			this.lblStatsCpu = new System.Windows.Forms.Label();
			this.pbVidaCpu = new System.Windows.Forms.ProgressBar();
			this.lblElementoCpu = new System.Windows.Forms.Label();
			this.lblNombreCpu = new System.Windows.Forms.Label();
			this.pnlSpriteCpu = new System.Windows.Forms.Panel();
			this.pnlVentajaElemental = new System.Windows.Forms.Panel();
			this.lblVentajaElemental = new System.Windows.Forms.Label();
			this.pnlSpriteJugador = new System.Windows.Forms.Panel();
			this.pnlHudJugador = new System.Windows.Forms.Panel();
			this.lblEspecialInfoJugador = new System.Windows.Forms.Label();
			this.pbEspecialJugador = new System.Windows.Forms.ProgressBar();
			this.lblBolitasJugador = new System.Windows.Forms.Label();
			this.lblStatsJugador = new System.Windows.Forms.Label();
			this.pbVidaJugador = new System.Windows.Forms.ProgressBar();
			this.lblElementoJugador = new System.Windows.Forms.Label();
			this.lblNombreJugador = new System.Windows.Forms.Label();
			this.pnlLateralDerecho = new System.Windows.Forms.Panel();
			this.lstDerrotadosCpu = new System.Windows.Forms.ListBox();
			this.lblHeaderDerrotadosCpu = new System.Windows.Forms.Label();
			this.lstBitacoraCombate = new System.Windows.Forms.ListBox();
			this.lblHeaderBitacora = new System.Windows.Forms.Label();
			this.lstColaJugador = new System.Windows.Forms.ListBox();
			this.lblHeaderCola = new System.Windows.Forms.Label();
			this.pnlMenuInferior = new System.Windows.Forms.Panel();
			this.btnReiniciarBatalla = new System.Windows.Forms.Button();
			this.btnCargarEnergia = new System.Windows.Forms.Button();
			this.btnCambiarLeviatan = new System.Windows.Forms.Button();
			this.btnDefender = new System.Windows.Forms.Button();
			this.btnEspecial = new System.Windows.Forms.Button();
			this.btnAtacar = new System.Windows.Forms.Button();
			this.lblDialogoCombate = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.tmrTurnoCpu = new System.Windows.Forms.Timer(this.components);
			this.tmrPausaDerrota = new System.Windows.Forms.Timer(this.components);
			this.pnlHeaderGba.SuspendLayout();
			this.pnlArenaCombate.SuspendLayout();
			this.pnlHudCpu.SuspendLayout();
			this.pnlVentajaElemental.SuspendLayout();
			this.pnlHudJugador.SuspendLayout();
			this.pnlLateralDerecho.SuspendLayout();
			this.pnlMenuInferior.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeaderGba
			// 
			this.pnlHeaderGba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
			this.pnlHeaderGba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlHeaderGba.Controls.Add(this.lblTurnoActual);
			this.pnlHeaderGba.Controls.Add(this.lblGuiaElemental);
			this.pnlHeaderGba.Controls.Add(this.lblTituloBatalla);
			this.pnlHeaderGba.Location = new System.Drawing.Point(12, 10);
			this.pnlHeaderGba.Name = "pnlHeaderGba";
			this.pnlHeaderGba.Size = new System.Drawing.Size(1256, 64);
			this.pnlHeaderGba.TabIndex = 1;
			// 
			// lblTituloBatalla
			// 
			this.lblTituloBatalla.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
			this.lblTituloBatalla.ForeColor = System.Drawing.Color.Gold;
			this.lblTituloBatalla.Location = new System.Drawing.Point(12, 6);
			this.lblTituloBatalla.Name = "lblTituloBatalla";
			this.lblTituloBatalla.Size = new System.Drawing.Size(920, 26);
			this.lblTituloBatalla.TabIndex = 0;
			this.lblTituloBatalla.Text = "BATALLA 1: LEVIATÁN DE LA FOSA (JUGADOR VS CPU)";
			// 
			// lblGuiaElemental
			// 
			this.lblGuiaElemental.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold);
			this.lblGuiaElemental.ForeColor = System.Drawing.Color.Aquamarine;
			this.lblGuiaElemental.Location = new System.Drawing.Point(12, 34);
			this.lblGuiaElemental.Name = "lblGuiaElemental";
			this.lblGuiaElemental.Size = new System.Drawing.Size(980, 22);
			this.lblGuiaElemental.TabIndex = 1;
			this.lblGuiaElemental.Text = "TABLA DE TIPOS ELEMENTALES:  [AGUA] vence a [FUEGO]  >  [FUEGO] vence a [AIRE]  >  [AIRE] vence a [TIERRA]  >  [TIERRA] vence a [AGUA]";
			// 
			// lblTurnoActual
			// 
			this.lblTurnoActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(41)))), ((int)(((byte)(18)))));
			this.lblTurnoActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTurnoActual.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold);
			this.lblTurnoActual.ForeColor = System.Drawing.Color.White;
			this.lblTurnoActual.Location = new System.Drawing.Point(1010, 10);
			this.lblTurnoActual.Name = "lblTurnoActual";
			this.lblTurnoActual.Size = new System.Drawing.Size(232, 42);
			this.lblTurnoActual.TabIndex = 2;
			this.lblTurnoActual.Text = "TURNO #01 - TU TURNO";
			this.lblTurnoActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlArenaCombate
			// 
			this.pnlArenaCombate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(26)))), ((int)(((byte)(42)))));
			this.pnlArenaCombate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlArenaCombate.Controls.Add(this.pnlHudCpu);
			this.pnlArenaCombate.Controls.Add(this.pnlSpriteCpu);
			this.pnlArenaCombate.Controls.Add(this.pnlVentajaElemental);
			this.pnlArenaCombate.Controls.Add(this.pnlSpriteJugador);
			this.pnlArenaCombate.Controls.Add(this.pnlHudJugador);
			this.pnlArenaCombate.Location = new System.Drawing.Point(12, 82);
			this.pnlArenaCombate.Name = "pnlArenaCombate";
			this.pnlArenaCombate.Size = new System.Drawing.Size(840, 396);
			this.pnlArenaCombate.TabIndex = 2;
			this.pnlArenaCombate.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlArenaCombatePaint);
			// 
			// pnlHudCpu
			// 
			this.pnlHudCpu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(62)))));
			this.pnlHudCpu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlHudCpu.Controls.Add(this.lblBolitasCpu);
			this.pnlHudCpu.Controls.Add(this.lblStatsCpu);
			this.pnlHudCpu.Controls.Add(this.pbVidaCpu);
			this.pnlHudCpu.Controls.Add(this.lblElementoCpu);
			this.pnlHudCpu.Controls.Add(this.lblNombreCpu);
			this.pnlHudCpu.Location = new System.Drawing.Point(18, 14);
			this.pnlHudCpu.Name = "pnlHudCpu";
			this.pnlHudCpu.Size = new System.Drawing.Size(450, 128);
			this.pnlHudCpu.TabIndex = 0;
			// 
			// lblNombreCpu
			// 
			this.lblNombreCpu.Font = new System.Drawing.Font("Consolas", 11.5F, System.Drawing.FontStyle.Bold);
			this.lblNombreCpu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(100)))));
			this.lblNombreCpu.Location = new System.Drawing.Point(10, 8);
			this.lblNombreCpu.Name = "lblNombreCpu";
			this.lblNombreCpu.Size = new System.Drawing.Size(300, 22);
			this.lblNombreCpu.TabIndex = 0;
			this.lblNombreCpu.Text = "CPU: Leviatán de la Fosa";
			// 
			// lblElementoCpu
			// 
			this.lblElementoCpu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(108)))), ((int)(((byte)(196)))));
			this.lblElementoCpu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblElementoCpu.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold);
			this.lblElementoCpu.ForeColor = System.Drawing.Color.White;
			this.lblElementoCpu.Location = new System.Drawing.Point(318, 7);
			this.lblElementoCpu.Name = "lblElementoCpu";
			this.lblElementoCpu.Size = new System.Drawing.Size(118, 24);
			this.lblElementoCpu.TabIndex = 1;
			this.lblElementoCpu.Text = "[AGUA]";
			this.lblElementoCpu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pbVidaCpu
			// 
			this.pbVidaCpu.Location = new System.Drawing.Point(10, 36);
			this.pbVidaCpu.Name = "pbVidaCpu";
			this.pbVidaCpu.Size = new System.Drawing.Size(426, 20);
			this.pbVidaCpu.TabIndex = 2;
			this.pbVidaCpu.Value = 100;
			// 
			// lblStatsCpu
			// 
			this.lblStatsCpu.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
			this.lblStatsCpu.ForeColor = System.Drawing.Color.White;
			this.lblStatsCpu.Location = new System.Drawing.Point(10, 62);
			this.lblStatsCpu.Name = "lblStatsCpu";
			this.lblStatsCpu.Size = new System.Drawing.Size(426, 36);
			this.lblStatsCpu.TabIndex = 3;
			this.lblStatsCpu.Text = "HP: 60/60 | ESCUDO: 25 | ATK: 15 | ENERGÍA: 2/10\r\nCARGA ESPECIAL: 0% | EMPATÍA: 75";
			// 
			// lblBolitasCpu
			// 
			this.lblBolitasCpu.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
			this.lblBolitasCpu.ForeColor = System.Drawing.Color.Salmon;
			this.lblBolitasCpu.Location = new System.Drawing.Point(10, 100);
			this.lblBolitasCpu.Name = "lblBolitasCpu";
			this.lblBolitasCpu.Size = new System.Drawing.Size(426, 22);
			this.lblBolitasCpu.TabIndex = 4;
			this.lblBolitasCpu.Text = "CPU VIVOS: ● ● ● (3/3) | DERROTADOS: 0/3";
			// 
			// pnlSpriteCpu
			// 
			this.pnlSpriteCpu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
			this.pnlSpriteCpu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSpriteCpu.Location = new System.Drawing.Point(510, 14);
			this.pnlSpriteCpu.Name = "pnlSpriteCpu";
			this.pnlSpriteCpu.Size = new System.Drawing.Size(308, 138);
			this.pnlSpriteCpu.TabIndex = 1;
			this.pnlSpriteCpu.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSpriteCpuPaint);
			// 
			// pnlVentajaElemental
			// 
			this.pnlVentajaElemental.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(46)))), ((int)(((byte)(38)))));
			this.pnlVentajaElemental.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlVentajaElemental.Controls.Add(this.lblVentajaElemental);
			this.pnlVentajaElemental.Location = new System.Drawing.Point(18, 160);
			this.pnlVentajaElemental.Name = "pnlVentajaElemental";
			this.pnlVentajaElemental.Size = new System.Drawing.Size(800, 56);
			this.pnlVentajaElemental.TabIndex = 2;
			// 
			// lblVentajaElemental
			// 
			this.lblVentajaElemental.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblVentajaElemental.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold);
			this.lblVentajaElemental.ForeColor = System.Drawing.Color.LightGreen;
			this.lblVentajaElemental.Location = new System.Drawing.Point(0, 0);
			this.lblVentajaElemental.Name = "lblVentajaElemental";
			this.lblVentajaElemental.Size = new System.Drawing.Size(798, 54);
			this.lblVentajaElemental.TabIndex = 0;
			this.lblVentajaElemental.Text = "ANÁLISIS ELEMENTAL DE VENTAJAS...";
			this.lblVentajaElemental.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlSpriteJugador
			// 
			this.pnlSpriteJugador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
			this.pnlSpriteJugador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSpriteJugador.Location = new System.Drawing.Point(18, 226);
			this.pnlSpriteJugador.Name = "pnlSpriteJugador";
			this.pnlSpriteJugador.Size = new System.Drawing.Size(308, 154);
			this.pnlSpriteJugador.TabIndex = 3;
			this.pnlSpriteJugador.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSpriteJugadorPaint);
			// 
			// pnlHudJugador
			// 
			this.pnlHudJugador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(62)))));
			this.pnlHudJugador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlHudJugador.Controls.Add(this.lblEspecialInfoJugador);
			this.pnlHudJugador.Controls.Add(this.pbEspecialJugador);
			this.pnlHudJugador.Controls.Add(this.lblBolitasJugador);
			this.pnlHudJugador.Controls.Add(this.lblStatsJugador);
			this.pnlHudJugador.Controls.Add(this.pbVidaJugador);
			this.pnlHudJugador.Controls.Add(this.lblElementoJugador);
			this.pnlHudJugador.Controls.Add(this.lblNombreJugador);
			this.pnlHudJugador.Location = new System.Drawing.Point(344, 226);
			this.pnlHudJugador.Name = "pnlHudJugador";
			this.pnlHudJugador.Size = new System.Drawing.Size(474, 154);
			this.pnlHudJugador.TabIndex = 4;
			// 
			// lblNombreJugador
			// 
			this.lblNombreJugador.Font = new System.Drawing.Font("Consolas", 11.5F, System.Drawing.FontStyle.Bold);
			this.lblNombreJugador.ForeColor = System.Drawing.Color.Cyan;
			this.lblNombreJugador.Location = new System.Drawing.Point(10, 8);
			this.lblNombreJugador.Name = "lblNombreJugador";
			this.lblNombreJugador.Size = new System.Drawing.Size(320, 22);
			this.lblNombreJugador.TabIndex = 0;
			this.lblNombreJugador.Text = "TÚ: Golem De Basalto";
			// 
			// lblElementoJugador
			// 
			this.lblElementoJugador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(98)))), ((int)(((byte)(42)))));
			this.lblElementoJugador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblElementoJugador.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold);
			this.lblElementoJugador.ForeColor = System.Drawing.Color.White;
			this.lblElementoJugador.Location = new System.Drawing.Point(342, 7);
			this.lblElementoJugador.Name = "lblElementoJugador";
			this.lblElementoJugador.Size = new System.Drawing.Size(118, 24);
			this.lblElementoJugador.TabIndex = 1;
			this.lblElementoJugador.Text = "[TIERRA]";
			this.lblElementoJugador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pbVidaJugador
			// 
			this.pbVidaJugador.Location = new System.Drawing.Point(10, 35);
			this.pbVidaJugador.Name = "pbVidaJugador";
			this.pbVidaJugador.Size = new System.Drawing.Size(450, 18);
			this.pbVidaJugador.TabIndex = 2;
			this.pbVidaJugador.Value = 100;
			// 
			// lblStatsJugador
			// 
			this.lblStatsJugador.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
			this.lblStatsJugador.ForeColor = System.Drawing.Color.White;
			this.lblStatsJugador.Location = new System.Drawing.Point(10, 58);
			this.lblStatsJugador.Name = "lblStatsJugador";
			this.lblStatsJugador.Size = new System.Drawing.Size(450, 20);
			this.lblStatsJugador.TabIndex = 3;
			this.lblStatsJugador.Text = "HP: 60/60 | ESCUDO: 30 | ATK: 10 | ENERGÍA: 2/10";
			// 
			// pbEspecialJugador
			// 
			this.pbEspecialJugador.Location = new System.Drawing.Point(10, 81);
			this.pbEspecialJugador.Name = "pbEspecialJugador";
			this.pbEspecialJugador.Size = new System.Drawing.Size(450, 14);
			this.pbEspecialJugador.TabIndex = 4;
			// 
			// lblEspecialInfoJugador
			// 
			this.lblEspecialInfoJugador.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
			this.lblEspecialInfoJugador.ForeColor = System.Drawing.Color.Khaki;
			this.lblEspecialInfoJugador.Location = new System.Drawing.Point(10, 99);
			this.lblEspecialInfoJugador.Name = "lblEspecialInfoJugador";
			this.lblEspecialInfoJugador.Size = new System.Drawing.Size(450, 26);
			this.lblEspecialInfoJugador.TabIndex = 5;
			this.lblEspecialInfoJugador.Text = "ESPECIAL (0%): [Muralla Monolítica]";
			// 
			// lblBolitasJugador
			// 
			this.lblBolitasJugador.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
			this.lblBolitasJugador.ForeColor = System.Drawing.Color.LightGreen;
			this.lblBolitasJugador.Location = new System.Drawing.Point(10, 127);
			this.lblBolitasJugador.Name = "lblBolitasJugador";
			this.lblBolitasJugador.Size = new System.Drawing.Size(450, 20);
			this.lblBolitasJugador.TabIndex = 6;
			this.lblBolitasJugador.Text = "TU EQUIPO VIVO: ● ● ● ● ● (5/5) | BAJAS: 0";
			// 
			// pnlLateralDerecho
			// 
			this.pnlLateralDerecho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(50)))));
			this.pnlLateralDerecho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLateralDerecho.Controls.Add(this.lstDerrotadosCpu);
			this.pnlLateralDerecho.Controls.Add(this.lblHeaderDerrotadosCpu);
			this.pnlLateralDerecho.Controls.Add(this.lstBitacoraCombate);
			this.pnlLateralDerecho.Controls.Add(this.lblHeaderBitacora);
			this.pnlLateralDerecho.Controls.Add(this.lstColaJugador);
			this.pnlLateralDerecho.Controls.Add(this.lblHeaderCola);
			this.pnlLateralDerecho.Location = new System.Drawing.Point(860, 82);
			this.pnlLateralDerecho.Name = "pnlLateralDerecho";
			this.pnlLateralDerecho.Size = new System.Drawing.Size(408, 396);
			this.pnlLateralDerecho.TabIndex = 3;
			// 
			// lblHeaderCola
			// 
			this.lblHeaderCola.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
			this.lblHeaderCola.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
			this.lblHeaderCola.ForeColor = System.Drawing.Color.Gold;
			this.lblHeaderCola.Location = new System.Drawing.Point(8, 6);
			this.lblHeaderCola.Name = "lblHeaderCola";
			this.lblHeaderCola.Size = new System.Drawing.Size(390, 22);
			this.lblHeaderCola.TabIndex = 0;
			this.lblHeaderCola.Text = " TU BANCA POKÉMON / CAMBIO (CLIC PARA ELEGIR)";
			this.lblHeaderCola.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lstColaJugador
			// 
			this.lstColaJugador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(20)))), ((int)(((byte)(32)))));
			this.lstColaJugador.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lstColaJugador.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold);
			this.lstColaJugador.ForeColor = System.Drawing.Color.Aquamarine;
			this.lstColaJugador.FormattingEnabled = true;
			this.lstColaJugador.ItemHeight = 14;
			this.lstColaJugador.Location = new System.Drawing.Point(8, 30);
			this.lstColaJugador.Name = "lstColaJugador";
			this.lstColaJugador.Size = new System.Drawing.Size(390, 88);
			this.lstColaJugador.TabIndex = 1;
			this.lstColaJugador.SelectedIndexChanged += new System.EventHandler(this.LstColaJugadorSelectedIndexChanged);
			this.lstColaJugador.DoubleClick += new System.EventHandler(this.LstColaJugadorDoubleClick);
			// 
			// lblHeaderDerrotadosCpu
			// 
			this.lblHeaderDerrotadosCpu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.lblHeaderDerrotadosCpu.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
			this.lblHeaderDerrotadosCpu.ForeColor = System.Drawing.Color.Gold;
			this.lblHeaderDerrotadosCpu.Location = new System.Drawing.Point(8, 124);
			this.lblHeaderDerrotadosCpu.Name = "lblHeaderDerrotadosCpu";
			this.lblHeaderDerrotadosCpu.Size = new System.Drawing.Size(390, 22);
			this.lblHeaderDerrotadosCpu.TabIndex = 4;
			this.lblHeaderDerrotadosCpu.Text = " CONTEO EQUIPO CPU (DERROTADOS: 0/3)";
			this.lblHeaderDerrotadosCpu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lstDerrotadosCpu
			// 
			this.lstDerrotadosCpu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(16)))), ((int)(((byte)(22)))));
			this.lstDerrotadosCpu.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold);
			this.lstDerrotadosCpu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(185)))), ((int)(((byte)(165)))));
			this.lstDerrotadosCpu.FormattingEnabled = true;
			this.lstDerrotadosCpu.ItemHeight = 14;
			this.lstDerrotadosCpu.Location = new System.Drawing.Point(8, 148);
			this.lstDerrotadosCpu.Name = "lstDerrotadosCpu";
			this.lstDerrotadosCpu.Size = new System.Drawing.Size(390, 74);
			this.lstDerrotadosCpu.TabIndex = 5;
			// 
			// lblHeaderBitacora
			// 
			this.lblHeaderBitacora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(58)))), ((int)(((byte)(92)))));
			this.lblHeaderBitacora.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
			this.lblHeaderBitacora.ForeColor = System.Drawing.Color.Gold;
			this.lblHeaderBitacora.Location = new System.Drawing.Point(8, 228);
			this.lblHeaderBitacora.Name = "lblHeaderBitacora";
			this.lblHeaderBitacora.Size = new System.Drawing.Size(390, 22);
			this.lblHeaderBitacora.TabIndex = 2;
			this.lblHeaderBitacora.Text = " HISTORIAL DE COMBATE POR TURNOS";
			this.lblHeaderBitacora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lstBitacoraCombate
			// 
			this.lstBitacoraCombate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(20)))), ((int)(((byte)(32)))));
			this.lstBitacoraCombate.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
			this.lstBitacoraCombate.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lstBitacoraCombate.FormattingEnabled = true;
			this.lstBitacoraCombate.HorizontalScrollbar = true;
			this.lstBitacoraCombate.Location = new System.Drawing.Point(8, 252);
			this.lstBitacoraCombate.Name = "lstBitacoraCombate";
			this.lstBitacoraCombate.Size = new System.Drawing.Size(390, 134);
			this.lstBitacoraCombate.TabIndex = 3;
			// 
			// pnlMenuInferior
			// 
			this.pnlMenuInferior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(54)))));
			this.pnlMenuInferior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlMenuInferior.Controls.Add(this.button1);
			this.pnlMenuInferior.Controls.Add(this.btnReiniciarBatalla);
			this.pnlMenuInferior.Controls.Add(this.btnCargarEnergia);
			this.pnlMenuInferior.Controls.Add(this.btnCambiarLeviatan);
			this.pnlMenuInferior.Controls.Add(this.btnDefender);
			this.pnlMenuInferior.Controls.Add(this.btnEspecial);
			this.pnlMenuInferior.Controls.Add(this.btnAtacar);
			this.pnlMenuInferior.Controls.Add(this.lblDialogoCombate);
			this.pnlMenuInferior.Location = new System.Drawing.Point(12, 486);
			this.pnlMenuInferior.Name = "pnlMenuInferior";
			this.pnlMenuInferior.Size = new System.Drawing.Size(1256, 152);
			this.pnlMenuInferior.TabIndex = 4;
			// 
			// lblDialogoCombate
			// 
			this.lblDialogoCombate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(36)))));
			this.lblDialogoCombate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblDialogoCombate.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold);
			this.lblDialogoCombate.ForeColor = System.Drawing.Color.White;
			this.lblDialogoCombate.Location = new System.Drawing.Point(12, 12);
			this.lblDialogoCombate.Name = "lblDialogoCombate";
			this.lblDialogoCombate.Padding = new System.Windows.Forms.Padding(12);
			this.lblDialogoCombate.Size = new System.Drawing.Size(520, 126);
			this.lblDialogoCombate.TabIndex = 0;
			this.lblDialogoCombate.Text = "¡Un Leviatán salvaje ha aparecido! ¿Qué debería hacer tu Leviatán?";
			// 
			// btnAtacar
			// 
			this.btnAtacar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(48)))), ((int)(((byte)(32)))));
			this.btnAtacar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAtacar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAtacar.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.btnAtacar.ForeColor = System.Drawing.Color.White;
			this.btnAtacar.Location = new System.Drawing.Point(546, 12);
			this.btnAtacar.Name = "btnAtacar";
			this.btnAtacar.Size = new System.Drawing.Size(230, 58);
			this.btnAtacar.TabIndex = 1;
			this.btnAtacar.Text = "⚔ ATACAR\r\n(1 ENERGÍA)";
			this.btnAtacar.UseVisualStyleBackColor = false;
			this.btnAtacar.Click += new System.EventHandler(this.BtnAtacarClick);
			// 
			// btnEspecial
			// 
			this.btnEspecial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(115)))), ((int)(((byte)(20)))));
			this.btnEspecial.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnEspecial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEspecial.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold);
			this.btnEspecial.ForeColor = System.Drawing.Color.White;
			this.btnEspecial.Location = new System.Drawing.Point(786, 12);
			this.btnEspecial.Name = "btnEspecial";
			this.btnEspecial.Size = new System.Drawing.Size(230, 58);
			this.btnEspecial.TabIndex = 2;
			this.btnEspecial.Text = "⚡ HABILIDAD ESPECIAL\r\n(2 ENERGÍA | 100%)";
			this.btnEspecial.UseVisualStyleBackColor = false;
			this.btnEspecial.Click += new System.EventHandler(this.BtnEspecialClick);
			// 
			// btnDefender
			// 
			this.btnDefender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(112)))), ((int)(((byte)(188)))));
			this.btnDefender.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnDefender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDefender.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.btnDefender.ForeColor = System.Drawing.Color.White;
			this.btnDefender.Location = new System.Drawing.Point(546, 80);
			this.btnDefender.Name = "btnDefender";
			this.btnDefender.Size = new System.Drawing.Size(230, 58);
			this.btnDefender.TabIndex = 3;
			this.btnDefender.Text = "🛡 DEFENDER\r\n(+ESCUDO | 1 EN)";
			this.btnDefender.UseVisualStyleBackColor = false;
			this.btnDefender.Click += new System.EventHandler(this.BtnDefenderClick);
			// 
			// btnCambiarLeviatan
			// 
			this.btnCambiarLeviatan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(145)))), ((int)(((byte)(98)))));
			this.btnCambiarLeviatan.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCambiarLeviatan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCambiarLeviatan.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold);
			this.btnCambiarLeviatan.ForeColor = System.Drawing.Color.White;
			this.btnCambiarLeviatan.Location = new System.Drawing.Point(786, 80);
			this.btnCambiarLeviatan.Name = "btnCambiarLeviatan";
			this.btnCambiarLeviatan.Size = new System.Drawing.Size(230, 58);
			this.btnCambiarLeviatan.TabIndex = 4;
			this.btnCambiarLeviatan.Text = "🔄 CAMBIAR LEVIATÁN\r\n(BUSCAR VENTAJA | 1 EN)";
			this.btnCambiarLeviatan.UseVisualStyleBackColor = false;
			this.btnCambiarLeviatan.Click += new System.EventHandler(this.BtnCambiarLeviatanClick);
			// 
			// btnCargarEnergia
			// 
			this.btnCargarEnergia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(68)))), ((int)(((byte)(158)))));
			this.btnCargarEnergia.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCargarEnergia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCargarEnergia.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
			this.btnCargarEnergia.ForeColor = System.Drawing.Color.White;
			this.btnCargarEnergia.Location = new System.Drawing.Point(1026, 12);
			this.btnCargarEnergia.Name = "btnCargarEnergia";
			this.btnCargarEnergia.Size = new System.Drawing.Size(216, 40);
			this.btnCargarEnergia.TabIndex = 5;
			this.btnCargarEnergia.Text = "🔋 CARGAR (+2 ENERGÍA)";
			this.btnCargarEnergia.UseVisualStyleBackColor = false;
			this.btnCargarEnergia.Click += new System.EventHandler(this.BtnCargarEnergiaClick);
			// 
			// btnReiniciarBatalla
			// 
			this.btnReiniciarBatalla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(80)))), ((int)(((byte)(100)))));
			this.btnReiniciarBatalla.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnReiniciarBatalla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReiniciarBatalla.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
			this.btnReiniciarBatalla.ForeColor = System.Drawing.Color.White;
			this.btnReiniciarBatalla.Location = new System.Drawing.Point(1026, 56);
			this.btnReiniciarBatalla.Name = "btnReiniciarBatalla";
			this.btnReiniciarBatalla.Size = new System.Drawing.Size(216, 32);
			this.btnReiniciarBatalla.TabIndex = 6;
			this.btnReiniciarBatalla.Text = "↻ REINICIAR BATALLA";
			this.btnReiniciarBatalla.UseVisualStyleBackColor = false;
			this.btnReiniciarBatalla.Click += new System.EventHandler(this.BtnReiniciarBatallaClick);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Gold;
			this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
			this.button1.ForeColor = System.Drawing.Color.Black;
			this.button1.Location = new System.Drawing.Point(1026, 94);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(216, 44);
			this.button1.TabIndex = 0;
			this.button1.Text = "Continuar";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// tmrTurnoCpu
			// 
			this.tmrTurnoCpu.Interval = 1700;
			this.tmrTurnoCpu.Tick += new System.EventHandler(this.TmrTurnoCpuTick);
			// 
			// tmrPausaDerrota
			// 
			this.tmrPausaDerrota.Interval = 2400;
			this.tmrPausaDerrota.Tick += new System.EventHandler(this.TmrPausaDerrotaTick);
			// 
			// Batalla
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(18)))), ((int)(((byte)(30)))));
			this.ClientSize = new System.Drawing.Size(1280, 650);
			this.Controls.Add(this.pnlMenuInferior);
			this.Controls.Add(this.pnlLateralDerecho);
			this.Controls.Add(this.pnlArenaCombate);
			this.Controls.Add(this.pnlHeaderGba);
			this.Name = "Batalla";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Batalla - Leviatanes 2";
			this.pnlHeaderGba.ResumeLayout(false);
			this.pnlArenaCombate.ResumeLayout(false);
			this.pnlHudCpu.ResumeLayout(false);
			this.pnlVentajaElemental.ResumeLayout(false);
			this.pnlHudJugador.ResumeLayout(false);
			this.pnlLateralDerecho.ResumeLayout(false);
			this.pnlMenuInferior.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.Panel pnlHeaderGba;
		private System.Windows.Forms.Label lblTurnoActual;
		private System.Windows.Forms.Label lblGuiaElemental;
		private System.Windows.Forms.Label lblTituloBatalla;
		private System.Windows.Forms.Panel pnlArenaCombate;
		private System.Windows.Forms.Panel pnlHudCpu;
		private System.Windows.Forms.Label lblBolitasCpu;
		private System.Windows.Forms.Label lblStatsCpu;
		private System.Windows.Forms.ProgressBar pbVidaCpu;
		private System.Windows.Forms.Label lblElementoCpu;
		private System.Windows.Forms.Label lblNombreCpu;
		private System.Windows.Forms.Panel pnlSpriteCpu;
		private System.Windows.Forms.Panel pnlVentajaElemental;
		private System.Windows.Forms.Label lblVentajaElemental;
		private System.Windows.Forms.Panel pnlSpriteJugador;
		private System.Windows.Forms.Panel pnlHudJugador;
		private System.Windows.Forms.Label lblEspecialInfoJugador;
		private System.Windows.Forms.ProgressBar pbEspecialJugador;
		private System.Windows.Forms.Label lblBolitasJugador;
		private System.Windows.Forms.Label lblStatsJugador;
		private System.Windows.Forms.ProgressBar pbVidaJugador;
		private System.Windows.Forms.Label lblElementoJugador;
		private System.Windows.Forms.Label lblNombreJugador;
		private System.Windows.Forms.Panel pnlLateralDerecho;
		private System.Windows.Forms.ListBox lstDerrotadosCpu;
		private System.Windows.Forms.Label lblHeaderDerrotadosCpu;
		private System.Windows.Forms.ListBox lstBitacoraCombate;
		private System.Windows.Forms.Label lblHeaderBitacora;
		private System.Windows.Forms.ListBox lstColaJugador;
		private System.Windows.Forms.Label lblHeaderCola;
		private System.Windows.Forms.Panel pnlMenuInferior;
		private System.Windows.Forms.Button btnReiniciarBatalla;
		private System.Windows.Forms.Button btnCargarEnergia;
		private System.Windows.Forms.Button btnCambiarLeviatan;
		private System.Windows.Forms.Button btnDefender;
		private System.Windows.Forms.Button btnEspecial;
		private System.Windows.Forms.Button btnAtacar;
		private System.Windows.Forms.Label lblDialogoCombate;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Timer tmrTurnoCpu;
		private System.Windows.Forms.Timer tmrPausaDerrota;
	}
}
