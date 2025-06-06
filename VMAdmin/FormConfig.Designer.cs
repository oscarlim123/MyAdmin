namespace VMAdmin
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            groupBox1 = new GroupBox();
            btnBorrarTrustedHosts = new Button();
            btnSetTrustedHosts = new Button();
            btnVerificarTrustedHosts = new Button();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnBorrarTrustedHosts);
            groupBox1.Controls.Add(btnSetTrustedHosts);
            groupBox1.Controls.Add(btnVerificarTrustedHosts);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(1, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(836, 192);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "PowerShell - TrustedHosts";
            // 
            // btnBorrarTrustedHosts
            // 
            btnBorrarTrustedHosts.Location = new Point(28, 126);
            btnBorrarTrustedHosts.Name = "btnBorrarTrustedHosts";
            btnBorrarTrustedHosts.Size = new Size(157, 40);
            btnBorrarTrustedHosts.TabIndex = 17;
            btnBorrarTrustedHosts.Text = "Borrar TrustedHosts";
            btnBorrarTrustedHosts.UseVisualStyleBackColor = true;
            btnBorrarTrustedHosts.Click += btnBorrarTrustedHosts_Click;
            // 
            // btnSetTrustedHosts
            // 
            btnSetTrustedHosts.Location = new Point(28, 80);
            btnSetTrustedHosts.Name = "btnSetTrustedHosts";
            btnSetTrustedHosts.Size = new Size(157, 40);
            btnSetTrustedHosts.TabIndex = 16;
            btnSetTrustedHosts.Text = "Set TrustedHosts";
            btnSetTrustedHosts.UseVisualStyleBackColor = true;
            btnSetTrustedHosts.Click += btnSetTrustedHosts_Click;
            // 
            // btnVerificarTrustedHosts
            // 
            btnVerificarTrustedHosts.Location = new Point(28, 34);
            btnVerificarTrustedHosts.Name = "btnVerificarTrustedHosts";
            btnVerificarTrustedHosts.Size = new Size(157, 40);
            btnVerificarTrustedHosts.TabIndex = 15;
            btnVerificarTrustedHosts.Text = "Ver TrustedHosts";
            btnVerificarTrustedHosts.UseVisualStyleBackColor = true;
            btnVerificarTrustedHosts.Click += btnVerificarTrustedHosts_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(236, 34);
            label1.Name = "label1";
            label1.Size = new Size(587, 140);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            label1.Click += label1_Click;
            // 
            // FormConfig
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(842, 217);
            Controls.Add(groupBox1);
            Name = "FormConfig";
            Text = "FormConfig";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Button btnBorrarTrustedHosts;
        private Button btnSetTrustedHosts;
        private Button btnVerificarTrustedHosts;
    }
}