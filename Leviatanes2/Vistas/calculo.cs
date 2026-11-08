/*
 * Created by SharpDevelop.
 * User: alexa
 * Date: 8/11/2026
 * Time: 1:15 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Leviatanes2
{
	/// <summary>
	/// Description of calculo.
	/// </summary>
	public partial class calculo : Form
{
    private int ejercicioSolicitado = 1;
    private string respuestaCorrecta = "";

   public calculo(int numeroEjercicio)
    {
        InitializeComponent();
        ejercicioSolicitado = numeroEjercicio;
        CargarEjercicioEspecifico();
    }

 
        private void CargarEjercicioEspecifico()
    {
        switch (ejercicioSolicitado)
        {
            case 1:
                lblEnunciado.Text = "Reto 1: Ecuación de la Recta.\n\n" +
                                    "La ecuación punto-pendiente se define como y - y1 = m(x - x1).\n" +
                                    "Encuentra la pendiente (m) de la recta que pasa por los puntos (1, 2) y (3, 6).\n\n" +
                                    "Nota: Escribe el valor numérico de m en la casilla blanca y presiona Comprobar.";
                respuestaCorrecta = "2";
                break;

            case 2:
                lblEnunciado.Text = "Reto 2: Límites Directos.\n\n" +
                                    "El límite de una función cuando x tiende a 'a' consiste en evaluar la función en dicho punto si no hay indeterminación.\n" +
                                    "Calcula el lim (x -> 3) de (2x + 4).\n\n" +
                                    "Nota: Escribe tu resultado en la casilla blanca y presiona Comprobar.";
                respuestaCorrecta = "10";
                break;

            case 3:
                lblEnunciado.Text = "Reto 3: Indeterminación 0/0.\n\n" +
                                    "Cuando un límite resulta 0/0 se debe factorizar la expresión para simplificar el término indeterminado.\n" +
                                    "Calcula el lim (x -> 2) de (x^2 - 4) / (x - 2).\n\n" +
                                    "Nota: Escribe tu resultado en la casilla blanca y presiona Comprobar.";
                respuestaCorrecta = "4";
                break;

            case 4:
                lblEnunciado.Text = "Reto 4: Derivada (Regla de la Potencia).\n\n" +
                                    "Para derivar f(x) = x^n se multiplica por el exponente y se le resta 1: f'(x) = n * x^(n-1).\n" +
                                    "Calcula la derivada de f(x) = x^4.\n\n" +
                                    "Nota: Escribe tu respuesta como 4x^3 en la casilla blanca y presiona Comprobar.";
                respuestaCorrecta = "4x^3";
                break;

            case 5:
                lblEnunciado.Text = "Reto 5: Derivada (Constante por Función).\n\n" +
                                    "La derivada de c * f(x) es igual a c * f'(x).\n" +
                                    "Calcula la derivada de f(x) = 3x^2.\n\n" +
                                    "Nota: Escribe tu respuesta como 6x en la casilla blanca y presiona Comprobar.";
                respuestaCorrecta = "6x";
                break;
        }

        txtRespuesta.Clear();
    }

   

		
		private	void BtnsiguienteClick(object sender, EventArgs e)
		{	
		 this.Close();
		}
		
		
		
		private void BtnComprobarClick(object sender, EventArgs e)
		{
			
	string respuestaUsuario = txtRespuesta.Text.Trim().ToLower();

        if (respuestaUsuario == respuestaCorrecta.ToLower())
        {
            MessageBox.Show("¡Respuesta correcta! Haz clic en Siguiente para volver a la historia.");
            this.Close(); // Cierra el formulario de cálculo y regresa automáticamente a la historia
        }
        else
        {
            MessageBox.Show("Respuesta incorrecta. Revisa el enunciado e inténtalo de nuevo.");
        }
		}
		
		
		
	}
}
