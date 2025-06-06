using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VMAdmin
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnVerificarTrustedHosts_Click(object sender, EventArgs e)
        {
            VerificarTrustedHosts();
        }

        private void btnSetTrustedHosts_Click(object sender, EventArgs e)
        {
            try
            {
                // IPs a agregar al TrustedHosts
                string ips = "*"; // Se puede leer esto desde un TextBox

                // Comando completo
                string comando = $"Set-Item -Path WSMan:\\localhost\\Client\\TrustedHosts -Value \"{ips}\" -Force";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{comando}\"",
                    Verb = "runas", // Ejecutar como administrador
                    UseShellExecute = true
                };

                Process.Start(psi);

                MessageBox.Show("IPs agregadas a TrustedHosts correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar TrustedHosts:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrarTrustedHosts_Click(object sender, EventArgs e)
        {
            LimpiarTrustedHosts();
        }

        //*****************************************************************************************//
        private void LimpiarTrustedHosts()
        {
            DialogResult confirmar = MessageBox.Show(
                "¿Estás seguro de que deseas limpiar todos los TrustedHosts?\nEsta acción no se puede deshacer.",
                "Confirmar limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmar == DialogResult.Yes)
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"Set-Item -Path WSMan:\\localhost\\Client\\TrustedHosts -Value '' -Force\"",
                        Verb = "runas", // Ejecutar como administrador
                        UseShellExecute = true
                    };

                    Process.Start(psi);

                    MessageBox.Show("TrustedHosts ha sido limpiado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al limpiar TrustedHosts:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //*****************************************************************************************//
        private void VerificarTrustedHosts()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-NoProfile -Command \"(Get-Item WSMan:\\localhost\\Client\\TrustedHosts).Value\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    string salida = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    if (string.IsNullOrWhiteSpace(salida))
                    {
                        MessageBox.Show("TrustedHosts está vacío.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"TrustedHosts configurados:\n{salida}", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar TrustedHosts:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
