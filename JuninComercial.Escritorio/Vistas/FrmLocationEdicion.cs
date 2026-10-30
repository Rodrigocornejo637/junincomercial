using JuninComercial.Escritorio.Datos;
using JuninComercial.Escritorio.Logica;
using JuninComercial.Escritorio.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace JuninComercial.Escritorio.Vistas;

public partial class FrmLocationEdicion : Form
{
    private readonly LocationServicio _servicio = new LocationServicio();
    private readonly LocationRepositorio _repoDatos = new LocationRepositorio();
    private readonly int? _idLocation;

    public FrmLocationEdicion(int? idLocation)
    {
        InitializeComponent();
        _idLocation = idLocation;
    }

    private void FrmLocationEdicion_Load(object? sender, EventArgs e)
    {
        this.Width = 550;
        cmbAddress.DropDownWidth = 450;

        CargarZonas();

        if (_idLocation.HasValue)
        {
            Text = "Editar Location";
            CargarDatosLocation(_idLocation.Value);
        }
        else
        {
            Text = "Nueva Location";
            CargarDirecciones(null);
            cmbIdZone.SelectedIndex = 0;
            cmbAddress.SelectedIndex = 0;
        }

        // Se conecta el evento al final de la carga inicial para evitar disparos accidentales
        cmbIdZone.SelectedIndexChanged -= cmbIdZone_SelectedIndexChanged;
        cmbIdZone.SelectedIndexChanged += cmbIdZone_SelectedIndexChanged;
    }

    private void CargarZonas()
    {
        var zonasList = new List<LocationRepositorio.ZonaItem>(_repoDatos.ObtenerZonas());

        zonasList.Insert(0, new LocationRepositorio.ZonaItem
        {
            IdZone = 0,
            Name = "-- Seleccione una zona --"
        });

        cmbIdZone.DataSource = zonasList;
        cmbIdZone.DisplayMember = "Name";
        cmbIdZone.ValueMember = "IdZone";
    }

    private void CargarDirecciones(int? idZone)
    {
        var direccionesList = new List<Direccion>(_repoDatos.ObtenerDirecciones(_idLocation, idZone));

        direccionesList.Insert(0, new Direccion
        {
            IdAddress = 0,
            StreetName = "-- Seleccione una dirección --"
        });

        cmbAddress.DataSource = null;
        cmbAddress.DataSource = direccionesList;
        cmbAddress.DisplayMember = "TextoMostrar";
        cmbAddress.ValueMember = "IdAddress";
    }

    private void cmbIdZone_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbIdZone.SelectedValue != null && int.TryParse(cmbIdZone.SelectedValue.ToString(), out int idZone) && idZone > 0)
        {
            CargarDirecciones(idZone);
        }
        else
        {
            CargarDirecciones(null);
        }
    }

    private void CargarDatosLocation(int id)
    {
        var loc = _servicio.BuscarPorId(id);
        if (loc != null)
        {
            txtLatitud.Text = loc.Latitude?.ToString(CultureInfo.InvariantCulture) ?? "";
            txtLongitud.Text = loc.Longitude?.ToString(CultureInfo.InvariantCulture) ?? "";

            if (loc.IdZone.HasValue)
            {
                cmbIdZone.SelectedValue = loc.IdZone.Value;
                CargarDirecciones(loc.IdZone.Value);
            }
            else
            {
                CargarDirecciones(null);
            }

            if (loc.IdAddress.HasValue)
            {
                cmbAddress.SelectedValue = loc.IdAddress.Value;
            }
        }
    }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        try
        {
            int idZona = cmbIdZone.SelectedValue != null ? Convert.ToInt32(cmbIdZone.SelectedValue) : 0;
            int idAddress = cmbAddress.SelectedValue != null ? Convert.ToInt32(cmbAddress.SelectedValue) : 0;

            if (idZona <= 0)
            {
                throw new ReglaNegocioException("Por favor, seleccione una zona válida.");
            }

            if (idAddress <= 0)
            {
                throw new ReglaNegocioException("Por favor, seleccione una dirección válida.");
            }

            double.TryParse(txtLatitud.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lat);
            double.TryParse(txtLongitud.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double lng);

            var loc = new Location
            {
                IdLocation = _idLocation ?? 0,
                IdZone = idZona,
                IdAddress = idAddress,
                Latitude = lat,
                Longitude = lng
            };

            _servicio.Guardar(loc);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (ReglaNegocioException ex)
        {
            MessageBox.Show(ex.Message, "Regla de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancelar_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}