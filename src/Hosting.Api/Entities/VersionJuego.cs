namespace Hosting.Api.Entities;

public class VersionJuego
{
    public int IdVersion { get; set; }

    public short IdJuego { get; set; }

    public string Version { get; set; } = null!;

    public string? EtiquetaImagen { get; set; }

    public bool Activa { get; set; }

    public Juego Juego { get; set; } = null!;
}