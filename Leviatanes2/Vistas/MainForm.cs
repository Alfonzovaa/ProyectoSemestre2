using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Leviatanes2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void Button1Click(object sender, EventArgs e)
        {
            // Cree un un objeto del tipo del formulario de CrearP para lleve a ese formulario con la opcion show
            CrearP abrirVentana = new CrearP();
            abrirVentana.Show();
            
            // Cuando el jugador cierre CrearP (con la X o por código), se cierra toda la app
    		abrirVentana.FormClosed += (s, args) => Application.Exit();

			// Y con esta opcion se cierra la ventana del menu
            this.Hide();
        }

        void Button2Click(object sender, EventArgs e)
        {
            // La misma dinamica para abrir la venta de CrearP
            VerPartidas abrirVentana = new VerPartidas();
            abrirVentana.Show();
            
    		abrirVentana.FormClosed += (s, args) => Application.Exit();
    		
            this.Hide();
        }

        void Button3Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}