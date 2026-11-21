namespace Hosting.Api.Entities;

public class Juego
{
    public short IdJuego { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public ICollection<VersionJuego> Versiones { get; set; }
        = new List<VersionJuego>();
}