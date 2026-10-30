namespace JuninComercial.Escritorio.Modelo;

public class LocationGridDto
{
    public int Id { get; set; }
    public string Zona { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public double? Latitud { get; set; }
    public double? Longitud { get; set; }
}