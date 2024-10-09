using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tienda_Parker.Database;

namespace Tienda_Parker
{
    public partial class formPrincipal : Form
    {
        string rol;
        Usuarios us;
        public formPrincipal(string rol,Usuarios usuarios)
        {
            InitializeComponent();
            this.rol = rol;
            this.us=usuarios;
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formUsuarios>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formUsuarios { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formEmpleado>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formEmpleado { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formProducto>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formProducto { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formProveedores>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formProveedores { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formVentas>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formVentas(us) { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formEntradas_Inventario>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formEntradas_Inventario { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }
        private void btnInformes_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca si el formulario ya está abierto
                var formularioExistente = this.MdiChildren.OfType<formInformes>().FirstOrDefault();

                // Si el formulario no está abierto, crea una nueva instancia, de lo contrario, lo trae al frente
                if (formularioExistente == null)
                {
                    var nuevoFormulario = new formInformes { MdiParent = this };
                    nuevoFormulario.Show();
                }
                else
                {
                    formularioExistente.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}");
            }

        }

        private void formPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            // Crear un cuadro de diálogo para guardar el archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivo de Respaldo (*.sql)|*.sql",
                Title = "Guardar Respaldo de Base de Datos",
                FileName = $"Respaldo_Tienda_Parker_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.sql" // Nombre por defecto
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string backupFilePath = saveFileDialog.FileName;

                // Configuración de la base de datos
                string databaseName = "tienda"; // Nombre de tu base de datos
                string userId = "root"; // Tu usuario de MySQL
                string password = "root"; // Tu contraseña de MySQL

                // Comando para realizar el respaldo
                string mysqldumpPath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe"; // Ruta al ejecutable de mysqldump
                string arguments = $"-u {userId} -p{password} --result-file=\"{backupFilePath}\" {databaseName}";

                // Iniciar el proceso
                try
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo
                    {
                        FileName = mysqldumpPath,
                        Arguments = arguments,
                        RedirectStandardOutput = false, // No redirigir la salida estándar
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    using (Process process = new Process { StartInfo = processStartInfo })
                    {
                        process.Start();
                        process.WaitForExit(); // Esperar a que el proceso termine
                    }

                    MessageBox.Show("Respaldo realizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al realizar el respaldo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            // Crear un cuadro de diálogo para abrir el archivo
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos SQL (*.sql)|*.sql",
                Title = "Seleccionar Archivo de Respaldo"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string backupFilePath = openFileDialog.FileName;

                // Configuración de la base de datos
                string databaseName = "tienda"; // Nombre de tu base de datos
                string userId = "root"; // Tu usuario de MySQL
                string password = "root"; // Tu contraseña de MySQL

                // Comando para restaurar la base de datos
                string mysqlPath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe"; // Ruta al ejecutable de mysql
                string arguments = $"-u {userId} -p{password} {databaseName} --default-character-set=utf8mb4 -e \"source {backupFilePath}\"";

                // Iniciar el proceso
                try
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo
                    {
                        FileName = mysqlPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true, // Redirigir salida de error
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    using (Process process = new Process { StartInfo = processStartInfo })
                    {
                        process.Start();

                        // Lee la salida estándar y la salida de error
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        process.WaitForExit(); // Esperar a que el proceso termine

                        // Mostrar mensaje de éxito o error
                        if (process.ExitCode == 0)
                        {
                            MessageBox.Show("Restauración realizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Error al restaurar la base de datos: {error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al restaurar la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void ConfigurarAccesosPorRol(string rol)
        {
            switch (rol)
            {
                case "Administrador":
                    // Administrador tiene acceso a todo
                    btnUsuarios.Enabled = true;
                    btnEmpleados.Enabled = true;
                    btnProductos.Enabled = true; // Productos
                    btnProveedores.Enabled = true; // Proveedores
                    btnFacturar.Enabled = true; // Ventas
                    btnEntradaInventario.Enabled = true; // Entradas de inventario
                    btnRespaldo.Enabled = true; // Respaldos
                    btnRestaurar.Enabled = true; // Restaurar
                    btnInformes.Enabled = true; // Acceso a informes
                    break;

                case "Vendedor":
                    // Vendedor tiene acceso limitado
                    btnUsuarios.Enabled = false; // Sin acceso a usuarios
                    btnEmpleados.Enabled = false; // Sin acceso a empleados
                    btnProductos.Enabled = true; // Puede ver productos
                    btnProveedores.Enabled = false; // Sin acceso a proveedores
                    btnFacturar.Enabled = true; // Puede realizar ventas
                    btnEntradaInventario.Enabled = false; // Sin acceso a inventario
                    btnRespaldo.Enabled = false; // Sin acceso a respaldo
                    btnRestaurar.Enabled = false; // Sin acceso a restaurar
                    btnInformes.Enabled = true; // Puede ver informes
                    break;

                case "Consulta":
                    // Consulta solo tiene acceso a visualizar
                    btnUsuarios.Enabled = false; // Sin acceso a usuarios
                    btnEmpleados.Enabled = false; // Sin acceso a empleados
                    btnProductos.Enabled = true; // Puede ver productos
                    btnProveedores.Enabled = false; // Sin acceso a proveedores
                    btnFacturar.Enabled = false; // Sin acceso a ventas
                    btnEntradaInventario.Enabled = false; // Sin acceso a inventario
                    btnRespaldo.Enabled = false; // Sin acceso a respaldo
                    btnRestaurar.Enabled = false; // Sin acceso a restaurar
                    btnInformes.Enabled = true; // Puede ver informes
                    break;

                default:
                    MessageBox.Show("Rol no reconocido, se aplicarán restricciones de acceso predeterminadas.");
                    // Se pueden aplicar restricciones predeterminadas si el rol no se reconoce
                    DeshabilitarTodo();
                    break;
            }
        }


        private void DeshabilitarTodo()
        {
            // Deshabilita todos los botones
            btnUsuarios.Enabled = false;
            btnEmpleados.Enabled = false;
            btnProductos.Enabled = false;
            btnProveedores.Enabled = false;
            btnFacturar.Enabled = false;
            btnEntradaInventario.Enabled = false;
            btnRespaldo.Enabled = false;
            btnRestaurar.Enabled = false;
        }

    }
}
