/*
 * Created by SharpDevelop.
 * User: Alfonzovaa
 * Date: 30/10/2026
 * Time: 11:24 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Leviatanes2
{
	partial class CrearP
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
			this.pnlMarcoIzquierdo = new System.Windows.Forms.Panel();
			this.pnlFormularioGba = new System.Windows.Forms.Panel();
			this.btnLimpiarCampos = new System.Windows.Forms.Button();
			this.btnGuardarPersonajeTxt = new System.Windows.Forms.Button();
			this.txtDescripcionEspecial = new System.Windows.Forms.TextBox();
			this.lblDescripcion = new System.Windows.Forms.Label();
			this.txtNombreEspecial = new System.Windows.Forms.TextBox();
			this.lblEspecial = new System.Windows.Forms.Label();
			this.btnAjustarCien = new System.Windows.Forms.Button();
			this.lblEstadoBalance = new System.Windows.Forms.Label();
			this.pbBalanceCien = new System.Windows.Forms.ProgressBar();
			this.numAtaque = new System.Windows.Forms.NumericUpDown();
			this.lblAtaque = new System.Windows.Forms.Label();
			this.numDefensa = new System.Windows.Forms.NumericUpDown();
			this.lblDefensa = new System.Windows.Forms.Label();
			this.numVida = new System.Windows.Forms.NumericUpDown();
			this.lblVida = new System.Windows.Forms.Label();
			this.lblReglaCien = new System.Windows.Forms.Label();
			this.numEmpatia = new System.Windows.Forms.NumericUpDown();
			this.lblEmpatia = new System.Windows.Forms.Label();
			this.cmbFaccion = new System.Windows.Forms.ComboBox();
			this.lblFaccion = new System.Windows.Forms.Label();
			this.txtNombre = new System.Windows.Forms.TextBox();
			this.lblNombre = new System.Windows.Forms.Label();
			this.lblHeaderFormulario = new System.Windows.Forms.Label();
			this.pnlMarcoCarta = new System.Windows.Forms.Panel();
			this.pnlCartaInterior = new System.Windows.Forms.Panel();
			this.lblCartaEspecialVista = new System.Windows.Forms.Label();
			this.lblCartaPasiva = new System.Windows.Forms.Label();
			this.lblCartaEmp = new System.Windows.Forms.Label();
			this.pbCartaAtk = new System.Windows.Forms.ProgressBar();
			this.lblCartaAtk = new System.Windows.Forms.Label();
			this.pbCartaDef = new System.Windows.Forms.ProgressBar();
			this.lblCartaDef = new System.Windows.Forms.Label();
			this.pbCartaHp = new System.Windows.Forms.ProgressBar();
			this.lblCartaHp = new System.Windows.Forms.Label();
			this.lblCartaNombreVista = new System.Windows.Forms.Label();
			this.pnlSpriteArte = new System.Windows.Forms.Panel();
			this.lblCartaFaccionBanner = new System.Windows.Forms.Label();
			this.pnlMarcoCatalogo = new System.Windows.Forms.Panel();
			this.pnlCatalogoInterior = new System.Windows.Forms.Panel();
			this.btnRestaurarBase = new System.Windows.Forms.Button();
			this.btnEliminarDeTxt = new System.Windows.Forms.Button();
			this.btnCargarEnEditor = new System.Windows.Forms.Button();
			this.lblTotalRegistrados = new System.Windows.Forms.Label();
			this.lstPersonajesTxt = new System.Windows.Forms.ListBox();
			this.lblHeaderCatalogo = new System.Windows.Forms.Label();
			this.pnlMarcoDialogoGba = new System.Windows.Forms.Panel();
			this.pnlDialogoInterior = new System.Windows.Forms.Panel();
			this.lblTextoProfesorGba = new System.Windows.Forms.Label();
			this.lblTituloDialogoGba = new System.Windows.Forms.Label();
			this.VolverMenu = new System.Windows.Forms.Button();
			this.pnlHeaderGba.SuspendLayout();
			this.pnlMarcoIzquierdo.SuspendLayout();
			this.pnlFormularioGba.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numAtaque)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDefensa)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numVida)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numEmpatia)).BeginInit();
			this.pnlMarcoCarta.SuspendLayout();
			this.pnlCartaInterior.SuspendLayout();
			this.pnlMarcoCatalogo.SuspendLayout();
			this.pnlCatalogoInterior.SuspendLayout();
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
			this.lblTituloGba.Text = "POKEDEX ELEMENTAL - CREADOR DE LEVIATANES";
			// 
			// lblSubtituloGba
			// 
			this.lblSubtituloGba.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSubtituloGba.ForeColor = System.Drawing.Color.White;
			this.lblSubtituloGba.Location = new System.Drawing.Point(16, 34);
			this.lblSubtituloGba.Name = "lblSubtituloGba";
			this.lblSubtituloGba.Size = new System.Drawing.Size(930, 18);
			this.lblSubtituloGba.TabIndex = 1;
			this.lblSubtituloGba.Text = "EDICION ROJO FUEGO GBA  |  PERSISTENCIA EN ARCHIVO (.TXT)";
			// 
			// pnlFranjaDorada
			// 
			this.pnlFranjaDorada.BackColor = System.Drawing.Color.Gold;
			this.pnlFranjaDorada.Location = new System.Drawing.Point(0, 56);
			this.pnlFranjaDorada.Name = "pnlFranjaDorada";
			this.pnlFranjaDorada.Size = new System.Drawing.Size(960, 4);
			this.pnlFranjaDorada.TabIndex = 1;
			// 
			// pnlMarcoIzquierdo
			// 
			this.pnlMarcoIzquierdo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(176)))), ((int)(((byte)(48)))));
			this.pnlMarcoIzquierdo.Controls.Add(this.pnlFormularioGba);
			this.pnlMarcoIzquierdo.Location = new System.Drawing.Point(12, 68);
			this.pnlMarcoIzquierdo.Name = "pnlMarcoIzquierdo";
			this.pnlMarcoIzquierdo.Size = new System.Drawing.Size(372, 472);
			this.pnlMarcoIzquierdo.TabIndex = 2;
			// 
			// pnlFormularioGba
			// 
			this.pnlFormularioGba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(216)))));
			this.pnlFormularioGba.Controls.Add(this.btnLimpiarCampos);
			this.pnlFormularioGba.Controls.Add(this.btnGuardarPersonajeTxt);
			this.pnlFormularioGba.Controls.Add(this.txtDescripcionEspecial);
			this.pnlFormularioGba.Controls.Add(this.lblDescripcion);
			this.pnlFormularioGba.Controls.Add(this.txtNombreEspecial);
			this.pnlFormularioGba.Controls.Add(this.lblEspecial);
			this.pnlFormularioGba.Controls.Add(this.btnAjustarCien);
			this.pnlFormularioGba.Controls.Add(this.lblEstadoBalance);
			this.pnlFormularioGba.Controls.Add(this.pbBalanceCien);
			this.pnlFormularioGba.Controls.Add(this.numAtaque);
			this.pnlFormularioGba.Controls.Add(this.lblAtaque);
			this.pnlFormularioGba.Controls.Add(this.numDefensa);
			this.pnlFormularioGba.Controls.Add(this.lblDefensa);
			this.pnlFormularioGba.Controls.Add(this.numVida);
			this.pnlFormularioGba.Controls.Add(this.lblVida);
			this.pnlFormularioGba.Controls.Add(this.lblReglaCien);
			this.pnlFormularioGba.Controls.Add(this.numEmpatia);
			this.pnlFormularioGba.Controls.Add(this.lblEmpatia);
			this.pnlFormularioGba.Controls.Add(this.cmbFaccion);
			this.pnlFormularioGba.Controls.Add(this.lblFaccion);
			this.pnlFormularioGba.Controls.Add(this.txtNombre);
			this.pnlFormularioGba.Controls.Add(this.lblNombre);
			this.pnlFormularioGba.Controls.Add(this.lblHeaderFormulario);
			this.pnlFormularioGba.Location = new System.Drawing.Point(4, 4);
			this.pnlFormularioGba.Name = "pnlFormularioGba";
			this.pnlFormularioGba.Size = new System.Drawing.Size(364, 464);
			this.pnlFormularioGba.TabIndex = 0;
			// 
			// lblHeaderFormulario
			// 
			this.lblHeaderFormulario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(56)))), ((int)(((byte)(88)))));
			this.lblHeaderFormulario.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderFormulario.ForeColor = System.Drawing.Color.Gold;
			this.lblHeaderFormulario.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderFormulario.Name = "lblHeaderFormulario";
			this.lblHeaderFormulario.Size = new System.Drawing.Size(364, 30);
			this.lblHeaderFormulario.TabIndex = 0;
			this.lblHeaderFormulario.Text = " 1. REGISTRAR NUEVO LEVIATAN";
			this.lblHeaderFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblNombre
			// 
			this.lblNombre.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblNombre.Location = new System.Drawing.Point(10, 42);
			this.lblNombre.Name = "lblNombre";
			this.lblNombre.Size = new System.Drawing.Size(110, 20);
			this.lblNombre.TabIndex = 1;
			this.lblNombre.Text = "NOMBRE CARTA:";
			// 
			// txtNombre
			// 
			this.txtNombre.BackColor = System.Drawing.Color.White;
			this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtNombre.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
			this.txtNombre.Location = new System.Drawing.Point(122, 39);
			this.txtNombre.Name = "txtNombre";
			this.txtNombre.Size = new System.Drawing.Size(230, 23);
			this.txtNombre.TabIndex = 2;
			this.txtNombre.Text = "Gyarados Del Caribe";
			this.txtNombre.TextChanged += new System.EventHandler(this.CamposVistaPreviaChanged);
			// 
			// lblFaccion
			// 
			this.lblFaccion.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFaccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblFaccion.Location = new System.Drawing.Point(10, 76);
			this.lblFaccion.Name = "lblFaccion";
			this.lblFaccion.Size = new System.Drawing.Size(80, 20);
			this.lblFaccion.TabIndex = 3;
			this.lblFaccion.Text = "FACCION:";
			// 
			// cmbFaccion
			// 
			this.cmbFaccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFaccion.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbFaccion.FormattingEnabled = true;
			this.cmbFaccion.Location = new System.Drawing.Point(92, 72);
			this.cmbFaccion.Name = "cmbFaccion";
			this.cmbFaccion.Size = new System.Drawing.Size(115, 23);
			this.cmbFaccion.TabIndex = 4;
			this.cmbFaccion.SelectedIndexChanged += new System.EventHandler(this.CamposVistaPreviaChanged);
			// 
			// lblEmpatia
			// 
			this.lblEmpatia.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEmpatia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblEmpatia.Location = new System.Drawing.Point(216, 76);
			this.lblEmpatia.Name = "lblEmpatia";
			this.lblEmpatia.Size = new System.Drawing.Size(72, 20);
			this.lblEmpatia.TabIndex = 5;
			this.lblEmpatia.Text = "EMPATIA:";
			// 
			// numEmpatia
			// 
			this.numEmpatia.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numEmpatia.Location = new System.Drawing.Point(290, 73);
			this.numEmpatia.Maximum = new decimal(new int[] {
									100,
									0,
									0,
									0});
			this.numEmpatia.Name = "numEmpatia";
			this.numEmpatia.Size = new System.Drawing.Size(62, 23);
			this.numEmpatia.TabIndex = 6;
			this.numEmpatia.Value = new decimal(new int[] {
									75,
									0,
									0,
									0});
			this.numEmpatia.ValueChanged += new System.EventHandler(this.CamposVistaPreviaChanged);
			// 
			// lblReglaCien
			// 
			this.lblReglaCien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(220)))), ((int)(((byte)(184)))));
			this.lblReglaCien.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblReglaCien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(35)))), ((int)(((byte)(25)))));
			this.lblReglaCien.Location = new System.Drawing.Point(10, 106);
			this.lblReglaCien.Name = "lblReglaCien";
			this.lblReglaCien.Size = new System.Drawing.Size(342, 22);
			this.lblReglaCien.TabIndex = 7;
			this.lblReglaCien.Text = "ATRIBUTOS BASE (REGLA: HP + DEF + ATK = 100)";
			this.lblReglaCien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblVida
			// 
			this.lblVida.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVida.ForeColor = System.Drawing.Color.DarkGreen;
			this.lblVida.Location = new System.Drawing.Point(10, 138);
			this.lblVida.Name = "lblVida";
			this.lblVida.Size = new System.Drawing.Size(42, 20);
			this.lblVida.TabIndex = 8;
			this.lblVida.Text = "VIDA:";
			// 
			// numVida
			// 
			this.numVida.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numVida.Location = new System.Drawing.Point(54, 135);
			this.numVida.Maximum = new decimal(new int[] {
									98,
									0,
									0,
									0});
			this.numVida.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.numVida.Name = "numVida";
			this.numVida.Size = new System.Drawing.Size(54, 23);
			this.numVida.TabIndex = 9;
			this.numVida.Value = new decimal(new int[] {
									50,
									0,
									0,
									0});
			this.numVida.ValueChanged += new System.EventHandler(this.AtributosCienChanged);
			// 
			// lblDefensa
			// 
			this.lblDefensa.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDefensa.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblDefensa.Location = new System.Drawing.Point(118, 138);
			this.lblDefensa.Name = "lblDefensa";
			this.lblDefensa.Size = new System.Drawing.Size(60, 20);
			this.lblDefensa.TabIndex = 10;
			this.lblDefensa.Text = "ESCUDO:";
			// 
			// numDefensa
			// 
			this.numDefensa.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numDefensa.Location = new System.Drawing.Point(180, 135);
			this.numDefensa.Maximum = new decimal(new int[] {
									98,
									0,
									0,
									0});
			this.numDefensa.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.numDefensa.Name = "numDefensa";
			this.numDefensa.Size = new System.Drawing.Size(54, 23);
			this.numDefensa.TabIndex = 11;
			this.numDefensa.Value = new decimal(new int[] {
									25,
									0,
									0,
									0});
			this.numDefensa.ValueChanged += new System.EventHandler(this.AtributosCienChanged);
			// 
			// lblAtaque
			// 
			this.lblAtaque.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAtaque.ForeColor = System.Drawing.Color.Firebrick;
			this.lblAtaque.Location = new System.Drawing.Point(242, 138);
			this.lblAtaque.Name = "lblAtaque";
			this.lblAtaque.Size = new System.Drawing.Size(54, 20);
			this.lblAtaque.TabIndex = 12;
			this.lblAtaque.Text = "ATAQUE:";
			// 
			// numAtaque
			// 
			this.numAtaque.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numAtaque.Location = new System.Drawing.Point(298, 135);
			this.numAtaque.Maximum = new decimal(new int[] {
									98,
									0,
									0,
									0});
			this.numAtaque.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.numAtaque.Name = "numAtaque";
			this.numAtaque.Size = new System.Drawing.Size(54, 23);
			this.numAtaque.TabIndex = 13;
			this.numAtaque.Value = new decimal(new int[] {
									25,
									0,
									0,
									0});
			this.numAtaque.ValueChanged += new System.EventHandler(this.AtributosCienChanged);
			// 
			// pbBalanceCien
			// 
			this.pbBalanceCien.Location = new System.Drawing.Point(10, 168);
			this.pbBalanceCien.Maximum = 100;
			this.pbBalanceCien.Name = "pbBalanceCien";
			this.pbBalanceCien.Size = new System.Drawing.Size(238, 20);
			this.pbBalanceCien.TabIndex = 14;
			this.pbBalanceCien.Value = 100;
			// 
			// lblEstadoBalance
			// 
			this.lblEstadoBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(242)))), ((int)(((byte)(215)))));
			this.lblEstadoBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblEstadoBalance.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEstadoBalance.ForeColor = System.Drawing.Color.DarkGreen;
			this.lblEstadoBalance.Location = new System.Drawing.Point(10, 194);
			this.lblEstadoBalance.Name = "lblEstadoBalance";
			this.lblEstadoBalance.Size = new System.Drawing.Size(238, 26);
			this.lblEstadoBalance.TabIndex = 15;
			this.lblEstadoBalance.Text = "50 + 25 + 25 = 100 [OK]";
			this.lblEstadoBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnAjustarCien
			// 
			this.btnAjustarCien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(76)))), ((int)(((byte)(112)))));
			this.btnAjustarCien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAjustarCien.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAjustarCien.ForeColor = System.Drawing.Color.White;
			this.btnAjustarCien.Location = new System.Drawing.Point(254, 168);
			this.btnAjustarCien.Name = "btnAjustarCien";
			this.btnAjustarCien.Size = new System.Drawing.Size(98, 52);
			this.btnAjustarCien.TabIndex = 16;
			this.btnAjustarCien.Text = "AJUSTAR A 100";
			this.btnAjustarCien.UseVisualStyleBackColor = false;
			this.btnAjustarCien.Click += new System.EventHandler(this.BtnAjustarCienClick);
			// 
			// lblEspecial
			// 
			this.lblEspecial.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEspecial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblEspecial.Location = new System.Drawing.Point(10, 234);
			this.lblEspecial.Name = "lblEspecial";
			this.lblEspecial.Size = new System.Drawing.Size(110, 20);
			this.lblEspecial.TabIndex = 17;
			this.lblEspecial.Text = "HAB. ESPECIAL:";
			// 
			// txtNombreEspecial
			// 
			this.txtNombreEspecial.BackColor = System.Drawing.Color.White;
			this.txtNombreEspecial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtNombreEspecial.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNombreEspecial.Location = new System.Drawing.Point(122, 231);
			this.txtNombreEspecial.Name = "txtNombreEspecial";
			this.txtNombreEspecial.Size = new System.Drawing.Size(230, 22);
			this.txtNombreEspecial.TabIndex = 18;
			this.txtNombreEspecial.Text = "Maremoto Abisal";
			this.txtNombreEspecial.TextChanged += new System.EventHandler(this.CamposVistaPreviaChanged);
			// 
			// lblDescripcion
			// 
			this.lblDescripcion.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblDescripcion.Location = new System.Drawing.Point(10, 264);
			this.lblDescripcion.Name = "lblDescripcion";
			this.lblDescripcion.Size = new System.Drawing.Size(300, 18);
			this.lblDescripcion.TabIndex = 19;
			this.lblDescripcion.Text = "DESCRIPCION DE LA TECNICA:";
			// 
			// txtDescripcionEspecial
			// 
			this.txtDescripcionEspecial.BackColor = System.Drawing.Color.White;
			this.txtDescripcionEspecial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDescripcionEspecial.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDescripcionEspecial.Location = new System.Drawing.Point(10, 286);
			this.txtDescripcionEspecial.Multiline = true;
			this.txtDescripcionEspecial.Name = "txtDescripcionEspecial";
			this.txtDescripcionEspecial.Size = new System.Drawing.Size(342, 92);
			this.txtDescripcionEspecial.TabIndex = 20;
			this.txtDescripcionEspecial.Text = "Invoca un tsunami colosal que drena 1 punto de energia enemiga y restaura 25% HP.";
			this.txtDescripcionEspecial.TextChanged += new System.EventHandler(this.CamposVistaPreviaChanged);
			// 
			// btnGuardarPersonajeTxt
			// 
			this.btnGuardarPersonajeTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(48)))), ((int)(((byte)(32)))));
			this.btnGuardarPersonajeTxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGuardarPersonajeTxt.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGuardarPersonajeTxt.ForeColor = System.Drawing.Color.Yellow;
			this.btnGuardarPersonajeTxt.Location = new System.Drawing.Point(10, 392);
			this.btnGuardarPersonajeTxt.Name = "btnGuardarPersonajeTxt";
			this.btnGuardarPersonajeTxt.Size = new System.Drawing.Size(238, 56);
			this.btnGuardarPersonajeTxt.TabIndex = 21;
			this.btnGuardarPersonajeTxt.Text = "► GUARDAR EN .TXT ◄";
			this.btnGuardarPersonajeTxt.UseVisualStyleBackColor = false;
			this.btnGuardarPersonajeTxt.Click += new System.EventHandler(this.BtnGuardarPersonajeTxtClick);
			// 
			// btnLimpiarCampos
			// 
			this.btnLimpiarCampos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(95)))), ((int)(((byte)(110)))));
			this.btnLimpiarCampos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLimpiarCampos.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLimpiarCampos.ForeColor = System.Drawing.Color.White;
			this.btnLimpiarCampos.Location = new System.Drawing.Point(254, 392);
			this.btnLimpiarCampos.Name = "btnLimpiarCampos";
			this.btnLimpiarCampos.Size = new System.Drawing.Size(98, 56);
			this.btnLimpiarCampos.TabIndex = 22;
			this.btnLimpiarCampos.Text = "LIMPIAR";
			this.btnLimpiarCampos.UseVisualStyleBackColor = false;
			this.btnLimpiarCampos.Click += new System.EventHandler(this.BtnLimpiarCamposClick);
			// 
			// pnlMarcoCarta
			// 
			this.pnlMarcoCarta.BackColor = System.Drawing.Color.Gold;
			this.pnlMarcoCarta.Controls.Add(this.pnlCartaInterior);
			this.pnlMarcoCarta.Location = new System.Drawing.Point(394, 68);
			this.pnlMarcoCarta.Name = "pnlMarcoCarta";
			this.pnlMarcoCarta.Size = new System.Drawing.Size(256, 472);
			this.pnlMarcoCarta.TabIndex = 3;
			// 
			// pnlCartaInterior
			// 
			this.pnlCartaInterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(34)))), ((int)(((byte)(52)))));
			this.pnlCartaInterior.Controls.Add(this.lblCartaEspecialVista);
			this.pnlCartaInterior.Controls.Add(this.lblCartaPasiva);
			this.pnlCartaInterior.Controls.Add(this.lblCartaEmp);
			this.pnlCartaInterior.Controls.Add(this.pbCartaAtk);
			this.pnlCartaInterior.Controls.Add(this.lblCartaAtk);
			this.pnlCartaInterior.Controls.Add(this.pbCartaDef);
			this.pnlCartaInterior.Controls.Add(this.lblCartaDef);
			this.pnlCartaInterior.Controls.Add(this.pbCartaHp);
			this.pnlCartaInterior.Controls.Add(this.lblCartaHp);
			this.pnlCartaInterior.Controls.Add(this.lblCartaNombreVista);
			this.pnlCartaInterior.Controls.Add(this.pnlSpriteArte);
			this.pnlCartaInterior.Controls.Add(this.lblCartaFaccionBanner);
			this.pnlCartaInterior.Location = new System.Drawing.Point(4, 4);
			this.pnlCartaInterior.Name = "pnlCartaInterior";
			this.pnlCartaInterior.Size = new System.Drawing.Size(248, 464);
			this.pnlCartaInterior.TabIndex = 0;
			// 
			// lblCartaFaccionBanner
			// 
			this.lblCartaFaccionBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(115)))), ((int)(((byte)(210)))));
			this.lblCartaFaccionBanner.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCartaFaccionBanner.ForeColor = System.Drawing.Color.White;
			this.lblCartaFaccionBanner.Location = new System.Drawing.Point(0, 0);
			this.lblCartaFaccionBanner.Name = "lblCartaFaccionBanner";
			this.lblCartaFaccionBanner.Size = new System.Drawing.Size(248, 30);
			this.lblCartaFaccionBanner.TabIndex = 0;
			this.lblCartaFaccionBanner.Text = "VISTA PREVIA :: [AGUA]";
			this.lblCartaFaccionBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlSpriteArte
			// 
			this.pnlSpriteArte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(24)))), ((int)(((byte)(38)))));
			this.pnlSpriteArte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSpriteArte.Location = new System.Drawing.Point(12, 38);
			this.pnlSpriteArte.Name = "pnlSpriteArte";
			this.pnlSpriteArte.Size = new System.Drawing.Size(224, 140);
			this.pnlSpriteArte.TabIndex = 1;
			this.pnlSpriteArte.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSpriteArtePaint);
			// 
			// lblCartaNombreVista
			// 
			this.lblCartaNombreVista.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCartaNombreVista.ForeColor = System.Drawing.Color.Gold;
			this.lblCartaNombreVista.Location = new System.Drawing.Point(8, 184);
			this.lblCartaNombreVista.Name = "lblCartaNombreVista";
			this.lblCartaNombreVista.Size = new System.Drawing.Size(232, 24);
			this.lblCartaNombreVista.TabIndex = 2;
			this.lblCartaNombreVista.Text = "Gyarados Del Caribe";
			this.lblCartaNombreVista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblCartaHp
			// 
			this.lblCartaHp.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCartaHp.ForeColor = System.Drawing.Color.LightGreen;
			this.lblCartaHp.Location = new System.Drawing.Point(12, 216);
			this.lblCartaHp.Name = "lblCartaHp";
			this.lblCartaHp.Size = new System.Drawing.Size(95, 18);
			this.lblCartaHp.TabIndex = 3;
			this.lblCartaHp.Text = "VIDA  : 50";
			// 
			// pbCartaHp
			// 
			this.pbCartaHp.Location = new System.Drawing.Point(110, 217);
			this.pbCartaHp.Maximum = 100;
			this.pbCartaHp.Name = "pbCartaHp";
			this.pbCartaHp.Size = new System.Drawing.Size(126, 14);
			this.pbCartaHp.TabIndex = 4;
			this.pbCartaHp.Value = 50;
			// 
			// lblCartaDef
			// 
			this.lblCartaDef.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCartaDef.ForeColor = System.Drawing.Color.LightSkyBlue;
			this.lblCartaDef.Location = new System.Drawing.Point(12, 240);
			this.lblCartaDef.Name = "lblCartaDef";
			this.lblCartaDef.Size = new System.Drawing.Size(95, 18);
			this.lblCartaDef.TabIndex = 5;
			this.lblCartaDef.Text = "ESCUDO: 25";
			// 
			// pbCartaDef
			// 
			this.pbCartaDef.Location = new System.Drawing.Point(110, 241);
			this.pbCartaDef.Maximum = 100;
			this.pbCartaDef.Name = "pbCartaDef";
			this.pbCartaDef.Size = new System.Drawing.Size(126, 14);
			this.pbCartaDef.TabIndex = 6;
			this.pbCartaDef.Value = 25;
			// 
			// lblCartaAtk
			// 
			this.lblCartaAtk.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCartaAtk.ForeColor = System.Drawing.Color.Orange;
			this.lblCartaAtk.Location = new System.Drawing.Point(12, 264);
			this.lblCartaAtk.Name = "lblCartaAtk";
			this.lblCartaAtk.Size = new System.Drawing.Size(95, 18);
			this.lblCartaAtk.TabIndex = 7;
			this.lblCartaAtk.Text = "ATAQUE: 25";
			// 
			// pbCartaAtk
			// 
			this.pbCartaAtk.Location = new System.Drawing.Point(110, 265);
			this.pbCartaAtk.Maximum = 100;
			this.pbCartaAtk.Name = "pbCartaAtk";
			this.pbCartaAtk.Size = new System.Drawing.Size(126, 14);
			this.pbCartaAtk.TabIndex = 8;
			this.pbCartaAtk.Value = 25;
			// 
			// lblCartaEmp
			// 
			this.lblCartaEmp.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCartaEmp.ForeColor = System.Drawing.Color.Plum;
			this.lblCartaEmp.Location = new System.Drawing.Point(12, 290);
			this.lblCartaEmp.Name = "lblCartaEmp";
			this.lblCartaEmp.Size = new System.Drawing.Size(224, 18);
			this.lblCartaEmp.TabIndex = 9;
			this.lblCartaEmp.Text = "EMPATIA: 75/100 | ENERGIA: 2";
			// 
			// lblCartaPasiva
			// 
			this.lblCartaPasiva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(48)))), ((int)(((byte)(72)))));
			this.lblCartaPasiva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblCartaPasiva.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCartaPasiva.ForeColor = System.Drawing.Color.Aquamarine;
			this.lblCartaPasiva.Location = new System.Drawing.Point(12, 314);
			this.lblCartaPasiva.Name = "lblCartaPasiva";
			this.lblCartaPasiva.Padding = new System.Windows.Forms.Padding(4);
			this.lblCartaPasiva.Size = new System.Drawing.Size(224, 58);
			this.lblCartaPasiva.TabIndex = 10;
			this.lblCartaPasiva.Text = "CLASE: CartaAgua (Roba 1 Energia, cura 25% HP y ataca).";
			// 
			// lblCartaEspecialVista
			// 
			this.lblCartaEspecialVista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(216)))));
			this.lblCartaEspecialVista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblCartaEspecialVista.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCartaEspecialVista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
			this.lblCartaEspecialVista.Location = new System.Drawing.Point(12, 378);
			this.lblCartaEspecialVista.Name = "lblCartaEspecialVista";
			this.lblCartaEspecialVista.Padding = new System.Windows.Forms.Padding(4);
			this.lblCartaEspecialVista.Size = new System.Drawing.Size(224, 74);
			this.lblCartaEspecialVista.TabIndex = 11;
			this.lblCartaEspecialVista.Text = "[Maremoto Abisal]: Invoca un tsunami colosal.";
			// 
			// pnlMarcoCatalogo
			// 
			this.pnlMarcoCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(176)))), ((int)(((byte)(48)))));
			this.pnlMarcoCatalogo.Controls.Add(this.pnlCatalogoInterior);
			this.pnlMarcoCatalogo.Location = new System.Drawing.Point(660, 68);
			this.pnlMarcoCatalogo.Name = "pnlMarcoCatalogo";
			this.pnlMarcoCatalogo.Size = new System.Drawing.Size(288, 472);
			this.pnlMarcoCatalogo.TabIndex = 4;
			// 
			// pnlCatalogoInterior
			// 
			this.pnlCatalogoInterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(244)))), ((int)(((byte)(216)))));
			this.pnlCatalogoInterior.Controls.Add(this.btnRestaurarBase);
			this.pnlCatalogoInterior.Controls.Add(this.btnEliminarDeTxt);
			this.pnlCatalogoInterior.Controls.Add(this.btnCargarEnEditor);
			this.pnlCatalogoInterior.Controls.Add(this.lblTotalRegistrados);
			this.pnlCatalogoInterior.Controls.Add(this.lstPersonajesTxt);
			this.pnlCatalogoInterior.Controls.Add(this.lblHeaderCatalogo);
			this.pnlCatalogoInterior.Location = new System.Drawing.Point(4, 4);
			this.pnlCatalogoInterior.Name = "pnlCatalogoInterior";
			this.pnlCatalogoInterior.Size = new System.Drawing.Size(280, 464);
			this.pnlCatalogoInterior.TabIndex = 0;
			// 
			// lblHeaderCatalogo
			// 
			this.lblHeaderCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(56)))), ((int)(((byte)(88)))));
			this.lblHeaderCatalogo.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeaderCatalogo.ForeColor = System.Drawing.Color.Gold;
			this.lblHeaderCatalogo.Location = new System.Drawing.Point(0, 0);
			this.lblHeaderCatalogo.Name = "lblHeaderCatalogo";
			this.lblHeaderCatalogo.Size = new System.Drawing.Size(280, 30);
			this.lblHeaderCatalogo.TabIndex = 0;
			this.lblHeaderCatalogo.Text = " 2. CATALOGO EN PERSONAJES.TXT";
			this.lblHeaderCatalogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lstPersonajesTxt
			// 
			this.lstPersonajesTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
			this.lstPersonajesTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstPersonajesTxt.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstPersonajesTxt.ForeColor = System.Drawing.Color.White;
			this.lstPersonajesTxt.HorizontalScrollbar = true;
			this.lstPersonajesTxt.ItemHeight = 14;
			this.lstPersonajesTxt.Location = new System.Drawing.Point(10, 38);
			this.lstPersonajesTxt.Name = "lstPersonajesTxt";
			this.lstPersonajesTxt.Size = new System.Drawing.Size(260, 324);
			this.lstPersonajesTxt.TabIndex = 1;
			this.lstPersonajesTxt.SelectedIndexChanged += new System.EventHandler(this.LstPersonajesTxtSelectedIndexChanged);
			// 
			// lblTotalRegistrados
			// 
			this.lblTotalRegistrados.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTotalRegistrados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(80)))));
			this.lblTotalRegistrados.Location = new System.Drawing.Point(10, 368);
			this.lblTotalRegistrados.Name = "lblTotalRegistrados";
			this.lblTotalRegistrados.Size = new System.Drawing.Size(260, 18);
			this.lblTotalRegistrados.TabIndex = 2;
			this.lblTotalRegistrados.Text = "Cartas en personajes.txt: 12";
			// 
			// btnCargarEnEditor
			// 
			this.btnCargarEnEditor.BackColor = System.Drawing.Color.SeaGreen;
			this.btnCargarEnEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCargarEnEditor.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCargarEnEditor.ForeColor = System.Drawing.Color.White;
			this.btnCargarEnEditor.Location = new System.Drawing.Point(10, 392);
			this.btnCargarEnEditor.Name = "btnCargarEnEditor";
			this.btnCargarEnEditor.Size = new System.Drawing.Size(82, 56);
			this.btnCargarEnEditor.TabIndex = 3;
			this.btnCargarEnEditor.Text = "EDITAR";
			this.btnCargarEnEditor.UseVisualStyleBackColor = false;
			this.btnCargarEnEditor.Click += new System.EventHandler(this.BtnCargarEnEditorClick);
			// 
			// btnEliminarDeTxt
			// 
			this.btnEliminarDeTxt.BackColor = System.Drawing.Color.Firebrick;
			this.btnEliminarDeTxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEliminarDeTxt.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnEliminarDeTxt.ForeColor = System.Drawing.Color.White;
			this.btnEliminarDeTxt.Location = new System.Drawing.Point(99, 392);
			this.btnEliminarDeTxt.Name = "btnEliminarDeTxt";
			this.btnEliminarDeTxt.Size = new System.Drawing.Size(82, 56);
			this.btnEliminarDeTxt.TabIndex = 4;
			this.btnEliminarDeTxt.Text = "BORRAR";
			this.btnEliminarDeTxt.UseVisualStyleBackColor = false;
			this.btnEliminarDeTxt.Click += new System.EventHandler(this.BtnEliminarDeTxtClick);
			// 
			// btnRestaurarBase
			// 
			this.btnRestaurarBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(85)))), ((int)(((byte)(110)))));
			this.btnRestaurarBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRestaurarBase.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRestaurarBase.ForeColor = System.Drawing.Color.White;
			this.btnRestaurarBase.Location = new System.Drawing.Point(188, 392);
			this.btnRestaurarBase.Name = "btnRestaurarBase";
			this.btnRestaurarBase.Size = new System.Drawing.Size(82, 56);
			this.btnRestaurarBase.TabIndex = 5;
			this.btnRestaurarBase.Text = "RESET 12";
			this.btnRestaurarBase.UseVisualStyleBackColor = false;
			this.btnRestaurarBase.Click += new System.EventHandler(this.BtnRestaurarBaseClick);
			// 
			// pnlMarcoDialogoGba
			// 
			this.pnlMarcoDialogoGba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(44)))), ((int)(((byte)(32)))));
			this.pnlMarcoDialogoGba.Controls.Add(this.pnlDialogoInterior);
			this.pnlMarcoDialogoGba.Location = new System.Drawing.Point(12, 548);
			this.pnlMarcoDialogoGba.Name = "pnlMarcoDialogoGba";
			this.pnlMarcoDialogoGba.Size = new System.Drawing.Size(752, 80);
			this.pnlMarcoDialogoGba.TabIndex = 5;
			// 
			// pnlDialogoInterior
			// 
			this.pnlDialogoInterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(62)))));
			this.pnlDialogoInterior.Controls.Add(this.lblTextoProfesorGba);
			this.pnlDialogoInterior.Controls.Add(this.lblTituloDialogoGba);
			this.pnlDialogoInterior.Location = new System.Drawing.Point(4, 4);
			this.pnlDialogoInterior.Name = "pnlDialogoInterior";
			this.pnlDialogoInterior.Size = new System.Drawing.Size(744, 72);
			this.pnlDialogoInterior.TabIndex = 0;
			// 
			// lblTituloDialogoGba
			// 
			this.lblTituloDialogoGba.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTituloDialogoGba.ForeColor = System.Drawing.Color.Gold;
			this.lblTituloDialogoGba.Location = new System.Drawing.Point(10, 6);
			this.lblTituloDialogoGba.Name = "lblTituloDialogoGba";
			this.lblTituloDialogoGba.Size = new System.Drawing.Size(724, 18);
			this.lblTituloDialogoGba.TabIndex = 0;
			this.lblTituloDialogoGba.Text = "PROF. LUCENA (CONSOLA GBA): ¡Bienvenido al Laboratorio de Leviatanes!";
			// 
			// lblTextoProfesorGba
			// 
			this.lblTextoProfesorGba.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTextoProfesorGba.ForeColor = System.Drawing.Color.White;
			this.lblTextoProfesorGba.Location = new System.Drawing.Point(10, 28);
			this.lblTextoProfesorGba.Name = "lblTextoProfesorGba";
			this.lblTextoProfesorGba.Size = new System.Drawing.Size(724, 38);
			this.lblTextoProfesorGba.TabIndex = 1;
			this.lblTextoProfesorGba.Text = "Reparte exactamente 100 puntos entre Vida (HP), Escudo (DEF) y Ataque (ATK) para " +
			"registrar tu criatura en personajes.txt ▼";
			// 
			// VolverMenu
			// 
			this.VolverMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(68)))), ((int)(((byte)(90)))));
			this.VolverMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.VolverMenu.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.VolverMenu.ForeColor = System.Drawing.Color.Gold;
			this.VolverMenu.Location = new System.Drawing.Point(774, 548);
			this.VolverMenu.Name = "VolverMenu";
			this.VolverMenu.Size = new System.Drawing.Size(174, 80);
			this.VolverMenu.TabIndex = 6;
			this.VolverMenu.Text = "<< VOLVER AL MENU";
			this.VolverMenu.UseVisualStyleBackColor = false;
			this.VolverMenu.Click += new System.EventHandler(this.VolverMenuClick);
			// 
			// CrearP
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(52)))));
			this.ClientSize = new System.Drawing.Size(960, 640);
			this.Controls.Add(this.pnlMarcoDialogoGba);
			this.Controls.Add(this.pnlMarcoCatalogo);
			this.Controls.Add(this.pnlMarcoCarta);
			this.Controls.Add(this.pnlMarcoIzquierdo);
			this.Controls.Add(this.pnlFranjaDorada);
			this.Controls.Add(this.pnlHeaderGba);
			this.Controls.Add(this.VolverMenu);
			this.Name = "CrearP";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CrearP - Pokedex Elemental GBA";
			this.pnlHeaderGba.ResumeLayout(false);
			this.pnlMarcoIzquierdo.ResumeLayout(false);
			this.pnlFormularioGba.ResumeLayout(false);
			this.pnlFormularioGba.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numAtaque)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDefensa)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numVida)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numEmpatia)).EndInit();
			this.pnlMarcoCarta.ResumeLayout(false);
			this.pnlCartaInterior.ResumeLayout(false);
			this.pnlMarcoCatalogo.ResumeLayout(false);
			this.pnlCatalogoInterior.ResumeLayout(false);
			this.pnlMarcoDialogoGba.ResumeLayout(false);
			this.pnlDialogoInterior.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lblTextoProfesorGba;
		private System.Windows.Forms.Label lblTituloDialogoGba;
		private System.Windows.Forms.Panel pnlDialogoInterior;
		private System.Windows.Forms.Panel pnlMarcoDialogoGba;
		private System.Windows.Forms.Button btnRestaurarBase;
		private System.Windows.Forms.Button btnEliminarDeTxt;
		private System.Windows.Forms.Button btnCargarEnEditor;
		private System.Windows.Forms.Label lblTotalRegistrados;
		private System.Windows.Forms.ListBox lstPersonajesTxt;
		private System.Windows.Forms.Label lblHeaderCatalogo;
		private System.Windows.Forms.Panel pnlCatalogoInterior;
		private System.Windows.Forms.Panel pnlMarcoCatalogo;
		private System.Windows.Forms.Label lblCartaEspecialVista;
		private System.Windows.Forms.Label lblCartaPasiva;
		private System.Windows.Forms.Label lblCartaEmp;
		private System.Windows.Forms.ProgressBar pbCartaAtk;
		private System.Windows.Forms.Label lblCartaAtk;
		private System.Windows.Forms.ProgressBar pbCartaDef;
		private System.Windows.Forms.Label lblCartaDef;
		private System.Windows.Forms.ProgressBar pbCartaHp;
		private System.Windows.Forms.Label lblCartaHp;
		private System.Windows.Forms.Label lblCartaNombreVista;
		private System.Windows.Forms.Panel pnlSpriteArte;
		private System.Windows.Forms.Label lblCartaFaccionBanner;
		private System.Windows.Forms.Panel pnlCartaInterior;
		private System.Windows.Forms.Panel pnlMarcoCarta;
		private System.Windows.Forms.Button btnLimpiarCampos;
		private System.Windows.Forms.Button btnGuardarPersonajeTxt;
		private System.Windows.Forms.TextBox txtDescripcionEspecial;
		private System.Windows.Forms.Label lblDescripcion;
		private System.Windows.Forms.TextBox txtNombreEspecial;
		private System.Windows.Forms.Label lblEspecial;
		private System.Windows.Forms.Button btnAjustarCien;
		private System.Windows.Forms.Label lblEstadoBalance;
		private System.Windows.Forms.ProgressBar pbBalanceCien;
		private System.Windows.Forms.NumericUpDown numAtaque;
		private System.Windows.Forms.Label lblAtaque;
		private System.Windows.Forms.NumericUpDown numDefensa;
		private System.Windows.Forms.Label lblDefensa;
		private System.Windows.Forms.NumericUpDown numVida;
		private System.Windows.Forms.Label lblVida;
		private System.Windows.Forms.Label lblReglaCien;
		private System.Windows.Forms.NumericUpDown numEmpatia;
		private System.Windows.Forms.Label lblEmpatia;
		private System.Windows.Forms.ComboBox cmbFaccion;
		private System.Windows.Forms.Label lblFaccion;
		private System.Windows.Forms.TextBox txtNombre;
		private System.Windows.Forms.Label lblNombre;
		private System.Windows.Forms.Label lblHeaderFormulario;
		private System.Windows.Forms.Panel pnlFormularioGba;
		private System.Windows.Forms.Panel pnlMarcoIzquierdo;
		private System.Windows.Forms.Panel pnlFranjaDorada;
		private System.Windows.Forms.Label lblSubtituloGba;
		private System.Windows.Forms.Label lblTituloGba;
		private System.Windows.Forms.Panel pnlHeaderGba;
		private System.Windows.Forms.Button VolverMenu;
	}
}
