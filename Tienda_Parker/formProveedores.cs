using DevExpress.XtraExport.Helpers;
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
    public partial class formProveedores : Form
    {
        public formProveedores()
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
            txtRuc.Enabled = campos;
            txtEmail.Enabled = campos;
            txtDir.Enabled = campos;
            txtTel.Enabled = campos;
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtRuc.Clear();
            txtDir.Clear();
            txtTel.Clear();
            txtEmail.Clear();
            txtNombre.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar(false,true,false,false,true,true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar(true, false, false, false, false, false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNombre.Text)||
                string.IsNullOrEmpty(txtRuc.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtDir.Text) ||
                string.IsNullOrEmpty(txtTel.Text)
                )
            {
                MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Proveedores np = new Proveedores(unitOfWork1);
            np.Nombre = txtNombre.Text;
            np.Contacto = txtRuc.Text;
            np.Direccion = txtDir.Text;
            np.Email = txtEmail.Text;
            np.Telefono = txtTel.Text;

            np.Save();
            unitOfWork1.CommitChanges();

            MessageBox.Show("Guardado con Exito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            xpCollectionProveedores.Reload();
            Habilitar(true, false, false, false, false, false);
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el grid
            if (gridViewProveedores.FocusedRowHandle >= 0)
            {
                // Obtener el usuario seleccionado
                int Seleccionado = (int)gridViewProveedores.GetRowCellValue(gridViewProveedores.FocusedRowHandle, "Id");

                // Mostrar mensaje de confirmación
                DialogResult confirmacion = MessageBox.Show($"¿Estás seguro que deseas eliminar?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    // Buscar el usuario en la XPCollection de forma manual
                    Proveedores Eliminar = null;

                    foreach (Proveedores registro in xpCollectionProveedores)
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
                        xpCollectionProveedores.Reload();

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
            if (gridViewProveedores.FocusedRowHandle >= 0)
            {

                int Seleccionado = (int)gridViewProveedores.GetRowCellValue(gridViewProveedores.FocusedRowHandle, "Id");


                if (string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtRuc.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtDir.Text) ||
                string.IsNullOrEmpty(txtTel.Text))
                {
                    MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar el usuario en la XPCollection de forma manual
                Proveedores Actualizar = null;

                foreach (Proveedores registro in xpCollectionProveedores)
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
                    Actualizar.Contacto = txtRuc.Text;
                    Actualizar.Direccion = txtDir.Text;
                    Actualizar.Telefono = txtTel.Text;
                    Actualizar.Email = txtEmail.Text;

                    // Guardar los cambios en la base de datos
                    Actualizar.Save();
                    unitOfWork1.CommitChanges();

                    // Mostrar mensaje de éxito
                    MessageBox.Show("Actualizado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar los datos del grid
                    xpCollectionProveedores.Reload();

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

        private void gridViewProveedores_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                Habilitar(false,false,true,true,true,true);
                txtNombre.Text = gridViewProveedores.GetRowCellValue(e.RowHandle, "Nombre").ToString();
                txtRuc.Text = gridViewProveedores.GetRowCellValue(e.RowHandle, "Contacto").ToString();
                txtDir.Text = gridViewProveedores.GetRowCellValue(e.RowHandle, "Direccion").ToString();
                txtTel.Text = gridViewProveedores.GetRowCellValue(e.RowHandle, "Telefono").ToString();
                txtEmail.Text = gridViewProveedores.GetRowCellValue(e.RowHandle, "Email").ToString();

            }
        }

        private void formProveedores_Load(object sender, EventArgs e)
        {
            Habilitar(true, false, false, false, false, false);
        }
    }
}
