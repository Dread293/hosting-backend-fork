using Hosting.Api.Data;
using Hosting.Api.DTOs;

namespace Hosting.Api.Services;

public class ServidorService
{
    private readonly HostingDbContext _context;
    private readonly DockerService _dockerService;
    private readonly ConfiguracionTerrariaService _configuracionTerrariaService;
    private readonly string _storageRoot;

    public ServidorService(
        HostingDbContext context,
        DockerService dockerService,
        ConfiguracionTerrariaService configuracionTerrariaService,
        IConfiguration configuration)
    {
        _context = context;
        _dockerService = dockerService;
        _configuracionTerrariaService = configuracionTerrariaService;

        _storageRoot = configuration["Hosting:StorageRoot"]
            ?? throw new InvalidOperationException(
                "No se configuró Hosting:StorageRoot."
            );
    }

    private string ObtenerRutaServidor(Guid idServidor)
    {
        return Path.Combine(_storageRoot, idServidor.ToString());
    }


    private async Task<(string RutaDatos, string RutaConfiguracion)>
        PrepararAlmacenamientoAsync(
            CrearServidorDTO solicitud,
            Guid idServidor)
    {
        string rutaServidor = ObtenerRutaServidor(idServidor);

        string rutaDatos = Path.Combine(rutaServidor, "data");

        string rutaConfiguracion = Path.Combine(
            rutaServidor,
            "serverconfig.txt"
        );

        await _configuracionTerrariaService.GenerarArchivoAsync(
            solicitud,
            idServidor,
            rutaDatos,
            rutaConfiguracion
        );

        return (rutaDatos, rutaConfiguracion);
    }

}