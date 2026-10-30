using System;
using JuninComercial.Escritorio.Datos;
using JuninComercial.Escritorio.Modelo;

namespace JuninComercial.Escritorio.Logica;

public class LocationServicio
{
    private readonly LocationRepositorio _repositorio = new LocationRepositorio();

    public Location[] ListarTodas()
    {
        return _repositorio.ListarTodas();
    }

    public Location? BuscarPorId(int id)
    {
        if (id <= 0)
            throw new ReglaNegocioException("El ID ingresado no es válido.");

        return _repositorio.BuscarPorId(id);
    }

    public Location[] BuscarPorZona(int idZona)
    {
        if (idZona <= 0)
            throw new ReglaNegocioException("La zona ingresada no es válida.");

        return _repositorio.BuscarPorZona(idZona);
    }

    public void Guardar(Location loc)
    {
        if (!loc.IdZone.HasValue || loc.IdZone <= 0)
            throw new ReglaNegocioException("Debe seleccionar una Zona válida.");

        if (!loc.IdAddress.HasValue || loc.IdAddress <= 0)
            throw new ReglaNegocioException("Debe seleccionar una Dirección válida.");

        if (!loc.Latitude.HasValue || !loc.Longitude.HasValue)
            throw new ReglaNegocioException("Debe ingresar las coordenadas de Latitud y Longitud.");

        if (loc.Latitude < -90 || loc.Latitude > 90)
            throw new ReglaNegocioException("La Latitud debe estar entre -90 y 90 grados.");

        if (loc.Longitude < -180 || loc.Longitude > 180)
            throw new ReglaNegocioException("La Longitud debe estar entre -180 y 180 grados.");

        // NUEVA REGLA: Verificar que la dirección no esté tomada por OTRA Location
        if (_repositorio.ExisteAddressEnOtraLocation(loc.IdAddress.Value, loc.IdLocation))
        {
            throw new ReglaNegocioException("La dirección seleccionada ya tiene una Location registrada en el sistema.");
        }

        if (_repositorio.ExisteCoordenada(loc.Latitude.Value, loc.Longitude.Value, loc.IdLocation))
        {
            throw new ReglaNegocioException("Ya existe una ubicación registrada en exactamente la misma coordenada GPS.");
        }

        if (loc.IdLocation == 0)
        {
            loc.DateCreate = DateTime.Now;
            loc.DateUpdate = DateTime.Now;
            _repositorio.Insertar(loc);
        }
        else
        {
            loc.DateUpdate = DateTime.Now;
            _repositorio.Modificar(loc);
        }
    }

    public void Eliminar(int id)
    {
        if (id <= 0)
            throw new ReglaNegocioException("El ID a eliminar no es válido.");

        // Validamos si la ubicación está en uso por alguna Entity
        if (_repositorio.EstaEnUso(id))
        {
            throw new ReglaNegocioException("No se puede eliminar la ubicación porque está asignada a un comercio o entidad activa.");
        }

        bool eliminado = _repositorio.Eliminar(id);
        if (!eliminado)
            throw new ReglaNegocioException("No se pudo eliminar la ubicación especificada.");
    }
}