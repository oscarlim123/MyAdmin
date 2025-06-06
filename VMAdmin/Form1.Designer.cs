namespace VMAdmin
{
    partial class FrmVMAdmin
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVMAdmin));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            TextBoxInicio = new MaskedTextBox();
            TextBoxFin = new MaskedTextBox();
            groupBox1 = new GroupBox();
            btnSubir = new Button();
            btnSSH = new Button();
            btnAgregarRango = new Button();
            btnSeleccionarRango = new Button();
            btnSettings = new Button();
            btnCancelar = new Button();
            btnScan = new Button();
            label2 = new Label();
            label1 = new Label();
            dataGridIPs = new DataGridView();
            IP = new DataGridViewTextBoxColumn();
            HOST = new DataGridViewTextBoxColumn();
            SO = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewTextBoxColumn();
            RDP = new DataGridViewTextBoxColumn();
            SSH = new DataGridViewTextBoxColumn();
            TELNET = new DataGridViewTextBoxColumn();
            HTTP = new DataGridViewTextBoxColumn();
            HTTPS = new DataGridViewTextBoxColumn();
            SMB = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            escritorioRemotoToolStripMenuItem = new ToolStripMenuItem();
            sSHToolStripMenuItem = new ToolStripMenuItem();
            telnetToolStripMenuItem = new ToolStripMenuItem();
            hTTPToolStripMenuItem = new ToolStripMenuItem();
            httpsToolStripMenuItem1 = new ToolStripMenuItem();
            abrirCToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            progressBarScan = new ProgressBar();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            labelTelnet = new Label();
            label12 = new Label();
            labelHTTPS = new Label();
            label11 = new Label();
            labelHTTP = new Label();
            label10 = new Label();
            label7 = new Label();
            labelSSH = new Label();
            label6 = new Label();
            labelRDP = new Label();
            label5 = new Label();
            labelActivas = new Label();
            label4 = new Label();
            labelTotal = new Label();
            label3 = new Label();
            panel1 = new Panel();
            tabPage2 = new TabPage();
            label9 = new Label();
            label8 = new Label();
            checkBoxSwitch = new CheckBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridIPs).BeginInit();
            contextMenuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // TextBoxInicio
            // 
            TextBoxInicio.Location = new Point(75, 20);
            TextBoxInicio.Name = "TextBoxInicio";
            TextBoxInicio.Size = new Size(157, 27);
            TextBoxInicio.TabIndex = 0;
            TextBoxInicio.MaskInputRejected += TextBoxInicio_MaskInputRejected;
            TextBoxInicio.TextChanged += TextBoxInicio_TextChanged;
            TextBoxInicio.KeyPress += TextBoxInicio_KeyPress;
            // 
            // TextBoxFin
            // 
            TextBoxFin.Location = new Point(75, 57);
            TextBoxFin.Name = "TextBoxFin";
            TextBoxFin.Size = new Size(157, 27);
            TextBoxFin.TabIndex = 1;
            TextBoxFin.MouseClick += TextBoxFin_MouseClick;
            TextBoxFin.Enter += TextBoxFin_Enter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnSubir);
            groupBox1.Controls.Add(btnSSH);
            groupBox1.Controls.Add(btnAgregarRango);
            groupBox1.Controls.Add(btnSeleccionarRango);
            groupBox1.Controls.Add(btnSettings);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(btnScan);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(TextBoxFin);
            groupBox1.Controls.Add(TextBoxInicio);
            groupBox1.Location = new Point(3, -2);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1298, 93);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            // 
            // btnSubir
            // 
            btnSubir.Image = (Image)resources.GetObject("btnSubir.Image");
            btnSubir.ImageAlign = ContentAlignment.TopCenter;
            btnSubir.Location = new Point(1040, 19);
            btnSubir.Name = "btnSubir";
            btnSubir.Size = new Size(80, 64);
            btnSubir.TabIndex = 9;
            btnSubir.Text = "Copiar";
            btnSubir.TextAlign = ContentAlignment.BottomCenter;
            btnSubir.UseVisualStyleBackColor = true;
            btnSubir.Click += btnSubir_Click;
            // 
            // btnSSH
            // 
            btnSSH.Enabled = false;
            btnSSH.Image = (Image)resources.GetObject("btnSSH.Image");
            btnSSH.Location = new Point(1212, 19);
            btnSSH.Name = "btnSSH";
            btnSSH.Size = new Size(80, 64);
            btnSSH.TabIndex = 8;
            btnSSH.UseVisualStyleBackColor = true;
            // 
            // btnAgregarRango
            // 
            btnAgregarRango.Location = new Point(238, 58);
            btnAgregarRango.Name = "btnAgregarRango";
            btnAgregarRango.Size = new Size(36, 25);
            btnAgregarRango.TabIndex = 11;
            btnAgregarRango.Text = "+";
            btnAgregarRango.UseVisualStyleBackColor = true;
            btnAgregarRango.Click += btnAgregarRango_Click;
            // 
            // btnSeleccionarRango
            // 
            btnSeleccionarRango.Location = new Point(236, 20);
            btnSeleccionarRango.Name = "btnSeleccionarRango";
            btnSeleccionarRango.Size = new Size(38, 26);
            btnSeleccionarRango.TabIndex = 10;
            btnSeleccionarRango.Text = "...";
            btnSeleccionarRango.UseVisualStyleBackColor = true;
            btnSeleccionarRango.Click += btnSeleccionarRango_Click;
            // 
            // btnSettings
            // 
            btnSettings.Image = (Image)resources.GetObject("btnSettings.Image");
            btnSettings.ImageAlign = ContentAlignment.TopCenter;
            btnSettings.Location = new Point(1126, 19);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(80, 63);
            btnSettings.TabIndex = 7;
            btnSettings.Text = "Config";
            btnSettings.TextAlign = ContentAlignment.BottomCenter;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Image = (Image)resources.GetObject("btnCancelar.Image");
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(405, 20);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(5, 0, 0, 0);
            btnCancelar.Size = new Size(109, 64);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnScan
            // 
            btnScan.Image = (Image)resources.GetObject("btnScan.Image");
            btnScan.ImageAlign = ContentAlignment.MiddleLeft;
            btnScan.Location = new Point(290, 20);
            btnScan.Name = "btnScan";
            btnScan.Padding = new Padding(5, 0, 0, 0);
            btnScan.Size = new Size(109, 64);
            btnScan.TabIndex = 4;
            btnScan.Text = "Iniciar";
            btnScan.UseVisualStyleBackColor = true;
            btnScan.Click += btnScan_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 60);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 3;
            label2.Text = "Hasta:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 23);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 2;
            label1.Text = "Desde:";
            // 
            // dataGridIPs
            // 
            dataGridIPs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridIPs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridIPs.Columns.AddRange(new DataGridViewColumn[] { IP, HOST, SO, Estado, RDP, SSH, TELNET, HTTP, HTTPS, SMB });
            dataGridIPs.ContextMenuStrip = contextMenuStrip1;
            dataGridIPs.Location = new Point(241, 97);
            dataGridIPs.Name = "dataGridIPs";
            dataGridIPs.RowHeadersWidth = 51;
            dataGridIPs.Size = new Size(1061, 461);
            dataGridIPs.TabIndex = 3;
            dataGridIPs.CellMouseDown += dataGridIPs_CellMouseDown;
            dataGridIPs.MouseDown += dataGridIPs_MouseDown;
            // 
            // IP
            // 
            IP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            IP.FillWeight = 45.7018F;
            IP.HeaderText = "IP";
            IP.MinimumWidth = 6;
            IP.Name = "IP";
            IP.Width = 150;
            // 
            // HOST
            // 
            HOST.FillWeight = 121.672142F;
            HOST.HeaderText = "NOMBRE";
            HOST.MinimumWidth = 20;
            HOST.Name = "HOST";
            // 
            // SO
            // 
            SO.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            SO.HeaderText = "SO";
            SO.MinimumWidth = 20;
            SO.Name = "SO";
            SO.Width = 50;
            // 
            // Estado
            // 
            Estado.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Estado.FillWeight = 305.34787F;
            Estado.HeaderText = "ESTADO";
            Estado.MinimumWidth = 6;
            Estado.Name = "Estado";
            Estado.Width = 80;
            // 
            // RDP
            // 
            RDP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RDP.DefaultCellStyle = dataGridViewCellStyle1;
            RDP.FillWeight = 19.6735573F;
            RDP.HeaderText = "RDP";
            RDP.MinimumWidth = 20;
            RDP.Name = "RDP";
            RDP.Width = 60;
            // 
            // SSH
            // 
            SSH.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            SSH.DefaultCellStyle = dataGridViewCellStyle2;
            SSH.FillWeight = 7.60451126F;
            SSH.HeaderText = "SSH";
            SSH.MinimumWidth = 20;
            SSH.Name = "SSH";
            SSH.Width = 60;
            // 
            // TELNET
            // 
            TELNET.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TELNET.DefaultCellStyle = dataGridViewCellStyle3;
            TELNET.HeaderText = "Telnet(21)";
            TELNET.MinimumWidth = 20;
            TELNET.Name = "TELNET";
            TELNET.Width = 125;
            // 
            // HTTP
            // 
            HTTP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            HTTP.DefaultCellStyle = dataGridViewCellStyle4;
            HTTP.HeaderText = "Http(80)";
            HTTP.MinimumWidth = 20;
            HTTP.Name = "HTTP";
            HTTP.Width = 125;
            // 
            // HTTPS
            // 
            HTTPS.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            HTTPS.DefaultCellStyle = dataGridViewCellStyle5;
            HTTPS.HeaderText = "Https(443)";
            HTTPS.MinimumWidth = 20;
            HTTPS.Name = "HTTPS";
            HTTPS.Width = 125;
            // 
            // SMB
            // 
            SMB.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            SMB.HeaderText = "SMB";
            SMB.MinimumWidth = 30;
            SMB.Name = "SMB";
            SMB.Width = 40;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { escritorioRemotoToolStripMenuItem, sSHToolStripMenuItem, telnetToolStripMenuItem, hTTPToolStripMenuItem, httpsToolStripMenuItem1, abrirCToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(194, 148);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // escritorioRemotoToolStripMenuItem
            // 
            escritorioRemotoToolStripMenuItem.Name = "escritorioRemotoToolStripMenuItem";
            escritorioRemotoToolStripMenuItem.Size = new Size(193, 24);
            escritorioRemotoToolStripMenuItem.Text = "Escritorio remoto";
            escritorioRemotoToolStripMenuItem.Click += escritorioRemotoToolStripMenuItem_Click;
            // 
            // sSHToolStripMenuItem
            // 
            sSHToolStripMenuItem.Name = "sSHToolStripMenuItem";
            sSHToolStripMenuItem.Size = new Size(193, 24);
            sSHToolStripMenuItem.Text = "SSH";
            sSHToolStripMenuItem.Click += sSHToolStripMenuItem_Click;
            // 
            // telnetToolStripMenuItem
            // 
            telnetToolStripMenuItem.Name = "telnetToolStripMenuItem";
            telnetToolStripMenuItem.Size = new Size(193, 24);
            telnetToolStripMenuItem.Text = "Telnet";
            telnetToolStripMenuItem.Click += telnetToolStripMenuItem_Click;
            telnetToolStripMenuItem.MouseHover += telnetToolStripMenuItem_MouseHover;
            // 
            // hTTPToolStripMenuItem
            // 
            hTTPToolStripMenuItem.Name = "hTTPToolStripMenuItem";
            hTTPToolStripMenuItem.Size = new Size(193, 24);
            hTTPToolStripMenuItem.Text = "Http";
            hTTPToolStripMenuItem.Click += hTTPToolStripMenuItem_Click;
            hTTPToolStripMenuItem.MouseHover += hTTPToolStripMenuItem_MouseHover;
            // 
            // httpsToolStripMenuItem1
            // 
            httpsToolStripMenuItem1.Name = "httpsToolStripMenuItem1";
            httpsToolStripMenuItem1.Size = new Size(193, 24);
            httpsToolStripMenuItem1.Text = "Https";
            httpsToolStripMenuItem1.Click += toolStripMenuItem1_Click;
            httpsToolStripMenuItem1.MouseHover += httpsToolStripMenuItem1_MouseHover;
            // 
            // abrirCToolStripMenuItem
            // 
            abrirCToolStripMenuItem.Name = "abrirCToolStripMenuItem";
            abrirCToolStripMenuItem.Size = new Size(193, 24);
            abrirCToolStripMenuItem.Text = "Abrir C$";
            abrirCToolStripMenuItem.Click += abrirCToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 591);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1314, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // progressBarScan
            // 
            progressBarScan.Location = new Point(241, 564);
            progressBarScan.Name = "progressBarScan";
            progressBarScan.Size = new Size(1060, 24);
            progressBarScan.Step = 1;
            progressBarScan.TabIndex = 4;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(3, 97);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(232, 491);
            tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.Transparent;
            tabPage1.Controls.Add(labelTelnet);
            tabPage1.Controls.Add(label12);
            tabPage1.Controls.Add(labelHTTPS);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(labelHTTP);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(labelSSH);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(labelRDP);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(labelActivas);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(labelTotal);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(224, 458);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Estadísticas";
            // 
            // labelTelnet
            // 
            labelTelnet.AutoSize = true;
            labelTelnet.BackColor = Color.White;
            labelTelnet.Cursor = Cursors.Hand;
            labelTelnet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelTelnet.ForeColor = SystemColors.Highlight;
            labelTelnet.Location = new Point(133, 164);
            labelTelnet.Name = "labelTelnet";
            labelTelnet.Size = new Size(60, 20);
            labelTelnet.TabIndex = 14;
            labelTelnet.Text = "label13";
            labelTelnet.Click += labelTelnet_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.White;
            label12.Location = new Point(15, 164);
            label12.Name = "label12";
            label12.Size = new Size(82, 20);
            label12.TabIndex = 13;
            label12.Text = "23 (Telnet):";
            // 
            // labelHTTPS
            // 
            labelHTTPS.AutoSize = true;
            labelHTTPS.BackColor = Color.White;
            labelHTTPS.Cursor = Cursors.Hand;
            labelHTTPS.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelHTTPS.ForeColor = SystemColors.Highlight;
            labelHTTPS.Location = new Point(133, 238);
            labelHTTPS.Name = "labelHTTPS";
            labelHTTPS.Size = new Size(60, 20);
            labelHTTPS.TabIndex = 12;
            labelHTTPS.Text = "label12";
            labelHTTPS.Click += labelHTTPS_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.White;
            label11.Location = new Point(15, 238);
            label11.Name = "label11";
            label11.Size = new Size(93, 20);
            label11.TabIndex = 11;
            label11.Text = "443 (HTTPS):";
            // 
            // labelHTTP
            // 
            labelHTTP.AutoSize = true;
            labelHTTP.BackColor = Color.White;
            labelHTTP.Cursor = Cursors.Hand;
            labelHTTP.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelHTTP.ForeColor = SystemColors.Highlight;
            labelHTTP.Location = new Point(133, 200);
            labelHTTP.Name = "labelHTTP";
            labelHTTP.Size = new Size(60, 20);
            labelHTTP.TabIndex = 10;
            labelHTTP.Text = "label11";
            labelHTTP.Click += labelHTTP_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.White;
            label10.Location = new Point(15, 200);
            label10.Name = "label10";
            label10.Size = new Size(77, 20);
            label10.TabIndex = 9;
            label10.Text = "80 (HTTP):";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(47, 18);
            label7.Name = "label7";
            label7.Size = new Size(107, 20);
            label7.TabIndex = 8;
            label7.Text = "Clic para filtrar";
            // 
            // labelSSH
            // 
            labelSSH.AutoSize = true;
            labelSSH.BackColor = Color.White;
            labelSSH.Cursor = Cursors.Hand;
            labelSSH.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelSSH.ForeColor = SystemColors.Highlight;
            labelSSH.Location = new Point(133, 129);
            labelSSH.Name = "labelSSH";
            labelSSH.Size = new Size(51, 20);
            labelSSH.TabIndex = 7;
            labelSSH.Text = "label7";
            labelSSH.Click += labelSSH_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.White;
            label6.Location = new Point(15, 129);
            label6.Name = "label6";
            label6.Size = new Size(69, 20);
            label6.TabIndex = 6;
            label6.Text = "22 (SSH):";
            // 
            // labelRDP
            // 
            labelRDP.AutoSize = true;
            labelRDP.BackColor = Color.White;
            labelRDP.Cursor = Cursors.Hand;
            labelRDP.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelRDP.ForeColor = SystemColors.Highlight;
            labelRDP.Location = new Point(133, 95);
            labelRDP.Name = "labelRDP";
            labelRDP.Size = new Size(51, 20);
            labelRDP.TabIndex = 5;
            labelRDP.Text = "label6";
            labelRDP.Click += labelRDP_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Location = new Point(15, 96);
            label5.Name = "label5";
            label5.Size = new Size(86, 20);
            label5.TabIndex = 4;
            label5.Text = "3389 (RDP):";
            // 
            // labelActivas
            // 
            labelActivas.AutoSize = true;
            labelActivas.BackColor = Color.White;
            labelActivas.Cursor = Cursors.Hand;
            labelActivas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelActivas.ForeColor = SystemColors.Highlight;
            labelActivas.Location = new Point(133, 63);
            labelActivas.Name = "labelActivas";
            labelActivas.Size = new Size(51, 20);
            labelActivas.TabIndex = 3;
            labelActivas.Text = "label5";
            labelActivas.Click += labelActivas_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Location = new Point(15, 63);
            label4.Name = "label4";
            label4.Size = new Size(59, 20);
            label4.TabIndex = 2;
            label4.Text = "Activas:";
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Cursor = Cursors.Hand;
            labelTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelTotal.Location = new Point(133, 310);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(51, 20);
            labelTotal.TabIndex = 1;
            labelTotal.Text = "label4";
            labelTotal.Click += labelTotal_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 310);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 0;
            label3.Text = "Total:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Location = new Point(2, 46);
            panel1.Name = "panel1";
            panel1.Size = new Size(221, 249);
            panel1.TabIndex = 15;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(checkBoxSwitch);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(224, 458);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Avanzado";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label9.Location = new Point(0, 412);
            label9.Name = "label9";
            label9.Size = new Size(200, 20);
            label9.TabIndex = 2;
            label9.Text = "Puede tardar más el scaneo";
            // 
            // label8
            // 
            label8.Location = new Point(0, 202);
            label8.Name = "label8";
            label8.Size = new Size(215, 210);
            label8.TabIndex = 1;
            label8.Text = resources.GetString("label8.Text");
            // 
            // checkBoxSwitch
            // 
            checkBoxSwitch.AutoSize = true;
            checkBoxSwitch.Enabled = false;
            checkBoxSwitch.Location = new Point(3, 175);
            checkBoxSwitch.Name = "checkBoxSwitch";
            checkBoxSwitch.Size = new Size(217, 24);
            checkBoxSwitch.TabIndex = 0;
            checkBoxSwitch.Text = "Detectar dispositivos de red";
            checkBoxSwitch.UseVisualStyleBackColor = true;
            // 
            // FrmVMAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1314, 613);
            Controls.Add(tabControl1);
            Controls.Add(progressBarScan);
            Controls.Add(statusStrip1);
            Controls.Add(dataGridIPs);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            Name = "FrmVMAdmin";
            StartPosition = FormStartPosition.CenterScreen;
            Load += FrmVMAdmin_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridIPs).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox TextBoxInicio;
        private MaskedTextBox TextBoxFin;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private Button btnScan;
        private DataGridView dataGridIPs;
        private StatusStrip statusStrip1;
        private ProgressBar progressBarScan;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label labelTotal;
        private Label label3;
        private Label labelRDP;
        private Label label5;
        private Label labelActivas;
        private Label label4;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem escritorioRemotoToolStripMenuItem;
        private Button btnSettings;
        private Button btnSSH;
        private ToolStripMenuItem sSHToolStripMenuItem;
        private Label labelSSH;
        private Label label6;
        private Label label7;
        private CheckBox checkBoxSwitch;
        private Label label9;
        private Label label8;
        private Button btnCancelar;
        private Label label10;
        private Label labelHTTPS;
        private Label label11;
        private Label labelHTTP;
        private Label labelTelnet;
        private Label label12;
        private Panel panel1;
        private ToolStripMenuItem telnetToolStripMenuItem;
        private ToolStripMenuItem hTTPToolStripMenuItem;
        private ToolStripMenuItem httpsToolStripMenuItem1;
        private DataGridViewTextBoxColumn IP;
        private DataGridViewTextBoxColumn HOST;
        private DataGridViewTextBoxColumn SO;
        private DataGridViewTextBoxColumn Estado;
        private DataGridViewTextBoxColumn RDP;
        private DataGridViewTextBoxColumn SSH;
        private DataGridViewTextBoxColumn TELNET;
        private DataGridViewTextBoxColumn HTTP;
        private DataGridViewTextBoxColumn HTTPS;
        private DataGridViewTextBoxColumn SMB;
        private ToolStripMenuItem abrirCToolStripMenuItem;
        private Button btnAgregarRango;
        private Button btnSeleccionarRango;
        private Button btnSubir;
    }
}
