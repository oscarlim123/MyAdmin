using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.VisualBasic.ApplicationServices;


namespace VMAdmin
{
    public partial class FormUpload : Form
    {
        private List<string> _estacionesSeleccionadas;
        private string _rutaDestinoPredeterminada = @"C$\Temp\"; // Opcional: valor por defecto

        public FormUpload(List<string> estacionesSeleccionadas)
        {
            InitializeComponent();

            _estacionesSeleccionadas = estacionesSeleccionadas ?? throw new ArgumentNullException(nameof(estacionesSeleccionadas));

            // Validación básica
            if (_estacionesSeleccionadas.Count == 0)
            {
                MessageBox.Show("No se seleccionaron estaciones", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // Opcional: Mostrar resumen al cargar
            lblResumen.Text = $"Se realizará la copia hacia {_estacionesSeleccionadas.Count} estación(es)";
            //btnExaminar.Click += btnExaminar_Click;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /************************************************************************************/

        private async void btnSubir_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (string.IsNullOrEmpty(textBoxArchivoACopiar.Text))
            {
                MessageBox.Show("Seleccione un archivo primero", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(textBoxArchivoACopiar.Text))
            {
                MessageBox.Show("El archivo seleccionado no existe", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener credenciales del formulario
            string usuario = textBoxUsuario.Text.Trim();
            string password = textBoxPassword.Text;

            // Validar credenciales básicas
            if (string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Ingrese el usuario", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Deshabilitar botón durante la operación
            btnSubir.Enabled = false;

            try
            {
                // Configurar progreso
                progressBar1.Maximum = _estacionesSeleccionadas.Count;
                progressBar1.Value = 0;
                listCopiaOK.Items.Clear();
                listCopiaError.Items.Clear();

                // Crear un log para depuración
                string logFile = Path.Combine(Application.StartupPath, "debug_log.txt");
                File.AppendAllText(logFile, $"=== Inicio operación: {DateTime.Now} ======================\r\n");

                // Copiar a cada estación
                foreach (var estacion in _estacionesSeleccionadas)
                {
                    string nombreArchivo = Path.GetFileName(textBoxArchivoACopiar.Text);
                    string rutaBase = textBoxDestino.Text.Trim();
                    rutaBase = string.IsNullOrEmpty(rutaBase) ? "C$\\Temp" : rutaBase.Replace("/", "\\").Trim('\\');
                    string destino = $@"\\{estacion}\{rutaBase}\{nombreArchivo}";
                    string directorioDestino = Path.GetDirectoryName(destino);

                    try
                    {
                        // Usar NetworkConnection con credenciales
                        NetworkCredential credenciales;

                        // Comprobamos si el usuario tiene formato DOMINIO\AdminUser sino asumimos admin local
                        if (usuario.Contains("\\"))
                        {
                            // Formato dominio\usuario
                            var partes = usuario.Split('\\');

                            if (partes.Length != 2 || string.IsNullOrWhiteSpace(partes[0]) || string.IsNullOrWhiteSpace(partes[1]))
                            {
                                MessageBox.Show("El formato del nombre de usuario del dominio debe ser: DOMINIO\\usuario",
                                                "Formato inválido",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                return;
                            }

                            credenciales = new NetworkCredential(partes[1], password, partes[0]);
                        }
                        else
                        {
                            // Usuario local
                            credenciales = new NetworkCredential(usuario, password, Environment.UserDomainName);
                        }


                        using (var networkConn = new NetworkConnection(
                            $@"\\{estacion}\{rutaBase}",
                            credenciales))
                        {

                            // Verificar y crear carpeta destino si es necesario
                            if (chkCrearRuta.Checked && !Directory.Exists(directorioDestino))
                            {
                                try
                                {
                                    Directory.CreateDirectory(directorioDestino);
                                    File.AppendAllText(logFile, $"Creado directorio: {directorioDestino}\r\n");
                                }
                                catch (Exception ex)
                                {
                                    File.AppendAllText(logFile, $"Error creando directorio {directorioDestino}: {ex.Message}\r\n");
                                    throw; // Propagar error
                                }
                            }


                            // Copiar archivo
                            File.Copy(textBoxArchivoACopiar.Text, destino, true);
                            listCopiaOK.Items.Add($"{estacion} - ✓");
                            File.AppendAllText(logFile, $"Archivo copiado a {destino}\r\n");


                            // =====================
                            // DESCOMPRESIÓN REMOTA 
                            // =====================
                            if (chkDescomprimir.Checked)
                            {
                                // Rutas en formato local para el equipo remoto
                                //string carpetaLocalRemota = rutaBase.Replace("C$", "C:");
                                //string rutaLocalRemota = Path.Combine(carpetaLocalRemota, nombreArchivo);
                                //string rutaDestinoDescomprimido = Path.Combine(carpetaLocalRemota, "");

                                // === CRUCIAL: ASEGURAR RUTAS LOCALES PARA EL SERVIDOR REMOTO (C:\...) ===
                                // Convertir la ruta base de C$ a C: para que PowerShell la entienda localmente
                                string carpetaLocalRemotaParaPS = rutaBase.Replace("C$", "C:", StringComparison.OrdinalIgnoreCase).Replace("c$", "c:", StringComparison.OrdinalIgnoreCase);

                                // Asegurarse de que la ruta de la carpeta termine con una barra si no la tiene
                                if (!carpetaLocalRemotaParaPS.EndsWith("\\") && !carpetaLocalRemotaParaPS.EndsWith("/"))
                                {
                                    carpetaLocalRemotaParaPS += "\\";
                                }

                                // Construir las rutas completas en formato LOCAL (C:\...) para PowerShell
                                string rutaRemotaZipPS = Path.Combine(carpetaLocalRemotaParaPS, nombreArchivo);
                                string rutaRemotaDestinoPS = carpetaLocalRemotaParaPS; // Descomprimir en la misma carpeta

                                File.AppendAllText(logFile, $"Descompresión en {estacion}:\r\n");
                                File.AppendAllText(logFile, $"  Archivo ZIP remoto (PS format): {rutaRemotaZipPS}\r\n");
                                File.AppendAllText(logFile, $"  Destino Descompresión (PS format): {rutaRemotaDestinoPS}\r\n");

                                // Llamada al método de descompresión con las rutas LOCALES para PowerShell
                                if (await Task.Run(() => DescomprimirConWinRM(estacion, usuario, password, rutaRemotaZipPS, rutaRemotaDestinoPS, logFile))) // Usar Task.Run para async
                                {
                                    listCopiaOK.Items.Add($"{estacion} - Descompresión ✓");
                                }
                                else
                                {
                                    listCopiaError.Items.Add($"{estacion} - Error en descompresión");
                                    File.AppendAllText(logFile, $"FALLO: No se pudo descomprimir en {estacion}\r\n");
                                }
                                //****************************************************

                            }
                            else if (chkDescomprimir.Checked && !Path.GetExtension(textBoxArchivoACopiar.Text).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                            {
                                // Log si se marcó descomprimir pero no es un ZIP
                                listCopiaError.Items.Add($"{estacion} - Advertencia: Archivo no es ZIP para descompresión.");
                                File.AppendAllText(logFile, $"Advertencia: '{textBoxArchivoACopiar.Text}' no es un archivo .zip. Descompresión omitida.\r\n");
                            }
                            // =====================
                            // FIN DESCOMPRESIÓN REMOTA
                            // =====================

                        }
                    }
                    catch (Exception ex)
                    {
                        //string errorDetails = $"Error accediendo a {estacion}: {ex.GetType().Name} - {ex.Message}";
                        string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        string errorDetails = $"[{timestamp}]\n" +
                                                $"Error en: {estacion}\n" +
                                                $"Ruta: {Path.GetDirectoryName(destino)}\n" +
                                                $"Dir. destino: {directorioDestino}\n" +
                                                $"Usuario: {textBoxUsuario.Text}\n" +
                                                $"Tipo error: {ex.GetType().Name}\n" +
                                                $"Mensaje: {ex.Message}\n" +
                                                //$"Stack Trace: {ex.StackTrace}\n" +
                                                new string('=', 50);

                        File.AppendAllText("log_conexiones.txt", errorDetails + Environment.NewLine);
                        //File.WriteAllText("log_conexiones.txt", errorDetails + Environment.NewLine);
                        File.AppendAllText(logFile, errorDetails + Environment.NewLine);

                        listCopiaError.Items.Add($"{estacion} - ✗: {ex.Message.Split('\n')[0]}");
                    }

                    progressBar1.Value++;
                    Application.DoEvents(); // Actualizar la UI
                }
            }
            finally
            {
                btnSubir.Enabled = true;
            }

            MessageBox.Show($"Proceso completado:\n" +
                           $"Correctas: {listCopiaOK.Items.Count}\n" +
                           $"Errores: {listCopiaError.Items.Count}",
                           "Resumen",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
        }

        /*******************************************************************************/

        // Método auxiliar para simplificar mensajes de error
        private string GetErrorMessage(Exception ex)
        {
            if (ex is UnauthorizedAccessException) return "Acceso denegado";
            if (ex is IOException ioEx) return ioEx.Message.Split('\n')[0];
            return ex.Message.Split('\n')[0];
        }

        /*******************************************************************************/
        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Configuración del diálogo
                openFileDialog.Title = "Seleccionar archivo a copiar";
                openFileDialog.Filter = "Todos los archivos (*.*)|*.*";
                openFileDialog.CheckFileExists = true;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Mostrar la ruta completa en el TextBox
                    textBoxArchivoACopiar.Text = openFileDialog.FileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //********************************************************************************************//

        // 2. Nuevo método para ejecución remota vía WinRM
        private bool DescomprimirConWinRM(string estacion, string usuario, string password,
                                          string rutaLocalRemotaPS, string rutaDestinoDescomprimidoPS,
                                          string logFile)
        {
            try
            {
                // === COMANDO POWERSHELL EN UNA SOLA LÍNEA Y MEJOR ESCAPADO PARA POWERSHELL 4.0 USANDO .NET ===
                // Usamos String.Format o interpolación directa para las rutas.
                // Cada comilla doble DENTRO del script de PowerShell debe ser doblemente escapada (o con "`")
                // No hay saltos de línea visibles para el shell/WinRM
                // === COMANDO POWERSHELL CON LÓGICA DE CARPETA DE DESTINO DINÁMICA ===
                string command = string.Format(
                    "$ErrorActionPreference = 'Stop'; " +
                    "try {{" +
                    "Add-Type -AssemblyName System.IO.Compression.FileSystem;" +
                    // Obtener el nombre del archivo ZIP sin extensión (ej. 'TextPrueba' de 'C:\instal\TextPrueba.zip')
                    "$zipFileName = [System.IO.Path]::GetFileNameWithoutExtension('{1}'); " +
                    // Construir la ruta completa de la carpeta de destino para el contenido del ZIP (ej. 'C:\instal\TextPrueba')
                    "$fullDestinationPath = [System.IO.Path]::Combine('{0}', $zipFileName); " +

                    // Si la carpeta de destino ya existe, la eliminamos para asegurar una extracción limpia (sobreescritura)
                    "if (Test-Path -Path $fullDestinationPath -PathType Container) {{" +
                    "   Remove-Item -Path $fullDestinationPath -Recurse -Force | Out-Null; " +
                    "}} " +

                    // Asegurar que la carpeta de destino final exista antes de descomprimir
                    "New-Item -Path $fullDestinationPath -ItemType Directory -Force | Out-Null; " +

                    // Descomprimir usando la clase .NET ZipFile en la nueva carpeta de destino
                    "[System.IO.Compression.ZipFile]::ExtractToDirectory('{1}', $fullDestinationPath); " +

                    "Write-Output 'SUCCESS_DECOMPRESSION'; " +
                    "exit 0; " +
                    "}} catch {{" +
                    "Write-Error \\\"ERROR_DECOMPRESSION: $($_.Exception.Message)\\\"; " +
                    "exit 1; " +
                    "}}", rutaDestinoDescomprimidoPS, rutaLocalRemotaPS
                );

                /*
                  string command = string.Format(
                    "$ErrorActionPreference = 'Stop'; try {{" +
                    "Add-Type -AssemblyName System.IO.Compression.FileSystem;" +
                    "if (-not (Test-Path -Path '{0}')) {{" +
                    "New-Item -Path '{0}' -ItemType Directory -Force | Out-Null;" +
                    "}};" +
                    "[System.IO.Compression.ZipFile]::ExtractToDirectory('{1}', '{0}', $true);" +
                    "Write-Output 'SUCCESS_DECOMPRESSION';" +
                    "exit 0;" +
                    "}} catch {{" +
                    "Write-Error \\\"ERROR_DECOMPRESSION: $($_.Exception.Message)\\\";" +
                    "exit 1;" +
                    "}}", rutaDestinoDescomprimidoPS, rutaLocalRemotaPS
                );
                */
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "winrs",
                    //Arguments = $"-r:{estacion} -u:{usuario} -p:{password} powershell -Command \"{command.Replace("\"", "`\"")}\"", // Usar `\"` es más seguro
                    Arguments = $"-r:{estacion} -u:{usuario} -p:{password} powershell -Command \"{command}\"", // 'command' ya viene bien escapado para WinRM
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true, // También redirigir StandardOutput para capturar Write-Output
                    CreateNoWindow = true
                };

                File.AppendAllText(logFile, $"Ejecutando WinRM: {psi.Arguments}\r\n");

                using (Process process = Process.Start(psi))
                {
                    // Esperar de forma asíncrona para no bloquear la UI
                    Task<string> stdOutputTask = process.StandardOutput.ReadToEndAsync();
                    Task<string> stdErrorTask = process.StandardError.ReadToEndAsync();

                    bool exited = process.WaitForExit(300000); // 5 minutos timeout

                    string standardOutput = stdOutputTask.Result;
                    string errorOutput = stdErrorTask.Result;

                    File.AppendAllText(logFile, $"WinRM StdOut: {standardOutput}\r\n");
                    File.AppendAllText(logFile, $"WinRM Error: {errorOutput}\r\n");
                    File.AppendAllText(logFile, $"WinRM Exit Code: {process.ExitCode}\r\n");

                    // La condición de éxito es que el proceso haya salido con código 0
                    // Y que la salida estándar contenga nuestro marcador de éxito del script de PowerShell
                    // Y que el stream de error del proceso winrs esté vacío (o no contenga nuestro marcador de error explícito)
                    return exited && process.ExitCode == 0 && standardOutput.Contains("SUCCESS_DECOMPRESSION") && !errorOutput.Contains("ERROR_DECOMPRESSION:");
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFile, $"Excepción en WinRM (DescomprimirConWinRM): {ex.Message}\r\n\r\n");
                return false;
            }

            /* // === SECCION COMANDO POWERSHELL PARA POWERSHELL 5.0 O SUPERIOR (No está probada) ===
            try
            {
                string command = $@"
                    $ErrorActionPreference = 'Stop'
                    try {{
                        if (-not (Test-Path '{rutaDestinoDescomprimido}')) {{
                            New-Item -Path '{rutaDestinoDescomprimido}' -ItemType Directory -Force | Out-Null
                        }}
                        Expand-Archive -Path '{rutaLocalRemota}' -DestinationPath '{rutaDestinoDescomprimido}' -Force
                        exit 0
                    }} catch {{
                        Write-Error $_.Exception.Message
                        exit 1
                    }}
                ";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "winrs",
                    Arguments = $"-r:{estacion} -u:{usuario} -p:{password} powershell -Command \"{command.Replace("\"", "\\\"")}\"",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                File.AppendAllText(logFile, $"Ejecutando WinRM: {psi.Arguments}\r\n");

                using (Process process = Process.Start(psi))
                {
                    bool exited = process.WaitForExit(300000); // 5 minutos timeout
                    string errorOutput = process.StandardError.ReadToEnd();

                    File.AppendAllText(logFile, $"WinRM Exit Code: {process.ExitCode}\r\n");
                    if (!string.IsNullOrEmpty(errorOutput))
                    {
                        File.AppendAllText(logFile, $"WinRM Error: {errorOutput}\r\n");
                    }

                    return exited && process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFile, $"Excepción en WinRM: {ex.Message}\r\n");
                return false;
            }
            */
        }

        /************************************************************************************/

        private string ConvertirRutaUNCaLocal(string rutaUNC)
        {
            // Ejemplo: \\estacion\C$\instal\archivo.zip → C:\instal\archivo.zip
            if (rutaUNC.StartsWith(@"\\"))
            {
                var partes = rutaUNC.Split('\\');
                if (partes.Length >= 4 && partes[2].EndsWith("$"))
                {
                    string unidad = partes[2].Substring(0, 1); // "C" de "C$"
                    string rutaRestante = string.Join("\\", partes.Skip(3));
                    return $"{unidad}:\\{rutaRestante}";
                }
            }

            // Si ya es una ruta local normal, la devuelve igual
            return rutaUNC;
        }

        private void listCopiaError_KeyDown(object sender, KeyEventArgs e)
        {
            // Asegurarse de que el control (Ctrl) esté presionado y la tecla 'C'
            if (e.Control && e.KeyCode == Keys.C)
            {
                // Verificar si hay elementos seleccionados
                if (listCopiaError.SelectedItems.Count > 0)
                {
                    // Unir todos los elementos seleccionados en un solo string, separados por saltos de línea
                    // Si solo se permite una selección, SelectedItem bastaría.
                    // Si se permite selección múltiple, SelectedItems es necesario.
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in listCopiaError.SelectedItems)
                    {
                        // Convertir el item a string. Si los items son objetos, considera sobreescribir ToString() en tu clase de objeto.
                        sb.AppendLine(item.ToString());
                    }

                    // Copiar el texto al portapapeles
                    Clipboard.SetText(sb.ToString().TrimEnd()); // TrimEnd para eliminar el último salto de línea
                    e.Handled = true; // Indicar que el evento ha sido manejado
                }
            }
        }
    }
}
