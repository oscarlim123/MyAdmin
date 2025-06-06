using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VMAdmin
{
    public partial class FormInputNombreRango : Form
    {
        public string NombreRango { get; set; }
        public string RangoIP { get; set; }

        public FormInputNombreRango()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarEntrada())
            {
                NombreRango = txtNombre.Text.Trim();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void FormInputNombreRango_Load(object sender, EventArgs e)
        {
            lblRango.Text = $"Rango IP: {RangoIP}";
            txtNombre.Select();
        }

        /**************************************************************************************/
        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para el rango", "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtNombre.Text.Length > 50)
            {
                MessageBox.Show("El nombre no puede exceder los 50 caracteres", "Error",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
