/*
 * Created by SharpDevelop.
 * User: Alfonzovaa
 * Date: 30/10/2026
 * Time: 11:27 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Leviatanes2
{
	/// <summary>
	/// Description of VerPartidas.
	/// </summary>
	public partial class VerPartidas : Form
	{
		public VerPartidas()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void VolverMenuClick(object sender, EventArgs e)
		{
			MainForm abrirVentana = new MainForm();
            abrirVentana.Show();
            
    		abrirVentana.FormClosed += (s, args) => Application.Exit();
    		
            this.Hide();
		}
	}
}
