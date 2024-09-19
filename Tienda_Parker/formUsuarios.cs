using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tienda_Parker.Database;

namespace Tienda_Parker
{
    public partial class formUsuarios : Form
    {
        public formUsuarios()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            txtUser.Clear();
            txtPass.Clear();
            cmbRol.SelectedIndex = -1;
            txtUser.Focus();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text)|| string.IsNullOrEmpty(cmbRol.Text))
            {
                MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Usuarios Nuevo = new Usuarios(unitOfWork1);
            Nuevo.Usuario = txtUser.Text;
            Nuevo.Contrasena = txtPass.Text;
            Nuevo.Roles=cmbRol.Text;

            Nuevo.Save();
            unitOfWork1.CommitChanges();

            MessageBox.Show("Guardado con Exito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            xpCollectionUsuario.Reload();
            Limpiar();
        }
    }
}
