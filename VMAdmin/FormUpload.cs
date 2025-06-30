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
using System.IO;

namespace VMAdmin
{
    public partial class FormUpload : Form
    {
        private List<string> _estacionesSeleccionadas;
        private string _rutaDestinoPredeterminada = @"C$\Temp\"; // Opcional: valor por defecto

        // Nuevas variables para gestionar el origen a copiar
        private enum SourceType { None, File, Folder }                      // Enumeración para el tipo de origen
        private SourceType _currentSourceType = SourceType.None;            // Tipo de origen actual
        private List<string> _selectedSourcePaths = new List<string>();     // Lista para almacenar las rutas de los archivos/carpetas        
        private BindingList<SourceItem> _selectedSourceItems = new BindingList<SourceItem>(); // Esta lista almacenará los objetos SourceItem para el DataGridView

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

            // Mostrar resumen al cargar
            lblResumen.Text = $"Se realizará la copia hacia {_estacionesSeleccionadas.Count} estación(es)";

            // Configuración inicial de los controles
            rbArchivos.Checked = true;
            _currentSourceType = SourceType.File;

            // Enlaza el DataGridView a la lista de SourceItem
            dataGridArchivosOrigen.AutoGenerateColumns = false; // Definir las columnas manualmente
            dataGridArchivosOrigen.DataSource = _selectedSourceItems;

            // Llama a un método para configurar las columnas del DataGridView
            SetupDataGridViewColumns();

            // Llama a un método para actualizar la UI según la selección inicial
            UpdateSourceUI();

        }

        /**********************************************************************************/

        // Método para configurar las columnas del DataGridView
        private void SetupDataGridViewColumns()
        {
            // Configurar estilos (opcional, se puede usar FormHelperMio.StyleDataGridView)
            dataGridArchivosOrigen.AllowUserToAddRows = false;
            dataGridArchivosOrigen.RowHeadersVisible = false;
            dataGridArchivosOrigen.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridArchivosOrigen.MultiSelect = true;
        }
        /**********************************************************************************/
        // Método para actualizar la UI (lo implemento más abajo)
        private void UpdateSourceUI()
        {
            //textBoxArchivoACopiar.Text = string.Empty;
            _selectedSourcePaths.Clear(); // Limpia la lista de rutas seleccionadas
        }
        /**********************************************************************************/
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /************************************************************************************/

        private async void btnSubir_Click(object sender, EventArgs e)
        {
            // === REVALIDACIÓN PARA EL NUEVO FLUJO DE MULTIPLES ARCHIVOS/CARPETAS ===
            if (_selectedSourceItems.Count == 0)
            {
                MessageBox.Show("No hay elementos seleccionados para copiar en la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_estacionesSeleccionadas.Count == 0)
            {
                MessageBox.Show("No se han seleccionado estaciones remotas para la copia.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // === ADVERTENCIA/CONFIRMACIÓN PARA DESCOMPRESIÓN MIXTA ===
            if (chkDescomprimir.Checked)
            {
                // Verificar si hay archivos seleccionados que NO son .zip
                bool hayArchivosNoZip = _selectedSourceItems.Any(item =>
                    item.Tipo == "Archivo" &&
                    !Path.GetExtension(item.RutaCompleta).Equals(".zip", StringComparison.OrdinalIgnoreCase)
                );

                if (hayArchivosNoZip)
                {
                    DialogResult result = MessageBox.Show(
                        "Ha marcado 'Descomprimir', pero hay archivos que NO son ZIP en su lista. " +
                        "Solo los archivos ZIP se descomprimirán automáticamente después de la copia. " +
                        "¿Desea continuar con esta operación mixta?",
                        "Confirmar Descompresión",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.No)
                    {
                        // Si el usuario elige NO, cancelar la operación
                        return;
                    }
                }
            }
            // === FIN ADVERTENCIA/CONFIRMACIÓN PARA DESCOMPRESIÓN MIXTA ===


            // textBoxDestino.Text tendrá algo como "C:\instal" o "D:\destino"
            // y lo convertirá a "C$\instal" o "D$\destino".
            if (!string.IsNullOrEmpty(textBoxDestino.Text))
            {
                // Limpiamos barras y convertimos a formato de compartición administrativa
                string tempDest = textBoxDestino.Text.Trim().Replace("/", "\\").TrimEnd('\\');
                if (tempDest.Length >= 2 && tempDest[1] == ':') // Si es C:\ o D:\
                {
                    _rutaDestinoPredeterminada = tempDest[0] + "$" + tempDest.Substring(2) + "\\";
                }
                else // Si ya es algo como "C$\Temp" o "Temp"
                {
                    _rutaDestinoPredeterminada = tempDest + "\\";
                }
            }
            else
            {
                _rutaDestinoPredeterminada = @"C$\Temp\"; // Valor por defecto si no se especifica
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
            Cursor = Cursors.WaitCursor;

            // Limpiar los listbox de resultados de una ejecución anterior
            listCopiaOK.Items.Clear();
            listCopiaError.Items.Clear();

            // Crear un log para depuración
            string logFile = Path.Combine(Application.StartupPath, "debug_log.txt");
            File.AppendAllText(logFile, $"\r\n============== Inicio operación: {DateTime.Now} ======================\r\n");

            try
            {
                progressBar1.Maximum = _estacionesSeleccionadas.Count * _selectedSourceItems.Count;
                progressBar1.Value = 0;

                // Copiar a cada estación
                foreach (string ipAddress in _estacionesSeleccionadas)
                {
                    string adminShareForConnection = "C$"; // Valor por defecto si no se puede determinar la unidad

                    // Extraer la unidad administrativa (ej. "C$", "D$") de _rutaDestinoPredeterminada. ya tiene el formato "X$\..." o "C$\Temp\".
                    if (_rutaDestinoPredeterminada.Length >= 2 && _rutaDestinoPredeterminada[1] == '$')
                    {
                        adminShareForConnection = _rutaDestinoPredeterminada.Substring(0, 2);
                    }

                    // Esta es la ruta para establecer la conexión SMB, debe ser un recurso compartido existente (ej. \\IP\C$)
                    string remoteShareBaseForConnection = $@"\\{ipAddress}\{adminShareForConnection}";

                    NetworkCredential credenciales;

                    // Comprobamos si el usuario tiene formato DOMINIO\AdminUser sino asumimos admin local
                    if (usuario.Contains("\\"))
                    {
                        var partes = usuario.Split('\\');
                        if (partes.Length != 2 || string.IsNullOrWhiteSpace(partes[0]) || string.IsNullOrWhiteSpace(partes[1]))
                        {
                            MessageBox.Show("El formato del nombre de usuario del dominio debe ser: DOMINIO\\usuario",
                                            "Formato inválido",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            return; // Detener la operación
                        }
                        credenciales = new NetworkCredential(partes[1], password, partes[0]);
                    }
                    else
                    {
                        // Usuario local o del dominio actual.
                        // Si el usuario es local en la remota, el dominio sería el nombre de la estación.
                        // Si es un usuario de dominio sin prefijo, .UserDomainName intentará resolverlo.
                        credenciales = new NetworkCredential(usuario, password, Environment.UserDomainName);
                    }

                    string remoteShareBase = $@"\\{ipAddress}\{_rutaDestinoPredeterminada}";
                    // Ejemplo: _rutaDestinoPredeterminada = "C$\instal\", entonces remoteShareBase = "\\192.168.1.100\C$\instal\"

                    using (var networkConn = new NetworkConnection(remoteShareBaseForConnection, credenciales))
                    {
                        // El NetworkConnection establece la autenticación para las operaciones de red.

                        progressBar2.Maximum = _selectedSourceItems.Count;
                        progressBar2.Value = 0;

                        // Por simplicidad, dejaremos que robocopy maneje la creación de directorios si es una carpeta.
                        // Para archivos, el destino directo es la carpeta y robocopy crea el archivo dentro.

                        foreach (SourceItem item in _selectedSourceItems)
                        {
                            // Esta será la cadena completa para Process.Start
                            string robocopyFullArguments; 

                            // Extraer solo la parte de la ruta sin la unidad (ej. "Temp")
                            string relativeDestPath = _rutaDestinoPredeterminada.Replace("C$", "").Replace("D$", "").Trim('\\');
                            string finalRemotePath = $@"\\{ipAddress}\{_rutaDestinoPredeterminada}"; // Ej: \\192.168.1.100\C$\Temp\

                            if (item.Tipo == "Archivo")
                            {
                                string sourceDirectory = Path.GetDirectoryName(item.RutaCompleta); // Carpeta que contiene el archivo Ej: "E:\IMAGES"
                                string sourceDirForRobocopy = sourceDirectory.Replace("\\", "\\\\");
                                string destinationDirectory = remoteShareBase.TrimEnd('\\');       // Carpeta donde se copiará el archivo
                                string fileName = item.Nombre;                                     // Nombre del archivo a copiar                         
                                string robocopyFlags = "/Z /NFL /NDL /R:1 /W:1";                   // Flags comunes para archivos

                                // Construimos la cadena robocopy original SIN ESACAPES ADICIONALES para CMD
                                robocopyFullArguments = $"robocopy \"{sourceDirForRobocopy}\" \"{destinationDirectory}\" \"{fileName}\" {robocopyFlags}";
                            }
                            else // Tipo == "Carpeta"
                            {
                                string sourceFolderToCopy = item.RutaCompleta.TrimEnd('\\');                 // La ruta completa a la carpeta local que se quiere copiar. (ej: "E:\MyFolder"). Aseguramos que no tenga barra final;                               
                                string remoteDestinationFolder = Path.Combine(remoteShareBase, item.Nombre); // El destino completo remoto (Path.Combine para unir la base remota con el nombre de la carpeta)
                                string robocopyFlags = "/E /Z /NFL /NDL /R:1 /W:1";                          // /E para copiar subdirectorios (incluso vacíos), /COPYALL para permisos/atributos

                                // Construimos la cadena robocopy original SIN ESACAPES ADICIONALES para CMD aún
                                robocopyFullArguments = $"robocopy \"{sourceFolderToCopy}\" \"{remoteDestinationFolder}\" {robocopyFlags}";
                            }

                            progressBar2.Invoke((MethodInvoker)delegate { progressBar2.Value++; });

                            try
                            {
                                // Llamada al método de ejecución de Robocopy
                                await ExecuteRobocopyAsync(robocopyFullArguments, ipAddress, logFile);

                                // =====================
                                // DESCOMPRESIÓN REMOTA (Mantenemos la lógica de WinRM aquí, ya que es una acción remota)
                                // =====================
                                if (chkDescomprimir.Checked && Path.GetExtension(item.RutaCompleta).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                                {
                                    // === CRUCIAL: ASEGURAR RUTAS LOCALES PARA EL SERVIDOR REMOTO (C:\...) ===
                                    // Convertir la ruta base de C$ a C: para que PowerShell la entienda localmente
                                    // rutaBase es "C$\Temp", queremos "C:\Temp"
                                    string carpetaLocalRemotaParaPS = _rutaDestinoPredeterminada.Replace("C$", "C:", StringComparison.OrdinalIgnoreCase)
                                                                                                  .Replace("D$", "D:", StringComparison.OrdinalIgnoreCase);

                                    // Asegurarse de que la ruta de la carpeta termine con una barra si no la tiene
                                    if (!carpetaLocalRemotaParaPS.EndsWith("\\") && !carpetaLocalRemotaParaPS.EndsWith("/"))
                                    {
                                        carpetaLocalRemotaParaPS += "\\";
                                    }

                                    // Construir las rutas completas en formato LOCAL (C:\...) para PowerShell
                                    string rutaRemotaZipPS = Path.Combine(carpetaLocalRemotaParaPS, item.Nombre); // item.Nombre es el nombre del archivo ZIP
                                    string rutaRemotaDestinoPS = carpetaLocalRemotaParaPS; // Descomprimir en la misma carpeta

                                    File.AppendAllText(logFile, $"Descompresión en {ipAddress}:\r\n");
                                    File.AppendAllText(logFile, $"  Archivo ZIP remoto (PS format): {rutaRemotaZipPS}\r\n");
                                    File.AppendAllText(logFile, $"  Destino Descompresión (PS format): {rutaRemotaDestinoPS}\r\n");

                                    // Llamada al método de descompresión con las rutas LOCALES para PowerShell
                                    if (await DescomprimirConWinRM(ipAddress, usuario, password, rutaRemotaZipPS, rutaRemotaDestinoPS, logFile)) // Usar Task.Run no es necesario si DescomprimirConWinRM ya es async
                                    {
                                        listCopiaOK.Invoke((MethodInvoker)delegate {
                                            listCopiaOK.Items.Add($"{ipAddress} - Descompresión ✓ de '{item.Nombre}'");
                                        });
                                    }
                                    else
                                    {
                                        listCopiaError.Invoke((MethodInvoker)delegate {
                                            listCopiaError.Items.Add($"{ipAddress} - Error en descompresión de '{item.Nombre}'");
                                        });
                                        File.AppendAllText(logFile, $"FALLO: No se pudo descomprimir '{item.Nombre}' en {ipAddress}\r\n");
                                    }
                                }
                                else if (chkDescomprimir.Checked && !Path.GetExtension(item.RutaCompleta).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Log si se marcó descomprimir pero no es un ZIP
                                    listCopiaError.Invoke((MethodInvoker)delegate {
                                        listCopiaError.Items.Add($"{ipAddress} - Advertencia: '{item.Nombre}' no es ZIP para descompresión.");
                                    });
                                    File.AppendAllText(logFile, $"Advertencia: '{item.Nombre}' no es un archivo .zip. Descompresión omitida.\r\n");
                                }
                                // =====================
                                // FIN DESCOMPRESIÓN REMOTA
                                // =====================
                            }
                            catch (Exception ex)
                            {
                                string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                                string errorDetails = $"[{timestamp}]\n" +
                                                      $"Error en: {ipAddress}\n" +
                                                      $"Archivo/Carpeta: {item.Nombre}\n" +
                                                      $"Tipo error: {ex.GetType().Name}\n" +
                                                      $"Mensaje: {ex.Message}\n" +
                                                      new string('=', 50);

                                File.AppendAllText("log_conexiones.txt", errorDetails + Environment.NewLine);
                                File.AppendAllText(logFile, errorDetails + Environment.NewLine);

                                listCopiaError.Invoke((MethodInvoker)delegate {
                                    listCopiaError.Items.Add($"{ipAddress} - ✗ '{item.Nombre}': {ex.Message.Split('\n')[0]}");
                                });
                            }

                            progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value++; }); // Actualizar progreso
                            Application.DoEvents(); // Procesar todos los mensajes pendientes en la cola de mensajes Para mantener la UI responsiva (considera un timer o un modelo más avanzado si es un uso intensivo)
                        }
                    } // Fin del 'using (networkConn ...)'
                }
                MessageBox.Show($"Proceso de copia y descompresión finalizado.\n" +
                                $"Correctas: {listCopiaOK.Items.Count}\n" +
                                $"Errores: {listCopiaError.Items.Count}",
                                "Copia Completa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado durante el proceso general: {ex.Message}", "Error Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.AppendAllText(logFile, $"Error fatal en btnSubir_Click: {ex.Message}\r\nStackTrace:\r\n{ex.StackTrace}\r\n");
            }
            finally
            {
                btnSubir.Enabled = true;
                Cursor = Cursors.Default;
            }
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
            if (_currentSourceType == SourceType.File)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Todos los archivos (*.*)|*.*";
                    ofd.Multiselect = true;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string filePath in ofd.FileNames)
                        {
                            try
                            {
                                // Evitar duplicados al llenar el DataGrid
                                if (!_selectedSourceItems.Any(item => item.RutaCompleta.Equals(filePath, StringComparison.OrdinalIgnoreCase)))
                                {
                                    FileInfo fi = new FileInfo(filePath);
                                    _selectedSourceItems.Add(new SourceItem
                                    {
                                        Tipo = "Archivo",
                                        Nombre = fi.Name,
                                        RutaCompleta = fi.FullName,
                                        Tamano = FormatBytes(fi.Length),
                                        FechaModificacion = fi.LastWriteTime
                                    });
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al leer información del archivo {filePath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // Actualizar el TextBox según la cantidad de archivos seleccionados
                        /*
                        if (_selectedSourceItems.Count == 1)
                        {
                            textBoxArchivoACopiar.Text = _selectedSourceItems[0].RutaCompleta;
                        }
                        else
                        {
                            textBoxArchivoACopiar.Text = $"Múltiples archivos seleccionados ({_selectedSourceItems.Count})";
                        }
                        */
                        // El DataGridView ya está enlazado a _selectedSourceItems, se actualizará automáticamente
                    }
                }
            }
            else if (_currentSourceType == SourceType.Folder)
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Seleccione la carpeta de origen";
                    fbd.ShowNewFolderButton = false;

                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        // Para una carpeta, solo hay una ruta seleccionada.
                        // Evitar duplicados
                        if (!_selectedSourceItems.Any(item => item.RutaCompleta.Equals(fbd.SelectedPath, StringComparison.OrdinalIgnoreCase)))
                        {
                            try
                            {
                                DirectoryInfo di = new DirectoryInfo(fbd.SelectedPath);
                                _selectedSourceItems.Add(new SourceItem
                                {
                                    Tipo = "Carpeta",
                                    Nombre = di.Name,
                                    RutaCompleta = di.FullName,
                                    Tamano = "N/A", // El tamaño de una carpeta se calcula de forma diferente
                                    FechaModificacion = di.LastWriteTime
                                });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error al leer información de la carpeta {fbd.SelectedPath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        //textBoxArchivoACopiar.Text = fbd.SelectedPath; // El TextBox muestra la ruta de la carpeta

                        // Es buena práctica invalidar el control para asegurar un refresco visual
                        dataGridArchivosOrigen.Invalidate();
                    }
                }
            }
        }
        /**************************************************************************************************/
        // Método auxiliar para formatear el tamaño de los bytes
        private string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i = 0;
            double dblSByte = bytes;
            while (dblSByte >= 1024 && i < Suffix.Length - 1)
            {
                dblSByte /= 1024;
                i++;
            }
            return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
        /**************************************************************************************************/
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //********************************************************************************************//

        // 2. Nuevo método para ejecución remota vía WinRM
        private async Task<bool> DescomprimirConWinRM(string estacion, string usuario, string password,
                                          string rutaLocalRemotaPS, string rutaDestinoDescomprimidoPS,
                                          string logFile)
        {
            try
            {
                // COMANDO POWERSHELL EN UNA SOLA LÍNEA Y MEJOR ESCAPADO PARA POWERSHELL 4.0 USANDO .NET
                // CON LÓGICA DE CARPETA DE DESTINO DINÁMICA ===
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

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "winrs",
                    Arguments = $"-r:{estacion} -u:{usuario} -p:{password} powershell -Command \"{command}\"", // 'command' ya viene bien escapado para WinRM
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
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
        /************************************************************************************/
        private void listCopiaError_KeyDown(object sender, KeyEventArgs e)
        {
            // Asegurarse de que el control (Ctrl) esté presionado y la tecla 'C'
            if (e.Control && e.KeyCode == Keys.C)
            {
                // Verificar si hay elementos seleccionados
                if (listCopiaError.SelectedItems.Count > 0)
                {
                    // Unir todos los elementos seleccionados en un solo string, separados por saltos de línea
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in listCopiaError.SelectedItems)
                    {
                        // Convertir el item a string. Si los items son objetos, sobreescribir ToString() en la clase de objeto.
                        sb.AppendLine(item.ToString());
                    }

                    // Copiar el texto al portapapeles
                    Clipboard.SetText(sb.ToString().TrimEnd()); // TrimEnd para eliminar el último salto de línea
                    e.Handled = true; // Indicar que el evento ha sido manejado
                }
            }
        }
        /************************************************************************************/
        // Método auxiliar para ejecutar Robocopy asíncrono para no bloquear la UI durante las copias.
        private async Task ExecuteRobocopyAsync(string robocopyCommandString, string stationIp, string logFile)
        {
            string cmdArguments = $"/c {robocopyCommandString}";

            string logMessage = $"Iniciando comando en {stationIp}: cmd.exe {cmdArguments}\r\n";
            File.AppendAllText(logFile, logMessage);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe", // Ejecutar a través de cmd.exe
                Arguments = cmdArguments, // Los argumentos para cmd.exe
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = psi })
            {
                StringBuilder output = new StringBuilder();
                StringBuilder errorOutput = new StringBuilder();

                process.OutputDataReceived += (sender, e) => { if (e.Data != null) output.AppendLine(e.Data); };
                process.ErrorDataReceived += (sender, e) => { if (e.Data != null) errorOutput.AppendLine(e.Data); };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await Task.Run(() => process.WaitForExit());

                string robocopyOutput = output.ToString();
                string robocopyErrorOutput = errorOutput.ToString();

                File.AppendAllText(logFile, "Salida de CMD/Robocopy:\r\n" + robocopyOutput + "\r\n");
                if (!string.IsNullOrEmpty(robocopyErrorOutput))
                {
                    File.AppendAllText(logFile, "Salida de Error de CMD/Robocopy:\r\n" + robocopyErrorOutput + "\r\n");
                }

                // El exit code del proceso será el de robocopy, porque cmd.exe /c pasa el exit code del comando ejecutado.
                int exitCode = process.ExitCode;

                if (exitCode >= 0 && exitCode <= 7) // Éxito o advertencias leves de Robocopy
                {
                    listCopiaOK.Invoke((MethodInvoker)delegate {
                        listCopiaOK.Items.Add($"{stationIp} - ✓ Copiado.");
                    });
                }
                else // Errores graves (8, 16, etc.)
                {
                    string specificError = "Error desconocido.";
                    if (robocopyErrorOutput.Contains("ERROR "))
                    {
                        specificError = robocopyErrorOutput.Split(new[] { "ERROR " }, StringSplitOptions.None)[1].Split('\n')[0].Trim();
                    }
                    else if (robocopyOutput.Contains("ERROR "))
                    {
                        specificError = robocopyOutput.Split(new[] { "ERROR " }, StringSplitOptions.None)[1].Split('\n')[0].Trim();
                    }

                    throw new Exception($"Error al copiar a {stationIp}. Código: {exitCode}. Detalles: {specificError}");
                }
            }
        }
        /************************************************************************************/
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        /************************************************************************************/
        private void rbArchivos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbArchivos.Checked)
            {
                _currentSourceType = SourceType.File;
                chkDescomprimir.Enabled = true;
                UpdateSourceUI();
            }
        }

        private void rbCarpetas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCarpetas.Checked)
            {
                _currentSourceType = SourceType.Folder;
                chkDescomprimir.Enabled = false;
                UpdateSourceUI();
            }
        }

        private void btnLimpiarLista_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea limpiar toda la lista de elementos a copiar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _selectedSourceItems.Clear(); // Limpia todos los elementos de la lista enlazada
            }
        }

        private void btnEliminarSeleccionado_Click(object sender, EventArgs e)
        {
            // Obtener una lista de los elementos seleccionados en el DataGrid
            List<SourceItem> itemsToRemove = new List<SourceItem>();
            foreach (DataGridViewRow row in dataGridArchivosOrigen.SelectedRows)
            {
                if (row.DataBoundItem is SourceItem item)
                {
                    itemsToRemove.Add(item);
                }
            }

            if (itemsToRemove.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos un elemento para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"¿Está seguro que desea eliminar {itemsToRemove.Count} elemento(s) seleccionado(s) de la lista?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Eliminar los elementos de la lista _selectedSourceItems
                // Es importante iterar de atrás hacia adelante o usar un foreach en una copia
                // para evitar problemas con la modificación de la colección mientras se itera
                foreach (SourceItem item in itemsToRemove)
                {
                    _selectedSourceItems.Remove(item);
                }
                // El DataGridView se actualizará automáticamente
            }
        }
    }

    // Clase auxiliar para los elementos del DataGridView
    public class SourceItem
    {
        public string Tipo { get; set; } // "Archivo" o "Carpeta"
        public string Nombre { get; set; }
        public string RutaCompleta { get; set; }
        public string Tamano { get; set; } // Formato legible, ej. "1.2 MB", "250 KB"
        public DateTime FechaModificacion { get; set; }
    }
}
