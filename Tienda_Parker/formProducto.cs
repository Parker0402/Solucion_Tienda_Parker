using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using Tienda_Parker.Database;

namespace Tienda_Parker
{
    public partial class formProducto : Form
    {
        public formProducto()
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

            txtNombre.Enabled = campos;
            txtDesc.Enabled = campos;
            txtCantidad.Enabled = campos;
            txtPrecioC.Enabled = campos;
            txtPrecioV.Enabled = campos;

        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtDesc.Clear();
            txtCantidad.Clear();
            txtPrecioC.Clear();
            txtPrecioV.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar(false, true, false, false, true, true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtDesc.Text) ||
                string.IsNullOrEmpty(txtCantidad.Text) ||
                string.IsNullOrEmpty(txtPrecioC.Text) ||
                String.IsNullOrEmpty(txtPrecioV.Text)
                )
            {
                MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Productos np = new Productos(unitOfWork1);
            np.Nombre = txtNombre.Text;
            np.Descripcion = txtDesc.Text;
            np.Cantidad = int.Parse(txtCantidad.Text);
            np.Precio_compra = decimal.Parse(txtPrecioC.Text);
            np.Precio_venta = decimal.Parse(txtPrecioV.Text);

            np.Save();
            unitOfWork1.CommitChanges();

            MessageBox.Show("Guardado con Exito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            xpCollectionProducto.Reload();
            Habilitar(true, false, false, false, false, false);
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el grid
            if (gridViewProducto.FocusedRowHandle >= 0)
            {
                // Obtener el usuario seleccionado
                int Seleccionado = (int)gridViewProducto.GetRowCellValue(gridViewProducto.FocusedRowHandle, "Id");

                // Mostrar mensaje de confirmación
                DialogResult confirmacion = MessageBox.Show($"¿Estás seguro que deseas eliminar?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    // Buscar el usuario en la XPCollection de forma manual
                    Productos Eliminar = null;

                    foreach (Productos registro in xpCollectionProducto)
                    {
                        if (registro.Id == Seleccionado)
                        {
                            Eliminar = registro;
                            break;
                        }
                    }

                    if (Eliminar != null)
                    {
                        // Eliminar el usuario de la colección y la base de datos
                        Eliminar.Delete();
                        unitOfWork1.CommitChanges();

                        // Mostrar mensaje de éxito
                        MessageBox.Show("Eliminado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar los datos del grid
                        xpCollectionProducto.Reload();

                        // Limpiar los campos
                        Limpiar();
                        Habilitar(true, false, false, false, false, false);

                    }
                    else
                    {
                        MessageBox.Show("No encontrado", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Si no hay ningún usuario seleccionado
                MessageBox.Show("Por favor, selecciona un registro para eliminar", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el grid
            if (gridViewProducto.FocusedRowHandle >= 0)
            {

                int Seleccionado = (int)gridViewProducto.GetRowCellValue(gridViewProducto.FocusedRowHandle, "Id");


                if (string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtDesc.Text) ||
                string.IsNullOrEmpty(txtPrecioC.Text) ||
                string.IsNullOrEmpty (txtPrecioV.Text) ||
                string.IsNullOrEmpty(txtCantidad.Text))
                {
                    MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar el usuario en la XPCollection de forma manual
                Productos Actualizar = null;

                foreach (Productos registro in xpCollectionProducto)
                {
                    if (registro.Id == Seleccionado)
                    {
                        Actualizar = registro;
                        break;
                    }
                }

                if (Actualizar != null)
                {
                    // Actualizar los valores del usuario con los valores de los controles
                    Actualizar.Nombre = txtNombre.Text;
                    Actualizar.Descripcion = txtDesc.Text;
                    Actualizar.Precio_compra = decimal.Parse(txtPrecioC.Text);
                    Actualizar.Precio_venta=decimal.Parse(txtPrecioV.Text);
                    Actualizar.Cantidad = int.Parse(txtCantidad.Text);

                    // Guardar los cambios en la base de datos
                    Actualizar.Save();
                    unitOfWork1.CommitChanges();

                    // Mostrar mensaje de éxito
                    MessageBox.Show("Actualizado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar los datos del grid
                    xpCollectionProducto.Reload();

                    // Limpiar los campos
                    Limpiar();
                    Habilitar(true, false, false, false, false, false);

                }
                else
                {
                    MessageBox.Show("No encontrado", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si no hay ningún usuario seleccionado
                MessageBox.Show("Por favor, selecciona un registro para actualizar", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridViewProducto_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                Habilitar(false, false, true, true, true, true);
                txtNombre.Text = gridViewProducto.GetRowCellValue(e.RowHandle, "Nombre").ToString();
                txtDesc.Text = gridViewProducto.GetRowCellValue(e.RowHandle, "Descripcion").ToString();
                txtPrecioC.Text = gridViewProducto.GetRowCellValue(e.RowHandle, "Precio_compra").ToString();
                txtPrecioV.Text = gridViewProducto.GetRowCellValue(e.RowHandle, "Precio_venta").ToString();
                txtCantidad.Text = gridViewProducto.GetRowCellValue(e.RowHandle, "Cantidad").ToString();
            }
        }

        private void formProducto_Load(object sender, EventArgs e)
        {
            Habilitar(true,false,false,false,false,false);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
