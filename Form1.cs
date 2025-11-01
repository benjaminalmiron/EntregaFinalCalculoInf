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
        List<string> pasosActuales = new List<string>(); // Lista que almacenará los pasos de resolución.
        int indicePaso = 0;  // Contador que indica qué paso se va a mostrar a continuación


        // Método que se ejecuta cuando se carga el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            // Agregamos las opciones de expresiones al ComboBox
            cbExpresiones.Items.Add("x * e^x");
            cbExpresiones.Items.Add("x * sin(x)");
            cbExpresiones.Items.Add("ln(x) * x^2");
            cbExpresiones.Items.Add("e^x * cos(x)");
            cbExpresiones.SelectedIndex = 0; // Seleccionamos por defecto la primera expresión al iniciar
        }

        // El botón "Calcular" muestra la primera línea del paso a paso de la integral
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // Validamos que el usuario haya seleccionado una expresión en el ComboBox
            if (cbExpresiones.SelectedIndex == -1)
            {
                // Si no seleccionó nada, mostramos un mensaje de advertencia
                MessageBox.Show("Seleccione una expresion valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Generamos todos los pasos de resolución de la integral según la expresión seleccionada
            pasosActuales = GenerarPasosPara(cbExpresiones.SelectedIndex);
            indicePaso = 0; // Reiniciamos el contador de pasos para empezar desde el principio
            rtbSalida.Clear(); // Limpiamos el RichTextBox antes de mostrar la información

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
            if (cbExpresiones.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una expresion valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Verificamos si la lista de pasos está vacía o nula
            if ((pasosActuales == null || pasosActuales.Count == 0))
            {
                // Si está vacía, generamos los pasos de la expresión seleccionada
                pasosActuales = GenerarPasosPara(cbExpresiones.SelectedIndex);
                indicePaso = 0; // Reiniciamos el contador de pasos
                rtbSalida.Clear(); // Limpiamos el RichTextBox
            }
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


        // Método que genera los pasos según la expresión elegida
        List<string> GenerarPasosPara(int id)
        {
            var pasos = new List<string>(); // Creamos una lista para almacenar cada paso como un string

            // Seleccionamos la expresión según el índice recibido
            switch (id)
            {
                case 0: // x * e^x
                    pasos.Add("Expresión: ∫ x * e^x dx"); // Mostramos la integral original
                    pasos.Add("Elegimos: u = x → du = 1 dx"); // Selección de u y su derivada
                    pasos.Add("           dv = e^x dx → v = e^x"); // Selección de dv y su integral
                    pasos.Add("Aplicamos: ∫ u dv = u*v - ∫ v du"); // Aplicación de la fórmula por partes
                    pasos.Add("Resultado parcial: x*e^x - ∫ e^x*1 dx"); // Paso intermedio
                    pasos.Add("∫ e^x dx = e^x");  // Resolver la integral restante
                    pasos.Add("Resultado final: e^x * (x - 1) + C"); // Resultado final de la integral
                    break;

                case 1: // x * sin(x)
                    pasos.Add("Expresión: ∫ x * sin(x) dx");
                    pasos.Add("u = x → du = 1 dx");
                    pasos.Add("dv = sin(x) dx → v = -cos(x)");
                    pasos.Add("∫ x sin(x) dx = -x cos(x) + ∫ cos(x) dx");
                    pasos.Add("∫ cos(x) dx = sin(x)");
                    pasos.Add("Resultado final: -x cos(x) + sin(x) + C");
                    break;

                case 2: // ln(x) * x^2
                    pasos.Add("Expresión: ∫ ln(x) * x^2 dx (x>0)");
                    pasos.Add("u = ln(x) → du = (1/x) dx");
                    pasos.Add("dv = x^2 dx → v = x^3 / 3");
                    pasos.Add("∫ ln(x)*x^2 dx = (x^3/3) ln(x) - ∫ (x^3/3)*(1/x) dx");
                    pasos.Add("Simplificamos: ∫ (x^2/3) dx = x^3 / 9");
                    pasos.Add("Resultado final: (x^3/3) ln(x) - x^3/9 + C");
                    break;

                case 3: // e^x * cos(x)
                    pasos.Add("Expresión: ∫ e^x * cos(x) dx");
                    pasos.Add("1ª iteración: u = e^x → du = e^x dx, dv = cos(x) dx → v = sin(x)");
                    pasos.Add("∫ e^x cos(x) dx = e^x sin(x) - ∫ e^x sin(x) dx");
                    pasos.Add("2ª iteración: u = e^x → du = e^x dx, dv = sin(x) dx → v = -cos(x)");
                    pasos.Add("∫ e^x sin(x) dx = -e^x cos(x) + ∫ e^x cos(x) dx");
                    pasos.Add("Despejando: 2 ∫ e^x cos(x) dx = e^x (sin(x) + cos(x))");
                    pasos.Add("Resultado final: ∫ e^x cos(x) dx = (e^x / 2) * (sin(x) + cos(x)) + C");
                    break;

                default: // Caso por si el índice no coincide con ninguna expresión
                    pasos.Add("Expresión no definida.");
                    break;
            }
            return pasos; // Devolvemos la lista completa de pasos generados
        }
    }
}