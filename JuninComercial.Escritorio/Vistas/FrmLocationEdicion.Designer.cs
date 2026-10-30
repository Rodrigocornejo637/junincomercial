namespace JuninComercial.Escritorio.Vistas
{
    partial class FrmLocationEdicion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblLatitud;
        private System.Windows.Forms.TextBox txtLatitud;
        private System.Windows.Forms.Label lblLongitud;
        private System.Windows.Forms.TextBox txtLongitud;
        private System.Windows.Forms.Label lblZona;
        private System.Windows.Forms.ComboBox cmbIdZone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.ComboBox cmbAddress;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.lblLatitud = new System.Windows.Forms.Label();
            this.txtLatitud = new System.Windows.Forms.TextBox();
            this.lblLongitud = new System.Windows.Forms.Label();
            this.txtLongitud = new System.Windows.Forms.TextBox();
            this.lblZona = new System.Windows.Forms.Label();
            this.cmbIdZone = new System.Windows.Forms.ComboBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.cmbAddress = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblLatitud
            this.lblLatitud.Location = new System.Drawing.Point(20, 20);
            this.lblLatitud.Size = new System.Drawing.Size(100, 25);
            this.lblLatitud.Text = "Latitud:";

            // txtLatitud
            this.txtLatitud.Location = new System.Drawing.Point(130, 20);
            this.txtLatitud.Size = new System.Drawing.Size(200, 25);
            this.txtLatitud.Name = "txtLatitud";

            // lblLongitud
            this.lblLongitud.Location = new System.Drawing.Point(20, 60);
            this.lblLongitud.Size = new System.Drawing.Size(100, 25);
            this.lblLongitud.Text = "Longitud:";

            // txtLongitud
            this.txtLongitud.Location = new System.Drawing.Point(130, 60);
            this.txtLongitud.Size = new System.Drawing.Size(200, 25);
            this.txtLongitud.Name = "txtLongitud";

            // lblZona
            this.lblZona.Location = new System.Drawing.Point(20, 100);
            this.lblZona.Size = new System.Drawing.Size(100, 25);
            this.lblZona.Text = "Zona:";

            // cmbIdZone
            this.cmbIdZone.Location = new System.Drawing.Point(130, 100);
            this.cmbIdZone.Size = new System.Drawing.Size(200, 25);
            this.cmbIdZone.Name = "cmbIdZone";
            this.cmbIdZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdZone.Items.AddRange(new object[] { "Norte", "Sur", "Este", "Oeste", "Centro" });

            // lblAddress
            this.lblAddress.Location = new System.Drawing.Point(20, 140);
            this.lblAddress.Size = new System.Drawing.Size(100, 25);
            this.lblAddress.Text = "Dirección (ID):";

            // cmbAddress
            this.cmbAddress.Location = new System.Drawing.Point(130, 140);
            this.cmbAddress.Size = new System.Drawing.Size(200, 25);
            this.cmbAddress.Name = "cmbAddress";
            this.cmbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // btnGuardar
            this.btnGuardar.Location = new System.Drawing.Point(130, 190);
            this.btnGuardar.Size = new System.Drawing.Size(90, 35);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            // btnCancelar
            this.btnCancelar.Location = new System.Drawing.Point(240, 190);
            this.btnCancelar.Size = new System.Drawing.Size(90, 35);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // FrmLocationEdicion
            this.ClientSize = new System.Drawing.Size(360, 250);
            this.Controls.Add(this.lblLatitud);
            this.Controls.Add(this.txtLatitud);
            this.Controls.Add(this.lblLongitud);
            this.Controls.Add(this.txtLongitud);
            this.Controls.Add(this.lblZona);
            this.Controls.Add(this.cmbIdZone);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.cmbAddress);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "FrmLocationEdicion";
            this.Text = "Location";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmLocationEdicion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}