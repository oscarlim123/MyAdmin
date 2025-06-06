using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VMAdmin
{
    internal class FormHelperMio
    {
        // Importar la función para redondear esquinas
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        /// <summary>
        /// Método para redondear formularios.
        /// </summary>
        /// <param name="form">El formulario a redondear.</param>
        /// <param name="cornerRadius">El radio de las esquinas.</param>
        public static void ApplyRoundedCorners(Form form, int cornerRadius)
        {
            form.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, form.Width, form.Height, cornerRadius, cornerRadius));
        }

        /**************************************************************************************/

        /// <summary>
        /// Método para aplicar estilo a un DataGridView.
        /// </summary>
        /// <param name="dgv">El DataGridView a estilizar.</param>
        public static void StyleDataGridView(DataGridView dgv)
        {
            // Configuración general
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.LightGray;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;

            // Estilo de encabezado
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.CornflowerBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Estilo de celdas
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightSlateGray;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            // Bordes de celdas
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Ajustar ancho de columnas al tamaño del grid
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /**************************************************************************************/

        /// <summary>
        /// Método para aplicar el estilo de tarjeta a un Panel.
        /// </summary>
        /// <param name="panel">El panel a estilizar.</param>
        /// <param name="cornerRadius">Radio de las esquinas.</param>
        /// <param name="backgroundColor">Color de fondo del panel.</param>
        public static void ApplyPanelStyle(Panel panel, int cornerRadius, Color backgroundColor, Color borderColor, int borderWidth = 1)
        {
            panel.BackColor = backgroundColor;

            // Aplicar bordes redondeados correctamente
            panel.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panel.Width, panel.Height, cornerRadius, cornerRadius));


        }

        
    }



    //**************************************************************************************/
    // Clase auxiliar para manejar la conexión de red de forma segura
    /**************************************************************************************/

    public class NetworkConnection : IDisposable
    {
        private string _networkName;
        private bool _connected = false;

        public NetworkConnection(string networkName, NetworkCredential credentials)
        {
            _networkName = networkName;

            // Estructura para pasar credenciales de forma segura
            var netResource = new NETRESOURCE
            {
                dwType = RESOURCETYPE_DISK,
                lpRemoteName = networkName
            };

            // Conectar al recurso de red
            int result = WNetAddConnection2(
                netResource,
                credentials.Password,
                credentials.UserName,
                0);

            if (result != 0)
            {
                throw new Win32Exception(result);
            }

            _connected = true;
        }

        ~NetworkConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_connected)
            {
                // Desconectar del recurso de red cuando se elimine este objeto
                WNetCancelConnection2(_networkName, 0, true);
                _connected = false;
            }
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NETRESOURCE netResource,
            string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags,
            bool force);

        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public int dwScope = 0;
            public int dwType = 0;
            public int dwDisplayType = 0;
            public int dwUsage = 0;
            public string lpLocalName = null;
            public string lpRemoteName = null;
            public string lpComment = null;
            public string lpProvider = null;
        }

        private const int RESOURCETYPE_DISK = 0x00000001;
    }

    //**************************************************************************************/
    // Clase auxiliar para definir la estructura de datos de las sesiones que se desean guardar.
    // Contiene métodos generales para el trabajo con la red.
    /**************************************************************************************/

    public class IpRange
    {
        public string Nombre { get; set; }
        public string IpInicio { get; set; }
        public string IpFin { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedad solo para visualización
        public string DisplayText => $"{Nombre.PadRight(20)} | {IpInicio} → {IpFin} | Creado: {FechaCreacion:dd/MM/yyyy}";


        /// <summary>
        /// Método para calcular total de direcciones IP.
        /// </summary>
        /// <param name="ipInicio">Dirección IP inicial.</param>
        /// <param name="ipFin">Dirección IP final.</param>
        public static long CalcularTotalIPs(string ipInicio, string ipFin)
        {
            try
            {
                // Convertir las IPs a valores numéricos
                long inicio = ConvertirIpANumero(ipInicio);
                long fin = ConvertirIpANumero(ipFin);

                // Calcular la diferencia (incluyendo ambas IPs en el conteo)
                return fin - inicio + 1;
            }
            catch
            {
                return -1; // Retorna -1 si hay error en el formato
            }
        }

        private static long ConvertirIpANumero(string ip)
        {
            if (!IPAddress.TryParse(ip, out IPAddress address))
                throw new FormatException("Formato de IP inválido");

            byte[] bytes = address.GetAddressBytes();

            // Convertir la IP a un número largo (big-endian)
            long resultado = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                resultado += bytes[i] * (long)Math.Pow(256, 3 - i);
            }

            return resultado;
        }
    }
}
