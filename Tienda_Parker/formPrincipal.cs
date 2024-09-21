using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tienda_Parker
{
    public partial class formPrincipal : Form
    {
        public formPrincipal()
        {
            InitializeComponent();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formUsuarios>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formUsuarios { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formEmpleado>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formEmpleado { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formProducto>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formProducto { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formProveedores>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formProveedores { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formEntradas_Inventario>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formEntradas_Inventario { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }
    }
}
