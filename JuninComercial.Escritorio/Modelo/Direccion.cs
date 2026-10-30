namespace JuninComercial.Escritorio.Modelo;

public class Direccion
{
    public int IdAddress { get; set; }
    public string StreetName { get; set; } = string.Empty;
    public string StreetNumber { get; set; } = string.Empty;
    public string? Floor { get; set; }
    public string? Apartment { get; set; }
    public string? Neighborhood { get; set; }

    // Propiedad que arma el texto amigable para la lista desplegable
    public string TextoMostrar
    {
        get
        {
            string texto = $"{StreetName} {StreetNumber}".Trim();

            // Si tiene piso/depto lo agregamos
            if (!string.IsNullOrWhiteSpace(Floor) || !string.IsNullOrWhiteSpace(Apartment))
            {
                texto += $" ({Floor}°{Apartment})";
            }

            // Si tiene barrio lo sumamos
            if (!string.IsNullOrWhiteSpace(Neighborhood))
            {
                texto += $" - B° {Neighborhood}";
            }

            return texto;
        }
    }

    public override string ToString() => TextoMostrar;
}