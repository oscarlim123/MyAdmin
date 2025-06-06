namespace VMAdmin
{
    partial class FormAbout
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
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Items.AddRange(new object[] { "La aplicación utiliza el método ExecuteRemoteCommand que sirve para ejecutar ", "cualquier script o comando de PowerShell de forma remota, siempre que:", "", "     1- WinRM esté habilitado y configurado en el equipo remoto (con los comandos ", "         Enable-PSRemoting, etc.).", "", "\tPasos completos para configurar WinRM correctamente:", "\t\t-Habilitar WinRM:", "\t\tEnable-PSRemoting -Force", "", "\t\t-Configurar TrustedHosts (desde PowerShell):", "\t\tSet-Item WSMan:\\localhost\\Client\\TrustedHosts -Value \"*\" -Force", "", "\t\t-Reiniciar el servicio:", "\t\tRestart-Service WinRM", "", "\t\t-Verificar configuración:", "\t\tGet-Item WSMan:\\localhost\\Client\\TrustedHosts", "", "     2- Tengas credenciales válidas con permisos administrativos en el equipo remoto.", "", "     3- El comando/script no requiera interacción gráfica (por ejemplo, no sirve para ", "         abrir notepads o ventanas interactivas).", "", "Limitaciones:", "     1- No funciona si WinRM está bloqueado por políticas de red/GPO.", "", "     2- No maneja sesiones interactivas (como Enter-PSSession).", "", "     3- El timeout es fijo" });
            listBox1.Location = new Point(3, 71);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(680, 364);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(688, 450);
            Controls.Add(listBox1);
            Name = "FormAbout";
            Text = "FormAbout";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
    }
}