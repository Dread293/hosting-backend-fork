using Hosting.Api.DTOs;

namespace Hosting.Api.Services;

public class ConfiguracionTerrariaService
{
    public async Task<string> GenerarArchivoAsync(
        CrearServidorDTO solicitud,
        Guid idServidor,
        string rutaDatos,
        string rutaConfiguracion)
    {
        string tamano = solicitud.TamanoMundo.Trim().ToLowerInvariant() switch
        {
            "pequeño" or "pequeno" => "1",
            "mediano" => "2",
            "grande" => "3",
            _ => throw new ArgumentException("Tamaño de mundo no válido.")
        };

        string dificultad = solicitud.Dificultad.Trim().ToLowerInvariant() switch
        {
            "clasico" or "clásico" => "0",
            "experto" => "1",
            "maestro" => "2",
            "viaje" => "3",
            _ => throw new ArgumentException("Dificultad no válida.")
        };

        if (solicitud.NombreMundo.Contains('\n') ||
            solicitud.NombreMundo.Contains('\r') ||
            (solicitud.Contrasena?.Contains('\n') ?? false) ||
            (solicitud.Contrasena?.Contains('\r') ?? false))
        {
            throw new ArgumentException("La configuración contiene caracteres no permitidos.");
        }

        Directory.CreateDirectory(Path.Combine(rutaDatos, "worlds"));
        Directory.CreateDirectory(Path.GetDirectoryName(rutaConfiguracion)!);

        string rutaMundo = $"/data/worlds/{idServidor}.wld";

        var lineas = new List<string>
        {
            $"world={rutaMundo}",
            $"autocreate={tamano}",
            $"worldname={solicitud.NombreMundo}",
            $"difficulty={dificultad}",
            $"maxplayers={solicitud.MaxJugadores}",
            "port=7777",
            "secure=1"
        };

        if (!string.IsNullOrWhiteSpace(solicitud.Contrasena))
        {
            lineas.Add($"password={solicitud.Contrasena}");
        }

        await File.WriteAllLinesAsync(rutaConfiguracion, lineas);

        return rutaConfiguracion;
    }
}