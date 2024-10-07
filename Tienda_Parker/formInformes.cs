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
    public partial class formInformes : Form
    {
        public formInformes()
        {
            InitializeComponent();
        }

        private void btnVentasPorFechas_Click(object sender, EventArgs e)
        {

            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<Informes.InformeVentasPorRangoDeFechas>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new Informes.InformeVentasPorRangoDeFechas { MdiParent = this };
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
