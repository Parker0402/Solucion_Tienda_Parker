using DevExpress.Data.Filtering;
using DevExpress.DirectX.NativeInterop.DXGI;
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
    public partial class formEmpleado : Form
    {
        public formEmpleado()
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
            txtCorreo.Enabled = campos;
            txtTelefono.Enabled = campos;
            txtDireccion.Enabled = campos;
            txtCargo.Enabled = campos;
            dtpIngreso.Enabled = campos;
            txtSalario.Enabled = campos;
            slueUsuario.Enabled = campos;
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtCargo.Clear();
            dtpIngreso.Value=DateTime.Now;
            txtSalario.Clear();
            slueUsuario.EditValue = null;
            txtNombre.Focus();  
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar(false, true, false, false, true, true);
            xpCollectionEmpleado.Reload();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) ||
                string.IsNullOrEmpty(txtCorreo.Text) ||
                string.IsNullOrEmpty(txtTelefono.Text) ||
                string.IsNullOrEmpty(txtDireccion.Text) ||
                string.IsNullOrEmpty(txtCargo.Text) ||
                string.IsNullOrEmpty(dtpIngreso.Text)||
                string.IsNullOrEmpty(txtSalario.Text) ||
                slueUsuario.EditValue==null
                )
            {
                MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            Empleados np = new Empleados(unitOfWork1);
            np.Nombre = txtNombre.Text;
            np.Email = txtCorreo.Text;
            np.Direccion = txtDireccion.Text;
            np.Cargo = txtCargo.Text;
            np.Telefono = txtTelefono.Text;
            np.Fecha_ingreso=dtpIngreso.Value;
            np.Salario= decimal.Parse(txtSalario.Text);
            np.Usuario_id = unitOfWork1.FindObject<Usuarios>(CriteriaOperator.Parse("Id = ?", (int)slueUsuario.EditValue));

            np.Save();
            unitOfWork1.CommitChanges();

            MessageBox.Show("Guardado con Exito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            xpCollectionEmpleado.Reload();
            Habilitar(true, false, false, false, false, false);
            Limpiar();
      
        }

        private void formEmpleado_Load(object sender, EventArgs e)
        {
            Habilitar(true, false, false, false, false, false);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el grid
            if (gridViewEmpleado.FocusedRowHandle >= 0)
            {
                // Obtener el usuario seleccionado
                int Seleccionado = (int)gridViewEmpleado.GetRowCellValue(gridViewEmpleado.FocusedRowHandle, "Id");

                // Mostrar mensaje de confirmación
                DialogResult confirmacion = MessageBox.Show($"¿Estás seguro que deseas eliminar?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    // Buscar el usuario en la XPCollection de forma manual
                    Empleados Eliminar = null;

                    foreach (Empleados registro in xpCollectionEmpleado)
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
                        xpCollectionEmpleado.Reload();

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
            if (gridViewEmpleado.FocusedRowHandle >= 0)
            {
                // Obtener el ID del empleado seleccionado
                int Seleccionado;
                try
                {
                    Seleccionado = (int)gridViewEmpleado.GetRowCellValue(gridViewEmpleado.FocusedRowHandle, "Id");
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show("Error al obtener el ID del empleado seleccionado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validación de campos vacíos
                if (string.IsNullOrEmpty(txtNombre.Text) ||
                    string.IsNullOrEmpty(txtCorreo.Text) ||
                    string.IsNullOrEmpty(txtTelefono.Text) ||
                    string.IsNullOrEmpty(txtDireccion.Text) ||
                    string.IsNullOrEmpty(txtCargo.Text) ||
                    string.IsNullOrEmpty(dtpIngreso.Text) ||
                    string.IsNullOrEmpty(txtSalario.Text) ||
                    slueUsuario.EditValue == null)
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar el empleado seleccionado
                Empleados Actualizar = null;
                foreach (Empleados registro in xpCollectionEmpleado)
                {
                    if (registro.Id == Seleccionado)
                    {
                        Actualizar = registro;
                        break;
                    }
                }

                if (Actualizar != null)
                {
                    // Actualizar los valores del empleado
                    Actualizar.Nombre = txtNombre.Text;
                    Actualizar.Email = txtCorreo.Text;
                    Actualizar.Direccion = txtDireccion.Text;
                    Actualizar.Cargo = txtCargo.Text;
                    Actualizar.Telefono = txtTelefono.Text;

                    // Parsear fecha de ingreso de forma segura
                    if (DateTime.TryParse(dtpIngreso.Text, out DateTime fechaIngreso))
                    {
                        Actualizar.Fecha_ingreso = fechaIngreso;
                    }
                    else
                    {
                        MessageBox.Show("La fecha de ingreso no es válida.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Convertir salario de forma segura
                    if (decimal.TryParse(txtSalario.Text, out decimal salario))
                    {
                        Actualizar.Salario = salario;
                    }
                    else
                    {
                        MessageBox.Show("El salario no es válido.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Manejar el caso de slueUsuario.EditValue
                    if (int.TryParse(slueUsuario.EditValue?.ToString(), out int usuarioId))
                    {
                        Actualizar.Usuario_id = unitOfWork1.FindObject<Usuarios>(CriteriaOperator.Parse("Id = ?", usuarioId));
                    }
                    else
                    {
                        MessageBox.Show("El valor seleccionado en el campo de usuario no es válido.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Guardar cambios y confirmar la actualización
                    try
                    {
                        Actualizar.Save();
                        unitOfWork1.CommitChanges();

                        MessageBox.Show("Actualizado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar los datos del grid
                        xpCollectionEmpleado.Reload();

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
                    MessageBox.Show("No se encontró el empleado seleccionado.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si no hay ningún empleado seleccionado
                MessageBox.Show("Por favor, selecciona un registro para actualizar.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar(true, false, false, false, false, false);
            xpCollectionEmpleado.Reload();
        }

        private void gridViewEmpleado_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                Habilitar(false, false, true, true, true, true);
                txtNombre.Text = gridViewEmpleado.GetRowCellValue(e.RowHandle, "Nombre").ToString();
                txtDireccion.Text = gridViewEmpleado.GetRowCellValue(e.RowHandle, "Direccion").ToString();
                txtCargo.Text = gridViewEmpleado.GetRowCellValue(e.RowHandle, "Cargo").ToString();
                txtCorreo.Text = gridViewEmpleado.GetRowCellValue(e.RowHandle, "Email").ToString();
                txtSalario.Text = gridViewEmpleado.GetRowCellValue(e.RowHandle, "Salario").ToString();
                txtTelefono.Text = gridViewEmpleado.GetRowCellValue(e.RowHandle, "Telefono").ToString();
                dtpIngreso.Text = gridViewEmpleado.GetRowCellValue(e.RowHandle, "Fecha_ingreso").ToString();
                slueUsuario.EditValue = gridViewEmpleado.GetRowCellValue(e.RowHandle,"Usuario_id!Key").ToString();

            }
        }
    }
}
