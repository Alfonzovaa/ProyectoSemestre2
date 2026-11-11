/*
 * Created by SharpDevelop.
 * User: Alfonzovaa
 * Date: 30/10/2026
 * Time: 11:27 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Leviatanes2.Clases;

namespace Leviatanes2
{
	/// <summary>
	/// Ventana modular VerPartidas: Ranking de Jugadores (jugadores.txt),
	/// Historial de Partidas (partidas.txt) y Visor de Bitacora (.txt) estilo GBA.
	/// </summary>
	public partial class VerPartidas : Form
	{
		private GestorPersistenciaTxt GestorTxt;
		private List<PerfilJugador> ListaJugadores;
		private List<RegistroPartidaTxt> ListaPartidas;

		public VerPartidas()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.ClientSize = new Size(UiResponsiveManager.ANCHO_BASE, UiResponsiveManager.ALTO_BASE);
			UiResponsiveManager.ConfigurarFormulario(this);

			InicializarModuloEstadisticas();
		}

		private void InicializarModuloEstadisticas()
		{
			GestorTxt = new GestorPersistenciaTxt();
			ListaJugadores = new List<PerfilJugador>();
			ListaPartidas = new List<RegistroPartidaTxt>();

			ConfigurarColumnasTablasGba();
			CargarDatosDesdeArchivosTxt();
		}

		private void ConfigurarColumnasTablasGba()
		{
			// Estilo visual GBA para DataGridView de Jugadores
			dgvRankingJugadores.EnableHeadersVisualStyles = false;
			dgvRankingJugadores.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 56, 88);
			dgvRankingJugadores.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
			dgvRankingJugadores.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 8.5F, FontStyle.Bold);
			dgvRankingJugadores.DefaultCellStyle.BackColor = Color.FromArgb(24, 34, 50);
			dgvRankingJugadores.DefaultCellStyle.ForeColor = Color.White;
			dgvRankingJugadores.DefaultCellStyle.SelectionBackColor = Color.FromArgb(204, 48, 32);
			dgvRankingJugadores.DefaultCellStyle.SelectionForeColor = Color.Yellow;
			dgvRankingJugadores.DefaultCellStyle.Font = new Font("Consolas", 8.5F, FontStyle.Bold);

			dgvRankingJugadores.Columns.Clear();
			dgvRankingJugadores.Columns.Add("ColId", "#");
			dgvRankingJugadores.Columns.Add("ColNombre", "Entrenador");
			dgvRankingJugadores.Columns.Add("ColPJ", "PJ");
			dgvRankingJugadores.Columns.Add("ColV", "V");
			dgvRankingJugadores.Columns.Add("ColD", "D");
			dgvRankingJugadores.Columns.Add("ColExito", "% Exito");

			dgvRankingJugadores.Columns[0].FillWeight = 32;
			dgvRankingJugadores.Columns[1].FillWeight = 125;
			dgvRankingJugadores.Columns[2].FillWeight = 42;
			dgvRankingJugadores.Columns[3].FillWeight = 42;
			dgvRankingJugadores.Columns[4].FillWeight = 42;
			dgvRankingJugadores.Columns[5].FillWeight = 75;

			// Estilo visual GBA para DataGridView de Historial de Partidas
			dgvHistorialPartidas.EnableHeadersVisualStyles = false;
			dgvHistorialPartidas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 56, 88);
			dgvHistorialPartidas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
			dgvHistorialPartidas.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 8.5F, FontStyle.Bold);
			dgvHistorialPartidas.DefaultCellStyle.BackColor = Color.FromArgb(24, 34, 50);
			dgvHistorialPartidas.DefaultCellStyle.ForeColor = Color.White;
			dgvHistorialPartidas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 96, 168);
			dgvHistorialPartidas.DefaultCellStyle.SelectionForeColor = Color.Yellow;
			dgvHistorialPartidas.DefaultCellStyle.Font = new Font("Consolas", 8.5F, FontStyle.Bold);

			dgvHistorialPartidas.Columns.Clear();
			dgvHistorialPartidas.Columns.Add("ColPartidaId", "#");
			dgvHistorialPartidas.Columns.Add("ColFecha", "Fecha y Hora");
			dgvHistorialPartidas.Columns.Add("ColJ1", "Jugador 1");
			dgvHistorialPartidas.Columns.Add("ColJ2", "Jugador 2");
			dgvHistorialPartidas.Columns.Add("ColGanador", "Vencedor");
			dgvHistorialPartidas.Columns.Add("ColEscenario", "Escenario");
			dgvHistorialPartidas.Columns.Add("ColTurnos", "Turnos");

			dgvHistorialPartidas.Columns[0].FillWeight = 30;
			dgvHistorialPartidas.Columns[1].FillWeight = 125;
			dgvHistorialPartidas.Columns[2].FillWeight = 90;
			dgvHistorialPartidas.Columns[3].FillWeight = 90;
			dgvHistorialPartidas.Columns[4].FillWeight = 90;
			dgvHistorialPartidas.Columns[5].FillWeight = 105;
			dgvHistorialPartidas.Columns[6].FillWeight = 55;
		}

		private void CargarDatosDesdeArchivosTxt()
		{
			// 1. Cargar Jugadores y ordenar por Victorias y Porcentaje de Exito
			ListaJugadores = GestorTxt.CargarJugadoresDesdeTxt();
			ListaJugadores.Sort(CompararJugadoresPorRanking);

			dgvRankingJugadores.Rows.Clear();
			for (int I = 0; I < ListaJugadores.Count; I++)
			{
				PerfilJugador J = ListaJugadores[I];
				int IndiceFila = dgvRankingJugadores.Rows.Add(
					J.IdJugador,
					J.NombreUsuario,
					J.PartidasJugadas,
					J.Victorias,
					J.Derrotas,
					string.Format("{0:0.00}%", J.PorcentajeExito));
				dgvRankingJugadores.Rows[IndiceFila].Tag = J;
			}

			if (dgvRankingJugadores.Rows.Count > 0)
			{
				dgvRankingJugadores.Rows[0].Selected = true;
				ActualizarDetalleJugadorSeleccionado();
			}

			// 2. Cargar Historial de Partidas (mas reciente primero)
			ListaPartidas = GestorTxt.CargarHistorialPartidasDesdeTxt();
			dgvHistorialPartidas.Rows.Clear();

			for (int I = ListaPartidas.Count - 1; I >= 0; I--)
			{
				RegistroPartidaTxt P = ListaPartidas[I];
				int IndiceFila = dgvHistorialPartidas.Rows.Add(
					P.IdPartida,
					P.FechaHora,
					P.JugadorUno,
					P.JugadorDos,
					P.Ganador,
					P.Escenario,
					P.TotalTurnos);
				dgvHistorialPartidas.Rows[IndiceFila].Tag = P;
			}

			if (dgvHistorialPartidas.Rows.Count > 0)
			{
				dgvHistorialPartidas.Rows[0].Selected = true;
				ActualizarVisorBitacoraSeleccionada();
			}
			else
			{
				txtVisorBitacora.Text = "Aun no hay partidas registradas en 'partidas.txt'.";
			}
		}

		private static int CompararJugadoresPorRanking(PerfilJugador A, PerfilJugador B)
		{
			int CompVictorias = B.Victorias.CompareTo(A.Victorias);
			if (CompVictorias != 0)
				return CompVictorias;
			return B.PorcentajeExito.CompareTo(A.PorcentajeExito);
		}

		private void ActualizarDetalleJugadorSeleccionado()
		{
			if (dgvRankingJugadores.SelectedRows.Count == 0)
				return;

			PerfilJugador J = dgvRankingJugadores.SelectedRows[0].Tag as PerfilJugador;
			if (J == null)
				return;

			int ValorBarra = (int)Math.Round(J.PorcentajeExito);
			pbPorcentajeExito.Value = Math.Min(100, Math.Max(0, ValorBarra));

			lblDetalleJugadorSel.Text = string.Format(
				"ENTRENADOR: {0} (ID #{1:00})\r\nPartidas: {2} | Victorias: {3} | Derrotas: {4} | Exito: {5:0.00}%",
				J.NombreUsuario.ToUpper(),
				J.IdJugador,
				J.PartidasJugadas,
				J.Victorias,
				J.Derrotas,
				J.PorcentajeExito);
		}

		private void ActualizarVisorBitacoraSeleccionada()
		{
			if (dgvHistorialPartidas.SelectedRows.Count == 0)
				return;

			RegistroPartidaTxt Reg = dgvHistorialPartidas.SelectedRows[0].Tag as RegistroPartidaTxt;
			if (Reg == null)
				return;

			lblHeaderBitacora.Text = string.Format(" 3. BITACORA DE PARTIDA #{0:00} ({1})", Reg.IdPartida, Reg.ArchivoBitacora);
			txtVisorBitacora.Text = GestorTxt.LeerContenidoBitacora(Reg.ArchivoBitacora);

			lblTextoProfesorGba.Text = string.Format(
				"PARTIDA #{0:00}: {1} vs {2} en [{3}] -> Vencedor: {4} en {5} turnos (Archivo: {6}) ▼",
				Reg.IdPartida,
				Reg.JugadorUno,
				Reg.JugadorDos,
				Reg.Escenario,
				Reg.Ganador,
				Reg.TotalTurnos,
				Reg.ArchivoBitacora);
		}

		void DgvRankingJugadoresSelectionChanged(object sender, EventArgs e)
		{
			ActualizarDetalleJugadorSeleccionado();
		}

		void DgvHistorialPartidasSelectionChanged(object sender, EventArgs e)
		{
			ActualizarVisorBitacoraSeleccionada();
		}

		void BtnRegistrarJugadorTxtClick(object sender, EventArgs e)
		{
			string Nombre = txtNuevoJugador.Text.Trim();
			if (string.IsNullOrEmpty(Nombre))
			{
				MessageBox.Show("Escribe un nombre de usuario para registrarlo en jugadores.txt.", "Nombre Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			PerfilJugador Registrado = GestorTxt.AutenticarORegistrarJugador(Nombre);
			CargarDatosDesdeArchivosTxt();

			lblTextoProfesorGba.Text = string.Format(
				"¡Entrenador '{0}' (ID #{1:00}) sincronizado en jugadores.txt! ▼",
				Registrado.NombreUsuario,
				Registrado.IdJugador);
		}

		void BtnRecargarTxtClick(object sender, EventArgs e)
		{
			CargarDatosDesdeArchivosTxt();
			lblTextoProfesorGba.Text = "Archivos jugadores.txt, partidas.txt y bitácoras .TXT recargados desde el disco ▼";
		}

		void VolverMenuClick(object sender, EventArgs e)
		{
			MainForm abrirVentana = new MainForm();
			UiResponsiveManager.CambiarVentana(this, abrirVentana);
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			Application.Exit();
		}
		
		void DgvHistorialPartidasCellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			
		}
	}
}
