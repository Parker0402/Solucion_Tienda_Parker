using DevExpress.Data.Filtering;
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
    public partial class formEntradas_Inventario : Form
    {
        public formEntradas_Inventario()
        {
            InitializeComponent();
        }
        private void Habilitar(bool nuevo, bool guardar, bool actualizar, bool eliminar, bool cancelar, bool campos)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnEliminar.Enabled = eliminar;
            btnCancelar.Enabled = cancelar;
            btnActualizar.Enabled = actualizar;

            searchProducto.Enabled = campos;
            searchProveedor.Enabled = campos;
            txtCantidad.Enabled = campos;
        }

        private void Limpiar()
        {
            txtCantidad.Clear();
            txtCantidad.Clear();
            searchProveedor.EditValue =  null;
            searchProducto.EditValue = null;
            searchProducto.Focus();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar(false, true, false, false, true, true);
            gridViewEntradaInventario.RefreshData();
        }

        private void formEntradas_Inventario_Load(object sender, EventArgs e)
        {
            Habilitar(true, false, false, false, false, false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos requeridos estén llenos
            if (string.IsNullOrEmpty(txtCantidad.Text) ||
                searchProducto.EditValue == null ||
                searchProveedor.EditValue == null)
            {
                MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Crear una nueva entrada en el inventario
                Entradas_inventario np = new Entradas_inventario(unitOfWork1);
                np.Cantidad = Convert.ToInt32(txtCantidad.Text);
                np.Producto_id = unitOfWork1.FindObject<Productos>(CriteriaOperator.Parse("Id = ?", (int)searchProducto.EditValue));
                np.Proveedor_id = unitOfWork1.FindObject<Proveedores>(CriteriaOperator.Parse("Id = ?", (int)searchProveedor.EditValue));
                np.Save();

               

                // Confirmar los cambios en la base de datos
                unitOfWork1.CommitChanges();

                MessageBox.Show("Guardado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar los datos del inventario
                gridViewEntradaInventario.RefreshData();
                xpCollectionEntradas_Inventario.Reload();

                // Limpiar los campos y habilitar/deshabilitar botones según sea necesario
                Habilitar(true, false, false, false, false, false);
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el grid
            if (gridViewEntradaInventario.FocusedRowHandle >= 0)
            {
                // Obtener el ID de la entrada de inventario seleccionada
                int Seleccionado = (int)gridViewEntradaInventario.GetRowCellValue(gridViewEntradaInventario.FocusedRowHandle, "Id");

                // Mostrar mensaje de confirmación
                DialogResult confirmacion = MessageBox.Show($"¿Estás seguro que deseas eliminar esta entrada de inventario?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    // Buscar la entrada de inventario seleccionada usando la misma UnitOfWork (unitOfWork1)
                    Entradas_inventario entradaEliminar = unitOfWork1.FindObject<Entradas_inventario>(CriteriaOperator.Parse("Id = ?", Seleccionado));

                    if (entradaEliminar != null)
                    {
                        // Obtener el producto relacionado
                        Productos producto = entradaEliminar.Producto_id;

                        if (producto != null)
                        {
                            // Restar la cantidad de la entrada de inventario del total en el producto
                            producto.Cantidad -= entradaEliminar.Cantidad;

                            // Guardar los cambios del producto
                            producto.Save();
                        }

                        // Eliminar la entrada de inventario de la base de datos
                        entradaEliminar.Delete();
                        unitOfWork1.CommitChanges();

                        // Mostrar mensaje de éxito
                        MessageBox.Show("Entrada de inventario eliminada con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar los datos del grid
                        xpCollectionEntradas_Inventario.Reload();

                        // Limpiar los campos
                        Limpiar();
                        Habilitar(true, false, false, false, false, false);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la entrada de inventario.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Si no hay ninguna entrada seleccionada
                MessageBox.Show("Por favor, selecciona un registro para eliminar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el grid
            if (gridViewEntradaInventario.FocusedRowHandle >= 0)
            {
                // Obtener el ID de la entrada de inventario seleccionada
                int Seleccionado;
                try
                {
                    Seleccionado = (int)gridViewEntradaInventario.GetRowCellValue(gridViewEntradaInventario.FocusedRowHandle, "Id");
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show("Error al obtener el ID de la entrada seleccionada: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validación de campos vacíos
                if (string.IsNullOrEmpty(txtCantidad.Text) ||
                    searchProducto.EditValue == null ||
                    searchProveedor.EditValue == null)
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validación de tipo de dato (cantidad)
                if (!int.TryParse(txtCantidad.Text, out int nuevaCantidad) || nuevaCantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser un número entero válido mayor que 0.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

           

                // Buscar la entrada de inventario seleccionada usando la misma UnitOfWork (unitOfWork1)
                Entradas_inventario Actualizar = unitOfWork1.FindObject<Entradas_inventario>(CriteriaOperator.Parse("Id = ?", Seleccionado));

                if (Actualizar != null)
                {
                    // Obtener el producto relacionado usando la misma UnitOfWork (unitOfWork1)
                    Productos producto = Actualizar.Producto_id;

                    if (producto != null)
                    {
                        // Ajuste de la cantidad en el inventario solo si la cantidad ha cambiado
                        int cantidadAnterior = Actualizar.Cantidad;

                        // Ajustar el inventario solo si la nueva cantidad es diferente de la anterior
                        if (nuevaCantidad != cantidadAnterior)
                        {
                            // Ajustar el inventario solo por la diferencia entre la nueva cantidad y la anterior
                            int diferenciaCantidad = nuevaCantidad - cantidadAnterior;
                            producto.Cantidad += diferenciaCantidad;

                            // Guardar los cambios en el producto
                            producto.Save();
                        }
                    }

                    // Actualizar los valores de la entrada de inventario
                    Actualizar.Cantidad = nuevaCantidad;

                    // Validación del producto seleccionado
                    if (int.TryParse(searchProducto.EditValue?.ToString(), out int productoId))
                    {
                        // Asegurarse de que el producto se encuentre usando la misma sesión (unitOfWork1)
                        Actualizar.Producto_id = unitOfWork1.FindObject<Productos>(CriteriaOperator.Parse("Id = ?", productoId));
                    }
                    else
                    {
                        MessageBox.Show("El valor seleccionado en el campo de producto no es válido.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Validación del proveedor seleccionado
                    if (int.TryParse(searchProveedor.EditValue?.ToString(), out int proveedorId))
                    {
                        // Asegurarse de que el proveedor se encuentre usando la misma sesión (unitOfWork1)
                        Actualizar.Proveedor_id = unitOfWork1.FindObject<Proveedores>(CriteriaOperator.Parse("Id = ?", proveedorId));
                    }
                    else
                    {
                        MessageBox.Show("El valor seleccionado en el campo de proveedor no es válido.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Guardar los cambios de la entrada de inventario
                    try
                    {
                        // Guardar el registro de inventario
                        Actualizar.Save();

                        // Aplicar los cambios en la base de datos
                        unitOfWork1.CommitChanges();

                        MessageBox.Show("Actualizado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar los datos del grid
                        xpCollectionEntradas_Inventario.Reload();

                        // Limpiar los campos
                        Limpiar();
                        Habilitar(true, false, false, false, false, false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar el registro: {ex.Message}", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró la entrada de inventario seleccionada.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si no hay ningún registro seleccionado
                MessageBox.Show("Por favor, selecciona un registro para actualizar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void gridViewEntradaInventario_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                Habilitar(false, false, true, true, true, true);
                txtCantidad.Text = gridViewEntradaInventario.GetRowCellValue(e.RowHandle, "Cantidad").ToString();
                searchProducto.EditValue = gridViewEntradaInventario.GetRowCellValue(e.RowHandle, "Producto_id!Key").ToString();
                searchProveedor.EditValue = gridViewEntradaInventario.GetRowCellValue(e.RowHandle, "Proveedor_id!Key").ToString();

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar(true, false, false, false, false, false);
            xpCollectionEntradas_Inventario.Reload();
        }
    }
}
