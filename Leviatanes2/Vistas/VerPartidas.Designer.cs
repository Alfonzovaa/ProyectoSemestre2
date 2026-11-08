/*
 * Created by SharpDevelop.
 * User: Alfonzovaa
 * Date: 30/10/2026
 * Time: 11:27 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Leviatanes2
{
	partial class VerPartidas
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
			this.pnlMarcoJugadores = new System.Windows.Forms.Panel();
			this.pnlInteriorJugadores = new System.Windows.Forms.Panel();
			this.btnRegistrarJugadorTxt = new System.Windows.Forms.Button();
			this.txtNuevoJugador = new System.Windows.Forms.TextBox();
			this.lblNuevoJugador = new System.Windows.Forms.Label();
			this.pbPorcentajeExito = new System.Windows.Forms.ProgressBar();
			this.lblDetalleJugadorSel = new System.Windows.Forms.Label();
			this.dgvRankingJugadores = new System.Windows.Forms.DataGridView();
			this.lblHeaderJugadores = new System.Windows.Forms.Label();
			this.pnlMarcoHistorial = new System.Windows.Forms.Panel();
			this.pnlInteriorHistorial = new System.Windows.Forms.Panel();
			this.dgvHistorialPartidas = new System.Windows.Forms.DataGridView();
			this.lblHeaderHistorial = new System.Windows.Forms.Label();
			this.pnlMarcoBitacora = new System.Windows.Forms.Panel();
			this.pnlInteriorBitacora = new System.Windows.Forms.Panel();
			this.txtVisorBitacora = new System.Windows.Forms.TextBox();
			this.lblHeaderBitacora = new System.Windows.Forms.Label();
			this.pnlMarcoDialogoGba = new System.Windows.Forms.Panel();
			this.pnlDialogoInterior = new System.Windows.Forms.Panel();
			this.lblTextoProfesorGba = new System.Windows.Forms.Label();
			this.lblTituloDialogoGba = new System.Windows.Forms.Label();
			this.btnRecargarTxt = new System.Windows.Forms.Button();
			this.VolverMenu = new System.Windows.Forms.Button();
			this.pnlHeaderGba.SuspendLayout();
			this.pnlMarcoJugadores.SuspendLayout();
			this.pnlInteriorJugadores.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvRankingJugadores)).BeginInit();
			this.pnlMarcoHistorial.SuspendLayout();
			this.pnlInteriorHistorial.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvHistorialPartidas)).BeginInit();
			this.pnlMarcoBitacora.SuspendLayout();
			this.pnlInteriorBitacora.SuspendLayout();
			this.pnlMarcoDialogoGba.SuspendLayout();
			this.pnlDialogoInterior.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlHeaderGba
			// 
			this.pnlHeaderGba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(44)))), ((int)(((byte)(32)))));
			this.pnlHeaderGba.Controls.Add(this.lblSubtituloGba);
			this.pnlHeaderGba.Controls.Add(this.lblTituloGba);
			this.pnlHeaderGba.Location = new System.Drawing.Point(0, 0);
			this.pnlHeaderGba.Name = "pnlHeaderGba";
			this.pnlHeaderGba.Size = new System.Drawing.Size(960, 56);
			this.pnlHeaderGba.TabIndex = 0;
			// 
			// lblTituloGba
			// 
			this.lblTituloGba.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTituloGba.ForeColor = System.Drawing.Color.Gold;
			this.lblTituloGba.Location = new System.Drawing.Point(14, 5);
			this.lblTituloGba.Name = "lblTituloGba";
			this.lblTituloGba.Size = new System.Drawing.Size(930, 28);
			this.lblTituloGba.TabIndex = 0;
			this.lblTituloGba.Text = "HALL DE LA FAMA - ESTADISTICAS E HISTORIAL (.TXT)";
			// 
			// lblSubtituloGba
			// 
			this.lblSubtituloGba.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSubtituloGba.ForeColor = System.Drawing.Color.White;
			this.lblSubtituloGba.Location = new System.Drawing.Point(16, 34);
			this.lblSubtituloGba.Name = "lblSubtituloGba";
			this.lblSubtituloGba.Size = new System.Drawing.Size(930, 18);
			this.lblSubtituloGba.TabIndex = 1;
			this.lblSubtituloGba.Text = "EDICION ROJO FUEGO GBA  |  ARCHIVOS: jugadores.txt  |  partidas.txt  |  Bitacoras" +
			" .TXT";
			// 
			// pnlFranjaDorada
			// 
			this.pnlFranjaDorada.BackColor = System.Drawing.Color.Gold;
			this.pnlFranjaDorada.Location = new System.Drawing.Point(0, 56);
			this.pnlFranjaDorada.Name = "pnlFranjaDorada";
			this.pnlFranjaDorada.Size = new System.Drawing.Size(960, 4);
			this.pnlFranjaDorada.TabIndex = 1;
			// 
			// pnlMarcoJugadores
			// 
			this.pnlMarcoJugadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(176)))), ((int)(((byte)(48)))));
			this.pnlMarcoJugadores.Controls.Add(this.pnlInteriorJugadores);
			this.pnlMarcoJugadores.Location = new System.Drawing.Point(12, 68);
			this.pnlMarcoJugadores.Name = "pnlMarcoJugadores";
			this.pnlMarcoJugadores.Size = new System.Drawing.Size(390, 472);
			this.pnlMarcoJugadores.TabIndex = 2;
			// 
			// pnlInteriorJugadores
			// 
			this.pnlInteriorJugadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(216)))));
			this.pnlInteriorJugadores.Controls.Add(this.btnRegistrarJugadorTxt);
			this.pnlInteriorJugadores.Controls.Add(this.txtNuevoJugador);
			this.pnlInteriorJugadores.Controls.Add(this.lblNuevoJugador);
			this.pnlInteriorJugadores.Controls.Add(this.pbPorcentajeExito);
			this.pnlInteriorJugadores.Controls.Add(this.lblDetalleJugadorSel);
			this.pnlInteriorJugadores.Controls.Add(this.dgvRankingJugadores);
			this.pnlInteriorJugadores.Controls.Add(this.lblHeaderJugadores);
			this.pnlInteriorJugadores.Location = new System.Drawing.Point(4, 4);
			this.pnlInteriorJugadores.Name = "pnlInteriorJugadores";
			this.pnlInteriorJugadores.Size = new System.Drawing.Size(382, 464);
			this.pnlInteriorJugadores.TabIndex = 0;
			// 
			// lblHeaderJugadores
			// 
			this.lblHeaderJugadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(56)))), ((int)(((byte)(88)))));
			this.lblHeaderJugadores.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderJugadores.ForeColor = System.Drawing.Color.Gold;
			this.lblHeaderJugadores.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderJugadores.Name = "lblHeaderJugadores";
			this.lblHeaderJugadores.Size = new System.Drawing.Size(382, 30);
			this.lblHeaderJugadores.TabIndex = 0;
			this.lblHeaderJugadores.Text = " 1. RANKING DE JUGADORES (jugadores.txt)";
			this.lblHeaderJugadores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dgvRankingJugadores
			// 
			this.dgvRankingJugadores.AllowUserToAddRows = false;
			this.dgvRankingJugadores.AllowUserToDeleteRows = false;
			this.dgvRankingJugadores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvRankingJugadores.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
			this.dgvRankingJugadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvRankingJugadores.Location = new System.Drawing.Point(10, 38);
			this.dgvRankingJugadores.MultiSelect = false;
			this.dgvRankingJugadores.Name = "dgvRankingJugadores";
			this.dgvRankingJugadores.ReadOnly = true;
			this.dgvRankingJugadores.RowHeadersVisible = false;
			this.dgvRankingJugadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvRankingJugadores.Size = new System.Drawing.Size(362, 265);
			this.dgvRankingJugadores.TabIndex = 1;
			this.dgvRankingJugadores.SelectionChanged += new System.EventHandler(this.DgvRankingJugadoresSelectionChanged);
			// 
			// lblDetalleJugadorSel
			// 
			this.lblDetalleJugadorSel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(62)))));
			this.lblDetalleJugadorSel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblDetalleJugadorSel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDetalleJugadorSel.ForeColor = System.Drawing.Color.Gold;
			this.lblDetalleJugadorSel.Location = new System.Drawing.Point(10, 312);
			this.lblDetalleJugadorSel.Name = "lblDetalleJugadorSel";
			this.lblDetalleJugadorSel.Padding = new System.Windows.Forms.Padding(5);
			this.lblDetalleJugadorSel.Size = new System.Drawing.Size(362, 48);
			this.lblDetalleJugadorSel.TabIndex = 2;
			this.lblDetalleJugadorSel.Text = "Selecciona un jugador para ver su efectividad...";
			// 
			// pbPorcentajeExito
			// 
			this.pbPorcentajeExito.Location = new System.Drawing.Point(10, 366);
			this.pbPorcentajeExito.Maximum = 100;
			this.pbPorcentajeExito.Name = "pbPorcentajeExito";
			this.pbPorcentajeExito.Size = new System.Drawing.Size(362, 16);
			this.pbPorcentajeExito.TabIndex = 3;
			this.pbPorcentajeExito.Value = 50;
			// 
			// lblNuevoJugador
			// 
			this.lblNuevoJugador.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNuevoJugador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblNuevoJugador.Location = new System.Drawing.Point(10, 392);
			this.lblNuevoJugador.Name = "lblNuevoJugador";
			this.lblNuevoJugador.Size = new System.Drawing.Size(360, 18);
			this.lblNuevoJugador.TabIndex = 4;
			this.lblNuevoJugador.Text = "REGISTRAR NUEVO ENTRENADOR EN JUGADORES.TXT:";
			// 
			// txtNuevoJugador
			// 
			this.txtNuevoJugador.BackColor = System.Drawing.Color.White;
			this.txtNuevoJugador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtNuevoJugador.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNuevoJugador.Location = new System.Drawing.Point(10, 414);
			this.txtNuevoJugador.Name = "txtNuevoJugador";
			this.txtNuevoJugador.Size = new System.Drawing.Size(214, 23);
			this.txtNuevoJugador.TabIndex = 5;
			this.txtNuevoJugador.Text = "NuevoEntrenador";
			// 
			// btnRegistrarJugadorTxt
			// 
			this.btnRegistrarJugadorTxt.BackColor = System.Drawing.Color.SeaGreen;
			this.btnRegistrarJugadorTxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRegistrarJugadorTxt.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRegistrarJugadorTxt.ForeColor = System.Drawing.Color.White;
			this.btnRegistrarJugadorTxt.Location = new System.Drawing.Point(232, 410);
			this.btnRegistrarJugadorTxt.Name = "btnRegistrarJugadorTxt";
			this.btnRegistrarJugadorTxt.Size = new System.Drawing.Size(140, 34);
			this.btnRegistrarJugadorTxt.TabIndex = 6;
			this.btnRegistrarJugadorTxt.Text = "+ GUARDAR .TXT";
			this.btnRegistrarJugadorTxt.UseVisualStyleBackColor = false;
			this.btnRegistrarJugadorTxt.Click += new System.EventHandler(this.BtnRegistrarJugadorTxtClick);
			// 
			// pnlMarcoHistorial
			// 
			this.pnlMarcoHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(176)))), ((int)(((byte)(48)))));
			this.pnlMarcoHistorial.Controls.Add(this.pnlInteriorHistorial);
			this.pnlMarcoHistorial.Location = new System.Drawing.Point(412, 68);
			this.pnlMarcoHistorial.Name = "pnlMarcoHistorial";
			this.pnlMarcoHistorial.Size = new System.Drawing.Size(536, 224);
			this.pnlMarcoHistorial.TabIndex = 3;
			// 
			// pnlInteriorHistorial
			// 
			this.pnlInteriorHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(216)))));
			this.pnlInteriorHistorial.Controls.Add(this.dgvHistorialPartidas);
			this.pnlInteriorHistorial.Controls.Add(this.lblHeaderHistorial);
			this.pnlInteriorHistorial.Location = new System.Drawing.Point(4, 4);
			this.pnlInteriorHistorial.Name = "pnlInteriorHistorial";
			this.pnlInteriorHistorial.Size = new System.Drawing.Size(528, 216);
			this.pnlInteriorHistorial.TabIndex = 0;
			// 
			// lblHeaderHistorial
			// 
			this.lblHeaderHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(56)))), ((int)(((byte)(88)))));
			this.lblHeaderHistorial.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderHistorial.ForeColor = System.Drawing.Color.Gold;
			this.lblHeaderHistorial.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderHistorial.Name = "lblHeaderHistorial";
			this.lblHeaderHistorial.Size = new System.Drawing.Size(528, 30);
			this.lblHeaderHistorial.TabIndex = 0;
			this.lblHeaderHistorial.Text = " 2. HISTORIAL DE PARTIDAS REGISTRADAS (partidas.txt)";
			this.lblHeaderHistorial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dgvHistorialPartidas
			// 
			this.dgvHistorialPartidas.AllowUserToAddRows = false;
			this.dgvHistorialPartidas.AllowUserToDeleteRows = false;
			this.dgvHistorialPartidas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvHistorialPartidas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
			this.dgvHistorialPartidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvHistorialPartidas.Location = new System.Drawing.Point(10, 38);
			this.dgvHistorialPartidas.MultiSelect = false;
			this.dgvHistorialPartidas.Name = "dgvHistorialPartidas";
			this.dgvHistorialPartidas.ReadOnly = true;
			this.dgvHistorialPartidas.RowHeadersVisible = false;
			this.dgvHistorialPartidas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvHistorialPartidas.Size = new System.Drawing.Size(508, 168);
			this.dgvHistorialPartidas.TabIndex = 1;
			this.dgvHistorialPartidas.SelectionChanged += new System.EventHandler(this.DgvHistorialPartidasSelectionChanged);
			// 
			// pnlMarcoBitacora
			// 
			this.pnlMarcoBitacora.BackColor = System.Drawing.Color.Gold;
			this.pnlMarcoBitacora.Controls.Add(this.pnlInteriorBitacora);
			this.pnlMarcoBitacora.Location = new System.Drawing.Point(412, 300);
			this.pnlMarcoBitacora.Name = "pnlMarcoBitacora";
			this.pnlMarcoBitacora.Size = new System.Drawing.Size(536, 240);
			this.pnlMarcoBitacora.TabIndex = 4;
			// 
			// pnlInteriorBitacora
			// 
			this.pnlInteriorBitacora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(52)))));
			this.pnlInteriorBitacora.Controls.Add(this.txtVisorBitacora);
			this.pnlInteriorBitacora.Controls.Add(this.lblHeaderBitacora);
			this.pnlInteriorBitacora.Location = new System.Drawing.Point(4, 4);
			this.pnlInteriorBitacora.Name = "pnlInteriorBitacora";
			this.pnlInteriorBitacora.Size = new System.Drawing.Size(528, 232);
			this.pnlInteriorBitacora.TabIndex = 0;
			// 
			// lblHeaderBitacora
			// 
			this.lblHeaderBitacora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(44)))), ((int)(((byte)(32)))));
			this.lblHeaderBitacora.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderBitacora.ForeColor = System.Drawing.Color.White;
			this.lblHeaderBitacora.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderBitacora.Name = "lblHeaderBitacora";
			this.lblHeaderBitacora.Size = new System.Drawing.Size(528, 30);
			this.lblHeaderBitacora.TabIndex = 0;
			this.lblHeaderBitacora.Text = " 3. VISOR DE BITACORA DE COMBATE TURNO A TURNO (.TXT)";
			this.lblHeaderBitacora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtVisorBitacora
			// 
			this.txtVisorBitacora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(20)))), ((int)(((byte)(32)))));
			this.txtVisorBitacora.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtVisorBitacora.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtVisorBitacora.ForeColor = System.Drawing.Color.LightGreen;
			this.txtVisorBitacora.Location = new System.Drawing.Point(10, 36);
			this.txtVisorBitacora.Multiline = true;
			this.txtVisorBitacora.Name = "txtVisorBitacora";
			this.txtVisorBitacora.ReadOnly = true;
			this.txtVisorBitacora.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtVisorBitacora.Size = new System.Drawing.Size(508, 186);
			this.txtVisorBitacora.TabIndex = 1;
			// 
			// pnlMarcoDialogoGba
			// 
			this.pnlMarcoDialogoGba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(44)))), ((int)(((byte)(32)))));
			this.pnlMarcoDialogoGba.Controls.Add(this.pnlDialogoInterior);
			this.pnlMarcoDialogoGba.Location = new System.Drawing.Point(12, 548);
			this.pnlMarcoDialogoGba.Name = "pnlMarcoDialogoGba";
			this.pnlMarcoDialogoGba.Size = new System.Drawing.Size(618, 80);
			this.pnlMarcoDialogoGba.TabIndex = 5;
			// 
			// pnlDialogoInterior
			// 
			this.pnlDialogoInterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(62)))));
			this.pnlDialogoInterior.Controls.Add(this.lblTextoProfesorGba);
			this.pnlDialogoInterior.Controls.Add(this.lblTituloDialogoGba);
			this.pnlDialogoInterior.Location = new System.Drawing.Point(4, 4);
			this.pnlDialogoInterior.Name = "pnlDialogoInterior";
			this.pnlDialogoInterior.Size = new System.Drawing.Size(610, 72);
			this.pnlDialogoInterior.TabIndex = 0;
			// 
			// lblTituloDialogoGba
			// 
			this.lblTituloDialogoGba.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTituloDialogoGba.ForeColor = System.Drawing.Color.Gold;
			this.lblTituloDialogoGba.Location = new System.Drawing.Point(10, 6);
			this.lblTituloDialogoGba.Name = "lblTituloDialogoGba";
			this.lblTituloDialogoGba.Size = new System.Drawing.Size(590, 18);
			this.lblTituloDialogoGba.TabIndex = 0;
			this.lblTituloDialogoGba.Text = "PROF. LUCENA (REGISTRO HISTORICO):";
			// 
			// lblTextoProfesorGba
			// 
			this.lblTextoProfesorGba.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTextoProfesorGba.ForeColor = System.Drawing.Color.White;
			this.lblTextoProfesorGba.Location = new System.Drawing.Point(10, 26);
			this.lblTextoProfesorGba.Name = "lblTextoProfesorGba";
			this.lblTextoProfesorGba.Size = new System.Drawing.Size(590, 40);
			this.lblTextoProfesorGba.TabIndex = 1;
			this.lblTextoProfesorGba.Text = "Haz clic en una partida de la tabla derecha para inspeccionar su archivo .TXT de " +
			"bitacora turno por turno ▼";
			// 
			// btnRecargarTxt
			// 
			this.btnRecargarTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
			this.btnRecargarTxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRecargarTxt.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRecargarTxt.ForeColor = System.Drawing.Color.White;
			this.btnRecargarTxt.Location = new System.Drawing.Point(640, 548);
			this.btnRecargarTxt.Name = "btnRecargarTxt";
			this.btnRecargarTxt.Size = new System.Drawing.Size(142, 80);
			this.btnRecargarTxt.TabIndex = 6;
			this.btnRecargarTxt.Text = "RECARGAR ARCHIVOS .TXT";
			this.btnRecargarTxt.UseVisualStyleBackColor = false;
			this.btnRecargarTxt.Click += new System.EventHandler(this.BtnRecargarTxtClick);
			// 
			// VolverMenu
			// 
			this.VolverMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(68)))), ((int)(((byte)(90)))));
			this.VolverMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.VolverMenu.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.VolverMenu.ForeColor = System.Drawing.Color.Gold;
			this.VolverMenu.Location = new System.Drawing.Point(792, 548);
			this.VolverMenu.Name = "VolverMenu";
			this.VolverMenu.Size = new System.Drawing.Size(156, 80);
			this.VolverMenu.TabIndex = 7;
			this.VolverMenu.Text = "<< VOLVER AL MENU";
			this.VolverMenu.UseVisualStyleBackColor = false;
			this.VolverMenu.Click += new System.EventHandler(this.VolverMenuClick);
			// 
			// VerPartidas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
			this.ClientSize = new System.Drawing.Size(960, 640);
			this.Controls.Add(this.btnRecargarTxt);
			this.Controls.Add(this.pnlMarcoDialogoGba);
			this.Controls.Add(this.pnlMarcoBitacora);
			this.Controls.Add(this.pnlMarcoHistorial);
			this.Controls.Add(this.pnlMarcoJugadores);
			this.Controls.Add(this.pnlFranjaDorada);
			this.Controls.Add(this.pnlHeaderGba);
			this.Controls.Add(this.VolverMenu);
			this.Name = "VerPartidas";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "VerPartidas - Hall de la Fama GBA";
			this.pnlHeaderGba.ResumeLayout(false);
			this.pnlMarcoJugadores.ResumeLayout(false);
			this.pnlInteriorJugadores.ResumeLayout(false);
			this.pnlInteriorJugadores.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvRankingJugadores)).EndInit();
			this.pnlMarcoHistorial.ResumeLayout(false);
			this.pnlInteriorHistorial.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvHistorialPartidas)).EndInit();
			this.pnlMarcoBitacora.ResumeLayout(false);
			this.pnlInteriorBitacora.ResumeLayout(false);
			this.pnlInteriorBitacora.PerformLayout();
			this.pnlMarcoDialogoGba.ResumeLayout(false);
			this.pnlDialogoInterior.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnRecargarTxt;
		private System.Windows.Forms.Label lblTextoProfesorGba;
		private System.Windows.Forms.Label lblTituloDialogoGba;
		private System.Windows.Forms.Panel pnlDialogoInterior;
		private System.Windows.Forms.Panel pnlMarcoDialogoGba;
		private System.Windows.Forms.TextBox txtVisorBitacora;
		private System.Windows.Forms.Label lblHeaderBitacora;
		private System.Windows.Forms.Panel pnlInteriorBitacora;
		private System.Windows.Forms.Panel pnlMarcoBitacora;
		private System.Windows.Forms.DataGridView dgvHistorialPartidas;
		private System.Windows.Forms.Label lblHeaderHistorial;
		private System.Windows.Forms.Panel pnlInteriorHistorial;
		private System.Windows.Forms.Panel pnlMarcoHistorial;
		private System.Windows.Forms.Button btnRegistrarJugadorTxt;
		private System.Windows.Forms.TextBox txtNuevoJugador;
		private System.Windows.Forms.Label lblNuevoJugador;
		private System.Windows.Forms.ProgressBar pbPorcentajeExito;
		private System.Windows.Forms.Label lblDetalleJugadorSel;
		private System.Windows.Forms.DataGridView dgvRankingJugadores;
		private System.Windows.Forms.Label lblHeaderJugadores;
		private System.Windows.Forms.Panel pnlInteriorJugadores;
		private System.Windows.Forms.Panel pnlMarcoJugadores;
		private System.Windows.Forms.Panel pnlFranjaDorada;
		private System.Windows.Forms.Label lblSubtituloGba;
		private System.Windows.Forms.Label lblTituloGba;
		private System.Windows.Forms.Panel pnlHeaderGba;
		private System.Windows.Forms.Button VolverMenu;
	}
}
