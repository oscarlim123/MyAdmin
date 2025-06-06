namespace VMAdmin
{
    partial class FormSeleccionRangoIP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSeleccionRangoIP));
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            grid = new DataGridView();
            txtBusqueda = new TextBox();
            btnSeleccionar = new Button();
            panel = new Panel();
            btnBorrar = new Button();
            btnCerrar = new Button();
            Nombre = new DataGridViewTextBoxColumn();
            IPInicio = new DataGridViewTextBoxColumn();
            IPFin = new DataGridViewTextBoxColumn();
            FechaCreacion = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.BackgroundColor = SystemColors.Window;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Columns.AddRange(new DataGridViewColumn[] { Nombre, IPInicio, IPFin, FechaCreacion });
            grid.Location = new Point(3, 54);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.RowHeadersWidth = 51;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(770, 297);
            grid.TabIndex = 0;
            grid.CellEndEdit += grid_CellEndEdit;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Font = new Font("Segoe UI", 10F);
            txtBusqueda.Location = new Point(3, 3);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.PlaceholderText = "Buscar por nombre o IP...";
            txtBusqueda.Size = new Size(299, 30);
            txtBusqueda.TabIndex = 1;
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.DialogResult = DialogResult.OK;
            btnSeleccionar.Image = (Image)resources.GetObject("btnSeleccionar.Image");
            btnSeleccionar.ImageAlign = ContentAlignment.MiddleLeft;
            btnSeleccionar.Location = new Point(541, 3);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.Padding = new Padding(3, 0, 0, 0);
            btnSeleccionar.Size = new Size(113, 40);
            btnSeleccionar.TabIndex = 2;
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.TextAlign = ContentAlignment.MiddleRight;
            btnSeleccionar.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            panel.Controls.Add(btnBorrar);
            panel.Controls.Add(txtBusqueda);
            panel.Controls.Add(btnSeleccionar);
            panel.Controls.Add(grid);
            panel.Location = new Point(12, 12);
            panel.Name = "panel";
            panel.Size = new Size(776, 354);
            panel.TabIndex = 3;
            panel.Paint += panel_Paint;
            // 
            // btnBorrar
            // 
            btnBorrar.Image = (Image)resources.GetObject("btnBorrar.Image");
            btnBorrar.ImageAlign = ContentAlignment.MiddleLeft;
            btnBorrar.Location = new Point(660, 3);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(113, 40);
            btnBorrar.TabIndex = 4;
            btnBorrar.Text = "Borrar";
            btnBorrar.UseVisualStyleBackColor = true;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Image = (Image)resources.GetObject("btnCerrar.Image");
            btnCerrar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCerrar.Location = new Point(672, 372);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Padding = new Padding(5, 0, 0, 0);
            btnCerrar.Size = new Size(113, 40);
            btnCerrar.TabIndex = 5;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // Nombre
            // 
            Nombre.DataPropertyName = "Nombre";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Nombre.DefaultCellStyle = dataGridViewCellStyle5;
            Nombre.HeaderText = "NOMBRE";
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.Width = 150;
            // 
            // IPInicio
            // 
            IPInicio.DataPropertyName = "IPInicio";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            IPInicio.DefaultCellStyle = dataGridViewCellStyle6;
            IPInicio.HeaderText = "IP INICIO";
            IPInicio.MinimumWidth = 6;
            IPInicio.Name = "IPInicio";
            IPInicio.ReadOnly = true;
            IPInicio.Width = 130;
            // 
            // IPFin
            // 
            IPFin.DataPropertyName = "IPFin";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            IPFin.DefaultCellStyle = dataGridViewCellStyle7;
            IPFin.HeaderText = "IP FIN";
            IPFin.MinimumWidth = 6;
            IPFin.Name = "IPFin";
            IPFin.ReadOnly = true;
            IPFin.Width = 130;
            // 
            // FechaCreacion
            // 
            FechaCreacion.DataPropertyName = "FechaCreacion";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.Format = "dd/mm/yyyy";
            FechaCreacion.DefaultCellStyle = dataGridViewCellStyle8;
            FechaCreacion.HeaderText = "CREACIÓN";
            FechaCreacion.MinimumWidth = 6;
            FechaCreacion.Name = "FechaCreacion";
            FechaCreacion.ReadOnly = true;
            FechaCreacion.Width = 120;
            // 
            // FormSeleccionRangoIP
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 417);
            Controls.Add(btnCerrar);
            Controls.Add(panel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FormSeleccionRangoIP";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Rangos de IPs";
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grid;
        private TextBox txtBusqueda;
        private Button btnSeleccionar;
        private Panel panel;
        private Button btnBorrar;
        private Button btnCerrar;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn IPInicio;
        private DataGridViewTextBoxColumn IPFin;
        private DataGridViewTextBoxColumn FechaCreacion;
    }
}