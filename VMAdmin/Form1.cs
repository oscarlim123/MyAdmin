using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace VMAdmin
{
    public partial class FrmVMAdmin : Form
    {
        private int total = 0;
        private int totalActivas = 0;
        private int totalInactivas = 0;
        private int totalRDP = 0;
        private int totalSSH = 0;
        private int totalTelnet = 0;
        private int totalHTTP = 0;
        private int totalHTTPS = 0;
        private int totalSMB = 0;
        private int totalWindowsON = 0;
        private CancellationTokenSource _cancelacion;

        public FrmVMAdmin()
        {
            InitializeComponent();
        }

        private void FrmVMAdmin_Load(object sender, EventArgs e)
        {

            //FormHelperMio.ApplyRoundedCorners(this, 20); // Esquinas redondeadas
            labelTotal.Text = "0";
            labelActivas.Text = "0";
            labelRDP.Text = "0";
            labelSSH.Text = "0";
            labelTelnet.Text = "0";
            labelHTTP.Text = "0";
            labelHTTPS.Text = "0";

            FormHelperMio.StyleDataGridView(dataGridIPs);
            CargarRangosGuardados();
        }

        private void ResetCounter()
        {
           
        }
        /********************************************************************************/
        /// <summary>
        /// Convierte una dirección IP en formato string a su representación numérica unsigned integer (32 bits)
        /// </summary>
        /// <param name="ip">Dirección IP en formato IPv4 (ej. "192.168.1.1")</param>
        /// <returns>Valor uint que representa la dirección IP</returns>
        /// <exception cref="FormatException">Se lanza cuando el formato de IP no es válido</exception>
        /// <exception cref="OverflowException">Se lanza cuando algún octeto excede el rango 0-255</exception>
        /// <example>
        /// <code>
        /// uint numericIp = IpToUint("192.168.1.1");
        /// // Devuelve 3232235777
        /// </code>
        /// </example>
        private uint IpToUint(string ip)
        {
            string[] parts = ip.Split('.');
            return (uint)(int.Parse(parts[0]) << 24 |
                        int.Parse(parts[1]) << 16 | 
                        int.Parse(parts[2]) << 8 | 
                        int.Parse(parts[3]));
        }

        /********************************************************************************/
        /// <summary>
        /// Convierte un valor numérico unsigned integer (32 bits) a su representación en formato de dirección IPv4
        /// </summary>
        /// <param name="ip">Valor numérico que representa una dirección IP (ej. 3232235777)</param>
        /// <returns>Cadena que representa la dirección IP en formato IPv4 (ej. "192.168.1.1")</returns>
        /// <remarks>
        /// Esta función realiza la operación inversa de <see cref="IpToUint(string)"/>.
        /// El valor de entrada se interpreta en orden de bytes big-endian (network byte order).
        /// </remarks>
        /// <example>
        /// <code>
        /// string ipAddress = UintToIp(3232235777);
        /// // Devuelve "192.168.1.1"
        /// </code>
        /// </example>
        private string UintToIp(uint ip)
        {
            if (ip == 0xFFFFFFFF) // 255.255.255.255
            {
                throw new ArgumentOutOfRangeException(nameof(ip), "La dirección de broadcast no está permitida");
            }
            return string.Join(".",
                (ip >> 24) & 0xFF,
                (ip >> 16) & 0xFF,
                (ip >> 8) & 0xFF,
                ip & 0xFF);
        }

        /********************************************************************************/
        /// <summary>
        /// Genera una lista secuencial de direcciones IP dentro de un rango especificado
        /// </summary>
        /// <param name="inicio">Dirección IP inicial del rango (inclusive) en formato IPv4</param>
        /// <param name="fin">Dirección IP final del rango (inclusive) en formato IPv4</param>
        /// <returns>Lista de strings con todas las direcciones IP en el rango especificado</returns>
        /// <exception cref="ArgumentException">
        /// Se lanza cuando:
        /// - La IP inicial es mayor que la IP final
        /// - Las IPs no pertenecen al mismo bloque de red clase C
        /// </exception>
        /// <exception cref="FormatException">Se lanza cuando alguna IP tiene formato inválido</exception>
        /// <remarks>
        /// Este método es útil para generar listas de IPs en subredes clase C (/24).
        /// Para rangos grandes (>254 IPs), considere usar un enfoque iterativo o paginado.
        /// </remarks>
        /// <example>
        /// <code>
        /// var ips = GenerarRangoDeIPs("192.168.1.1", "192.168.1.5");
        /// // Devuelve: ["192.168.1.1", "192.168.1.2", "192.168.1.3", "192.168.1.4", "192.168.1.5"]
        /// </code>
        /// </example>
        /// <seealso cref="IpToUint(string)"/>
        /// <seealso cref="UintToIp(uint)"/>
        private List<string> GenerarRangoDeIPs(string inicio, string fin)
        {
            // Validación de rango
            uint ipInicio = IpToUint(inicio);
            uint ipFin = IpToUint(fin);

            if (ipInicio > ipFin)
            {
                throw new ArgumentException("La IP inicial no puede ser mayor que la IP final", nameof(inicio));
            }

            int totalIPs = (int)(ipFin - ipInicio) + 1;

            // Validación para rangos muy grandes
            if (totalIPs > 65536) // Límite razonable
            {
                throw new ArgumentException("El rango no puede exceder 65536 direcciones IP");
            }

            var lista = new List<string>(capacity: totalIPs);

            for (uint i = ipInicio; i <= ipFin; i++)
            {
                lista.Add(UintToIp(i));
            }

            return lista;

            /*
            var lista = new List<string>();
            uint ipInicio = IpToUint(inicio);
            uint ipFin = IpToUint(fin);

            for (uint i = ipInicio; i <= ipFin; i++)
            {
                lista.Add(UintToIp(i));
            }

            return lista;
            */
        }

        /********************************************************************************/

        /// <summary>
        /// Determina si dos direcciones IP pertenecen al mismo bloque de red Clase C (/24)
        /// </summary>
        /// <param name="ip1">Primera dirección IP en formato IPv4 (ej. "192.168.1.10")</param>
        /// <param name="ip2">Segunda dirección IP en formato IPv4 (ej. "192.168.1.20")</param>
        /// <returns>
        /// True si ambas IPs comparten los primeros tres octetos (misma red y subred), False en caso contrario
        /// </returns>
        /// <exception cref="FormatException">
        /// Se lanza cuando alguna dirección IP no tiene un formato válido (no contiene exactamente 4 octetos)
        /// </exception>
        /// <remarks>
        /// Este método es útil para validar que direcciones IP pertenezcan a la misma subred /24 (máscara 255.255.255.0)
        /// antes de realizar operaciones con rangos de IPs.
        /// </remarks>
        /// <example>
        /// <code>
        /// bool mismoBloque = EsMismoBloque24("192.168.1.10", "192.168.1.20"); // Devuelve true
        /// bool mismoBloque = EsMismoBloque24("192.168.1.10", "192.168.2.20"); // Devuelve false
        /// </code>
        /// </example>
        private bool EsMismoBloque24(string ip1, string ip2)
        {
            string[] p1 = ip1.Split('.');
            string[] p2 = ip2.Split('.');
            return p1[0] == p2[0] && p1[1] == p2[1] && p1[2] == p2[2];
        }

        /********************************************************************************/

        private bool ValidarRangoClaseC(string inicio, string fin)
        {
            if (!EsMismoBloque24(inicio, fin))
            {
                MessageBox.Show("Solo se permiten rangos de Clase C dentro de la misma subred (ej. 172.0.0.1 a 172.0.0.254)");
                return false;
            }

            if (IpToUint(inicio) > IpToUint(fin))
            {
                MessageBox.Show("La IP de inicio debe ser menor o igual que la IP final.");
                return false;
            }

            if (!EsIPValida(inicio) || !EsIPValida(fin))
            {
                MessageBox.Show("Una o ambas IPs no tienen un formato válido.");
                return false;
            }

            return true;
        }

        /********************************************************************************/

        private bool EsIPValida(string ip)
        {
            return System.Net.IPAddress.TryParse(ip, out _);
        }

        /********************************************************************************/

        private async Task<bool> HacerPingAsync(string ip)
        {
            using (var ping = new Ping())
            {
                try
                {
                    var reply = await ping.SendPingAsync(ip, 1000);
                    return reply.Status == IPStatus.Success;
                }
                catch
                {
                    return false;
                }
            }
        }

        /********************************************************************************/

        private async Task EscanearIPsAsync(CancellationToken token, List<string> ips, int maxConcurrentTasks = 20)
        {
            progressBarScan.Minimum = 0;
            progressBarScan.Maximum = ips.Count;
            progressBarScan.Value = 0;

            btnScan.Enabled = false;
            var semaphore = new SemaphoreSlim(maxConcurrentTasks);
            var tasks = new List<Task>();

            foreach (var ip in ips)
            {
                // Cancelado el scaneo por el usuario
                if (token.IsCancellationRequested)
                {
                    MessageBox.Show("Escaneo finalizado o cancelado.");
                    break; 
                    
                }

                total++;

                await semaphore.WaitAsync();

                var task = Task.Run(async () =>
                {
                    bool activa = await HacerPingAsync(ip);
                    bool rdp = false;
                    bool ssh = false;
                    bool telnet = false;
                    bool http = false;
                    bool https = false;
                    bool smbOpen = false;//*******
                    bool isLikelyWindows = false;//******

                    // Comprobamos puerto RDP
                    if (activa)
                    {
                        totalActivas++;
                        rdp = await PuertoAbiertoAsync(ip, 3389);
                        ssh = await PuertoAbiertoAsync(ip, 22);
                        telnet = await PuertoAbiertoAsync(ip, 23);
                        http = await PuertoAbiertoAsync(ip, 80, 500);
                        https = await PuertoAbiertoAsync(ip, 443, 500);
                        smbOpen = await PuertoAbiertoAsync(ip, 445);  // Puerto SMB (compartición Windows) //*************
                        isLikelyWindows = smbOpen && (await GetTtlAsync(ip) >= 120);  // TTL de Windows usualmente 128 //***********

                        if (rdp) { totalRDP++; }
                        if (ssh) { totalSSH++; }
                        if (telnet) { totalTelnet++; }
                        if (http) { totalHTTP++; }
                        if (https) { totalHTTPS++; }
                        if (smbOpen) { totalSMB++; }
                    }
                    else
                    {
                        totalInactivas++;
                    }

                    string estadoTexto = activa ? "✅ Activo" : "❌ Inactivo";
                    string estadoRDP = rdp ? "✅" : "";
                    string estadoSSH = ssh ? "✅" : "";
                    string estadoTelnet = telnet ? "✅" : "";
                    string estadoHTTP = http ? "✅" : "";
                    string estadoHTTPS = https ? "✅" : "";
                    string estadoSMB = smbOpen ? "✅" : "";
                    string isWindows = isLikelyWindows ? "Win" : "";

                    int rowIndex = -1;
                    Invoke(new Action(() =>
                    {
                        rowIndex = dataGridIPs.Rows.Add(
                            ip,
                            (activa) ? "Resolviendo..." : "N/A",
                            isWindows,
                            estadoTexto,
                            estadoRDP,
                            estadoSSH,
                            estadoTelnet,
                            estadoHTTP,
                            estadoHTTPS,
                            estadoSMB
                        );

                        progressBarScan.PerformStep();
                        ActualizarEstadisticas();
                    }));


                    // Resolvemos DNS de forma asíncrona
                    if (activa)
                    {
                        _ = Task.Run(() =>
                        {
                            string host;
                            try
                            {
                                host = Dns.GetHostEntry(ip).HostName;
                            }
                            catch
                            {
                                host = "N/A";
                            }

                            Invoke(new Action(() =>
                            {
                                if (rowIndex >= 0 && rowIndex < dataGridIPs.Rows.Count)
                                {
                                    dataGridIPs.Rows[rowIndex].Cells["HOST"].Value = host;
                                }
                            }));
                        });
                    }
                    semaphore.Release();
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            labelTotal.Text = total.ToString();
            labelActivas.Text = totalActivas.ToString();
            labelRDP.Text = totalRDP.ToString();
            labelSSH.Text = totalSSH.ToString();
            labelTelnet.Text = totalTelnet.ToString();
            labelHTTP.Text = totalHTTP.ToString();
            labelHTTPS.Text = totalHTTPS.ToString();

            btnScan.Enabled = true;
        }

        /********************************************************************************/

        private async Task<int> GetTtlAsync(string ip)
        {
            using (var ping = new Ping())
            {
                try
                {
                    var reply = await ping.SendPingAsync(ip, 1000);
                    return reply.Options?.Ttl ?? 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        /********************************************************************************/

        // Verifica si un puerto está abierto
        private async Task<bool> PuertoAbiertoAsync(string ip, int puerto, int timeout = 1000)
        {
            try
            {
                using (var cliente = new TcpClient())
                {
                    var tarea = cliente.ConnectAsync(ip, puerto);
                    var resultado = await Task.WhenAny(tarea, Task.Delay(timeout));
                    return resultado == tarea && cliente.Connected;
                }
            }
            catch
            {
                return false;
            }
        }

        /********************************************************************************/

        private void ActualizarEstadisticas()
        {
            labelTotal.Text = total.ToString();
            labelActivas.Text = totalActivas.ToString();
            labelRDP.Text = totalRDP.ToString();
            labelSSH.Text = totalSSH.ToString();
            labelTelnet.Text = totalTelnet.ToString();
            labelHTTP.Text = totalHTTP.ToString();
            labelHTTPS.Text = totalHTTPS.ToString();
            /*
            labelTotal.Text = "0";
            labelActivas.Text = "0";
            labelRDP.Text = "0";
            labelSSH.Text = "0";
            labelTelnet.Text = "0";
            labelHTTP.Text = "0";
            labelHTTPS.Text = "0";
            */
        }


        /********************************************************************************/

        private async void btnScan_Click(object sender, EventArgs e)
        {
            _cancelacion = new CancellationTokenSource();
            btnCancelar.Enabled = true;

            labelTotal.Text = labelTotal.ToString();
            ActualizarEstadisticas(); // Para poner todo en cero visualmente

            if (!ValidarRangoClaseC(TextBoxInicio.Text, TextBoxFin.Text))
                return;

            dataGridIPs.Rows.Clear(); // Limpia resultados anteriores

            var listaIPs = GenerarRangoDeIPs(TextBoxInicio.Text, TextBoxFin.Text);
            await EscanearIPsAsync(_cancelacion.Token,listaIPs, 20); // Se puede variar el número de tareas
            btnCancelar.Enabled = false;
            progressBarScan.Value = 0;
        }

        /********************************************************************************/

        private void seleccionarOctetoIP()
        {
            string[] segmentos = TextBoxFin.Text.Split('.');
            if (segmentos.Length == 4)
            {
                int start = TextBoxFin.Text.LastIndexOf('.') + 1;
                int length = TextBoxFin.Text.Length - start;
                TextBoxFin.SelectionStart = start;
                TextBoxFin.SelectionLength = length;
            }
        }

        /********************************************************************************/
        private void FiltrarDataGrid(Func<DataGridViewRow, bool> condicion)
        {
            foreach (DataGridViewRow fila in dataGridIPs.Rows)
            {
                fila.Visible = condicion(fila);
            }
        }

        /********************************************************************************/
        private void dataGridIPs_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dataGridIPs.HitTest(e.X, e.Y);
                if (hitTestInfo.RowIndex >= 0)
                {
                    dataGridIPs.ClearSelection();
                    dataGridIPs.Rows[hitTestInfo.RowIndex].Selected = true;
                }
            }
        }

        /********************************************************************************/

        private void escritorioRemotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridIPs.SelectedRows.Count > 0)
            {
                string ip = dataGridIPs.SelectedRows[0].Cells["IP"].Value.ToString();
                try
                {
                    System.Diagnostics.Process.Start("mstsc", $"/v:{ip}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al intentar conectar por RDP: {ex.Message}");
                }
            }
        }

        /********************************************************************************/

        private void dataGridIPs_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dataGridIPs.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    // Seleccionamos la fila bajo el cursor
                    dataGridIPs.ClearSelection();
                    dataGridIPs.Rows[hit.RowIndex].Selected = true;

                    // Opcional: establecer la celda activa
                    dataGridIPs.CurrentCell = dataGridIPs.Rows[hit.RowIndex].Cells[0];
                }
            }
        }

        /********************************************************************************/

        // Activar o desactivar "Escritorio Remoto", "SSH", "Telnet", "HTTP", "HTTPS" en el menú contextual
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dataGridIPs.SelectedRows.Count > 0)
            {
                var fila = dataGridIPs.SelectedRows[0];

                // Suponiendo que la columna "RDP" tiene ✅ o ❌
                string estadoRdp = fila.Cells["RDP"].Value?.ToString() ?? "";
                string estadoSSH = fila.Cells["SSH"].Value?.ToString() ?? "";
                string estadoTelnet = fila.Cells["TELNET"].Value?.ToString() ?? "";
                string estadoHTTP = fila.Cells["HTTP"].Value?.ToString() ?? "";
                string estadoHTTPS = fila.Cells["HTTPS"].Value?.ToString() ?? "";
                string so = fila.Cells["SO"].Value?.ToString() ?? ""; //********
                string smbStatus = fila.Cells["SMB"].Value?.ToString() ?? ""; //*********

                // Activa solo si el estado es "✅"
                escritorioRemotoToolStripMenuItem.Enabled = estadoRdp == "✅";
                sSHToolStripMenuItem.Enabled = estadoSSH == "✅";
                telnetToolStripMenuItem.Enabled = estadoTelnet == "✅";
                hTTPToolStripMenuItem.Enabled = estadoHTTP == "✅";
                httpsToolStripMenuItem1.Enabled = estadoHTTPS == "✅";
                // Mostrar "Abrir C$" solo para Windows con SMB abierto
                abrirCToolStripMenuItem.Visible = (so == "Win" && smbStatus == "✅");

                //btnRDP.Enabled = true;
            }
            else
            {
                escritorioRemotoToolStripMenuItem.Enabled = false;
                sSHToolStripMenuItem.Enabled = false;
                telnetToolStripMenuItem.Enabled = false;
                hTTPToolStripMenuItem.Enabled = false;
                httpsToolStripMenuItem1.Enabled = false;
                //btnRDP.Enabled = false;
            }
        }

        /********************************************************************************/

        private void sSHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string puttyPath = Path.Combine(Application.StartupPath, "Recursos", "putty.exe");
            string ip = dataGridIPs.SelectedRows[0].Cells["IP"].Value.ToString();

            if (File.Exists(puttyPath))
            {
                Process.Start(puttyPath, $"-ssh {ip}");
            }
            else
            {
                MessageBox.Show("No se encontró putty.exe en la carpeta Recursos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /********************************************************************************/
        private void TextBoxInicio_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void TextBoxInicio_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void TextBoxInicio_TextChanged(object sender, EventArgs e)
        {
            TextBoxFin.Text = TextBoxInicio.Text;
        }

        private void TextBoxFin_Enter(object sender, EventArgs e)
        {
            seleccionarOctetoIP();
        }

        private void TextBoxFin_MouseClick(object sender, MouseEventArgs e)
        {
            seleccionarOctetoIP();
        }

        private void labelRDP_Click(object sender, EventArgs e)
        {
            FiltrarDataGrid(fila => fila.Cells["RDP"].Value.ToString() == "✅");
        }

        private void labelSSH_Click(object sender, EventArgs e)
        {
            FiltrarDataGrid(fila => fila.Cells["SSH"].Value.ToString() == "✅");
        }

        private void labelActivas_Click(object sender, EventArgs e)
        {
            FiltrarDataGrid(fila => fila.Cells["Estado"].Value.ToString().Contains("Activo"));
        }

        private void labelTotal_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow fila in dataGridIPs.Rows)
            {
                fila.Visible = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _cancelacion?.Cancel();
            
        }

        private void labelTelnet_Click(object sender, EventArgs e)
        {
            FiltrarDataGrid(fila => fila.Cells["TELNET"].Value.ToString() == "✅");
        }

        private void labelHTTP_Click(object sender, EventArgs e)
        {
            FiltrarDataGrid(fila => fila.Cells["HTTP"].Value.ToString() == "✅");
        }

        private void labelHTTPS_Click(object sender, EventArgs e)
        {
            FiltrarDataGrid(fila => fila.Cells["HTTPS"].Value.ToString() == "✅");
        }

        /********************************************************************************/

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridIPs.SelectedRows.Count == 0) return;

            string estadoHTTP = dataGridIPs.SelectedRows[0].Cells["HTTPS"].Value?.ToString() ?? "";
            if (estadoHTTP != "✅")
            {
                MessageBox.Show("El puerto HTTPS (443) no está disponible.");
                return;
            }

            string ip = dataGridIPs.SelectedRows[0].Cells["IP"].Value.ToString();
            Process.Start(new ProcessStartInfo($"https://{ip}") { UseShellExecute = true });
        }

        /********************************************************************************/

        private void telnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string puttyPath = Path.Combine(Application.StartupPath, "Recursos", "putty.exe");
            string ip = dataGridIPs.SelectedRows[0].Cells["IP"].Value.ToString();

            if (File.Exists(puttyPath))
            {
                Process.Start(puttyPath, $"-telnet {ip}"); // Usar PuTTY para Telnet
            }
            else
            {
                // Alternativa: Usar el cliente Telnet de Windows (necesita habilitarse en "Características de Windows")
                Process.Start("cmd.exe", $"/c start telnet {ip}");
            }
        }

        /********************************************************************************/

        private void hTTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridIPs.SelectedRows.Count == 0) return;

            string estadoHTTP = dataGridIPs.SelectedRows[0].Cells["HTTP"].Value?.ToString() ?? "";
            if (estadoHTTP != "✅")
            {
                MessageBox.Show("El puerto HTTP (80) no está disponible.");
                return;
            }

            string ip = dataGridIPs.SelectedRows[0].Cells["IP"].Value.ToString();
            Process.Start(new ProcessStartInfo($"http://{ip}") { UseShellExecute = true }); // Abre en navegador
        }

        private void hTTPToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            hTTPToolStripMenuItem.ToolTipText = hTTPToolStripMenuItem.Enabled ? "Abrir en navegador" : "Puerto HTTP (80) cerrado";
        }

        private void httpsToolStripMenuItem1_MouseHover(object sender, EventArgs e)
        {
            httpsToolStripMenuItem1.ToolTipText = httpsToolStripMenuItem1.Enabled ? "Abrir en navegador" : "Puerto HTTP (443) cerrado";
        }

        private void telnetToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            telnetToolStripMenuItem.ToolTipText = telnetToolStripMenuItem.Enabled ? "Abrir conexión telnet" : "Puerto 23 cerrado";
        }

        /********************************************************************************/

        private readonly SharedResourceManager _resourceManager = new SharedResourceManager();

        private void abrirCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridIPs.SelectedRows.Count == 0) return;


            string ip = dataGridIPs.SelectedRows[0].Cells["IP"].Value?.ToString();
            if (string.IsNullOrEmpty(ip)) return;

            // Crear un formulario para solicitar credenciales
            Form credentialsForm = new Form();
            credentialsForm.Text = $"Credenciales para {ip}";
            credentialsForm.Size = new Size(390, 190);
            credentialsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            credentialsForm.StartPosition = FormStartPosition.CenterParent;
            credentialsForm.MaximizeBox = false;
            credentialsForm.MinimizeBox = false;

            Label userLabel = new Label();
            userLabel.Text = "Usuario:";
            userLabel.Location = new Point(20, 20);
            userLabel.Size = new Size(80, 20);
            credentialsForm.Controls.Add(userLabel);

            TextBox userTextBox = new TextBox();
            userTextBox.Location = new Point(130, 20);
            userTextBox.Size = new Size(210, 20);
            credentialsForm.Controls.Add(userTextBox);

            Label passLabel = new Label();
            passLabel.Text = "Contraseña:";
            passLabel.Location = new Point(20, 50);
            passLabel.Size = new Size(100, 20);
            credentialsForm.Controls.Add(passLabel);

            TextBox passTextBox = new TextBox();
            passTextBox.Location = new Point(130, 50);
            passTextBox.Size = new Size(210, 20);
            passTextBox.PasswordChar = '*';
            credentialsForm.Controls.Add(passTextBox);

            Button okButton = new Button();
            okButton.Text = "Aceptar";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(130, 90);
            okButton.Size = new Size(100, 35);
            credentialsForm.Controls.Add(okButton);
            credentialsForm.AcceptButton = okButton;

            if (credentialsForm.ShowDialog() == DialogResult.OK)
            {
                string username = userTextBox.Text;
                string password = passTextBox.Text;

                // Usar las credenciales con el recurso compartido
                string rutaUNC = $@"\\{ip}\C$";


                /* TRY CORRECTO
                try
                {
                    // Establecer la conexión con credenciales
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.Arguments = $"/c net use {rutaUNC} /user:{username} {password} && explorer {rutaUNC}";
                    psi.CreateNoWindow = true;
                    psi.UseShellExecute = false;

                    Process p = Process.Start(psi);
                    p.WaitForExit();

                    if (p.ExitCode == 0)
                    {
                        //MessageBox.Show("Error al conectar. Verifica las credenciales.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al acceder: {ex.Message}");
                }  
                */




                try
                {
                    // Establecer conexión usando la API de .NET en lugar de cmd
                    using (new NetworkConnection(rutaUNC, new NetworkCredential(username, password)))
                    {
                        // Abrir el explorador en la ruta especificada
                        Process.Start("explorer.exe", rutaUNC);

                        // Opcional: Mostrar mensaje de éxito
                        // MessageBox.Show($"Conexión establecida con éxito a {rutaUNC}", "Conexión Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // La conexión se limpiará automáticamente al salir del bloque using
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al acceder: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /********************************************************************************/

        private void btnAgregarRango_Click(object sender, EventArgs e)
        {
            // Mostrar diálogo para nombre
            using (var dialog = new FormInputNombreRango())
            {
                dialog.RangoIP = $"{TextBoxInicio.Text} - {TextBoxFin.Text}";

                // Sugerir nombre automático
                dialog.NombreRango = $"Rango_{TextBoxInicio.Text.Replace(".", "_")}_a_{TextBoxFin.Text.Replace(".", "_")}";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var nuevoRango = new IpRange
                    {
                        Nombre = dialog.NombreRango,
                        IpInicio = TextBoxInicio.Text,
                        IpFin = TextBoxFin.Text,
                        FechaCreacion = DateTime.Now
                    };

                    rangosGuardados.Add(nuevoRango);
                    GuardarRangos();

                    MessageBox.Show($"Rango '{dialog.NombreRango}' guardado correctamente",
                                  "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /********************************************************************************/
        private void btnSeleccionarRango_Click(object sender, EventArgs e)
        {
            using (var selector = new FormSeleccionRangoIP(rangosGuardados))
            {                
                if (selector.ShowDialog() == DialogResult.OK && selector.RangoSeleccionado != null)
                {                   
                    TextBoxInicio.Text = selector.RangoSeleccionado.IpInicio;
                    TextBoxFin.Text = selector.RangoSeleccionado.IpFin;
                }
                // Recargar si hubo cambios
                if (selector.SeModificoListado)
                {
                    CargarRangosGuardados(); // vuelve a leer ipRanges.json
                }
            }
        }

        /**************************************************************************************
         * Sección de métodos para los rangos de IPs
         */
        private List<IpRange> rangosGuardados = new List<IpRange>();
        private readonly string archivoConfig = "ipRanges.json";

        // Cargar rangos guardados al iniciar
        private void CargarRangosGuardados()
        {
            if (File.Exists(archivoConfig))
            {
                string json = File.ReadAllText(archivoConfig);
                rangosGuardados = JsonSerializer.Deserialize<List<IpRange>>(json);
            }
        }

        // Guardar nuevos rangos
        private void GuardarRangos()
        {
            string json = JsonSerializer.Serialize(rangosGuardados);
            File.WriteAllText(archivoConfig, json);
        }

        /**
        * FIN Sección de métodos para los rangos de IPs
        */


        //*****************************************************************************************//
        private void btnSubir_Click(object sender, EventArgs e)
        {
            // 1. Obtener las IPs seleccionadas del DataGridView
            List<string> ipsSeleccionadas = new List<string>();

            foreach (DataGridViewRow row in dataGridIPs.SelectedRows)
            {
                if (row.Cells["IP"].Value != null)
                {
                    ipsSeleccionadas.Add(row.Cells["IP"].Value.ToString());
                }
            }

            // 2. Verificar que hay selección
            if (ipsSeleccionadas.Count == 0)
            {
                MessageBox.Show("Por favor seleccione al menos una estación",
                               "Advertencia",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            // 3. Abrir FormUpload pasando las IPs
            FormUpload formUpload = new FormUpload(ipsSeleccionadas);
            formUpload.ShowDialog(); // Muestra como diálogo modal
        }

        private void btnVerificarTrustedHosts_Click(object sender, EventArgs e)
        {

        }
        //*****************************************************************************************//
        private void btnSetTrustedHosts_Click(object sender, EventArgs e)
        {

        }

        private void btnBorrarTrustedHosts_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormConfig config = new FormConfig();
            config.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var selector = new FormSeleccionRangoIP(rangosGuardados))
            {
                if (selector.ShowDialog() == DialogResult.OK && selector.RangoSeleccionado != null)
                {
                    TextBoxInicio.Text = selector.RangoSeleccionado.IpInicio;
                    TextBoxFin.Text = selector.RangoSeleccionado.IpFin;
                }
            }
        }
    }






}
