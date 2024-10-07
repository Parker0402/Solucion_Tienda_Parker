
namespace Tienda_Parker
{
    partial class formPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPrincipal));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnInformes = new DevExpress.XtraEditors.SimpleButton();
            this.btnEntradaInventario = new DevExpress.XtraEditors.SimpleButton();
            this.btnFacturar = new DevExpress.XtraEditors.SimpleButton();
            this.btnProveedores = new DevExpress.XtraEditors.SimpleButton();
            this.btnProductos = new DevExpress.XtraEditors.SimpleButton();
            this.btnEmpleados = new DevExpress.XtraEditors.SimpleButton();
            this.btnUsuarios = new DevExpress.XtraEditors.SimpleButton();
            this.btnInicio = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnRestaurar = new DevExpress.XtraEditors.SimpleButton();
            this.btnRespaldo = new DevExpress.XtraEditors.SimpleButton();
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnInformes);
            this.panelControl1.Controls.Add(this.btnEntradaInventario);
            this.panelControl1.Controls.Add(this.btnFacturar);
            this.panelControl1.Controls.Add(this.btnProveedores);
            this.panelControl1.Controls.Add(this.btnProductos);
            this.panelControl1.Controls.Add(this.btnEmpleados);
            this.panelControl1.Controls.Add(this.btnUsuarios);
            this.panelControl1.Controls.Add(this.btnInicio);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(129, 639);
            this.panelControl1.TabIndex = 0;
            // 
            // btnInformes
            // 
            this.btnInformes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInformes.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInformes.ImageOptions.Image")));
            this.btnInformes.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnInformes.Location = new System.Drawing.Point(2, 464);
            this.btnInformes.Name = "btnInformes";
            this.btnInformes.Size = new System.Drawing.Size(125, 66);
            this.btnInformes.TabIndex = 6;
            this.btnInformes.Text = "Informes";
            this.btnInformes.Click += new System.EventHandler(this.btnInformes_Click);
            // 
            // btnEntradaInventario
            // 
            this.btnEntradaInventario.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEntradaInventario.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEntradaInventario.ImageOptions.Image")));
            this.btnEntradaInventario.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnEntradaInventario.Location = new System.Drawing.Point(2, 398);
            this.btnEntradaInventario.Name = "btnEntradaInventario";
            this.btnEntradaInventario.Size = new System.Drawing.Size(125, 66);
            this.btnEntradaInventario.TabIndex = 5;
            this.btnEntradaInventario.Text = "Ingreso de Productos";
            this.btnEntradaInventario.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // btnFacturar
            // 
            this.btnFacturar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFacturar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturar.ImageOptions.Image")));
            this.btnFacturar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnFacturar.Location = new System.Drawing.Point(2, 332);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(125, 66);
            this.btnFacturar.TabIndex = 4;
            this.btnFacturar.Text = "Facturar";
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // btnProveedores
            // 
            this.btnProveedores.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProveedores.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnProveedores.ImageOptions.Image")));
            this.btnProveedores.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnProveedores.Location = new System.Drawing.Point(2, 266);
            this.btnProveedores.Name = "btnProveedores";
            this.btnProveedores.Size = new System.Drawing.Size(125, 66);
            this.btnProveedores.TabIndex = 3;
            this.btnProveedores.Text = "Gestion de Proveedores";
            this.btnProveedores.Click += new System.EventHandler(this.btnProveedores_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProductos.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnProductos.ImageOptions.Image")));
            this.btnProductos.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnProductos.Location = new System.Drawing.Point(2, 200);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(125, 66);
            this.btnProductos.TabIndex = 2;
            this.btnProductos.Text = "Inventario";
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnEmpleados
            // 
            this.btnEmpleados.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEmpleados.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEmpleados.ImageOptions.Image")));
            this.btnEmpleados.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnEmpleados.Location = new System.Drawing.Point(2, 134);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(125, 66);
            this.btnEmpleados.TabIndex = 1;
            this.btnEmpleados.Text = "Gestion de Empleados";
            this.btnEmpleados.Click += new System.EventHandler(this.btnEmpleados_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUsuarios.ImageOptions.Image")));
            this.btnUsuarios.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnUsuarios.Location = new System.Drawing.Point(2, 68);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(125, 66);
            this.btnUsuarios.TabIndex = 0;
            this.btnUsuarios.Text = "Gestion de Usuarios";
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInicio.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInicio.ImageOptions.Image")));
            this.btnInicio.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.btnInicio.Location = new System.Drawing.Point(2, 2);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(125, 66);
            this.btnInicio.TabIndex = 0;
            this.btnInicio.Text = "Inicio";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnRestaurar);
            this.panelControl2.Controls.Add(this.btnRespaldo);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(129, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(671, 34);
            this.panelControl2.TabIndex = 1;
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(101, 5);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(75, 23);
            this.btnRestaurar.TabIndex = 0;
            this.btnRestaurar.Text = "Restaurar BD";
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // btnRespaldo
            // 
            this.btnRespaldo.Location = new System.Drawing.Point(6, 5);
            this.btnRespaldo.Name = "btnRespaldo";
            this.btnRespaldo.Size = new System.Drawing.Size(75, 23);
            this.btnRespaldo.TabIndex = 0;
            this.btnRespaldo.Text = "Respaldar BD";
            this.btnRespaldo.Click += new System.EventHandler(this.btnRespaldo_Click);
            // 
            // documentManager1
            // 
            this.documentManager1.MdiParent = this;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // formPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 639);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "formPrincipal";
            this.Text = "Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnInicio;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnUsuarios;
        private DevExpress.XtraEditors.SimpleButton btnFacturar;
        private DevExpress.XtraEditors.SimpleButton btnProveedores;
        private DevExpress.XtraEditors.SimpleButton btnProductos;
        private DevExpress.XtraEditors.SimpleButton btnEmpleados;
        private DevExpress.XtraEditors.SimpleButton btnEntradaInventario;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraEditors.SimpleButton btnRestaurar;
        private DevExpress.XtraEditors.SimpleButton btnRespaldo;
        private DevExpress.XtraEditors.SimpleButton btnInformes;
    }
}