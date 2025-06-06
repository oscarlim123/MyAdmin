using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VMAdmin
{
    public partial class FormSeleccionRangoIP : Form
    {
        //private DataGridView grid;
        //private TextBox txtBusqueda;

        // Propiedad para acceder al rango seleccionado
        public IpRange RangoSeleccionado { get; private set; }
        private List<IpRange> rangos = new List<IpRange>();
        public bool SeModificoListado { get; private set; } = false;
        private bool suspendValidating = false;

        // Constructor que recibe la lista de rangos
        public FormSeleccionRangoIP(List<IpRange> rangos)
        {
            InitializeComponent();
            ConfigurarInterfaz();
            CargarDatos(rangos);
        }

        /*********************************************************************/
        private void ConfigurarInterfaz()
        {
            FormHelperMio.StyleDataGridView(grid);

            grid.AutoGenerateColumns = false;

            // Eventos
            grid.CellDoubleClick += Grid_CellDoubleClick;
            grid.KeyDown += Grid_KeyDown;
            grid.CellEndEdit += (s, e) =>
            {
                if (grid.Columns[e.ColumnIndex].DataPropertyName == "Nombre")
                {
                    GuardarRangos(); // método que ya tienes para guardar en JSON
                    MessageBox.Show("Nombre del rango actualizado.", "Actualizado",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            grid.CellValidating += (s, e) =>
            {
                if (suspendValidating) return;

                if (grid.Columns[e.ColumnIndex].DataPropertyName == "Nombre")
                {
                    string nuevoNombre = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue?.ToString()?.Trim();

                    if (string.IsNullOrWhiteSpace(nuevoNombre))
                    {
                        MessageBox.Show("El nombre no puede estar vacío.", "Advertencia",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }

                    // Validar duplicados (ignora la fila actual en la comparación)
                    bool nombreDuplicado = rangos
                        .Where((r, index) => index != e.RowIndex)
                        .Any(r => r.Nombre.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase));

                    if (nombreDuplicado)
                    {
                        MessageBox.Show("Ya existe un rango con ese nombre.", "Duplicado",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;

                        // Selecciona el texto al volver al modo edición
                        grid.BeginInvoke(new Action(() =>
                        {
                            grid.CurrentCell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            grid.BeginEdit(true);
                        }));
                    }
                }
            };


            // Filtrado de búsqueda
            txtBusqueda.TextChanged += (s, e) =>
            {
                suspendValidating = true;
                var filtro = txtBusqueda.Text.ToLower();

                grid.DataSource = rangos
                    .Where(r => r.Nombre.ToLower().Contains(filtro)
                             || r.IpInicio.Contains(filtro)
                             || r.IpFin.Contains(filtro))
                    .OrderByDescending(r => r.FechaCreacion)
                    .ToList();
                suspendValidating = false;
            };

            btnSeleccionar.Click += (s, e) =>
            {
                if (grid.SelectedRows.Count > 0)
                {
                    this.RangoSeleccionado = grid.SelectedRows[0].DataBoundItem as IpRange;
                }
            };
        }

        /*********************************************************************/
        private void CargarDatos(List<IpRange> datos)
        {
            if (grid != null)
            {
                rangos = datos.OrderByDescending(r => r.FechaCreacion).ToList();
                grid.DataSource = rangos;
            }
        }

        /*********************************************************************/
        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid != null && e.RowIndex >= 0)
            {
                this.RangoSeleccionado = grid.Rows[e.RowIndex].DataBoundItem as IpRange;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /*********************************************************************/
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var grid = sender as DataGridView;
                if (grid?.SelectedRows.Count > 0)
                {
                    this.RangoSeleccionado = grid.SelectedRows[0].DataBoundItem as IpRange;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                e.Handled = true;
            }
        }
        /*********************************************************************/
        private void GuardarRangos()
        {
            string archivoConfig = "ipRanges.json";
            string json = JsonSerializer.Serialize(rangos);
            File.WriteAllText(archivoConfig, json);
        }
        /*********************************************************************/

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione uno o más rangos para eliminar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Está seguro que desea eliminar los rangos seleccionados?",
                                          "Confirmar eliminación",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            // Obtener los rangos seleccionados
            var seleccionados = grid.SelectedRows
                                    .Cast<DataGridViewRow>()
                                    .Select(r => r.DataBoundItem as IpRange)
                                    .Where(r => r != null)
                                    .ToList();

            // Eliminar del listado
            foreach (var item in seleccionados)
            {
                rangos.Remove(item);
            }

            // Guardar y refrescar
            GuardarRangos();
            grid.DataSource = null;
            grid.DataSource = rangos.OrderByDescending(r => r.FechaCreacion).ToList();

            SeModificoListado = true;

            MessageBox.Show("Rangos eliminados correctamente.", "Eliminado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
