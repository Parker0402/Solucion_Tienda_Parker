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
    public partial class formUsuarios : Form
    {
        public formUsuarios()
        {
            InitializeComponent();
        }
        private void Permisos(bool Nuevo,bool Guardar, bool actualizar, bool eliminar, bool cancelar, bool campos)
        {
            txtUser.Enabled=campos;
            txtPass.Enabled=campos;
            cmbRol.Enabled=campos;

            btnNuevo.Enabled=Nuevo;
            btnGuardar.Enabled=Guardar;
            btnEliminar.Enabled=eliminar;
            btnCancelar.Enabled=cancelar;
            btnActualizar.Enabled=actualizar;

        }
        private void Limpiar()
        {
            txtUser.Clear();
            txtPass.Clear();
            cmbRol.SelectedIndex = -1;
            txtUser.Focus();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text)|| string.IsNullOrEmpty(cmbRol.Text))
            {
                MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Usuarios Nuevo = new Usuarios(unitOfWork1);
            Nuevo.Usuario = txtUser.Text;
            Nuevo.Contrasena = txtPass.Text;
            Nuevo.Roles=cmbRol.Text;

            Nuevo.Save();
            unitOfWork1.CommitChanges();

            MessageBox.Show("Guardado con Exito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            xpCollectionUsuario.Reload();
            Permisos(true,false,false,false,false,false);
            Limpiar();
        }

        private void gridViewUser_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                Permisos(false,false,true,true,true,true);
                txtUser.Text=gridViewUser.GetRowCellValue(e.RowHandle,"Usuario").ToString();
                txtPass.Text=gridViewUser.GetRowCellValue(e.RowHandle,"Contrasena").ToString();
                cmbRol.Text=gridViewUser.GetRowCellValue(e.RowHandle,"Roles").ToString();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una fila seleccionada en el grid
            if (gridViewUser.FocusedRowHandle >= 0)
            {
                // Obtener el usuario seleccionado
                string usuarioSeleccionado = gridViewUser.GetRowCellValue(gridViewUser.FocusedRowHandle, "Usuario").ToString();

                // Mostrar mensaje de confirmación
                DialogResult confirmacion = MessageBox.Show($"¿Estás seguro que deseas eliminar el usuario {usuarioSeleccionado}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    // Buscar el usuario en la XPCollection de forma manual
                    Usuarios usuarioAEliminar = null;

                    foreach (Usuarios usuario in xpCollectionUsuario)
                    {
                        if (usuario.Usuario == usuarioSeleccionado)
                        {
                            usuarioAEliminar = usuario;
                            break;
                        }
                    }

                    if (usuarioAEliminar != null)
                    {
                        // Eliminar el usuario de la colección y la base de datos
                        usuarioAEliminar.Delete();
                        unitOfWork1.CommitChanges();

                        // Mostrar mensaje de éxito
                        MessageBox.Show("Eliminado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar los datos del grid
                        xpCollectionUsuario.Reload();

                        // Limpiar los campos
                        Limpiar();
                        Permisos(true, false, false, false, false, false);

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
            if (gridViewUser.FocusedRowHandle >= 0)
            {
                // Obtener el usuario seleccionado
                string usuarioSeleccionado = gridViewUser.GetRowCellValue(gridViewUser.FocusedRowHandle, "Usuario").ToString();

                // Validar que los campos de usuario y contraseña no estén vacíos
                if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text) || string.IsNullOrEmpty(cmbRol.Text))
                {
                    MessageBox.Show("Campos Requeridos", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar el usuario en la XPCollection de forma manual
                Usuarios usuarioAActualizar = null;

                foreach (Usuarios usuario in xpCollectionUsuario)
                {
                    if (usuario.Usuario == usuarioSeleccionado)
                    {
                        usuarioAActualizar = usuario;
                        break;
                    }
                }

                if (usuarioAActualizar != null)
                {
                    // Actualizar los valores del usuario con los valores de los controles
                    usuarioAActualizar.Usuario = txtUser.Text;
                    usuarioAActualizar.Contrasena = txtPass.Text;
                    usuarioAActualizar.Roles = cmbRol.Text;

                    // Guardar los cambios en la base de datos
                    usuarioAActualizar.Save();
                    unitOfWork1.CommitChanges();

                    // Mostrar mensaje de éxito
                    MessageBox.Show("Actualizado con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar los datos del grid
                    xpCollectionUsuario.Reload();

                    // Limpiar los campos
                    Limpiar();
                    Permisos(true, false, false, false, false, false);

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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Permisos(false,true,false,false,true,true);
            // Limpiar los campos del formulario para permitir el ingreso de un nuevo usuario
            Limpiar();
     
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos del formulario para permitir el ingreso de un nuevo usuario
            Limpiar();
            Permisos(true, false, false, false, false, false);
        }

        private void formUsuarios_Load(object sender, EventArgs e)
        {
            Permisos(true,false,false,false,false,false);
        }
    }
}
