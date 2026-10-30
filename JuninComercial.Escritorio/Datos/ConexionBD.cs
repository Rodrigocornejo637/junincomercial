using MySqlConnector;
using System.Configuration;

namespace JuninComercial.Escritorio.Datos;

public static class ConexionBD
{
    public static MySqlConnection Abrir()
    {
        string cadena = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        var conexion = new MySqlConnection(cadena);
        conexion.Open();
        return conexion;
    }
}