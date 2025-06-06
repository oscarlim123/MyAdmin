namespace VMAdmin
{
    partial class FormUpload
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpload));
            listCopiaOK = new ListBox();
            label1 = new Label();
            listCopiaError = new ListBox();
            label2 = new Label();
            btnExaminar = new Button();
            textBoxArchivoACopiar = new TextBox();
            label3 = new Label();
            textBoxDestino = new TextBox();
            button2 = new Button();
            btnSubir = new Button();
            progressBar1 = new ProgressBar();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBoxUsuario = new TextBox();
            textBoxPassword = new TextBox();
            groupBox1 = new GroupBox();
            chkDescomprimir = new CheckBox();
            pictureBox1 = new PictureBox();
            chkCrearRuta = new CheckBox();
            groupBox2 = new GroupBox();
            lblResumen = new Label();
            openFileDialog1 = new OpenFileDialog();
            toolTip1 = new ToolTip(components);
            rButtonFicheros = new RadioButton();
            rButtonCarpeta = new RadioButton();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // listCopiaOK
            // 
            listCopiaOK.FormattingEnabled = true;
            listCopiaOK.HorizontalScrollbar = true;
            listCopiaOK.Location = new Point(12, 265);
            listCopiaOK.Name = "listCopiaOK";
            listCopiaOK.Size = new Size(353, 264);
            listCopiaOK.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 242);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 1;
            label1.Text = "Copiado en...";
            label1.Click += label1_Click;
            // 
            // listCopiaError
            // 
            listCopiaError.FormattingEnabled = true;
            listCopiaError.HorizontalScrollbar = true;
            listCopiaError.Location = new Point(371, 265);
            listCopiaError.Name = "listCopiaError";
            listCopiaError.Size = new Size(362, 264);
            listCopiaError.TabIndex = 2;
            listCopiaError.KeyDown += listCopiaError_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(371, 242);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 3;
            label2.Text = "Errores en...";
            label2.Click += label2_Click;
            // 
            // btnExaminar
            // 
            btnExaminar.Location = new Point(335, 112);
            btnExaminar.Name = "btnExaminar";
            btnExaminar.Size = new Size(35, 29);
            btnExaminar.TabIndex = 5;
            btnExaminar.Text = "...";
            btnExaminar.UseVisualStyleBackColor = true;
            btnExaminar.Click += btnExaminar_Click;
            // 
            // textBoxArchivoACopiar
            // 
            textBoxArchivoACopiar.Location = new Point(142, 113);
            textBoxArchivoACopiar.Name = "textBoxArchivoACopiar";
            textBoxArchivoACopiar.Size = new Size(193, 27);
            textBoxArchivoACopiar.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 153);
            label3.Name = "label3";
            label3.Size = new Size(92, 20);
            label3.TabIndex = 7;
            label3.Text = "Ruta destino";
            // 
            // textBoxDestino
            // 
            textBoxDestino.Location = new Point(142, 150);
            textBoxDestino.Name = "textBoxDestino";
            textBoxDestino.PlaceholderText = "C$\\Temp\\";
            textBoxDestino.Size = new Size(193, 27);
            textBoxDestino.TabIndex = 8;
            // 
            // button2
            // 
            button2.Location = new Point(656, 194);
            button2.Name = "button2";
            button2.Size = new Size(76, 45);
            button2.TabIndex = 9;
            button2.Text = "Salir";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btnSubir
            // 
            btnSubir.Image = (Image)resources.GetObject("btnSubir.Image");
            btnSubir.ImageAlign = ContentAlignment.MiddleLeft;
            btnSubir.Location = new Point(504, 194);
            btnSubir.Name = "btnSubir";
            btnSubir.Size = new Size(146, 45);
            btnSubir.TabIndex = 10;
            btnSubir.Text = "Iniciar copia";
            btnSubir.TextAlign = ContentAlignment.MiddleRight;
            btnSubir.UseVisualStyleBackColor = true;
            btnSubir.Click += btnSubir_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 535);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(720, 15);
            progressBar1.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 76);
            label4.Name = "label4";
            label4.Size = new Size(106, 20);
            label4.TabIndex = 12;
            label4.Text = "Archivo origen";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(41, 34);
            label5.Name = "label5";
            label5.Size = new Size(62, 20);
            label5.TabIndex = 13;
            label5.Text = "Usuario:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 70);
            label6.Name = "label6";
            label6.Size = new Size(86, 20);
            label6.TabIndex = 14;
            label6.Text = "Contraseña:";
            // 
            // textBoxUsuario
            // 
            textBoxUsuario.Location = new Point(105, 30);
            textBoxUsuario.Name = "textBoxUsuario";
            textBoxUsuario.PlaceholderText = "Administrator";
            textBoxUsuario.Size = new Size(228, 27);
            textBoxUsuario.TabIndex = 15;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(105, 67);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Size = new Size(228, 27);
            textBoxPassword.TabIndex = 16;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rButtonCarpeta);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(rButtonFicheros);
            groupBox1.Controls.Add(chkDescomprimir);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(chkCrearRuta);
            groupBox1.Location = new Point(10, 40);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(369, 199);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Origen";
            // 
            // chkDescomprimir
            // 
            chkDescomprimir.AutoSize = true;
            chkDescomprimir.Location = new Point(9, 168);
            chkDescomprimir.Name = "chkDescomprimir";
            chkDescomprimir.Size = new Size(250, 24);
            chkDescomprimir.TabIndex = 22;
            chkDescomprimir.Text = "Descomprimir después de copiar";
            chkDescomprimir.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(327, 109);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 28);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            toolTip1.SetToolTip(pictureBox1, "La ruta debe existir en el(los) equipos remotos.\r\n");
            // 
            // chkCrearRuta
            // 
            chkCrearRuta.AutoSize = true;
            chkCrearRuta.Enabled = false;
            chkCrearRuta.Location = new Point(9, 146);
            chkCrearRuta.Name = "chkCrearRuta";
            chkCrearRuta.Size = new Size(173, 24);
            chkCrearRuta.TabIndex = 20;
            chkCrearRuta.Text = "Crear ruta si no existe";
            chkCrearRuta.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxPassword);
            groupBox2.Controls.Add(textBoxUsuario);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(388, 40);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(345, 106);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            groupBox2.Text = "Credenciales";
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Location = new Point(12, 9);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(50, 20);
            lblResumen.TabIndex = 19;
            lblResumen.Text = "label7";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // rButtonFicheros
            // 
            rButtonFicheros.AutoSize = true;
            rButtonFicheros.Checked = true;
            rButtonFicheros.Location = new Point(10, 30);
            rButtonFicheros.Name = "rButtonFicheros";
            rButtonFicheros.Size = new Size(84, 24);
            rButtonFicheros.TabIndex = 23;
            rButtonFicheros.TabStop = true;
            rButtonFicheros.Text = "Ficheros";
            rButtonFicheros.UseVisualStyleBackColor = true;
            // 
            // rButtonCarpeta
            // 
            rButtonCarpeta.AutoSize = true;
            rButtonCarpeta.Location = new Point(171, 30);
            rButtonCarpeta.Name = "rButtonCarpeta";
            rButtonCarpeta.Size = new Size(88, 24);
            rButtonCarpeta.TabIndex = 24;
            rButtonCarpeta.Text = "Carpetas";
            rButtonCarpeta.UseVisualStyleBackColor = true;
            // 
            // FormUpload
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(744, 559);
            Controls.Add(lblResumen);
            Controls.Add(progressBar1);
            Controls.Add(btnSubir);
            Controls.Add(button2);
            Controls.Add(textBoxDestino);
            Controls.Add(label3);
            Controls.Add(textBoxArchivoACopiar);
            Controls.Add(btnExaminar);
            Controls.Add(label2);
            Controls.Add(listCopiaError);
            Controls.Add(label1);
            Controls.Add(listCopiaOK);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "FormUpload";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormUpload";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listCopiaOK;
        private Label label1;
        private ListBox listCopiaError;
        private Label label2;
        private Button btnExaminar;
        private TextBox textBoxArchivoACopiar;
        private Label label3;
        private TextBox textBoxDestino;
        private Button button2;
        private Button btnSubir;
        private ProgressBar progressBar1;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBoxUsuario;
        private TextBox textBoxPassword;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblResumen;
        private OpenFileDialog openFileDialog1;
        private CheckBox chkCrearRuta;
        private PictureBox pictureBox1;
        private ToolTip toolTip1;
        private CheckBox chkDescomprimir;
        private RadioButton rButtonCarpeta;
        private RadioButton rButtonFicheros;
    }
}