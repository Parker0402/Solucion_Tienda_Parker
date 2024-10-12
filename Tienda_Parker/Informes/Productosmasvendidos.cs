using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Filtering.Templates;
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
    public partial class Productosmasvendidos : DevExpress.XtraEditors.XtraForm
    {
        public Productosmasvendidos()
        {
            InitializeComponent();
        }

        private void xpCollectionDetalleVentas_ListChanged(object sender, ListChangedEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();

        }

        private void btnCargarDatos_Click(object sender, EventArgs e)
        {
            DateTime INICIAL = DateTime.Parse(deFechaInicial.Text);
            DateTime Final = DateTime.Parse(deFechaFinal.Text);
            ObtenerProductoMasVendido(INICIAL, Final);
        }

        private void ObtenerProductoMasVendido(DateTime deFechaInicial, DateTime deFechaFinal)
        {
            // Filtrar las ventas entre las dos fechas
            var ventasFiltradas = xpCollectionDetalleVentas
                .OfType<Detalle_facturas>() // Suponiendo que xpCollectionDetalleVentas contiene objetos de tipo DetalleVenta
                .Where(venta => venta.Factura_id.Fecha_factura >= deFechaInicial && venta.Factura_id.Fecha_factura <= deFechaFinal)
                .ToList();

            // Agrupar por producto y sumar las cantidades vendidas
            var productosVendidos = ventasFiltradas
                .GroupBy(venta => venta.Producto_id)
                .Select(grupo => new
                {
                    Producto = grupo.Key,
                    CantidadVendida = grupo.Sum(venta => venta.Cantidad)
                })
                .OrderByDescending(grupo => grupo.CantidadVendida)
                .FirstOrDefault();

            // Mostrar el producto más vendido
            if (productosVendidos != null)
            {
                var productoMasVendido = productosVendidos.Producto;
                var cantidadVendida = productosVendidos.CantidadVendida;

                // Aquí puedes mostrar o hacer lo que necesites con el producto más vendido
                MessageBox.Show($"Producto más vendido: {productoMasVendido.Nombre} con {cantidadVendida} unidades.");

                // Ahora cargamos los detalles de las ventas de este producto más vendido en el GridControl
                var detalleVentasProducto = ventasFiltradas
                    .Where(venta => venta.Producto_id == productoMasVendido)
                    .ToList();

                // Asignamos los detalles filtrados al GridControl
                gridControl1.DataSource = detalleVentasProducto;
                gridControl1.RefreshDataSource();
            }
            else
            {
                MessageBox.Show("No se encontraron ventas en el rango de fechas seleccionado.");
            }
        }

        private void Productosmasvendidos_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
        }
    }
}