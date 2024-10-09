using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tienda_Parker.Database;

namespace Tienda_Parker.Informes
{
    public partial class InformeVentasPorRangoDeFechas : Form
    {

        public InformeVentasPorRangoDeFechas()
        {
            InitializeComponent();
        }

        private void InformeVentasPorRangoDeFechas_Load(object sender, EventArgs e)
        {
            deFechaFinal.Text = DateTime.Now.ToString("d");
        }

        private void btnCargarDatos_Click(object sender, EventArgs e)
        {
            // Verifica que las fechas sean válidas
            if (DateTime.TryParse(deFechaInicio.Text, out DateTime fechaInicio) &&
                DateTime.TryParse(deFechaFinal.Text, out DateTime fechaFinal))
            {
                CargarHistorialVentas(fechaInicio, fechaFinal);
            }
            else
            {
                MessageBox.Show("Por favor, ingresa fechas válidas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarHistorialVentas(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                // Validar que la fecha de inicio no sea mayor que la fecha final
                if (fechaInicio > fechaFinal)
                {
                    MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar que las fechas no sean en el futuro
                DateTime fechaActual = DateTime.Now;
                if (fechaInicio > fechaActual || fechaFinal > fechaActual)
                {
                    MessageBox.Show("Las fechas no pueden estar en el futuro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear el servicio de historial de ventas
               
                    // Consultar las ventas en el rango de fechas
                    var ventas = unitOfWork1.Query<Historial_ventas>()
                        .Where(v => v.Fecha_factura >= fechaInicio && v.Fecha_factura <= fechaFinal)
                        .ToList();

                    // Verificar si se encontraron ventas
                    if (ventas.Count == 0)
                    {
                        MessageBox.Show("No se encontraron ventas en el rango de fechas especificado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Establecer el DataSource del grid
                    gridControl1.DataSource = ventas; // Asumiendo que ventas es una lista de Historial_ventas
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar el historial de ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Crear un cuadro de diálogo para guardar el archivo
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                saveFileDialog.Title = "Guardar Informe de Ventas";
                saveFileDialog.FileName = $"Informe_Ventas_{deFechaInicio.Text.Replace("/", "-")}_a_{deFechaFinal.Text.Replace("/", "-")}.xlsx"; // Nombre por defecto

                // Verificar si el usuario ha elegido un nombre de archivo
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Exportar el contenido del gridControl1 a Excel
                    try
                    {
                        // Lógica para exportar a Excel
                        gridControl1.ExportToXlsx(saveFileDialog.FileName);

                        // Mensaje de éxito
                        MessageBox.Show("El informe se ha exportado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al exportar el informe: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
