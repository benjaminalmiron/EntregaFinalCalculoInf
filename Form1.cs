using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace EntregaFinalCalculoInf
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load; // LLama el evento Load del formulario al método Form1_Load
        }

        // Campos de estado(variables que guardan información durante la ejecución)
        List<string> pasosActuales = new List<string>();    // Lista que almacenará los pasos de resolución.
        int indicePaso = 0;                                 // Contador que indica qué paso se va a mostrar a continuación


        // Método que se ejecuta cuando se carga el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            // Agregamos las opciones de expresiones al ComboBox
            cbExpresiones.Items.Add("x * e^x");
            cbExpresiones.Items.Add("x * sin(x)");
            cbExpresiones.Items.Add("sin(x) * x");
            cbExpresiones.Items.Add("cos(x) * (2x + 5)");
            cbExpresiones.Items.Add("ln(x) * x^2");
            cbExpresiones.Items.Add("e^x * cos(x)");
            cbExpresiones.Items.Add("cos(x)");
            cbExpresiones.Items.Add("Esta no es una funcion");
            cbExpresiones.Items.Add("");

            // Seleccionamos por defecto la primera expresión al iniciar
            cbExpresiones.SelectedIndex = 0;
        }

        // El botón "Calcular" muestra la primera línea del paso a paso de la resolucion de la integral
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Analizador analizador = new Analizador(cbExpresiones.SelectedItem.ToString());

            // Obtenemos todos los pasos de resolución de la integral según la expresión seleccionada
            pasosActuales = analizador.getPasos();

            indicePaso = 0;     // Reiniciamos el contador de pasos para empezar desde el principio
            rtbSalida.Clear();  // Limpiamos el RichTextBox antes de mostrar la información

            // Mostramos la primera línea (la expresión) si hay pasos generados
            if (pasosActuales.Count > 0)
            {
                // Mostramos solo la primera línea (la expresión)
                rtbSalida.AppendText(pasosActuales[0] + "\n\n");
                indicePaso = 1; // Configuramos el índice para que el siguiente paso que se muestre sea el segundo
            }
        }

        // Botón "Siguiente Paso" muestra los pasos de la integral uno por uno
        private void btnSiguientePaso_Click(object sender, EventArgs e)
        {
            // Mostramos el siguiente paso solo si hay más pasos por mostrar
            if (indicePaso < pasosActuales.Count)
            {
                // Agregamos el paso actual al RichTextBox
                rtbSalida.AppendText(pasosActuales[indicePaso] + "\n\n");
                indicePaso++; // Incrementamos el contador para el próximo paso
            }
            else
            {
                // Si ya se mostraron todos los pasos, informamos al usuario
                MessageBox.Show("No hay mas pasos para mostrar.", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Botón "Reset" limpia todo para reiniciar la visualización
        private void btnReset_Click(object sender, EventArgs e)
        {
            pasosActuales.Clear(); // Vaciamos la lista de pasos de la integral
            indicePaso = 0;
            rtbSalida.Clear();
        }

        private void cbExpresiones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}