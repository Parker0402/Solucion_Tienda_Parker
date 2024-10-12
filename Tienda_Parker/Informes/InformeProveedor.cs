using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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

namespace Tienda_Parker.Informes
{
    public partial class InformeProveedor : DevExpress.XtraEditors.XtraForm
    {
        public InformeProveedor()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void slueProveedor_EditValueChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado en el searchLookupEdit
            var proveedorSeleccionado = slueProveedor.EditValue;

            // Si proveedorSeleccionado es null, cargamos todos los registros
            if (proveedorSeleccionado == null)
            {
                xpCollectionEntradaInv.Criteria = null; // Elimina cualquier filtro para cargar todo
            }
            else
            {
                // Si se seleccionó un proveedor, filtramos por ese proveedor
                xpCollectionEntradaInv.Criteria = CriteriaOperator.Parse("Proveedor_id = ?", proveedorSeleccionado);
            }

            // Recargar los datos en el grid
            gridControl1.DataSource = xpCollectionEntradaInv;
            gridControl1.RefreshDataSource();
        }
    }
}