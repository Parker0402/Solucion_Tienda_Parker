using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
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
using static DevExpress.Data.Filtering.Helpers.SubExprHelper.ThreadHoppingFiltering;

namespace Tienda_Parker
{
    public partial class formGraficos : DevExpress.XtraEditors.XtraForm
    {
        public formGraficos()
        {
            InitializeComponent();
            CargarGrafico2();
            CargarGrafico1();
        }

        // Método para cargar y configurar el gráfico
        private void CargarGrafico2()
        {
            // Asignar xpCollectionHistorial_Ventas como fuente de datos
            xpCollectionHistorial_Ventas.Reload();

            // Agrupar datos por mes y calcular el total de ventas
            var ventasPorMes = xpCollectionHistorial_Ventas
            .OfType<Historial_ventas>()  // Asegurarse que está casteado a tu clase HistorialVentas
                .GroupBy(v => new { v.Fecha_factura.Year, v.Fecha_factura.Month })
                .Select(g => new
                {
                    Mes = g.Key.Month,
                    Año = g.Key.Year,
                    TotalVentas = g.Sum(v => v.Total)
                })
                .ToList();

            // Crear una nueva serie para el gráfico de barras
            Series series = new Series("Ventas por Mes", ViewType.Bar);

            // Asignar los valores a la serie
            series.ArgumentDataMember = "Mes";  // El argumento será el mes
            series.ValueDataMembers.AddRange(new string[] { "TotalVentas" });  // El valor será el total de ventas

            // Asignar la fuente de datos a la serie
            series.DataSource = ventasPorMes;

            // Agregar la serie al ChartControl
            chartControl2.Series.Add(series);

            // Formatear el eje X para mostrar Mes y Año
            XYDiagram diagram = (XYDiagram)chartControl2.Diagram;
            diagram.AxisX.Title.Text = "Mes";
            diagram.AxisX.Label.TextPattern = "{Año}-{Mes}";  // Formato Año-Mes

            // Refrescar el gráfico
            chartControl2.Refresh();
        }

        private void CargarGrafico1()
        {
            // Recargar la colección 
            xpCollection1.Reload();

            // Agrupar datos por mes, año y usuario, y calcular el total de ventas
            var ventasPorUsuario = xpCollection1
                .OfType<Facturas>()  // Cast de la colección a tu clase 'Factura'
                .GroupBy(v => new { v.Fecha_factura.Year, v.Fecha_factura.Month, v.Usuario_id }) // Agrupar por año, mes y usuario
                .Select(g => new
                {
                    Mes = g.Key.Month,
                    Año = g.Key.Year,
                    Usuario = g.Key.Usuario_id,  // ID del usuario (puedes ajustar esto a 'Usuario.Nombre' si tienes un campo de nombre)
                    TotalVentas = g.Sum(v => v.Total)  // Sumar el total de ventas por grupo
                })
                .ToList();

            // Crear una nueva serie para el gráfico de barras
            Series seriesVentas = new Series("Ventas por Usuario", ViewType.Bar);

            // Iterar sobre las ventas agrupadas y asignar los valores manualmente a la serie
            foreach (var venta in ventasPorUsuario)
            {
                // El argumento será el mes y año en formato simple (ejemplo: "09-2024")
                string argumento = $"{venta.Mes:D2}-{venta.Año}";  // Formato MM-YYYY
                                                                   // Agregar el punto con el argumento y el total de ventas
                seriesVentas.Points.Add(new SeriesPoint(argumento, venta.TotalVentas));
            }

            // Agregar la serie al ChartControl
            chartControl1.Series.Add(seriesVentas);

            // Configurar el eje X para mostrar Mes y Año
            XYDiagram diagramVentas = (XYDiagram)chartControl1.Diagram;
            diagramVentas.AxisX.Title.Text = "Mes-Año";  // El eje X representará los meses y años
            diagramVentas.AxisX.Label.TextPattern = "{A}";  // Mostrar el argumento que ya tiene formato MM-YYYY
            diagramVentas.AxisY.Title.Text = "Total Ventas";  // Eje Y representará el total de ventas

            // Refrescar el gráfico
            chartControl1.Refresh();





        }
    }
}