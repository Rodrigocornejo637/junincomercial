namespace JuninComercial.Escritorio.Modelo;

public class Location
{
    public int IdLocation { get; set; }
    public int? IdZone { get; set; }
    public int? IdAddress { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int? IdUserCreate { get; set; }
    public int? IdUserUpdate { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }

    public Location() { }

    public override string ToString() => $"Location #{IdLocation} ({Latitude}, {Longitude})";
}