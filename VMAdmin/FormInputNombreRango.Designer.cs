namespace VMAdmin
{
    partial class FormInputNombreRango
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles básicos
        private Label lblInstruccion;
        private Label lblRango;
        private TextBox txtNombre;
        private Button btnAceptar;
        private Button btnCancelar;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblInstruccion = new Label();
            lblRango = new Label();
            txtNombre = new TextBox();
            btnAceptar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblInstruccion
            // 
            lblInstruccion.AutoSize = true;
            lblInstruccion.Location = new Point(10, 10);
            lblInstruccion.Name = "lblInstruccion";
            lblInstruccion.Size = new Size(321, 20);
            lblInstruccion.TabIndex = 0;
            lblInstruccion.Text = "Ingrese un nombre descriptivo para este rango:";
            // 
            // lblRango
            // 
            lblRango.AutoSize = true;
            lblRango.Location = new Point(10, 40);
            lblRango.Name = "lblRango";
            lblRango.Size = new Size(0, 20);
            lblRango.TabIndex = 1;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(10, 63);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(331, 27);
            txtNombre.TabIndex = 2;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(141, 110);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(97, 40);
            btnAceptar.TabIndex = 3;
            btnAceptar.Text = "Guardar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(244, 110);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(97, 40);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            // 
            // FormInputNombreRango
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(346, 154);
            Controls.Add(lblInstruccion);
            Controls.Add(lblRango);
            Controls.Add(txtNombre);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormInputNombreRango";
            Text = "Nombre del Rango IP";
            Load += FormInputNombreRango_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}