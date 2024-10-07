using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using Tienda_Parker.Database;

namespace Tienda_Parker
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void formLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            bool usr = false;
            foreach (Usuarios U in xpCollectionUsuario)
            {
                if (U.Usuario.Equals(txtUsuario.Text) &&
                U.Contrasena.Equals(txtContrasena.Text))
                {
                    formPrincipal fp = new formPrincipal(U.Roles,U);
                    this.Visible = false;
                    fp.ShowDialog();
                    this.Visible = true;
                    txtContrasena.Clear();
                    txtUsuario.Clear();
                    txtUsuario.Focus();

                    usr = true;
                }
            }
            if (!usr)
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContrasena.Clear();
                txtUsuario.Clear();
                txtUsuario.Focus();
            }
        
        }
    }
}
