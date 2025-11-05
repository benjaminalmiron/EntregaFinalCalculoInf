namespace EntregaFinalCalculoInf
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbExpresiones = new ComboBox();
            btnCalcular = new Button();
            btnSiguientePaso = new Button();
            rtbSalida = new RichTextBox();
            btnReset = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // cbExpresiones
            // 
            cbExpresiones.FormattingEnabled = true;
            cbExpresiones.Location = new Point(25, 95);
            cbExpresiones.Margin = new Padding(3, 4, 3, 4);
            cbExpresiones.Name = "cbExpresiones";
            cbExpresiones.Size = new Size(396, 28);
            cbExpresiones.TabIndex = 0;
            cbExpresiones.SelectedIndexChanged += cbExpresiones_SelectedIndexChanged;
            // 
            // btnCalcular
            // 
            btnCalcular.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(25, 170);
            btnCalcular.Margin = new Padding(3, 4, 3, 4);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(86, 31);
            btnCalcular.TabIndex = 1;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnSiguientePaso
            // 
            btnSiguientePaso.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSiguientePaso.Location = new Point(25, 236);
            btnSiguientePaso.Margin = new Padding(3, 4, 3, 4);
            btnSiguientePaso.Name = "btnSiguientePaso";
            btnSiguientePaso.Size = new Size(86, 31);
            btnSiguientePaso.TabIndex = 2;
            btnSiguientePaso.Text = "Siguiente paso";
            btnSiguientePaso.UseVisualStyleBackColor = true;
            btnSiguientePaso.Click += btnSiguientePaso_Click;
            // 
            // rtbSalida
            // 
            rtbSalida.Location = new Point(459, 64);
            rtbSalida.Margin = new Padding(3, 4, 3, 4);
            rtbSalida.Name = "rtbSalida";
            rtbSalida.Size = new Size(520, 523);
            rtbSalida.TabIndex = 3;
            rtbSalida.Text = "";
            // 
            // btnReset
            // 
            btnReset.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReset.Location = new Point(25, 306);
            btnReset.Margin = new Padding(3, 4, 3, 4);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(86, 31);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reiniciar";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(25, 30);
            label1.Name = "label1";
            label1.Size = new Size(552, 18);
            label1.TabIndex = 5;
            label1.Text = "Seleccione una expresion para realizar el paso a paso de la misma.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(1007, 600);
            Controls.Add(label1);
            Controls.Add(btnReset);
            Controls.Add(rtbSalida);
            Controls.Add(btnSiguientePaso);
            Controls.Add(btnCalcular);
            Controls.Add(cbExpresiones);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbExpresiones;
        private Button btnCalcular;
        private Button btnSiguientePaso;
        private RichTextBox rtbSalida;
        private Button btnReset;
        private Label label1;
    }
}
