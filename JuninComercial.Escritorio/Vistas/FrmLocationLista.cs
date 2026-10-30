using JuninComercial.Escritorio.Datos;
using JuninComercial.Escritorio.Logica;
using JuninComercial.Escritorio.Modelo;
using System;
using System.Windows.Forms;

namespace JuninComercial.Escritorio.Vistas;

public partial class FrmLocationLista : Form
{
    private readonly LocationRepositorio _repo = new LocationRepositorio();
    private readonly LocationServicio _servicio = new LocationServicio();

    public FrmLocationLista()
    {
        InitializeComponent();
        dgvLocation.SelectionChanged += dgvLocation_SelectionChanged;
    }

    private void FrmLocationLista_Load(object? sender, EventArgs e)
    {
        CargarGrilla();
    }

    private void CargarGrilla()
    {
        var datos = _repo.ListarParaGrilla();
        dgvLocation.DataSource = datos;

        if (dgvLocation.Columns["Id"] != null)
            dgvLocation.Columns["Id"]!.Width = 60;
        if (dgvLocation.Columns["Zona"] != null)
            dgvLocation.Columns["Zona"]!.Width = 120;
        if (dgvLocation.Columns["Direccion"] != null)
            dgvLocation.Columns["Direccion"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        ActualizarEstadoBotones();
    }

    private void dgvLocation_SelectionChanged(object? sender, EventArgs e)
    {
        ActualizarEstadoBotones();
    }

    private void dgvLocation_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            btnEditar_Click(sender, e);
        }
    }

    private void ActualizarEstadoBotones()
    {
        bool haySeleccion = dgvLocation.CurrentRow != null && dgvLocation.SelectedRows.Count > 0;
        btnEditar.Enabled = haySeleccion;
        btnEliminar.Enabled = haySeleccion;
    }

    private void btnNueva_Click(object? sender, EventArgs e)
    {
        using var frm = new FrmLocationEdicion(null);
        if (frm.ShowDialog() == DialogResult.OK)
            CargarGrilla();
    }

    private void btnEditar_Click(object? sender, EventArgs e)
    {
        if (dgvLocation.CurrentRow == null) return;

        var idValue = dgvLocation.CurrentRow.Cells["Id"].Value;
        if (idValue != null && int.TryParse(idValue.ToString(), out int id))
        {
            using var frm = new FrmLocationEdicion(id);
            if (frm.ShowDialog() == DialogResult.OK)
                CargarGrilla();
        }
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        if (dgvLocation.CurrentRow == null) return;

        var idValue = dgvLocation.CurrentRow.Cells["Id"].Value;
        if (idValue != null && int.TryParse(idValue.ToString(), out int id))
        {
            var confirm = MessageBox.Show(
                $"¿Está seguro de eliminar la Location ID #{id}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Llama a la capa de servicio para validar si está en uso antes de borrar
                    _servicio.Eliminar(id);

                    CargarGrilla();
                    MessageBox.Show("Ubicación eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (ReglaNegocioException ex)
                {
                    // Muestra el mensaje de advertencia si la ubicación está tomada por un comercio
                    MessageBox.Show(ex.Message, "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}