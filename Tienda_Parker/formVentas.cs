using DevExpress.Data.Filtering;
using DevExpress.Xpo;
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
    public partial class formVentas : Form
    {
        Usuarios us;
        Facturas facturas;
        // Usar una lista para almacenar detalles de la factura en memoria
        private List<Detalle_facturas> detallesFactura;
        public formVentas(Usuarios usuarios)
        {
            InitializeComponent();
            this.us = usuarios;
            detallesFactura = new List<Detalle_facturas>(); // Inicializa la lista aquí
            gridControl1.DataSource = null;
        }

        private void Habilitar(bool nuevo, bool guardar,bool agregar,bool eliminar, bool cancelar, bool campos)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnCancelar.Enabled = cancelar;
            btnAgregarProducto.Enabled = agregar;
            btnEliminarProducto.Enabled = eliminar;

            cmbProducto.Enabled = campos;
            cmbCliente.Enabled = campos;
            txtCantidad.Enabled = campos;
        }
        private void LimpiarLista()
        {
            detallesFactura.Clear();
            gridControl1.DataSource = null; // Limpia el gridControl
        }

        private void Limpiar()
        {
            cmbProducto.EditValue = null;
            txtPrecioUnitario.Clear();
            txtSubTotal.Clear();
            txtCantidad.Clear();
            cmbCliente.SelectedIndex = 0;
            cmbProducto.Focus();
            cmbUsuario.EditValue = us.Id;
            txtFactura.Text = ObtenerProximoIdFactura().ToString();
        }
        public int ObtenerProximoIdFactura()
        {
            try
            {
                // Obtener el último ID de la tabla de Facturas usando UnitOfWork
                var ultimoId = unitOfWork1.Query<Facturas>()
                                           .OrderByDescending(f => f.Id)
                                           .Select(f => f.Id)
                                           .FirstOrDefault();

                return ultimoId + 1; // Retorna el siguiente ID
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el último ID de factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1; // Retorna 1 como valor por defecto en caso de error
            }
        }

        private void formVentas_Load(object sender, EventArgs e)
        {
            Habilitar(true, false, false,false,false, false);
            Limpiar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar(false,true,true,true,true,true);
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if(e.RowHandle>=0)
            {
                txtCantidad.Text = gridView1.GetRowCellValue(e.RowHandle, "Cantidad").ToString();
                txtPrecioUnitario.Text = gridView1.GetRowCellValue(e.RowHandle,"Precio_unitario").ToString();
                txtSubTotal.Text = gridView1.GetRowCellValue(e.RowHandle, "Subtotal").ToString();
                cmbProducto.EditValue = gridView1.GetRowCellValue(e.RowHandle, "Producto_id!Key").ToString();
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Validar campos vacíos
            if (string.IsNullOrEmpty(txtCantidad.Text) ||
                string.IsNullOrEmpty(txtPrecioUnitario.Text) ||
                cmbProducto.EditValue == null)
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar tipo de dato (cantidad)
            if (!int.TryParse(txtCantidad.Text, out int cantidaddetalle) || cantidaddetalle <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número entero válido mayor que 0.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar tipo de dato (precio unitario)
            if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precioUnitario) || precioUnitario <= 0)
            {
                MessageBox.Show("El precio unitario debe ser un valor numérico válido mayor que 0.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear un nuevo objeto Detalle_facturas
            Detalle_facturas nuevoDetalle = new Detalle_facturas(unitOfWork1)
            {
                Cantidad = cantidaddetalle,
                Precio_unitario = precioUnitario,
                Subtotal = decimal.Parse(txtSubTotal.Text),
                Producto_id = (Productos)searchViewProductos.GetFocusedRow()
            };

            // Agregar el nuevo detalle a la lista temporal
            detallesFactura.Add(nuevoDetalle);

            // Actualizar el DataSource del gridControl
            gridControl1.DataSource = null; // Limpia el DataSource antes de asignar la nueva lista
            gridControl1.DataSource = detallesFactura; // Asigna la lista de detalles a gridControl
            Limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar campos antes de guardar la factura
            if (detallesFactura.Count == 0)
            {
                MessageBox.Show("No hay productos para guardar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Asegúrate de que el usuario esté seleccionado
            if (cmbUsuario.EditValue == null)
            {
                MessageBox.Show("Debes seleccionar un usuario.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /// Obtener el usuario seleccionado desde la misma sesión
            Usuarios usuarioSeleccionado = unitOfWork1.FindObject<Usuarios>(CriteriaOperator.Parse("Id = ?", us.Id));

            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("El usuario no se encuentra en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Crear la factura y asignar los detalles
            facturas = new Facturas(unitOfWork1)
            {
                Usuario_id = usuarioSeleccionado, // Asignar el usuario seleccionado
                Cliente = cmbCliente.Text,
                Total = detallesFactura.Sum(d => d.Subtotal) // Calcular el total de la factura
            };

            // Guardar la factura y los detalles en la base de datos
            try
            {
                // Agregar todos los detalles a la factura
                foreach (var detalle in detallesFactura)
                {
                    detalle.Factura_id = facturas; // Asegúrate de vincular el detalle a la factura
                    facturas.Detalle_facturass.Add(detalle); // Agregar el detalle a la colección de la factura
                }

                unitOfWork1.CommitChanges();
                MessageBox.Show("Factura guardada exitosamente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar(); // Limpiar campos y reiniciar el formulario
                Habilitar(true, false, false, false, false, false);
                LimpiarLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la factura: {ex.Message}", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbProducto_EditValueChanged(object sender, EventArgs e)
        {
            // Obtén el objeto Producto actualmente seleccionado
            Productos p = (Productos)searchViewProductos.GetFocusedRow();

            // Verifica que el objeto no sea nulo
            if (p != null)
            {
                // Asigna el precio del producto al TextBox, formateando a dos decimales
                txtPrecioUnitario.Text = p.Precio_venta.ToString("F2");
            }
            else
            {
                // Limpiar el TextBox si no hay un producto seleccionado
                txtPrecioUnitario.Clear();
            }
        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {
            // Verificar que ambos campos contengan valores válidos
            if (int.TryParse(txtCantidad.Text, out int cantidad) &&
                decimal.TryParse(txtPrecioUnitario.Text, out decimal precioUnitario))
            {
                // Calcular el subtotal
                decimal subtotal = cantidad * precioUnitario;

                // Asignar el subtotal al TextBox de subtotal
                txtSubTotal.Text = subtotal.ToString("F2"); // Formatear a dos decimales
            }
            else
            {
                // Limpiar el campo de subtotal si hay un error en los valores
                txtSubTotal.Clear();
            }
        }
    }
}
