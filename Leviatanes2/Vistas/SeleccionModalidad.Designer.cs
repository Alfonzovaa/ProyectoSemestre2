/*
 * Created by SharpDevelop.
 * User: Alfonzovaa
 * Date: 08/11/2026
 * Time: 08:10 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Leviatanes2
{
	partial class SeleccionModalidad
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
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlHeaderGba = new System.Windows.Forms.Panel();
			this.lblSubtituloGba = new System.Windows.Forms.Label();
			this.lblTituloGba = new System.Windows.Forms.Label();
			this.pnlFranjaDorada = new System.Windows.Forms.Panel();
			this.pnlMarcoModalidad = new System.Windows.Forms.Panel();
			this.pnlInteriorModalidad = new System.Windows.Forms.Panel();
			this.lblEstadoRed = new System.Windows.Forms.Label();
			this.btnUnirse = new System.Windows.Forms.Button();
			this.btnHospedar = new System.Windows.Forms.Button();
			this.txtPuerto = new System.Windows.Forms.TextBox();
			this.lblPuerto = new System.Windows.Forms.Label();
			this.txtIp = new System.Windows.Forms.TextBox();
			this.lblIp = new System.Windows.Forms.Label();
			this.lblReglaEscenarioAleatorio = new System.Windows.Forms.Label();
			this.rbModoRedLan = new System.Windows.Forms.RadioButton();
			this.rbModoCpu = new System.Windows.Forms.RadioButton();
			this.rbModoLocal = new System.Windows.Forms.RadioButton();
			this.lblHeaderModalidad = new System.Windows.Forms.Label();
			this.pnlMarcoEntrenadores = new System.Windows.Forms.Panel();
			this.pnlInteriorEntrenadores = new System.Windows.Forms.Panel();
			this.btnGuardarNombres = new System.Windows.Forms.Button();
			this.lblStatsEntrenador2 = new System.Windows.Forms.Label();
			this.txtEntrenador2 = new System.Windows.Forms.TextBox();
			this.lblEntrenador2 = new System.Windows.Forms.Label();
			this.lblStatsEntrenador1 = new System.Windows.Forms.Label();
			this.txtEntrenador1 = new System.Windows.Forms.TextBox();
			this.lblEntrenador1 = new System.Windows.Forms.Label();
			this.lblHeaderEntrenadores = new System.Windows.Forms.Label();
			this.pnlMarcoCatalogo = new System.Windows.Forms.Panel();
			this.pnlInteriorCatalogo = new System.Windows.Forms.Panel();
			this.btnAgregarEquipo2 = new System.Windows.Forms.Button();
			this.btnAgregarEquipo1 = new System.Windows.Forms.Button();
			this.lstCatalogo = new System.Windows.Forms.ListBox();
			this.lblHeaderCatalogo = new System.Windows.Forms.Label();
			this.pnlMarcoPreview = new System.Windows.Forms.Panel();
			this.pnlInteriorPreview = new System.Windows.Forms.Panel();
			this.lblPreviewEspecial = new System.Windows.Forms.Label();
			this.lblPreviewStats = new System.Windows.Forms.Label();
			this.lblPreviewNombre = new System.Windows.Forms.Label();
			this.pnlPreviewSprite = new System.Windows.Forms.Panel();
			this.lblPreviewFaccion = new System.Windows.Forms.Label();
			this.pnlMarcoEquipo1 = new System.Windows.Forms.Panel();
			this.pnlInteriorEquipo1 = new System.Windows.Forms.Panel();
			this.btnVaciarEquipo1 = new System.Windows.Forms.Button();
			this.btnAutoEquipo1 = new System.Windows.Forms.Button();
			this.lstEquipo1 = new System.Windows.Forms.ListBox();
			this.lblHeaderEquipo1 = new System.Windows.Forms.Label();
			this.pnlMarcoEquipo2 = new System.Windows.Forms.Panel();
			this.pnlInteriorEquipo2 = new System.Windows.Forms.Panel();
			this.btnVaciarEquipo2 = new System.Windows.Forms.Button();
			this.btnAutoEquipo2 = new System.Windows.Forms.Button();
			this.lstEquipo2 = new System.Windows.Forms.ListBox();
			this.lblHeaderEquipo2 = new System.Windows.Forms.Label();
			this.pnlMarcoDialogoGba = new System.Windows.Forms.Panel();
			this.pnlInteriorDialogoGba = new System.Windows.Forms.Panel();
			this.lblTextoProfesorGba = new System.Windows.Forms.Label();
			this.lblTituloDialogoGba = new System.Windows.Forms.Label();
			this.btnIrBatalla = new System.Windows.Forms.Button();
			this.btnVolverMenu = new System.Windows.Forms.Button();
			this.pnlHeaderGba.SuspendLayout();
			this.pnlMarcoModalidad.SuspendLayout();
			this.pnlInteriorModalidad.SuspendLayout();
			this.pnlMarcoEntrenadores.SuspendLayout();
			this.pnlInteriorEntrenadores.SuspendLayout();
			this.pnlMarcoCatalogo.SuspendLayout();
			this.pnlInteriorCatalogo.SuspendLayout();
			this.pnlMarcoPreview.SuspendLayout();
			this.pnlInteriorPreview.SuspendLayout();
			this.pnlMarcoEquipo1.SuspendLayout();
			this.pnlInteriorEquipo1.SuspendLayout();
			this.pnlMarcoEquipo2.SuspendLayout();
			this.pnlInteriorEquipo2.SuspendLayout();
			this.pnlMarcoDialogoGba.SuspendLayout();
			this.pnlInteriorDialogoGba.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeaderGba
			// 
			this.pnlHeaderGba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(41)))), ((int)(((byte)(18)))));
			this.pnlHeaderGba.Controls.Add(this.lblSubtituloGba);
			this.pnlHeaderGba.Controls.Add(this.lblTituloGba);
			this.pnlHeaderGba.Location = new System.Drawing.Point(0, 0);
			this.pnlHeaderGba.Name = "pnlHeaderGba";
			this.pnlHeaderGba.Size = new System.Drawing.Size(960, 52);
			this.pnlHeaderGba.TabIndex = 0;
			// 
			// lblSubtituloGba
			// 
			this.lblSubtituloGba.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSubtituloGba.ForeColor = System.Drawing.Color.White;
			this.lblSubtituloGba.Location = new System.Drawing.Point(16, 30);
			this.lblSubtituloGba.Name = "lblSubtituloGba";
			this.lblSubtituloGba.Size = new System.Drawing.Size(930, 18);
			this.lblSubtituloGba.TabIndex = 1;
			this.lblSubtituloGba.Text = "EDICION ROJO FUEGO GBA | PERSISTENCIA EN ARCHIVO (.TXT)";
			// 
			// lblTituloGba
			// 
			this.lblTituloGba.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTituloGba.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.lblTituloGba.Location = new System.Drawing.Point(14, 4);
			this.lblTituloGba.Name = "lblTituloGba";
			this.lblTituloGba.Size = new System.Drawing.Size(930, 26);
			this.lblTituloGba.TabIndex = 0;
			this.lblTituloGba.Text = "ARENA ELEMENTAL GBA - SELECCION DE MODALIDAD Y EQUIPOS";
			// 
			// pnlFranjaDorada
			// 
			this.pnlFranjaDorada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.pnlFranjaDorada.Location = new System.Drawing.Point(0, 52);
			this.pnlFranjaDorada.Name = "pnlFranjaDorada";
			this.pnlFranjaDorada.Size = new System.Drawing.Size(960, 4);
			this.pnlFranjaDorada.TabIndex = 1;
			// 
			// pnlMarcoModalidad
			// 
			this.pnlMarcoModalidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.pnlMarcoModalidad.Controls.Add(this.pnlInteriorModalidad);
			this.pnlMarcoModalidad.Location = new System.Drawing.Point(12, 62);
			this.pnlMarcoModalidad.Name = "pnlMarcoModalidad";
			this.pnlMarcoModalidad.Size = new System.Drawing.Size(936, 128);
			this.pnlMarcoModalidad.TabIndex = 2;
			// 
			// pnlInteriorModalidad
			// 
			this.pnlInteriorModalidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(243)))), ((int)(((byte)(212)))));
			this.pnlInteriorModalidad.Controls.Add(this.lblEstadoRed);
			this.pnlInteriorModalidad.Controls.Add(this.btnUnirse);
			this.pnlInteriorModalidad.Controls.Add(this.btnHospedar);
			this.pnlInteriorModalidad.Controls.Add(this.txtPuerto);
			this.pnlInteriorModalidad.Controls.Add(this.lblPuerto);
			this.pnlInteriorModalidad.Controls.Add(this.txtIp);
			this.pnlInteriorModalidad.Controls.Add(this.lblIp);
			this.pnlInteriorModalidad.Controls.Add(this.lblReglaEscenarioAleatorio);
			this.pnlInteriorModalidad.Controls.Add(this.rbModoRedLan);
			this.pnlInteriorModalidad.Controls.Add(this.rbModoCpu);
			this.pnlInteriorModalidad.Controls.Add(this.rbModoLocal);
			this.pnlInteriorModalidad.Controls.Add(this.lblHeaderModalidad);
			this.pnlInteriorModalidad.Location = new System.Drawing.Point(3, 3);
			this.pnlInteriorModalidad.Name = "pnlInteriorModalidad";
			this.pnlInteriorModalidad.Size = new System.Drawing.Size(930, 122);
			this.pnlInteriorModalidad.TabIndex = 0;
			// 
			// lblEstadoRed
			// 
			this.lblEstadoRed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(36)))), ((int)(((byte)(54)))));
			this.lblEstadoRed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblEstadoRed.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEstadoRed.ForeColor = System.Drawing.Color.Aquamarine;
			this.lblEstadoRed.Location = new System.Drawing.Point(412, 58);
			this.lblEstadoRed.Name = "lblEstadoRed";
			this.lblEstadoRed.Padding = new System.Windows.Forms.Padding(4);
			this.lblEstadoRed.Size = new System.Drawing.Size(506, 34);
			this.lblEstadoRed.TabIndex = 11;
			this.lblEstadoRed.Text = "Estado Cable Link: Modo Local activo (Sin conexion TCP requerida).";
			this.lblEstadoRed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnUnirse
			// 
			this.btnUnirse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(135)))), ((int)(((byte)(105)))));
			this.btnUnirse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUnirse.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUnirse.ForeColor = System.Drawing.Color.White;
			this.btnUnirse.Location = new System.Drawing.Point(812, 27);
			this.btnUnirse.Name = "btnUnirse";
			this.btnUnirse.Size = new System.Drawing.Size(106, 26);
			this.btnUnirse.TabIndex = 10;
			this.btnUnirse.Text = "UNIRSE SALA";
			this.btnUnirse.UseVisualStyleBackColor = false;
			this.btnUnirse.Click += new System.EventHandler(this.BtnUnirseClick);
			// 
			// btnHospedar
			// 
			this.btnHospedar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
			this.btnHospedar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnHospedar.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHospedar.ForeColor = System.Drawing.Color.White;
			this.btnHospedar.Location = new System.Drawing.Point(698, 27);
			this.btnHospedar.Name = "btnHospedar";
			this.btnHospedar.Size = new System.Drawing.Size(106, 26);
			this.btnHospedar.TabIndex = 9;
			this.btnHospedar.Text = "HOSPEDAR SALA";
			this.btnHospedar.UseVisualStyleBackColor = false;
			this.btnHospedar.Click += new System.EventHandler(this.BtnHospedarClick);
			// 
			// txtPuerto
			// 
			this.txtPuerto.BackColor = System.Drawing.Color.White;
			this.txtPuerto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPuerto.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPuerto.Location = new System.Drawing.Point(630, 29);
			this.txtPuerto.Name = "txtPuerto";
			this.txtPuerto.Size = new System.Drawing.Size(58, 22);
			this.txtPuerto.TabIndex = 8;
			this.txtPuerto.Text = "5000";
			// 
			// lblPuerto
			// 
			this.lblPuerto.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPuerto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblPuerto.Location = new System.Drawing.Point(568, 32);
			this.lblPuerto.Name = "lblPuerto";
			this.lblPuerto.Size = new System.Drawing.Size(60, 18);
			this.lblPuerto.TabIndex = 7;
			this.lblPuerto.Text = "PUERTO:";
			// 
			// txtIp
			// 
			this.txtIp.BackColor = System.Drawing.Color.White;
			this.txtIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtIp.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtIp.Location = new System.Drawing.Point(448, 29);
			this.txtIp.Name = "txtIp";
			this.txtIp.Size = new System.Drawing.Size(112, 22);
			this.txtIp.TabIndex = 6;
			this.txtIp.Text = "127.0.0.1";
			// 
			// lblIp
			// 
			this.lblIp.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblIp.Location = new System.Drawing.Point(412, 32);
			this.lblIp.Name = "lblIp";
			this.lblIp.Size = new System.Drawing.Size(34, 18);
			this.lblIp.TabIndex = 5;
			this.lblIp.Text = "IP:";
			// 
			// lblReglaEscenarioAleatorio
			// 
			this.lblReglaEscenarioAleatorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(224)))), ((int)(((byte)(185)))));
			this.lblReglaEscenarioAleatorio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblReglaEscenarioAleatorio.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblReglaEscenarioAleatorio.ForeColor = System.Drawing.Color.DarkRed;
			this.lblReglaEscenarioAleatorio.Location = new System.Drawing.Point(12, 98);
			this.lblReglaEscenarioAleatorio.Name = "lblReglaEscenarioAleatorio";
			this.lblReglaEscenarioAleatorio.Size = new System.Drawing.Size(906, 20);
			this.lblReglaEscenarioAleatorio.TabIndex = 4;
			this.lblReglaEscenarioAleatorio.Text = "Escenario de la Arena: Aleatorio Dinámico (Sorteado al iniciar la batalla y rotad" +
			"o cada 2 turnos)";
			this.lblReglaEscenarioAleatorio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// rbModoRedLan
			// 
			this.rbModoRedLan.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbModoRedLan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.rbModoRedLan.Location = new System.Drawing.Point(12, 74);
			this.rbModoRedLan.Name = "rbModoRedLan";
			this.rbModoRedLan.Size = new System.Drawing.Size(390, 22);
			this.rbModoRedLan.TabIndex = 3;
			this.rbModoRedLan.Text = "MODO CABLE LINK / RED LAN (Sockets TCP)";
			this.rbModoRedLan.UseVisualStyleBackColor = true;
			this.rbModoRedLan.CheckedChanged += new System.EventHandler(this.ModalidadCheckedChanged);
			// 
			// rbModoCpu
			// 
			this.rbModoCpu.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbModoCpu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.rbModoCpu.Location = new System.Drawing.Point(12, 51);
			this.rbModoCpu.Name = "rbModoCpu";
			this.rbModoCpu.Size = new System.Drawing.Size(390, 22);
			this.rbModoCpu.TabIndex = 2;
			this.rbModoCpu.Text = "MODO ENTRENAMIENTO (Jugador 1 vs CPU IA)";
			this.rbModoCpu.UseVisualStyleBackColor = true;
			this.rbModoCpu.CheckedChanged += new System.EventHandler(this.ModalidadCheckedChanged);
			// 
			// rbModoLocal
			// 
			this.rbModoLocal.Checked = true;
			this.rbModoLocal.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rbModoLocal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.rbModoLocal.Location = new System.Drawing.Point(12, 28);
			this.rbModoLocal.Name = "rbModoLocal";
			this.rbModoLocal.Size = new System.Drawing.Size(390, 22);
			this.rbModoLocal.TabIndex = 1;
			this.rbModoLocal.TabStop = true;
			this.rbModoLocal.Text = "MODO DUELO LOCAL (Jugador 1 vs Jugador 2)";
			this.rbModoLocal.UseVisualStyleBackColor = true;
			this.rbModoLocal.CheckedChanged += new System.EventHandler(this.ModalidadCheckedChanged);
			// 
			// lblHeaderModalidad
			// 
			this.lblHeaderModalidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(84)))));
			this.lblHeaderModalidad.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderModalidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.lblHeaderModalidad.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderModalidad.Name = "lblHeaderModalidad";
			this.lblHeaderModalidad.Size = new System.Drawing.Size(930, 24);
			this.lblHeaderModalidad.TabIndex = 0;
			this.lblHeaderModalidad.Text = " 1. MODALIDAD DE COMBATE Y CONFIGURACION DE SALA CABLE LINK (SOCKETS TCP/IP)";
			this.lblHeaderModalidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlMarcoEntrenadores
			// 
			this.pnlMarcoEntrenadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.pnlMarcoEntrenadores.Controls.Add(this.pnlInteriorEntrenadores);
			this.pnlMarcoEntrenadores.Location = new System.Drawing.Point(12, 196);
			this.pnlMarcoEntrenadores.Name = "pnlMarcoEntrenadores";
			this.pnlMarcoEntrenadores.Size = new System.Drawing.Size(936, 76);
			this.pnlMarcoEntrenadores.TabIndex = 3;
			// 
			// pnlInteriorEntrenadores
			// 
			this.pnlInteriorEntrenadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(243)))), ((int)(((byte)(212)))));
			this.pnlInteriorEntrenadores.Controls.Add(this.btnGuardarNombres);
			this.pnlInteriorEntrenadores.Controls.Add(this.lblStatsEntrenador2);
			this.pnlInteriorEntrenadores.Controls.Add(this.txtEntrenador2);
			this.pnlInteriorEntrenadores.Controls.Add(this.lblEntrenador2);
			this.pnlInteriorEntrenadores.Controls.Add(this.lblStatsEntrenador1);
			this.pnlInteriorEntrenadores.Controls.Add(this.txtEntrenador1);
			this.pnlInteriorEntrenadores.Controls.Add(this.lblEntrenador1);
			this.pnlInteriorEntrenadores.Controls.Add(this.lblHeaderEntrenadores);
			this.pnlInteriorEntrenadores.Location = new System.Drawing.Point(3, 3);
			this.pnlInteriorEntrenadores.Name = "pnlInteriorEntrenadores";
			this.pnlInteriorEntrenadores.Size = new System.Drawing.Size(930, 70);
			this.pnlInteriorEntrenadores.TabIndex = 0;
			// 
			// btnGuardarNombres
			// 
			this.btnGuardarNombres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(110)))), ((int)(((byte)(180)))));
			this.btnGuardarNombres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGuardarNombres.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGuardarNombres.ForeColor = System.Drawing.Color.White;
			this.btnGuardarNombres.Location = new System.Drawing.Point(678, 26);
			this.btnGuardarNombres.Name = "btnGuardarNombres";
			this.btnGuardarNombres.Size = new System.Drawing.Size(240, 38);
			this.btnGuardarNombres.TabIndex = 7;
			this.btnGuardarNombres.Text = "GUARDAR / CARGAR NOMBRES";
			this.btnGuardarNombres.UseVisualStyleBackColor = false;
			this.btnGuardarNombres.Click += new System.EventHandler(this.BtnGuardarNombresClick);
			// 
			// lblStatsEntrenador2
			// 
			this.lblStatsEntrenador2.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatsEntrenador2.ForeColor = System.Drawing.Color.Firebrick;
			this.lblStatsEntrenador2.Location = new System.Drawing.Point(345, 49);
			this.lblStatsEntrenador2.Name = "lblStatsEntrenador2";
			this.lblStatsEntrenador2.Size = new System.Drawing.Size(320, 18);
			this.lblStatsEntrenador2.TabIndex = 6;
			this.lblStatsEntrenador2.Text = "PJ: 0 | V: 0 | D: 0 | Exito: 0.00%";
			// 
			// txtEntrenador2
			// 
			this.txtEntrenador2.BackColor = System.Drawing.Color.White;
			this.txtEntrenador2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEntrenador2.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEntrenador2.Location = new System.Drawing.Point(450, 25);
			this.txtEntrenador2.Name = "txtEntrenador2";
			this.txtEntrenador2.Size = new System.Drawing.Size(165, 22);
			this.txtEntrenador2.TabIndex = 5;
			this.txtEntrenador2.Text = "JugadorDos";
			// 
			// lblEntrenador2
			// 
			this.lblEntrenador2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEntrenador2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblEntrenador2.Location = new System.Drawing.Point(345, 27);
			this.lblEntrenador2.Name = "lblEntrenador2";
			this.lblEntrenador2.Size = new System.Drawing.Size(102, 18);
			this.lblEntrenador2.TabIndex = 4;
			this.lblEntrenador2.Text = "Entrenador 2:";
			// 
			// lblStatsEntrenador1
			// 
			this.lblStatsEntrenador1.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatsEntrenador1.ForeColor = System.Drawing.Color.DarkGreen;
			this.lblStatsEntrenador1.Location = new System.Drawing.Point(12, 49);
			this.lblStatsEntrenador1.Name = "lblStatsEntrenador1";
			this.lblStatsEntrenador1.Size = new System.Drawing.Size(320, 18);
			this.lblStatsEntrenador1.TabIndex = 3;
			this.lblStatsEntrenador1.Text = "PJ: 0 | V: 0 | D: 0 | Exito: 0.00%";
			// 
			// txtEntrenador1
			// 
			this.txtEntrenador1.BackColor = System.Drawing.Color.White;
			this.txtEntrenador1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtEntrenador1.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEntrenador1.Location = new System.Drawing.Point(116, 25);
			this.txtEntrenador1.Name = "txtEntrenador1";
			this.txtEntrenador1.Size = new System.Drawing.Size(165, 22);
			this.txtEntrenador1.TabIndex = 2;
			this.txtEntrenador1.Text = "JugadorUno";
			// 
			// lblEntrenador1
			// 
			this.lblEntrenador1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEntrenador1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblEntrenador1.Location = new System.Drawing.Point(12, 27);
			this.lblEntrenador1.Name = "lblEntrenador1";
			this.lblEntrenador1.Size = new System.Drawing.Size(102, 18);
			this.lblEntrenador1.TabIndex = 1;
			this.lblEntrenador1.Text = "Entrenador 1:";
			// 
			// lblHeaderEntrenadores
			// 
			this.lblHeaderEntrenadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(84)))));
			this.lblHeaderEntrenadores.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderEntrenadores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.lblHeaderEntrenadores.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderEntrenadores.Name = "lblHeaderEntrenadores";
			this.lblHeaderEntrenadores.Size = new System.Drawing.Size(930, 22);
			this.lblHeaderEntrenadores.TabIndex = 0;
			this.lblHeaderEntrenadores.Text = " 2. PERFILES DE ENTRENADORES Y ESTADISTICAS (jugadores.txt)";
			this.lblHeaderEntrenadores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlMarcoCatalogo
			// 
			this.pnlMarcoCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.pnlMarcoCatalogo.Controls.Add(this.pnlInteriorCatalogo);
			this.pnlMarcoCatalogo.Location = new System.Drawing.Point(12, 278);
			this.pnlMarcoCatalogo.Name = "pnlMarcoCatalogo";
			this.pnlMarcoCatalogo.Size = new System.Drawing.Size(320, 266);
			this.pnlMarcoCatalogo.TabIndex = 4;
			// 
			// pnlInteriorCatalogo
			// 
			this.pnlInteriorCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(243)))), ((int)(((byte)(212)))));
			this.pnlInteriorCatalogo.Controls.Add(this.btnAgregarEquipo2);
			this.pnlInteriorCatalogo.Controls.Add(this.btnAgregarEquipo1);
			this.pnlInteriorCatalogo.Controls.Add(this.lstCatalogo);
			this.pnlInteriorCatalogo.Controls.Add(this.lblHeaderCatalogo);
			this.pnlInteriorCatalogo.Location = new System.Drawing.Point(3, 3);
			this.pnlInteriorCatalogo.Name = "pnlInteriorCatalogo";
			this.pnlInteriorCatalogo.Size = new System.Drawing.Size(314, 260);
			this.pnlInteriorCatalogo.TabIndex = 0;
			// 
			// btnAgregarEquipo2
			// 
			this.btnAgregarEquipo2.BackColor = System.Drawing.Color.Firebrick;
			this.btnAgregarEquipo2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAgregarEquipo2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAgregarEquipo2.ForeColor = System.Drawing.Color.White;
			this.btnAgregarEquipo2.Location = new System.Drawing.Point(161, 218);
			this.btnAgregarEquipo2.Name = "btnAgregarEquipo2";
			this.btnAgregarEquipo2.Size = new System.Drawing.Size(145, 34);
			this.btnAgregarEquipo2.TabIndex = 3;
			this.btnAgregarEquipo2.Text = "+ A EQUIPO 2";
			this.btnAgregarEquipo2.UseVisualStyleBackColor = false;
			this.btnAgregarEquipo2.Click += new System.EventHandler(this.BtnAgregarEquipo2Click);
			// 
			// btnAgregarEquipo1
			// 
			this.btnAgregarEquipo1.BackColor = System.Drawing.Color.SeaGreen;
			this.btnAgregarEquipo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAgregarEquipo1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAgregarEquipo1.ForeColor = System.Drawing.Color.White;
			this.btnAgregarEquipo1.Location = new System.Drawing.Point(8, 218);
			this.btnAgregarEquipo1.Name = "btnAgregarEquipo1";
			this.btnAgregarEquipo1.Size = new System.Drawing.Size(145, 34);
			this.btnAgregarEquipo1.TabIndex = 2;
			this.btnAgregarEquipo1.Text = "+ A EQUIPO 1";
			this.btnAgregarEquipo1.UseVisualStyleBackColor = false;
			this.btnAgregarEquipo1.Click += new System.EventHandler(this.BtnAgregarEquipo1Click);
			// 
			// lstCatalogo
			// 
			this.lstCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
			this.lstCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstCatalogo.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstCatalogo.ForeColor = System.Drawing.Color.White;
			this.lstCatalogo.HorizontalScrollbar = true;
			this.lstCatalogo.Location = new System.Drawing.Point(8, 28);
			this.lstCatalogo.Name = "lstCatalogo";
			this.lstCatalogo.Size = new System.Drawing.Size(298, 184);
			this.lstCatalogo.TabIndex = 1;
			this.lstCatalogo.SelectedIndexChanged += new System.EventHandler(this.LstCatalogoSelectedIndexChanged);
			// 
			// lblHeaderCatalogo
			// 
			this.lblHeaderCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(52)))), ((int)(((byte)(84)))));
			this.lblHeaderCatalogo.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderCatalogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.lblHeaderCatalogo.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderCatalogo.Name = "lblHeaderCatalogo";
			this.lblHeaderCatalogo.Size = new System.Drawing.Size(314, 24);
			this.lblHeaderCatalogo.TabIndex = 0;
			this.lblHeaderCatalogo.Text = " 3. CATALOGO ELEMENTAL (personajes.txt)";
			this.lblHeaderCatalogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlMarcoPreview
			// 
			this.pnlMarcoPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.pnlMarcoPreview.Controls.Add(this.pnlInteriorPreview);
			this.pnlMarcoPreview.Location = new System.Drawing.Point(340, 278);
			this.pnlMarcoPreview.Name = "pnlMarcoPreview";
			this.pnlMarcoPreview.Size = new System.Drawing.Size(212, 266);
			this.pnlMarcoPreview.TabIndex = 5;
			// 
			// pnlInteriorPreview
			// 
			this.pnlInteriorPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(52)))));
			this.pnlInteriorPreview.Controls.Add(this.lblPreviewEspecial);
			this.pnlInteriorPreview.Controls.Add(this.lblPreviewStats);
			this.pnlInteriorPreview.Controls.Add(this.lblPreviewNombre);
			this.pnlInteriorPreview.Controls.Add(this.pnlPreviewSprite);
			this.pnlInteriorPreview.Controls.Add(this.lblPreviewFaccion);
			this.pnlInteriorPreview.Location = new System.Drawing.Point(3, 3);
			this.pnlInteriorPreview.Name = "pnlInteriorPreview";
			this.pnlInteriorPreview.Size = new System.Drawing.Size(206, 260);
			this.pnlInteriorPreview.TabIndex = 0;
			// 
			// lblPreviewEspecial
			// 
			this.lblPreviewEspecial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(243)))), ((int)(((byte)(212)))));
			this.lblPreviewEspecial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPreviewEspecial.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPreviewEspecial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblPreviewEspecial.Location = new System.Drawing.Point(8, 186);
			this.lblPreviewEspecial.Name = "lblPreviewEspecial";
			this.lblPreviewEspecial.Padding = new System.Windows.Forms.Padding(3);
			this.lblPreviewEspecial.Size = new System.Drawing.Size(190, 66);
			this.lblPreviewEspecial.TabIndex = 4;
			this.lblPreviewEspecial.Text = "[Llamarada Solar]: Ignora escudo rival.";
			// 
			// lblPreviewStats
			// 
			this.lblPreviewStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(28)))), ((int)(((byte)(44)))));
			this.lblPreviewStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPreviewStats.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPreviewStats.ForeColor = System.Drawing.Color.LightGreen;
			this.lblPreviewStats.Location = new System.Drawing.Point(8, 126);
			this.lblPreviewStats.Name = "lblPreviewStats";
			this.lblPreviewStats.Padding = new System.Windows.Forms.Padding(4);
			this.lblPreviewStats.Size = new System.Drawing.Size(190, 56);
			this.lblPreviewStats.TabIndex = 3;
			this.lblPreviewStats.Text = "HP: 45 | DEF: 20 | ATK: 35\r\nEMPATIA: 40 | SUMA: 100/100";
			// 
			// lblPreviewNombre
			// 
			this.lblPreviewNombre.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPreviewNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.lblPreviewNombre.Location = new System.Drawing.Point(4, 103);
			this.lblPreviewNombre.Name = "lblPreviewNombre";
			this.lblPreviewNombre.Size = new System.Drawing.Size(198, 20);
			this.lblPreviewNombre.TabIndex = 2;
			this.lblPreviewNombre.Text = "Ignis Leviatan Pirico";
			this.lblPreviewNombre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlPreviewSprite
			// 
			this.pnlPreviewSprite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(38)))));
			this.pnlPreviewSprite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlPreviewSprite.Location = new System.Drawing.Point(8, 28);
			this.pnlPreviewSprite.Name = "pnlPreviewSprite";
			this.pnlPreviewSprite.Size = new System.Drawing.Size(190, 72);
			this.pnlPreviewSprite.TabIndex = 1;
			this.pnlPreviewSprite.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlPreviewSpritePaint);
			// 
			// lblPreviewFaccion
			// 
			this.lblPreviewFaccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(41)))), ((int)(((byte)(18)))));
			this.lblPreviewFaccion.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPreviewFaccion.ForeColor = System.Drawing.Color.White;
			this.lblPreviewFaccion.Location = new System.Drawing.Point(0, 0);
			this.lblPreviewFaccion.Name = "lblPreviewFaccion";
			this.lblPreviewFaccion.Size = new System.Drawing.Size(206, 24);
			this.lblPreviewFaccion.TabIndex = 0;
			this.lblPreviewFaccion.Text = "POKEDEX :: [FUEGO]";
			this.lblPreviewFaccion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlMarcoEquipo1
			// 
			this.pnlMarcoEquipo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.pnlMarcoEquipo1.Controls.Add(this.pnlInteriorEquipo1);
			this.pnlMarcoEquipo1.Location = new System.Drawing.Point(560, 278);
			this.pnlMarcoEquipo1.Name = "pnlMarcoEquipo1";
			this.pnlMarcoEquipo1.Size = new System.Drawing.Size(190, 266);
			this.pnlMarcoEquipo1.TabIndex = 6;
			// 
			// pnlInteriorEquipo1
			// 
			this.pnlInteriorEquipo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(243)))), ((int)(((byte)(212)))));
			this.pnlInteriorEquipo1.Controls.Add(this.btnVaciarEquipo1);
			this.pnlInteriorEquipo1.Controls.Add(this.btnAutoEquipo1);
			this.pnlInteriorEquipo1.Controls.Add(this.lstEquipo1);
			this.pnlInteriorEquipo1.Controls.Add(this.lblHeaderEquipo1);
			this.pnlInteriorEquipo1.Location = new System.Drawing.Point(3, 3);
			this.pnlInteriorEquipo1.Name = "pnlInteriorEquipo1";
			this.pnlInteriorEquipo1.Size = new System.Drawing.Size(184, 260);
			this.pnlInteriorEquipo1.TabIndex = 0;
			// 
			// btnVaciarEquipo1
			// 
			this.btnVaciarEquipo1.BackColor = System.Drawing.Color.DimGray;
			this.btnVaciarEquipo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVaciarEquipo1.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnVaciarEquipo1.ForeColor = System.Drawing.Color.White;
			this.btnVaciarEquipo1.Location = new System.Drawing.Point(96, 218);
			this.btnVaciarEquipo1.Name = "btnVaciarEquipo1";
			this.btnVaciarEquipo1.Size = new System.Drawing.Size(80, 34);
			this.btnVaciarEquipo1.TabIndex = 3;
			this.btnVaciarEquipo1.Text = "VACIAR";
			this.btnVaciarEquipo1.UseVisualStyleBackColor = false;
			this.btnVaciarEquipo1.Click += new System.EventHandler(this.BtnVaciarEquipo1Click);
			// 
			// btnAutoEquipo1
			// 
			this.btnAutoEquipo1.BackColor = System.Drawing.Color.DarkSlateGray;
			this.btnAutoEquipo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAutoEquipo1.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAutoEquipo1.ForeColor = System.Drawing.Color.White;
			this.btnAutoEquipo1.Location = new System.Drawing.Point(8, 218);
			this.btnAutoEquipo1.Name = "btnAutoEquipo1";
			this.btnAutoEquipo1.Size = new System.Drawing.Size(80, 34);
			this.btnAutoEquipo1.TabIndex = 2;
			this.btnAutoEquipo1.Text = "AUTO 5";
			this.btnAutoEquipo1.UseVisualStyleBackColor = false;
			this.btnAutoEquipo1.Click += new System.EventHandler(this.BtnAutoEquipo1Click);
			// 
			// lstEquipo1
			// 
			this.lstEquipo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(26)))));
			this.lstEquipo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstEquipo1.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstEquipo1.ForeColor = System.Drawing.Color.LightGreen;
			this.lstEquipo1.HorizontalScrollbar = true;
			this.lstEquipo1.Location = new System.Drawing.Point(8, 28);
			this.lstEquipo1.Name = "lstEquipo1";
			this.lstEquipo1.Size = new System.Drawing.Size(168, 184);
			this.lstEquipo1.TabIndex = 1;
			// 
			// lblHeaderEquipo1
			// 
			this.lblHeaderEquipo1.BackColor = System.Drawing.Color.SeaGreen;
			this.lblHeaderEquipo1.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderEquipo1.ForeColor = System.Drawing.Color.White;
			this.lblHeaderEquipo1.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderEquipo1.Name = "lblHeaderEquipo1";
			this.lblHeaderEquipo1.Size = new System.Drawing.Size(184, 24);
			this.lblHeaderEquipo1.TabIndex = 0;
			this.lblHeaderEquipo1.Text = "EQUIPO J1 (5/5 CARTAS)";
			this.lblHeaderEquipo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlMarcoEquipo2
			// 
			this.pnlMarcoEquipo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.pnlMarcoEquipo2.Controls.Add(this.pnlInteriorEquipo2);
			this.pnlMarcoEquipo2.Location = new System.Drawing.Point(758, 278);
			this.pnlMarcoEquipo2.Name = "pnlMarcoEquipo2";
			this.pnlMarcoEquipo2.Size = new System.Drawing.Size(190, 266);
			this.pnlMarcoEquipo2.TabIndex = 7;
			// 
			// pnlInteriorEquipo2
			// 
			this.pnlInteriorEquipo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(243)))), ((int)(((byte)(212)))));
			this.pnlInteriorEquipo2.Controls.Add(this.btnVaciarEquipo2);
			this.pnlInteriorEquipo2.Controls.Add(this.btnAutoEquipo2);
			this.pnlInteriorEquipo2.Controls.Add(this.lstEquipo2);
			this.pnlInteriorEquipo2.Controls.Add(this.lblHeaderEquipo2);
			this.pnlInteriorEquipo2.Location = new System.Drawing.Point(3, 3);
			this.pnlInteriorEquipo2.Name = "pnlInteriorEquipo2";
			this.pnlInteriorEquipo2.Size = new System.Drawing.Size(184, 260);
			this.pnlInteriorEquipo2.TabIndex = 0;
			// 
			// btnVaciarEquipo2
			// 
			this.btnVaciarEquipo2.BackColor = System.Drawing.Color.DimGray;
			this.btnVaciarEquipo2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVaciarEquipo2.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnVaciarEquipo2.ForeColor = System.Drawing.Color.White;
			this.btnVaciarEquipo2.Location = new System.Drawing.Point(96, 218);
			this.btnVaciarEquipo2.Name = "btnVaciarEquipo2";
			this.btnVaciarEquipo2.Size = new System.Drawing.Size(80, 34);
			this.btnVaciarEquipo2.TabIndex = 3;
			this.btnVaciarEquipo2.Text = "VACIAR";
			this.btnVaciarEquipo2.UseVisualStyleBackColor = false;
			this.btnVaciarEquipo2.Click += new System.EventHandler(this.BtnVaciarEquipo2Click);
			// 
			// btnAutoEquipo2
			// 
			this.btnAutoEquipo2.BackColor = System.Drawing.Color.DarkSlateGray;
			this.btnAutoEquipo2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAutoEquipo2.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAutoEquipo2.ForeColor = System.Drawing.Color.White;
			this.btnAutoEquipo2.Location = new System.Drawing.Point(8, 218);
			this.btnAutoEquipo2.Name = "btnAutoEquipo2";
			this.btnAutoEquipo2.Size = new System.Drawing.Size(80, 34);
			this.btnAutoEquipo2.TabIndex = 2;
			this.btnAutoEquipo2.Text = "AUTO 5";
			this.btnAutoEquipo2.UseVisualStyleBackColor = false;
			this.btnAutoEquipo2.Click += new System.EventHandler(this.BtnAutoEquipo2Click);
			// 
			// lstEquipo2
			// 
			this.lstEquipo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(22)))), ((int)(((byte)(26)))));
			this.lstEquipo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstEquipo2.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstEquipo2.ForeColor = System.Drawing.Color.LightCoral;
			this.lstEquipo2.HorizontalScrollbar = true;
			this.lstEquipo2.Location = new System.Drawing.Point(8, 28);
			this.lstEquipo2.Name = "lstEquipo2";
			this.lstEquipo2.Size = new System.Drawing.Size(168, 184);
			this.lstEquipo2.TabIndex = 1;
			// 
			// lblHeaderEquipo2
			// 
			this.lblHeaderEquipo2.BackColor = System.Drawing.Color.Firebrick;
			this.lblHeaderEquipo2.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderEquipo2.ForeColor = System.Drawing.Color.White;
			this.lblHeaderEquipo2.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderEquipo2.Name = "lblHeaderEquipo2";
			this.lblHeaderEquipo2.Size = new System.Drawing.Size(184, 24);
			this.lblHeaderEquipo2.TabIndex = 0;
			this.lblHeaderEquipo2.Text = "EQUIPO J2 (5/5 CARTAS)";
			this.lblHeaderEquipo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlMarcoDialogoGba
			// 
			this.pnlMarcoDialogoGba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(41)))), ((int)(((byte)(18)))));
			this.pnlMarcoDialogoGba.Controls.Add(this.pnlInteriorDialogoGba);
			this.pnlMarcoDialogoGba.Location = new System.Drawing.Point(12, 550);
			this.pnlMarcoDialogoGba.Name = "pnlMarcoDialogoGba";
			this.pnlMarcoDialogoGba.Size = new System.Drawing.Size(460, 78);
			this.pnlMarcoDialogoGba.TabIndex = 8;
			// 
			// pnlInteriorDialogoGba
			// 
			this.pnlInteriorDialogoGba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(62)))));
			this.pnlInteriorDialogoGba.Controls.Add(this.lblTextoProfesorGba);
			this.pnlInteriorDialogoGba.Controls.Add(this.lblTituloDialogoGba);
			this.pnlInteriorDialogoGba.Location = new System.Drawing.Point(3, 3);
			this.pnlInteriorDialogoGba.Name = "pnlInteriorDialogoGba";
			this.pnlInteriorDialogoGba.Size = new System.Drawing.Size(454, 72);
			this.pnlInteriorDialogoGba.TabIndex = 0;
			// 
			// lblTextoProfesorGba
			// 
			this.lblTextoProfesorGba.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTextoProfesorGba.ForeColor = System.Drawing.Color.White;
			this.lblTextoProfesorGba.Location = new System.Drawing.Point(8, 23);
			this.lblTextoProfesorGba.Name = "lblTextoProfesorGba";
			this.lblTextoProfesorGba.Size = new System.Drawing.Size(438, 44);
			this.lblTextoProfesorGba.TabIndex = 1;
			this.lblTextoProfesorGba.Text = "Configura la modalidad y arma los equipos de 5 cartas. El escenario inicial se so" +
			"rteara al azar al iniciar ▼";
			// 
			// lblTituloDialogoGba
			// 
			this.lblTituloDialogoGba.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTituloDialogoGba.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.lblTituloDialogoGba.Location = new System.Drawing.Point(8, 5);
			this.lblTituloDialogoGba.Name = "lblTituloDialogoGba";
			this.lblTituloDialogoGba.Size = new System.Drawing.Size(438, 16);
			this.lblTituloDialogoGba.TabIndex = 0;
			this.lblTituloDialogoGba.Text = "PROF. LUCENA (CONSOLA GBA):";
			// 
			// btnIrBatalla
			// 
			this.btnIrBatalla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(41)))), ((int)(((byte)(18)))));
			this.btnIrBatalla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnIrBatalla.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnIrBatalla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.btnIrBatalla.Location = new System.Drawing.Point(480, 550);
			this.btnIrBatalla.Name = "btnIrBatalla";
			this.btnIrBatalla.Size = new System.Drawing.Size(310, 78);
			this.btnIrBatalla.TabIndex = 9;
			this.btnIrBatalla.Text = "CONTINUAR>>";
			this.btnIrBatalla.UseVisualStyleBackColor = false;
			this.btnIrBatalla.Click += new System.EventHandler(this.BtnIrBatallaClick);
			// 
			// btnVolverMenu
			// 
			this.btnVolverMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(68)))), ((int)(((byte)(90)))));
			this.btnVolverMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnVolverMenu.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnVolverMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(203)))), ((int)(((byte)(66)))));
			this.btnVolverMenu.Location = new System.Drawing.Point(798, 550);
			this.btnVolverMenu.Name = "btnVolverMenu";
			this.btnVolverMenu.Size = new System.Drawing.Size(150, 78);
			this.btnVolverMenu.TabIndex = 10;
			this.btnVolverMenu.Text = "<< VOLVER AL MENU";
			this.btnVolverMenu.UseVisualStyleBackColor = false;
			this.btnVolverMenu.Click += new System.EventHandler(this.BtnVolverMenuClick);
			// 
			// SeleccionModalidad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(47)))));
			this.ClientSize = new System.Drawing.Size(960, 640);
			this.Controls.Add(this.btnVolverMenu);
			this.Controls.Add(this.btnIrBatalla);
			this.Controls.Add(this.pnlMarcoDialogoGba);
			this.Controls.Add(this.pnlMarcoEquipo2);
			this.Controls.Add(this.pnlMarcoEquipo1);
			this.Controls.Add(this.pnlMarcoPreview);
			this.Controls.Add(this.pnlMarcoCatalogo);
			this.Controls.Add(this.pnlMarcoEntrenadores);
			this.Controls.Add(this.pnlMarcoModalidad);
			this.Controls.Add(this.pnlFranjaDorada);
			this.Controls.Add(this.pnlHeaderGba);
			this.Name = "SeleccionModalidad";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Arena Elemental GBA - Seleccion de Modalidad y Equipos";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SeleccionModalidadFormClosed);
			this.pnlHeaderGba.ResumeLayout(false);
			this.pnlMarcoModalidad.ResumeLayout(false);
			this.pnlInteriorModalidad.ResumeLayout(false);
			this.pnlInteriorModalidad.PerformLayout();
			this.pnlMarcoEntrenadores.ResumeLayout(false);
			this.pnlInteriorEntrenadores.ResumeLayout(false);
			this.pnlInteriorEntrenadores.PerformLayout();
			this.pnlMarcoCatalogo.ResumeLayout(false);
			this.pnlInteriorCatalogo.ResumeLayout(false);
			this.pnlMarcoPreview.ResumeLayout(false);
			this.pnlInteriorPreview.ResumeLayout(false);
			this.pnlMarcoEquipo1.ResumeLayout(false);
			this.pnlInteriorEquipo1.ResumeLayout(false);
			this.pnlMarcoEquipo2.ResumeLayout(false);
			this.pnlInteriorEquipo2.ResumeLayout(false);
			this.pnlMarcoDialogoGba.ResumeLayout(false);
			this.pnlInteriorDialogoGba.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnVolverMenu;
		private System.Windows.Forms.Button btnIrBatalla;
		private System.Windows.Forms.Label lblTextoProfesorGba;
		private System.Windows.Forms.Label lblTituloDialogoGba;
		private System.Windows.Forms.Panel pnlInteriorDialogoGba;
		private System.Windows.Forms.Panel pnlMarcoDialogoGba;
		private System.Windows.Forms.Button btnVaciarEquipo2;
		private System.Windows.Forms.Button btnAutoEquipo2;
		private System.Windows.Forms.ListBox lstEquipo2;
		private System.Windows.Forms.Label lblHeaderEquipo2;
		private System.Windows.Forms.Panel pnlInteriorEquipo2;
		private System.Windows.Forms.Panel pnlMarcoEquipo2;
		private System.Windows.Forms.Button btnVaciarEquipo1;
		private System.Windows.Forms.Button btnAutoEquipo1;
		private System.Windows.Forms.ListBox lstEquipo1;
		private System.Windows.Forms.Label lblHeaderEquipo1;
		private System.Windows.Forms.Panel pnlInteriorEquipo1;
		private System.Windows.Forms.Panel pnlMarcoEquipo1;
		private System.Windows.Forms.Label lblPreviewEspecial;
		private System.Windows.Forms.Label lblPreviewStats;
		private System.Windows.Forms.Label lblPreviewNombre;
		private System.Windows.Forms.Panel pnlPreviewSprite;
		private System.Windows.Forms.Label lblPreviewFaccion;
		private System.Windows.Forms.Panel pnlInteriorPreview;
		private System.Windows.Forms.Panel pnlMarcoPreview;
		private System.Windows.Forms.Button btnAgregarEquipo2;
		private System.Windows.Forms.Button btnAgregarEquipo1;
		private System.Windows.Forms.ListBox lstCatalogo;
		private System.Windows.Forms.Label lblHeaderCatalogo;
		private System.Windows.Forms.Panel pnlInteriorCatalogo;
		private System.Windows.Forms.Panel pnlMarcoCatalogo;
		private System.Windows.Forms.Button btnGuardarNombres;
		private System.Windows.Forms.Label lblStatsEntrenador2;
		private System.Windows.Forms.TextBox txtEntrenador2;
		private System.Windows.Forms.Label lblEntrenador2;
		private System.Windows.Forms.Label lblStatsEntrenador1;
		private System.Windows.Forms.TextBox txtEntrenador1;
		private System.Windows.Forms.Label lblEntrenador1;
		private System.Windows.Forms.Label lblHeaderEntrenadores;
		private System.Windows.Forms.Panel pnlInteriorEntrenadores;
		private System.Windows.Forms.Panel pnlMarcoEntrenadores;
		private System.Windows.Forms.Label lblEstadoRed;
		private System.Windows.Forms.Button btnUnirse;
		private System.Windows.Forms.Button btnHospedar;
		private System.Windows.Forms.TextBox txtPuerto;
		private System.Windows.Forms.Label lblPuerto;
		private System.Windows.Forms.TextBox txtIp;
		private System.Windows.Forms.Label lblIp;
		private System.Windows.Forms.Label lblReglaEscenarioAleatorio;
		private System.Windows.Forms.RadioButton rbModoRedLan;
		private System.Windows.Forms.RadioButton rbModoCpu;
		private System.Windows.Forms.RadioButton rbModoLocal;
		private System.Windows.Forms.Label lblHeaderModalidad;
		private System.Windows.Forms.Panel pnlInteriorModalidad;
		private System.Windows.Forms.Panel pnlMarcoModalidad;
		private System.Windows.Forms.Panel pnlFranjaDorada;
		private System.Windows.Forms.Label lblSubtituloGba;
		private System.Windows.Forms.Label lblTituloGba;
		private System.Windows.Forms.Panel pnlHeaderGba;
	}
}

