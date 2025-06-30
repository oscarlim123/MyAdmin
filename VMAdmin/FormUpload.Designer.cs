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
            label3 = new Label();
            textBoxDestino = new TextBox();
            button2 = new Button();
            btnSubir = new Button();
            progressBar1 = new ProgressBar();
            label5 = new Label();
            label6 = new Label();
            textBoxUsuario = new TextBox();
            textBoxPassword = new TextBox();
            groupBox1 = new GroupBox();
            btnLimpiarLista = new Button();
            btnEliminarSeleccionado = new Button();
            dataGridArchivosOrigen = new DataGridView();
            ColTipo = new DataGridViewTextBoxColumn();
            ColNombre = new DataGridViewTextBoxColumn();
            ColRutaCompleta = new DataGridViewTextBoxColumn();
            ColTamano = new DataGridViewTextBoxColumn();
            ColFechaModificacion = new DataGridViewTextBoxColumn();
            rbCarpetas = new RadioButton();
            rbArchivos = new RadioButton();
            chkDescomprimir = new CheckBox();
            groupBox2 = new GroupBox();
            lblResumen = new Label();
            openFileDialog1 = new OpenFileDialog();
            toolTip1 = new ToolTip(components);
            progressBar2 = new ProgressBar();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridArchivosOrigen).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // listCopiaOK
            // 
            listCopiaOK.FormattingEnabled = true;
            listCopiaOK.HorizontalScrollbar = true;
            listCopiaOK.Location = new Point(12, 561);
            listCopiaOK.Name = "listCopiaOK";
            listCopiaOK.Size = new Size(353, 184);
            listCopiaOK.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 538);
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
            listCopiaError.Location = new Point(371, 561);
            listCopiaError.Name = "listCopiaError";
            listCopiaError.Size = new Size(362, 184);
            listCopiaError.TabIndex = 2;
            listCopiaError.KeyDown += listCopiaError_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(371, 538);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 3;
            label2.Text = "Errores en...";
            label2.Click += label2_Click;
            // 
            // btnExaminar
            // 
            btnExaminar.Image = (Image)resources.GetObject("btnExaminar.Image");
            btnExaminar.ImageAlign = ContentAlignment.MiddleLeft;
            btnExaminar.Location = new Point(10, 136);
            btnExaminar.Name = "btnExaminar";
            btnExaminar.Padding = new Padding(5, 0, 0, 0);
            btnExaminar.Size = new Size(146, 41);
            btnExaminar.TabIndex = 5;
            btnExaminar.Text = "Seleccionar...";
            btnExaminar.TextAlign = ContentAlignment.MiddleRight;
            toolTip1.SetToolTip(btnExaminar, "Seleccionar archivos a copiar");
            btnExaminar.UseVisualStyleBackColor = true;
            btnExaminar.Click += btnExaminar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 446);
            label3.Name = "label3";
            label3.Size = new Size(92, 20);
            label3.TabIndex = 7;
            label3.Text = "Ruta destino";
            // 
            // textBoxDestino
            // 
            textBoxDestino.Location = new Point(106, 443);
            textBoxDestino.Name = "textBoxDestino";
            textBoxDestino.PlaceholderText = "C$\\Temp\\";
            textBoxDestino.Size = new Size(621, 27);
            textBoxDestino.TabIndex = 8;
            // 
            // button2
            // 
            button2.Location = new Point(657, 14);
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
            btnSubir.Location = new Point(505, 14);
            btnSubir.Name = "btnSubir";
            btnSubir.Padding = new Padding(5, 0, 0, 0);
            btnSubir.Size = new Size(146, 45);
            btnSubir.TabIndex = 10;
            btnSubir.Text = "Iniciar copia";
            btnSubir.TextAlign = ContentAlignment.MiddleRight;
            toolTip1.SetToolTip(btnSubir, "Iniciar copia de archivos seleccionados hacia estaciones remotas");
            btnSubir.UseVisualStyleBackColor = true;
            btnSubir.Click += btnSubir_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 485);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(715, 15);
            progressBar1.TabIndex = 11;
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
            groupBox1.Controls.Add(btnLimpiarLista);
            groupBox1.Controls.Add(btnEliminarSeleccionado);
            groupBox1.Controls.Add(dataGridArchivosOrigen);
            groupBox1.Controls.Add(rbCarpetas);
            groupBox1.Controls.Add(rbArchivos);
            groupBox1.Controls.Add(chkDescomprimir);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(btnExaminar);
            groupBox1.Location = new Point(10, 65);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(723, 371);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Origen";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // btnLimpiarLista
            // 
            btnLimpiarLista.Image = (Image)resources.GetObject("btnLimpiarLista.Image");
            btnLimpiarLista.Location = new Point(663, 136);
            btnLimpiarLista.Name = "btnLimpiarLista";
            btnLimpiarLista.Size = new Size(50, 41);
            btnLimpiarLista.TabIndex = 26;
            toolTip1.SetToolTip(btnLimpiarLista, "Borrar todo");
            btnLimpiarLista.UseVisualStyleBackColor = true;
            btnLimpiarLista.Click += btnLimpiarLista_Click;
            // 
            // btnEliminarSeleccionado
            // 
            btnEliminarSeleccionado.Image = (Image)resources.GetObject("btnEliminarSeleccionado.Image");
            btnEliminarSeleccionado.Location = new Point(607, 136);
            btnEliminarSeleccionado.Name = "btnEliminarSeleccionado";
            btnEliminarSeleccionado.Size = new Size(50, 41);
            btnEliminarSeleccionado.TabIndex = 25;
            toolTip1.SetToolTip(btnEliminarSeleccionado, "Eliminar seleccionado");
            btnEliminarSeleccionado.UseVisualStyleBackColor = true;
            btnEliminarSeleccionado.Click += btnEliminarSeleccionado_Click;
            // 
            // dataGridArchivosOrigen
            // 
            dataGridArchivosOrigen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridArchivosOrigen.Columns.AddRange(new DataGridViewColumn[] { ColTipo, ColNombre, ColRutaCompleta, ColTamano, ColFechaModificacion });
            dataGridArchivosOrigen.Location = new Point(6, 183);
            dataGridArchivosOrigen.Name = "dataGridArchivosOrigen";
            dataGridArchivosOrigen.RowHeadersWidth = 51;
            dataGridArchivosOrigen.Size = new Size(711, 182);
            dataGridArchivosOrigen.TabIndex = 20;
            // 
            // ColTipo
            // 
            ColTipo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ColTipo.DataPropertyName = "Tipo";
            ColTipo.HeaderText = "Tipo";
            ColTipo.MinimumWidth = 6;
            ColTipo.Name = "ColTipo";
            ColTipo.ReadOnly = true;
            ColTipo.Width = 68;
            // 
            // ColNombre
            // 
            ColNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ColNombre.DataPropertyName = "Nombre";
            ColNombre.HeaderText = "Nombre";
            ColNombre.MinimumWidth = 6;
            ColNombre.Name = "ColNombre";
            ColNombre.ReadOnly = true;
            ColNombre.Width = 93;
            // 
            // ColRutaCompleta
            // 
            ColRutaCompleta.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColRutaCompleta.DataPropertyName = "RutaCompleta";
            ColRutaCompleta.HeaderText = "Ruta Completa";
            ColRutaCompleta.MinimumWidth = 6;
            ColRutaCompleta.Name = "ColRutaCompleta";
            ColRutaCompleta.ReadOnly = true;
            // 
            // ColTamano
            // 
            ColTamano.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ColTamano.DataPropertyName = "Tamano";
            ColTamano.HeaderText = "Tamaño";
            ColTamano.MinimumWidth = 6;
            ColTamano.Name = "ColTamano";
            ColTamano.ReadOnly = true;
            ColTamano.Width = 90;
            // 
            // ColFechaModificacion
            // 
            ColFechaModificacion.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ColFechaModificacion.DataPropertyName = "FechaModificacion";
            ColFechaModificacion.HeaderText = "Fecha Modificación";
            ColFechaModificacion.MinimumWidth = 6;
            ColFechaModificacion.Name = "ColFechaModificacion";
            ColFechaModificacion.ReadOnly = true;
            ColFechaModificacion.Width = 153;
            // 
            // rbCarpetas
            // 
            rbCarpetas.AutoSize = true;
            rbCarpetas.Location = new Point(171, 30);
            rbCarpetas.Name = "rbCarpetas";
            rbCarpetas.Size = new Size(88, 24);
            rbCarpetas.TabIndex = 24;
            rbCarpetas.Text = "Carpetas";
            rbCarpetas.UseVisualStyleBackColor = true;
            rbCarpetas.CheckedChanged += rbCarpetas_CheckedChanged;
            // 
            // rbArchivos
            // 
            rbArchivos.AutoSize = true;
            rbArchivos.Checked = true;
            rbArchivos.Location = new Point(10, 30);
            rbArchivos.Name = "rbArchivos";
            rbArchivos.Size = new Size(86, 24);
            rbArchivos.TabIndex = 23;
            rbArchivos.TabStop = true;
            rbArchivos.Text = "Archivos";
            rbArchivos.UseVisualStyleBackColor = true;
            rbArchivos.CheckedChanged += rbArchivos_CheckedChanged;
            // 
            // chkDescomprimir
            // 
            chkDescomprimir.AutoSize = true;
            chkDescomprimir.Location = new Point(10, 64);
            chkDescomprimir.Name = "chkDescomprimir";
            chkDescomprimir.Size = new Size(250, 24);
            chkDescomprimir.TabIndex = 22;
            chkDescomprimir.Text = "Descomprimir después de copiar";
            chkDescomprimir.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxPassword);
            groupBox2.Controls.Add(textBoxUsuario);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(368, 18);
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
            // progressBar2
            // 
            progressBar2.Location = new Point(12, 506);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(715, 15);
            progressBar2.TabIndex = 20;
            // 
            // FormUpload
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(741, 756);
            Controls.Add(progressBar2);
            Controls.Add(lblResumen);
            Controls.Add(progressBar1);
            Controls.Add(textBoxDestino);
            Controls.Add(btnSubir);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(listCopiaError);
            Controls.Add(label1);
            Controls.Add(listCopiaOK);
            Controls.Add(groupBox1);
            Name = "FormUpload";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormUpload";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridArchivosOrigen).EndInit();
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
        private Label label3;
        private TextBox textBoxDestino;
        private Button button2;
        private Button btnSubir;
        private ProgressBar progressBar1;
        private Label label5;
        private Label label6;
        private TextBox textBoxUsuario;
        private TextBox textBoxPassword;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblResumen;
        private OpenFileDialog openFileDialog1;
        private ToolTip toolTip1;
        private CheckBox chkDescomprimir;
        private RadioButton rbCarpetas;
        private RadioButton rbArchivos;
        private DataGridView dataGridArchivosOrigen;
        private DataGridViewTextBoxColumn ColTipo;
        private DataGridViewTextBoxColumn ColNombre;
        private DataGridViewTextBoxColumn ColRutaCompleta;
        private DataGridViewTextBoxColumn ColTamano;
        private DataGridViewTextBoxColumn ColFechaModificacion;
        private Button btnLimpiarLista;
        private Button btnEliminarSeleccionado;
        private ProgressBar progressBar2;
    }
}