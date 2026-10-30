using System;
using MySqlConnector; 
using JuninComercial.Escritorio.Modelo;

namespace JuninComercial.Escritorio.Datos;

public class LocationRepositorio
{
    private void CerrarSeguro(MySqlConnection? conexion)
    {
        if (conexion != null && conexion.State != System.Data.ConnectionState.Closed)
        {
            conexion.Close();
        }
    }


    public List<Direccion> ObtenerDirecciones(int? idLocationActual, int? idZone)
    {
        var list = new List<Direccion>();
        using var conexion = ConexionBD.Abrir();

        // 1. Consulta base: Traemos todas las direcciones y verificamos si están en uso
        string sql = @"
        SELECT a.id_address, a.street_name, a.street_number, a.neighborhood,
               CONCAT(a.street_name, ' ', IFNULL(a.street_number, ''), ' (', IFNULL(a.neighborhood, ''), ')') AS TextoMostrar
        FROM address a
        LEFT JOIN location l ON a.id_address = l.id_address
        WHERE (l.id_location IS NULL ";

        // 2. Condición: Si estamos editando, también traemos la dirección de esta ubicación
        if (idLocationActual.HasValue && idLocationActual.Value > 0)
        {
            sql += " OR l.id_location = @idLocationActual";
        }

        // Cerramos el bloque de disponibilidad
        sql += ")";

        // 3. Filtro por Zona (Barrios)
        if (idZone.HasValue && idZone.Value > 0)
        {
            sql += " AND ("; // Abrimos paréntesis para aislar el filtro de zona

            switch (idZone.Value)
            {
                case 1: // Norte
                    sql += "a.neighborhood IN ('La Colonia', 'Alto Verde')";
                    break;
                case 2: // Sur
                    sql += "a.neighborhood IN ('Medrano', 'Ingeniero Giagnoni', 'Philipps', 'Phillips')";
                    break;
                case 3: // Este
                    sql += "a.neighborhood IN ('Algarrobo Grande', 'Mundo Nuevo', 'Philipps', 'Phillips', 'San Isidro')";
                    break;
                case 4: // Oeste
                    sql += "a.neighborhood IN ('Los Barriales', 'Rodríguez Peña')";
                    break;
                case 5: // Centro
                    sql += "a.neighborhood IN ('Junín', 'Centro', 'Ciudad')";
                    break;
                default:
                    // Si la zona no coincide con ninguna, forzamos a que no traiga resultados para no mezclar
                    sql += "1 = 0";
                    break;
            }

            sql += ")"; // Cerramos paréntesis del filtro de zona
        }

        sql += " ORDER BY a.street_name, a.street_number;";

        using var cmd = new MySqlCommand(sql, conexion);

        if (idLocationActual.HasValue && idLocationActual.Value > 0)
        {
            cmd.Parameters.AddWithValue("@idLocationActual", idLocationActual.Value);
        }

        using var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            list.Add(new Direccion
            {
                IdAddress = Convert.ToInt32(dr["id_address"]),
                StreetName = dr["TextoMostrar"].ToString() ?? ""
            });
        }

        return list;
    }

    public bool ExisteAddressEnOtraLocation(int idAddress, int idLocationActual)
    {
        string sql = "SELECT COUNT(*) FROM location WHERE id_address = @idAddress AND id_location <> @idLocationActual";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@idAddress", idAddress);
            cmd.Parameters.AddWithValue("@idLocationActual", idLocationActual);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        finally { CerrarSeguro(conexion); }
    }

    public bool ExisteCoordenada(double latitude, double longitude, int idLocationExcluir)
    {
        string sql = "SELECT COUNT(*) FROM location WHERE latitude = @lat AND longitude = @lng AND id_location <> @idExcluir";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@lat", latitude);
            cmd.Parameters.AddWithValue("@lng", longitude);
            cmd.Parameters.AddWithValue("@idExcluir", idLocationExcluir);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        finally { CerrarSeguro(conexion); }
    }

    // --- MÉTODOS RECUPERADOS QUE HABÍAN QUEDADO AFUERA ---
    public Location[] ListarTodas()
    {
        var listaTemporal = new System.Collections.ArrayList();
        string sql = "SELECT id_location, id_zone, id_address, latitude, longitude FROM location ORDER BY id_location DESC";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listaTemporal.Add(new Location
                {
                    IdLocation = reader.GetInt32("id_location"),
                    IdZone = reader.IsDBNull(reader.GetOrdinal("id_zone")) ? null : reader.GetInt32("id_zone"),
                    IdAddress = reader.IsDBNull(reader.GetOrdinal("id_address")) ? null : reader.GetInt32("id_address"),
                    Latitude = reader.IsDBNull(reader.GetOrdinal("latitude")) ? null : reader.GetDouble("latitude"),
                    Longitude = reader.IsDBNull(reader.GetOrdinal("longitude")) ? null : reader.GetDouble("longitude")
                });
            }
        }
        finally { CerrarSeguro(conexion); }

        return (Location[])listaTemporal.ToArray(typeof(Location));
    }

    public Location[] BuscarPorZona(int idZona)
    {
        var listaTemporal = new System.Collections.ArrayList();
        string sql = "SELECT id_location, id_zone, id_address, latitude, longitude FROM location WHERE id_zone = @idZone";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@idZone", idZona);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listaTemporal.Add(new Location
                {
                    IdLocation = reader.GetInt32("id_location"),
                    IdZone = reader.IsDBNull(reader.GetOrdinal("id_zone")) ? null : reader.GetInt32("id_zone"),
                    IdAddress = reader.IsDBNull(reader.GetOrdinal("id_address")) ? null : reader.GetInt32("id_address"),
                    Latitude = reader.IsDBNull(reader.GetOrdinal("latitude")) ? null : reader.GetDouble("latitude"),
                    Longitude = reader.IsDBNull(reader.GetOrdinal("longitude")) ? null : reader.GetDouble("longitude")
                });
            }
        }
        finally { CerrarSeguro(conexion); }

        return (Location[])listaTemporal.ToArray(typeof(Location));
    }
    // -----------------------------------------------------

    public LocationGridDto[] ListarParaGrilla()
    {
        var listaTemporal = new System.Collections.ArrayList();
        string sql = @"
            SELECT 
                l.id_location,
                COALESCE(z.name, CONCAT('Zona ', l.id_zone)) AS zona_nombre,
                COALESCE(CONCAT(a.street_name, ' ', a.street_number), 'Sin dirección') AS direccion_nombre,
                l.latitude,
                l.longitude
            FROM location l
            LEFT JOIN zone z ON l.id_zone = z.id_zone
            LEFT JOIN address a ON l.id_address = a.id_address
            ORDER BY l.id_location ASC;";

        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listaTemporal.Add(new LocationGridDto
                {
                    Id = reader.GetInt32("id_location"),
                    Zona = reader.IsDBNull(reader.GetOrdinal("zona_nombre")) ? "N/A" : reader.GetString("zona_nombre"),
                    Direccion = reader.IsDBNull(reader.GetOrdinal("direccion_nombre")) ? "N/A" : reader.GetString("direccion_nombre"),
                    Latitud = reader.IsDBNull(reader.GetOrdinal("latitude")) ? null : reader.GetDouble("latitude"),
                    Longitud = reader.IsDBNull(reader.GetOrdinal("longitude")) ? null : reader.GetDouble("longitude")
                });
            }
        }
        finally { CerrarSeguro(conexion); }

        return (LocationGridDto[])listaTemporal.ToArray(typeof(LocationGridDto));
    }

    public Location? BuscarPorId(int id)
    {
        string sql = "SELECT id_location, id_zone, id_address, latitude, longitude FROM location WHERE id_location = @id";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Location
                {
                    IdLocation = reader.GetInt32("id_location"),
                    IdZone = reader.IsDBNull(reader.GetOrdinal("id_zone")) ? null : reader.GetInt32("id_zone"),
                    IdAddress = reader.IsDBNull(reader.GetOrdinal("id_address")) ? null : reader.GetInt32("id_address"),
                    Latitude = reader.IsDBNull(reader.GetOrdinal("latitude")) ? null : reader.GetDouble("latitude"),
                    Longitude = reader.IsDBNull(reader.GetOrdinal("longitude")) ? null : reader.GetDouble("longitude")
                };
            }
        }
        finally { CerrarSeguro(conexion); }

        return null;
    }

    public void Insertar(Location loc)
    {
        string sql = @"INSERT INTO location (id_zone, id_address, latitude, longitude, id_user_create, id_user_update, date_create, date_update)
                      VALUES (@idZone, @idAddress, @lat, @lng, 1, 1, @dateCreate, @dateUpdate)";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@idZone", loc.IdZone);
            cmd.Parameters.AddWithValue("@idAddress", loc.IdAddress);
            cmd.Parameters.AddWithValue("@lat", loc.Latitude);
            cmd.Parameters.AddWithValue("@lng", loc.Longitude);
            cmd.Parameters.AddWithValue("@dateCreate", loc.DateCreate ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@dateUpdate", loc.DateUpdate ?? DateTime.Now);
            cmd.ExecuteNonQuery();
        }
        finally { CerrarSeguro(conexion); }
    }

    public void Modificar(Location loc)
    {
        string sql = @"UPDATE location 
                      SET id_zone = @idZone, id_address = @idAddress, latitude = @lat, longitude = @lng, 
                          id_user_update = 1, date_update = @dateUpdate
                      WHERE id_location = @idLocation";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@idZone", loc.IdZone);
            cmd.Parameters.AddWithValue("@idAddress", loc.IdAddress);
            cmd.Parameters.AddWithValue("@lat", loc.Latitude);
            cmd.Parameters.AddWithValue("@lng", loc.Longitude);
            cmd.Parameters.AddWithValue("@dateUpdate", loc.DateUpdate ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@idLocation", loc.IdLocation);
            cmd.ExecuteNonQuery();
        }
        finally { CerrarSeguro(conexion); }
    }

    public bool Eliminar(int id)
    {
        string sql = "DELETE FROM location WHERE id_location = @id";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
        finally { CerrarSeguro(conexion); }
    }
    // --- CLASE Y MÉTODO PARA CARGAR EL DESPLEGABLE DE ZONAS ---
    public class ZonaItem
    {
        public int IdZone { get; set; }
        public string Name { get; set; } = "";
    }

    public ZonaItem[] ObtenerZonas()
    {
        var listaTemporal = new System.Collections.ArrayList();
        string sql = "SELECT id_zone, name FROM zone ORDER BY name;";

        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listaTemporal.Add(new ZonaItem
                {
                    IdZone = reader.GetInt32("id_zone"),
                    Name = reader.GetString("name")
                });
            }
        }
        finally { CerrarSeguro(conexion); }

        return (ZonaItem[])listaTemporal.ToArray(typeof(ZonaItem));
    }
    public bool EstaEnUso(int idLocation)
    {
        string sql = "SELECT COUNT(*) FROM entity WHERE id_location = @id;";
        MySqlConnection? conexion = null;
        try
        {
            conexion = ConexionBD.Abrir();
            var cmd = new MySqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@id", idLocation);

            long cantidad = Convert.ToInt64(cmd.ExecuteScalar());
            return cantidad > 0;
        }
        finally
        {
            CerrarSeguro(conexion);
        }
    }
}