namespace JuninComercial.Escritorio.Vistas
{
    partial class FrmLocationLista
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.DataGridView dgvLocation;
        private System.Windows.Forms.Label lblVacio;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;

        private void InitializeComponent()
        {
            this.dgvLocation = new System.Windows.Forms.DataGridView();
            this.lblVacio = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnNueva = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocation)).BeginInit();
            this.SuspendLayout();

            // dgvLocation
            this.dgvLocation.Location = new System.Drawing.Point(20, 20);
            this.dgvLocation.Size = new System.Drawing.Size(740, 400);
            this.dgvLocation.Name = "dgvLocation";
            this.dgvLocation.ReadOnly = true;
            this.dgvLocation.AllowUserToAddRows = false;
            this.dgvLocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocation.MultiSelect = false;
            this.dgvLocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLocation.SelectionChanged += new System.EventHandler(this.dgvLocation_SelectionChanged);
            this.dgvLocation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocation_CellDoubleClick);

            // lblVacio
            this.lblVacio.Location = new System.Drawing.Point(20, 200);
            this.lblVacio.Size = new System.Drawing.Size(740, 40);
            this.lblVacio.Name = "lblVacio";
            this.lblVacio.Text = "No hay locations registradas.";
            this.lblVacio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVacio.Visible = false;

            // btnNueva
            this.btnNueva.Location = new System.Drawing.Point(20, 440);
            this.btnNueva.Size = new System.Drawing.Size(120, 40);
            this.btnNueva.Name = "btnNueva";
            this.btnNueva.Text = "Nueva";
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);

            // btnEditar
            this.btnEditar.Location = new System.Drawing.Point(160, 440);
            this.btnEditar.Size = new System.Drawing.Size(120, 40);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            // btnEliminar
            this.btnEliminar.Location = new System.Drawing.Point(300, 440);
            this.btnEliminar.Size = new System.Drawing.Size(120, 40);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            // lblEstado
            this.lblEstado.Location = new System.Drawing.Point(20, 500);
            this.lblEstado.Size = new System.Drawing.Size(300, 25);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Text = "0 location(s)";

            // FrmLocationLista
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.dgvLocation);
            this.Controls.Add(this.lblVacio);
            this.Controls.Add(this.btnNueva);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblEstado);
            this.Name = "FrmLocationLista";
            this.Text = "Junín Comercial - Locations";
            this.Load += new System.EventHandler(this.FrmLocationLista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocation)).EndInit();
            this.ResumeLayout(false);
        }
    }
}